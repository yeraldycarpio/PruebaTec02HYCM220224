using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoTec02HYCM220224.Models;

namespace ProyectoTec02HYCM220224.Controllers
{
    public class SillasController : Controller
    {
        private readonly SILLAS2Context _context;

        public SillasController(SILLAS2Context context)
        {
            _context = context;
        }

        // GET: Sillas
        public async Task<IActionResult> Index()
        {
            var sillas = await _context.Sillas.Include(s => s.IdMaterialNavigation).ToListAsync();
            return View(sillas);
            //var sILLAS2Context = _context.Sillas.Include(s => s.IdMaterialNavigation);
            //return View(await sILLAS2Context.ToListAsync());
        }

        // GET: Sillas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sillas == null)
            {
                return NotFound();
            }

            var silla = await _context.Sillas
                .Include(s => s.IdMaterialNavigation)
                .FirstOrDefaultAsync(m => m.IdSilla == id);
            if (silla == null)
            {
                return NotFound();
            }

            return View(silla);
        }

        // GET: Sillas/Create
        public IActionResult Create()
        {
            ViewData["IdMaterial"] = new SelectList(_context.Materials, "IdMaterial", "Nombre");
            return View();
        }

        // POST: Sillas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSilla,Nombre,Modelo,Marca,Nombre")] Silla silla, IFormFile imagen)
        {
            if (imagen!=null && imagen.Length > 0)
            {
                using(var memoryStream = new MemoryStream())
                {   
                    await imagen.CopyToAsync(memoryStream);
                    silla.Imagen = memoryStream.ToArray();

                }
            }

            _context.Add(silla);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //if (ModelState.IsValid)
            //{
            //    _context.Add(silla);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["IdMaterial"] = new SelectList(_context.Materials, "IdMaterial", "IdMaterial", silla.IdMaterial);
            //return View(silla);
        }

        // GET: Sillas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sillas == null)
            {
                return NotFound();
            }

            var silla = await _context.Sillas.FindAsync(id);
            if (silla == null)
            {
                return NotFound();
            }
            ViewData["IdMaterial"] = new SelectList(_context.Materials, "IdMaterial", "Nombre", silla.IdMaterial);
            return View(silla);
        }

        // POST: Sillas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSilla,Nombre,Modelo,Marca,Imagen,Nombre")] Silla silla)
        {
            if (id != silla.IdSilla)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(silla);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SillaExists(silla.IdSilla))
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
            ViewData["IdMaterial"] = new SelectList(_context.Materials, "IdMaterial", "Nombre", silla.IdMaterial);
            return View(silla);
        }

        // GET: Sillas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sillas == null)
            {
                return NotFound();
            }

            var silla = await _context.Sillas
                .Include(s => s.IdMaterialNavigation)
                .FirstOrDefaultAsync(m => m.IdSilla == id);
            if (silla == null)
            {
                return NotFound();
            }

            return View(silla);
        }

        // POST: Sillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sillas == null)
            {
                return Problem("Entity set 'SILLAS2Context.Sillas'  is null.");
            }
            var silla = await _context.Sillas.FindAsync(id);
            if (silla != null)
            {
                _context.Sillas.Remove(silla);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SillaExists(int id)
        {
          return (_context.Sillas?.Any(e => e.IdSilla == id)).GetValueOrDefault();
        }
    }
}
