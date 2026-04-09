using System.ComponentModel.DataAnnotations;

namespace LASMSProject.Models.ViewModels
{
    public class LeaveRequestViewModel
    {
        [Required]
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}