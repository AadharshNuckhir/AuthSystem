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
    [Authorize(Roles = "Admin,Author,Publisher")]
    public class AgentsController : Controller
    {
        private readonly AutSystemContext _context;

        public AgentsController(AutSystemContext context)
        {
            _context = context;
        }

        // GET: Agents
        public async Task<IActionResult> Index(string genreName)
        {
            var viewModel = new AgentGenreViewModel
            {
                Agents = await _context.Agents.ToListAsync(),
                AgentGenre = await _context.AgentGenres.ToListAsync(),
                Genre = await _context.Genres.ToListAsync(),
                GenreSearch = genreName
            };

            if (!string.IsNullOrEmpty(genreName))
            {
                // Filter authors based on genre name
                viewModel.Agents = from agent in viewModel.Agents
                                   join agentGenre in viewModel.AgentGenre on agent.Id equals agentGenre.AgentId
                                   join genre in viewModel.Genre on agentGenre.GenreId equals genre.Id
                                   where genre.Name == genreName
                                   select agent;
            }

            return View(viewModel);
        }

        // GET: Agents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agents == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // GET: Agents/Create
        [AllowOnlyAdminRolesAttribute]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowOnlyAdminRolesAttribute]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AgencyName,FirstName,LastName,Email")] Agent agent)
        {
            _context.Add(agent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Agents/Edit/5
        [AllowOnlyAdminRolesAttribute]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agents == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents.FindAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            return View(agent);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowOnlyAdminRolesAttribute]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AgencyName,FirstName,LastName,Email")] Agent agent)
        {
            if (id != agent.Id)
            {
                return NotFound();
            }

            _context.Update(agent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Agents/Delete/5
        [AllowOnlyAdminRolesAttribute]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agents == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [AllowOnlyAdminRolesAttribute]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agents == null)
            {
                return Problem("Entity set 'AutSystemContext.Agents'  is null.");
            }
            var agent = await _context.Agents.FindAsync(id);
            if (agent != null)
            {
                _context.Agents.Remove(agent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentExists(int id)
        {
          return (_context.Agents?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
