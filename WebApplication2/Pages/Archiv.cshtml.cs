using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication2.Pages
{
    public class ArchivModel : PageModel
    {
        private readonly WebApplication2.Data.ApplicationDbContext _context;
        public List<Posts> Post { get; set; }
        public ArchivModel(WebApplication2.Data.ApplicationDbContext context)
        {
            _context = context;
            Post = _context.Post.OrderByDescending(x => x.Date_of_publish).ToList();
        }
        public void OnGet()
        {
        }
    }
}
