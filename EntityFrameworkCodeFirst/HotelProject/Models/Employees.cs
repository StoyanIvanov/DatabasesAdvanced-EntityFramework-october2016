using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProject.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3), MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MinLength(3), MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [NotMapped] //The attribute not be create column FullName
        public string FullName { get; set; }

        [StringLength(50)]
        public string Title { get; set; }
        
        public string Notes { get; set; }
    }
}