using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication1.Models
{
    public class Posts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [Required]
        public string Text { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
        [Required]
        public string UserId { get; set; }
        public DateTime Date_of_publish { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
