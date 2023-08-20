using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Areas.Identity.Data;
using AuthSystem.Data;
using AuthSystem.Exceptions;

namespace AuthSystem.Controllers
{
    [AllowOnlyPublisherRoles]
    public class BidsController : Controller
    {
        private readonly AutSystemContext _context;

        public BidsController(AutSystemContext context)
        {
            _context = context;
        }

        // GET: Bids
        public async Task<IActionResult> Index()
        {
            var autSystemContext = _context.Bids.Include(b => b.Book).Include(b => b.Publisher).OrderBy(b=>b.Price);
            return View(await autSystemContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Id");
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,PublisherId,Price")] Bid bid)
        {
            _context.Add(bid);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BidExists(int id)
        {
          return (_context.Bids?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
