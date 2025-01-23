using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Management_System.Data;
using Student_Management_System.Models;

namespace Student_Management_System.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ViewCourses()
        {

            return View(await _context.Courses.ToListAsync());
        }

        //Get:Taking the user to the page 
        public IActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(Course course)
        {

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("ViewCourses");


        }


        //Get-Update- Retrieving the current/selected record that we want to update
        public IActionResult EditCourse(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var obj = _context.Courses.Find(ID);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);

        }



        //GET: A Specific cOURSE details (URL:localhost6734.../Course/Details/6)

        public async Task<IActionResult> CourseDetails(int? Id)
        {
            if (Id == null || _context.Courses == null)
            {
                return NotFound();
            }

            // Retrieving the one course's record with the PK selected
            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == Id);


            // Use the course object as needed

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(int id, Course course)
        {

            try
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewCourses");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }

            return RedirectToAction("ViewCourses");



        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> DeleteCourse(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return RedirectToAction(nameof(ViewCourses));
            }

            try
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

                return RedirectToAction("ViewCourses");
            }
            catch (DbUpdateException ex)
            {
                ViewData["Fail"] = "Something went wrong";
            }


            return View(course);
        }
    }
}
