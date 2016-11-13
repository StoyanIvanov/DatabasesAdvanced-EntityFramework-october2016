using System;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models
{

    public class Resource
    {
        public enum ResourcesTypes
        {
            video,
            presentation,
            document,
            other
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
    }
}