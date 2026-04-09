using System.ComponentModel.DataAnnotations;

namespace LASMSProject.Models
{
    public class Designation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
