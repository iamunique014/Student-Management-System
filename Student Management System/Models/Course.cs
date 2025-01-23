using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Student_Management_System.Models
{
    public class Course
    {

        [Key]
        public int CourseID { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Credits")]
        public int Credits { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Status")]
        public CourseStatus Status { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }

    public enum CourseStatus
    {
        Active,
        Deleted
    }
}
