using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.ViewModels
{
    public class TournamentsIM
    {
        public int Id { get; set; }
        [Required]
        public string Tournament_name { get; set; }
        [Required]
        public string Info_box { get; set; }
        [Required]
        public DateTime Date_of_publish { get; set; }
    }
}
