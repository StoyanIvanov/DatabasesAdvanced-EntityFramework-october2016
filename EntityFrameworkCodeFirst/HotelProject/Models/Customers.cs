using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProject.Models
{
    public class Customers
    {
        [Key]
        public int AccountNumber { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string LastName { get; set; }

        [Required]
        [MinLength(6), MaxLength(11)]
        public string PhoneNumber { get; set; }

        [MinLength(3), MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string EmergencyName { get; set; }

        [MinLength(6), MaxLength(11)]
        public string EmergencyNumber { get; set; }

        public string Notes { get; set; }



    }
}