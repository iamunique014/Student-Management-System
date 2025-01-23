using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace Student_Management_System.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        [Required(ErrorMessage = "The Student field is required.")]
        [DisplayName("Student")]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "The Course field is required.")]
        [DisplayName("Course")]
        public int CourseID { get; set; }
        [DisplayName("Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }
        public Grade? Grade { get; set; }

        public EnrollmentStatus Status { get; set; }

        public string? StatusMessage { get; set; }

        // Navigation properties

        [ForeignKey(nameof(CourseID))]
        public required Course Course { get; set; }

        [ForeignKey(nameof(StudentID))]
        public required Student Student { get; set; }
    }
    public enum EnrollmentStatus
    {
        Active,
        Deleted
    }
}
