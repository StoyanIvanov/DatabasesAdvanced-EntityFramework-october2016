using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Visitation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime VisitationDate { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string Comments { get; set; }

        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }
    }
}