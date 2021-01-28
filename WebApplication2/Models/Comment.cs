using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication2.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Commentor { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [ForeignKey("PostId")]
        public Posts Post { get; set; }
        [Required]
        public int PostId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
        [Required]
        public string UserId { get; set; }

    }
}
