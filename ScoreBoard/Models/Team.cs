using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScoreBoard.Models
{
    public class Team
    {
        public int ID { get; set; }

        [DisplayName("Team Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Team Members")]
        [Required]
        public string Members { get; set; }

        [DisplayName("Programming Language")]
        [Required]
        public string Language { get; set; }

        public int Score { get; set; }
        public Guid CookieID { get; set; }
    }
}