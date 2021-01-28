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
    public class ListPostModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;

        public ListPostModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Posts> Posts { get;set; }

        public async Task OnGetAsync()
        {
            Posts = await _context.Post
                .Include(p => p.User).ToListAsync();
        }
    }
}
