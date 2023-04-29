using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eCommerceWebsite.Data;
using eCommerceWebsite.Models;
using Microsoft.AspNetCore.Authorization;

namespace eCommerceWebsite.Controllers
{
    [Authorize(Roles="Admin")]

    public class eCommerceLeadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public eCommerceLeadController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: eCommerceLead
        public async Task<IActionResult> Index()
        {
              return _context.eCommerceLead != null ? 
                          View(await _context.eCommerceLead.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.eCommerceLead'  is null.");
        }

        // GET: eCommerceLead/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.eCommerceLead == null)
            {
                return NotFound();
            }

            var eCommerceLeadEntity = await _context.eCommerceLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eCommerceLeadEntity == null)
            {
                return NotFound();
            }

            return View(eCommerceLeadEntity);
        }

        // GET: eCommerceLead/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: eCommerceLead/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Mobile,Source")] eCommerceLeadEntity eCommerceLeadEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eCommerceLeadEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eCommerceLeadEntity);
        }

        // GET: eCommerceLead/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.eCommerceLead == null)
            {
                return NotFound();
            }

            var eCommerceLeadEntity = await _context.eCommerceLead.FindAsync(id);
            if (eCommerceLeadEntity == null)
            {
                return NotFound();
            }
            return View(eCommerceLeadEntity);
        }

        // POST: eCommerceLead/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Mobile,Source")] eCommerceLeadEntity eCommerceLeadEntity)
        {
            if (id != eCommerceLeadEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eCommerceLeadEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!eCommerceLeadEntityExists(eCommerceLeadEntity.Id))
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
            return View(eCommerceLeadEntity);
        }

        // GET: eCommerceLead/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.eCommerceLead == null)
            {
                return NotFound();
            }

            var eCommerceLeadEntity = await _context.eCommerceLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eCommerceLeadEntity == null)
            {
                return NotFound();
            }

            return View(eCommerceLeadEntity);
        }

        // POST: eCommerceLead/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.eCommerceLead == null)
            {
                return Problem("Entity set 'ApplicationDbContext.eCommerceLead'  is null.");
            }
            var eCommerceLeadEntity = await _context.eCommerceLead.FindAsync(id);
            if (eCommerceLeadEntity != null)
            {
                _context.eCommerceLead.Remove(eCommerceLeadEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool eCommerceLeadEntityExists(int id)
        {
          return (_context.eCommerceLead?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
