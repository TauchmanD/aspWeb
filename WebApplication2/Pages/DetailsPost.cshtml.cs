using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Pages
{
    public class DetailsPostModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public string Comment { get; set; }
        public List<Comment> comments { get; set; }

        public DetailsPostModel(WebApplication2.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Posts Posts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            comments = _context.Comments.Where(c => c.PostId == id).ToList();
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


        
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var myId = id;
            
            var UserId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var email = _userManager.Users.ToList().Where(c => UserId == c.Id).FirstOrDefault().Email;
            Comment comment = new Comment
            {
                Text = Comment,
                UserId = UserId,
                Date = DateTime.Now,
                PostId = id,
                Commentor = email
            };
            if (comment.Text==null)
            {
                return RedirectToPage("./DetailsPost", new { id = myId });
            }
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./DetailsPost", new { id = myId});
        }
    }
}
