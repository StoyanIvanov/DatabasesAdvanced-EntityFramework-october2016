using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace HospitalProject.Models
{
    public class Diagnose
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string Comments { get; set; }

        //public Patient Patient { get; set; }
    }
}