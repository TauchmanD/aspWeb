using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication2.Data;

namespace WebApplication2.Areas.Admin.Pages
{
    [Authorize(Policy = "Admin")]
    public class EditPostModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;
        public SelectList UsersList { get; set; }

        public EditPostModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Posts Posts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            UsersList = new SelectList(_context.Users.Where(u => u.Id == "ADMINUSER").ToList(), nameof(IdentityUser.Id), nameof(IdentityUser.UserName));
            if (id == null)
            {
                return NotFound();
            }

            Posts = await _context.Post
                .Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Posts == null)
            {
                return NotFound();
            }
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Posts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostsExists(Posts.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./ListPost");
        }

        private bool PostsExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
