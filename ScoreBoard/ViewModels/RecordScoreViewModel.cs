using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ScoreBoard.ViewModels
{
    public class RecordScoreViewModel
    {
        public IEnumerable<SelectListItem> Teams { get; set; }

        [Required]
        [DisplayName("Your team")]
        public int SelectedTeamID { get; set; }

        [DisplayName("Your source code size in bytes")]
        public int Score { get; set; }
    }
}