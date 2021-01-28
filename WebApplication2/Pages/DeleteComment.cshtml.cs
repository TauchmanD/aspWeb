using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Pages
{
    public class DeleteCommentModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;

        public DeleteCommentModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Comment Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment = await _context.Comments
                .Include(c => c.Post)
                .Include(c => c.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Comment == null)
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

            Comment = await _context.Comments.FindAsync(id);
            var myId = Comment.PostId;
            if (Comment != null)
            {
                _context.Comments.Remove(Comment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./DetailsPost", new { id = myId});
        }
    }
}
