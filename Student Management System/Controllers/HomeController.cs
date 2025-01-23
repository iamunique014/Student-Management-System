using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Student_Management_System.Data;
using Student_Management_System.Models;
using System.Diagnostics;

namespace Student_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SchoolContext _context;

        public HomeController(ILogger<HomeController> logger, SchoolContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminMenu()
        {


            //student stats
            var totalStudents = _context.Students.Count();
            var activeStudents = _context.Students
                .Count(s => s.status == StudentStatus.Active);

            var inActiveStudents = _context.Students
                .Count(s => s.status == StudentStatus.Deleted);

            //Course Stats
            var totalCourses = _context.Courses.Count();
            var leastPopularCourse = _context.Courses
                .Include(c => c.Enrollments)
                .OrderBy(c => c.Enrollments.Count())
                .FirstOrDefault();

            var mostPopularCourse = _context.Courses
               .Include(c => c.Enrollments)
               .OrderByDescending(c => c.Enrollments.Count())
               .FirstOrDefault();

            //Enrollment Stats
            var totalEnrollments = _context.Enrollments.Count();
            var totalGraded = _context.Enrollments
               .Where(c => c.Grade.HasValue)
               .Count();

            var totalNonGraded = _context.Enrollments
            .Where(c => !c.Grade.HasValue)
            .Count();

            //student stats
            ViewBag.TotalStudents =  totalStudents;
            ViewBag.ActiveStudents = activeStudents;
            ViewBag.InActiveStudents = inActiveStudents;

            //Course Stats
            ViewBag.TotalCourses = totalCourses;
            ViewBag.MostPopularCourse = mostPopularCourse?.Title;
            ViewBag.LeastPopularCourse = leastPopularCourse?.Title;

            //Enrollment Stats
            ViewBag.TotalEnrollments = totalEnrollments;
            ViewBag.TotalGraded = totalGraded;
            ViewBag.TotalNonGraded = totalNonGraded;


            return View();
        }
        //nno use
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
