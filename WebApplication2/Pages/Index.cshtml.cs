using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly WebApplication2.Data.ApplicationDbContext _context;
        public string UserName { get; set; }
        public string UserId { get; set; }
        public List<Posts> Post { get; set; }
        public List<Tournaments> FinishedTournament { get; set; }
        public List<Tournaments> UnfinishedTournament { get; set; }
        public DateTime Today { get; set; }


        public IndexModel(ILogger<IndexModel> logger, WebApplication2.Data.ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            Today = DateTime.Now;
            FinishedTournament = new List<Tournaments>();
            UnfinishedTournament = new List<Tournaments>();
        }
        
        public void OnGet()
        {
            FinishedTournament = _context.Tournament.Where(x => x.Date_of_publish < Today).ToList();
            UnfinishedTournament = _context.Tournament.Where(x => x.Date_of_publish >= Today).ToList();

            Post = _context.Post.OrderByDescending(x => x.Date_of_publish).ToList();
            UserName = User.Identity.Name;
            var UserData = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            if (UserData != null)
            {
                UserId = UserData.Value;
            }
           
        }
    }
}
