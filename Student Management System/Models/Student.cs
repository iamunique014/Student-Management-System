using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace Student_Management_System.Models
{

    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Status")]
        public StudentStatus status { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        // For displaying Full Name
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

    }
    public enum StudentStatus
    {
        Active,
        Deleted
    }
}
