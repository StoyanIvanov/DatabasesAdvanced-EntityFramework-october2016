using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Models
{
    public class Homework
    {
        public enum ContentTypes
        {
            application,
            pdf,
            zip
        }

        [Key]
        public int HomeworkId { get; set; }

        public string Content { get; set; }

        [Required]
        public ContentTypes ContentType { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }

        public virtual Course Cource { get; set; }

        public virtual Student Student { get; set; }
    }
}
