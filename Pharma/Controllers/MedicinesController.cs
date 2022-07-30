using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharma.Data;
using Pharma.Migrations;
using Pharma.Models;

namespace Pharma.Controllers
{
    public class MedicinesController : Controller
    {
        private readonly AuthDbContext _context;

        public MedicinesController(AuthDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Medicines
        public async Task<IActionResult> Index(string medicinetype, string searchString)
        {
            IQueryable<string> medicinequery = from m in _context.Medicine
                                               orderby m.Medicine_Type
                                               select m.Medicine_Type;
            var medicine = from m in _context.Medicine
                           select m;
            if(!string.IsNullOrEmpty(searchString))
            {
                medicine = medicine.Where(s => s.Medicine_Name.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(medicinetype))
            {
                medicine = medicine.Where(x => x.Medicine_Type == medicinetype);
            }
            var MedicineTypeList = new MedicineListModel
            {
                MedicineList = new SelectList(await medicinequery.Distinct().ToListAsync()),
                Medicines = await medicine.ToListAsync()
            };
            /* return _context.Medicine != null ? 
                         View(await _context.Medicine.ToListAsync()) :
                         Problem("Entity set 'MedicineContext.Medicine'  is null.");
       */
            return View(MedicineTypeList);
        }

        // GET: Medicines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medicine == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicine
                .FirstOrDefaultAsync(m => m.Medicine_Id == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        // GET: Medicines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Medicine_Id,Medicine_Name,Medicine_Type,Medicine_Reg_Number,Medicine_Size,Medicine_Unit,Medicine_Form,Medicine_Qty,Medicine_Price,Medicine_Expiry_Date")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicine);
        }

        // GET: Medicines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medicine == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicine.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return View(medicine);
        }

        // POST: Medicines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Medicine_Id,Medicine_Name,Medicine_Type,Medicine_Reg_Number,Medicine_Size,Medicine_Unit,Medicine_Form,Medicine_Qty,Medicine_Price,Medicine_Expiry_Date")] Medicine medicine)
        {
            if (id != medicine.Medicine_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicineExists(medicine.Medicine_Id))
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
            return View(medicine);
        }

        // GET: Medicines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medicine == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicine
                .FirstOrDefaultAsync(m => m.Medicine_Id == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        // POST: Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medicine == null)
            {
                return Problem("Entity set 'MedicineContext.Medicine'  is null.");
            }
            var medicine = await _context.Medicine.FindAsync(id);
            if (medicine != null)
            {
                _context.Medicine.Remove(medicine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicineExists(int id)
        {
          return (_context.Medicine?.Any(e => e.Medicine_Id == id)).GetValueOrDefault();
        }
    }
}
