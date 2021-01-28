using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Tournaments
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tournament_name { get; set; }
        [Required]
        public string Info_box { get; set; }
        [Required]
        public DateTime Date_of_publish { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
        [Required]
        public string UserId { get; set; }

    }
}
