using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models
{

    public class Resource
    {
        ICollection<ScoolLicense> licenses;
        public enum ResourcesTypes
        {
            video,
            presentation,
            document,
            other
        }

        public Resource()
        {
            this.licenses = new HashSet<ScoolLicense>();
        }

        [Key]
        public int ResourceId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ResourcesTypes Type { get; set; }

        [Required]
        public string URL { get; set; }

        public virtual Course Cource { get; set; }

        public virtual ICollection<ScoolLicense> Licenses
        {
            get { return this.licenses; }
            set { this.licenses = value; }
        }

    }
}