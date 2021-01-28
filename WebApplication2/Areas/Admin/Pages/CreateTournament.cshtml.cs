using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication2.Data;

namespace WebApplication2.Areas.Admin.Pages
{
    [Authorize(Policy = "Admin")]
    public class CreateTournamentModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;
        public SelectList UsersList { get; set; }

        public CreateTournamentModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            UsersList = new SelectList(_context.Users.Where(u => u.Id == "ADMINUSER").ToList(), nameof(IdentityUser.Id), nameof(IdentityUser.UserName));
            return Page();
        }

        [BindProperty]
        public Tournaments Tournaments { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tournament.Add(Tournaments);
            await _context.SaveChangesAsync();

            return RedirectToPage("./ListTournament");
        }
    }
}
