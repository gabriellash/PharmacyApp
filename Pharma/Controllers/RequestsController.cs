using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharma.Migrations;
using Pharma.Models;
using Pharma.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Pharma.Controllers
{
    public class RequestsController : Controller
    {
        private readonly AuthDbContext _context;

        public RequestsController(AuthDbContext context)
        {
            _context = context;
        }
       
        // GET: Requests
        public async Task<IActionResult> Index()
        {
        var RequestContext = _context.Request.Include(o=> o.Medicine);
            /*  return _context.Request != null ? 
                          View(await _context.Request.ToListAsync()) :
                          Problem("Entity set 'RequestContext.Request'  is null.");
           */ 
            return View(await RequestContext.ToListAsync());
       
        }

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Request == null)
            {
                return NotFound();
            }

            var request = await _context.Request
                .Include(o=> o.Medicine)
                .FirstOrDefaultAsync(m => m.Request_Id == id);
            if (request == null)
            {
                return NotFound();
            }
            TempData["User"] = request.Request_User_Id;
            TempData["Medicine"] = request.Medicine.Medicine_Name;
            return View(request);
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            ViewData["Medicine_Id"] = new SelectList(_context.Medicine, "Medicine_Id", "Medicine_Name");
            return View();
            
        }
       

   

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Request_Id,Request_Date,Request_User_Id,Medicine_Id")] Request request)
        {
          //  if (ModelState.IsValid)
          //  {
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
          //  }
            ViewData["Medicine_Id"] = new SelectList(_context.Medicine, "Medicine_Id", "Medicine_Name", request.Medicine_Id);
            return View(request);
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Request == null)
            {
                return NotFound();
            }

            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);

        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Request_Id,Request_Date,Request_User_Id,Medicine_Id")] Request request)
        {
            if (id != request.Request_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.Request_Id))
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
            return View(request);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Request == null)
            {
                return NotFound();
            }

            var request = await _context.Request
                .FirstOrDefaultAsync(m => m.Request_Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Request == null)
            {
                return Problem("Entity set 'RequestContext.Request'  is null.");
            }
            var request = await _context.Request.FindAsync(id);
            if (request != null)
            {
                _context.Request.Remove(request);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
          return (_context.Request?.Any(e => e.Request_Id == id)).GetValueOrDefault();
        }
    }
}
