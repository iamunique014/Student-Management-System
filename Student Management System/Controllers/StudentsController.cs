using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Management_System.Data;
using Student_Management_System.Models;

namespace Student_Management_System.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Students records from Database
        public async Task<IActionResult> ViewStudents()
        {

            var students = await _context.Students
                .Include(s => s.Enrollments)
                .Where(s => s.status == StudentStatus.Active)
                .ToListAsync();

            return View(students);
        }

        public IActionResult ManageStudent()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageStudent(int? Id)
        {
            if (Id == null || _context.Students == null)
            {
                return NotFound();
            }

            // Retrieving the one student's record with the PK selected
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentID == Id && s.status == StudentStatus.Active);


            // Use the student object as needed

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        //GET: A Specific student details (URL:localhost6734.../Student/Details/6)

        public async Task<IActionResult> StudentDetails(int? Id)
        {
            if (Id == null || _context.Students == null)
            {
                return NotFound();
            }

            // Retrieving the one student's record with the PK selected
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentID == Id && s.status == StudentStatus.Active);


            // Use the student object as needed

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        //Get:Taking the user to the page 
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(Student student)
        {

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("ViewStudents");


        }





        //Get-Update- Retrieving the current/selected record that we want to update
        public IActionResult EditStudent(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var obj = _context.Students.Find(ID);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(int id, Student student)
        {

            try
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewStudents");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }

            return RedirectToAction("ViewStudents");



        }

        // GET: Students/Delete/5
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StudentID == id && s.status == StudentStatus.Active);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Get Active students only
            var student = await _context.Students
                 .FirstOrDefaultAsync(s => s.StudentID == id && s.status == StudentStatus.Active);


            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                // Perform soft delete
                student.status = StudentStatus.Deleted;
                _context.Update(student);
                await _context.SaveChangesAsync();

                return RedirectToAction("ViewStudents");
            }
            catch (DbUpdateException ex)
            {
                ViewData["Fail"] = "Something went wrong";
            }


            return View(student);
        }

    }
}
