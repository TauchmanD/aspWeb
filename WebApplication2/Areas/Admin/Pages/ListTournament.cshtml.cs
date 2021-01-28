using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication2.Data;

namespace WebApplication2.Areas.Admin.Pages
{
    [Authorize(Policy = "Admin")]
    public class ListTournamentModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;

        public ListTournamentModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Tournaments> Tournaments { get;set; }

        public async Task OnGetAsync()
        {
            Tournaments = await _context.Tournament
                .Include(t => t.User).ToListAsync();
        }
    }
}
