using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Management_System.Data;
using Student_Management_System.Models;
using System.Linq;

namespace Student_Management_System.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly SchoolContext _context;

        public EnrollmentsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ViewEnrollments()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.Status == EnrollmentStatus.Active)
                .ToListAsync();

            return View(enrollments);

        }

        public async Task<IActionResult> ViewInActiveEnrollments()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.Status == EnrollmentStatus.Deleted)
                .ToListAsync();

            return View(enrollments);

        }

        // GET: Enrollment/Add
        public IActionResult CreateEnrollment()
        {
            ViewData["Students"] = new SelectList(_context.Students.Where(s => s.status == StudentStatus.Active), "StudentID", "FullName");
            ViewData["Courses"] = new SelectList(_context.Courses.Where(c => c.Status == CourseStatus.Active), "CourseID", "Title");
            return View();
        }

        // POST: Enrollment/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEnrollment([Bind("StudentID, CourseID")] Enrollment enrollment)
        {

            var existingEnrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentID == enrollment.StudentID && e.CourseID == enrollment.CourseID);

            if (existingEnrollment != null)
            {
                //Custom validation error
                //ModelState.AddModelError("", "This student is already enrolled in the selected course.");
                enrollment.StatusMessage = "Error This student is already enrolled in the selected course";
                PopulateDropdowns(enrollment.StudentID, enrollment.CourseID);
                return View(enrollment);
            }

            enrollment.EnrollmentDate = DateTime.Now;
            try
            {
                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();
                enrollment.StatusMessage = "Student Enrolled Successfully";
                return RedirectToAction("ViewEnrollments");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.ToString());
            }


            Console.WriteLine("// If something failed, Display the form again with errors");
            PopulateDropdowns(enrollment.StudentID, enrollment.CourseID);
            return View(enrollment);

        }





        //Get-Update- Retrieving the current/selected record that we want to update
        public IActionResult EditEnrollment(int? ID, string mode = "Edit")
        {

            var enrollment = _context.Enrollments
               .Include(e => e.Student)
               .Include(e => e.Course)
               .FirstOrDefault(e => e.EnrollmentID == ID);

            if (enrollment == null)
            {
                return NotFound();
            }

            ViewData["Students"] = new SelectList(_context.Students.Where(s => s.status == StudentStatus.Active), "StudentID", "FullName", enrollment.StudentID);
            ViewData["Courses"] = new SelectList(_context.Courses.Where(c => c.Status == CourseStatus.Active), "CourseID", "Title", enrollment.CourseID);


            ViewBag.Mode = mode; // Pass the mode to the view
            return View(enrollment);

        }



        // POST: Enrollment/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEnrollment(int id, [Bind("EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                Console.WriteLine("Bad Request enrollment.EnrollmentID: {0} AND id: {1}", enrollment.EnrollmentID, id);
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"EnrollmentID: {enrollment.EnrollmentID}, StudentID: {enrollment.StudentID}, CourseID: {enrollment.CourseID}");

                    Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
                }
            }



            // Prevent duplicate enrollments
            var existingEnrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentID == enrollment.StudentID && e.CourseID == enrollment.CourseID && e.EnrollmentID != id);

            if (existingEnrollment != null)
            {

                ModelState.AddModelError("", "This student is already enrolled in the selected course.");
                PopulateDropdowns(enrollment.StudentID, enrollment.CourseID);
                return View(enrollment);
            }

            try
            {
                enrollment.EnrollmentDate = DateTime.Now;
                _context.Update(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewEnrollments");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Enrollments.Any(e => e.EnrollmentID == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


        }
        // Helper method for dropdowns
        private void PopulateDropdowns(int selectedStudentId, int selectedCourseId)
        {
            ViewData["Students"] = new SelectList(
                _context.Students.Where(s => s.status == StudentStatus.Active),
                "StudentID", "FullName", selectedStudentId);

            ViewData["Courses"] = new SelectList(
                _context.Courses.Where(c => c.Status == CourseStatus.Active),
                "CourseID", "Title", selectedCourseId);
        }

        //Get Delete Enrollment
        public async Task<IActionResult> DeleteEnrollment(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }


            var enrollment = await _context.Enrollments
                .AsNoTracking()
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.EnrollmentID == id && e.Status == EnrollmentStatus.Active);

            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("DeleteEnrollment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                enrollment.Status = EnrollmentStatus.Deleted;
                _context.Update(enrollment);
                await _context.SaveChangesAsync();

                return RedirectToAction("ViewEnrollments");
            }
            catch (DbUpdateException ex)
            {
                ViewData["Fail"] = "Something went wrong";
            }


            return View(enrollment);
        }
    }
}

