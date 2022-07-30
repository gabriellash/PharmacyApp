using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pharma.Data;
using Pharma.Models;

namespace Pharma.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AuthDbContext _context;

        public OrdersController(AuthDbContext context)
        {
            _context = context;
        }




        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var authDbContext = _context.Order.Include(o => o.Medicine);
            return View(await authDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Medicine)
                .FirstOrDefaultAsync(m => m.Order_Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["Order_Medicine_Id"] = new SelectList(_context.Medicine, "Medicine_Id", "Medicine_Name");

           
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Order_Id,Order_Date,Order_User_Id,Order_UserName,Order_Medicine_Id")] Order order)
        {
            //if (ModelState.IsValid)
            // {
           SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TPQVVV4\SQLEXPRESS;Initial Catalog=pharmacyapplication;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Medicine set Medicine_Qty = Medicine_Qty - 1 where Medicine_Id =  '"+ order.Order_Medicine_Id.ToString() +"'";
            cmd.ExecuteNonQuery();

            _context.Add(order);
            
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          //  }
            
            ViewData["Order_Medicine_Id"] = new SelectList(_context.Medicine, "Medicine_Id", "Medicine_Name", order.Order_Medicine_Id);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["Order_Medicine_Id"] = new SelectList(_context.Medicine, "Medicine_Id", "Medicine_Name", order.Order_Medicine_Id);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Order_Id,Order_Date,Order_User_Id,Order_UserName,Order_Medicine_Id")] Order order)
        {
            if (id != order.Order_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Order_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Order_Medicine_Id"] = new SelectList(_context.Medicine, "Medicine_Id", "Medicine_Name", order.Order_Medicine_Id);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Medicine)
                .FirstOrDefaultAsync(m => m.Order_Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'AuthDbContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.Order_Id == id)).GetValueOrDefault();
        }
    }
}
