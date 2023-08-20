using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Areas.Identity.Data;
using AuthSystem.Data;
using Microsoft.AspNetCore.Authorization;
using AuthSystem.ViewModels;
using AuthSystem.Exceptions;

namespace AuthSystem.Controllers
{
    [Authorize(Roles = "Admin,Author")]
    public class PublishersController : Controller
    {
        private readonly AutSystemContext _context;

        public PublishersController(AutSystemContext context)
        {
            _context = context;
        }

        // GET: Publishers
        public async Task<IActionResult> Index(string genreName)
        {
            var viewModel = new PublisherGenreViewModel
            {
                Publishers = await _context.Publishers.ToListAsync(),
                PublisherGenre = await _context.PublisherGenres.ToListAsync(),
                Genre = await _context.Genres.ToListAsync(),
                GenreSearch = genreName
            };

            if (!string.IsNullOrEmpty(genreName))
            {
                // Filter authors based on genre name
                viewModel.Publishers = from publisher in viewModel.Publishers
                                       join publisherGenre in viewModel.PublisherGenre on publisher.Id equals publisherGenre.PublisherId
                                       join genre in viewModel.Genre on publisherGenre.GenreId equals genre.Id
                                       where genre.Name == genreName
                                       select publisher;
            }

            return View(viewModel);
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        [AllowOnlyAdminRolesAttribute]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowOnlyAdminRolesAttribute]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email")] Publisher publisher)
        {
            _context.Add(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Publishers/Edit/5
        [AllowOnlyAdminRolesAttribute]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowOnlyAdminRolesAttribute]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email")] Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return NotFound();
            }

            _context.Update(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Publishers/Delete/5
        [AllowOnlyAdminRolesAttribute]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [AllowOnlyAdminRolesAttribute]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Publishers == null)
            {
                return Problem("Entity set 'AutSystemContext.Publishers'  is null.");
            }
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
            return (_context.Publishers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
