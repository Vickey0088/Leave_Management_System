using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LASMSProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Required]
        public string EmployeeCode { get; set; } 

        public DateTime JoiningDate { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }   // Department Foreign Key

        public int? DesignationId { get; set; }
        public Designation? Designation { get; set; }
        public string? ManagerId { get; set; }   // Manager self Foreign Key
        [ForeignKey("ManagerId")]
        public ApplicationUser? Manager { get; set; }
    }
}