using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication2.Data;

namespace WebApplication2.Areas.Admin.Pages
{
    public class DeletePostModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;

        public DeletePostModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Posts Posts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Posts = await _context.Post.FindAsync(id);

            if (Posts != null)
            {
                _context.Post.Remove(Posts);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ListPost");
        }
    }
}
