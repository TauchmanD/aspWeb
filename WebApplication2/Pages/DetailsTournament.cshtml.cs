using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication2.Data;

namespace WebApplication2.Pages
{
    public class DetailsTournamentModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;

        public DetailsTournamentModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Tournaments Tournaments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tournaments = await _context.Tournament
                .Include(t => t.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Tournaments == null)
            {
                return NotFound();
            }
            return Page();
        }

    }
}
