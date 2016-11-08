using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace HospitalProject.Models
{
    public class Patient
    {
        private string email;

        public Patient()
        {
            this.Visitations=new List<Visitation>();
            this.Diagnoses=new List<Diagnose>();
            this.Medicaments=new List<Medicament>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string LastName { get; set; }

        [MaxLength(250)]
        [Column(TypeName = "varchar")]
        public string Address { get; set; }

        [Required]

        public string Email
        {
            get { return this.email; }
            set
            {
                if (!Regex.Match(value, "^([^\\.\\-\\/_][\\w\\.\\-\\/_]*[^\\.\\-\\/_]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", RegexOptions.ECMAScript).Success)
                {
                    throw new ArgumentOutOfRangeException("Invalid email");
                }

                this.email = value;
            }
        }

        [Required]
        public DateTime BurthDate { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public ICollection<Visitation> Visitations { get; set; }


        public ICollection<Diagnose> Diagnoses { get; set; }


        public ICollection<Medicament> Medicaments { get; set; }

    }
}