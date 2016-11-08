using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Medicament
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string Name { get; set; }

        //public Patient Patient { get; set; }
    }
}