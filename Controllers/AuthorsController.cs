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
    [Authorize(Roles = "Admin,Agent,Publisher")]
    public class AuthorsController : Controller
    {
        private readonly AutSystemContext _context;

        public AuthorsController(AutSystemContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index(string genreName)
        {
            var viewModel = new AuthorGenreViewModel
            {
                Authors = await _context.Authors.ToListAsync(),
                AuthorGenre = await _context.AuthorGenres.ToListAsync(),
                Genre = await _context.Genres.ToListAsync(),
                GenreSearch = genreName
            };

            if (!string.IsNullOrEmpty(genreName))
            {
                // Filter authors based on genre name
                viewModel.Authors = from author in viewModel.Authors
                                    join authorGenre in viewModel.AuthorGenre on author.Id equals authorGenre.AuthorId
                                    join genre in viewModel.Genre on authorGenre.GenreId equals genre.Id
                                    where genre.Name == genreName
                                    select author;
            }

            return View(viewModel);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .Include(a => a.Agent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        [AllowOnlyAdminRolesAttribute]
        public IActionResult Create()
        {
            ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Id");
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowOnlyAdminRolesAttribute]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,AgentId,Email")] Author author)
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Authors/Edit/5
        [AllowOnlyAdminRolesAttribute]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Id", author.AgentId);
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowOnlyAdminRolesAttribute]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,AgentId,Email")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            _context.Update(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Delete/5
        [AllowOnlyAdminRolesAttribute]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .Include(a => a.Agent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [AllowOnlyAdminRolesAttribute]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Authors == null)
            {
                return Problem("Entity set 'AutSystemContext.Authors'  is null.");
            }
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
          return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
