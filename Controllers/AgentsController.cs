using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Areas.Identity.Data;
using AuthSystem.Data;

namespace AuthSystem.Controllers
{
    public class AgentsController : Controller
    {
        private readonly AutSystemContext _context;

        public AgentsController(AutSystemContext context)
        {
            _context = context;
        }

        // GET: Agents
        public async Task<IActionResult> Index(int? selectedGenreId)
        {
            var autSystemContext = _context.Agents.Include(a=>a.InterestedGenres);

            if (selectedGenreId.HasValue)
                //autSystemContext = autSystemContext.Where(a => a.InterestedGenres.Any(g=> g.Id == selectedGenreId.Value)).ToList();

            return View(await autSystemContext.ToListAsync());
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AgencyName,FirstName,LastName,Email")] Agent agent)
        {
            _context.Add(agent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Agents/Edit/5
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

        public async Task OnGetAsync(string searchString)
        {
            var autSystemContext = _context.Agents;
            
            // Apply Filter
        }
    }
}
