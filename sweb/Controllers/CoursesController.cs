using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWEB_app.Models;
using sweb.Models;

namespace sweb.Controllers
{
    public class CoursesController : Controller
    {
        private readonly swebContext _context;

        public CoursesController(swebContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index(string searchSemestar , string searchString, string searchPrograma, string searchTeacher)
        {
           
            var courses = from m in _context.Course
                        select m;
            var teachers = from v in _context.Teacher
                          select v;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(s => s.Title.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchPrograma))
            {
                courses = courses.Where(s => s.Programe.Contains(searchPrograma));
            }
            if (!String.IsNullOrEmpty(searchSemestar))
            {
                int x = 0;

                Int32.TryParse(searchSemestar, out x);
                courses = courses.Where(s => s.Semester == x);
            }
            if (!String.IsNullOrEmpty(searchTeacher))
            {
                int x = 0;

                Int32.TryParse(searchTeacher, out x);
                courses = courses.Where(s => s.FirstTeacherID == x || s.SecondTeacherID == x);
                
            }
            

            return View(await courses.ToListAsync());
            //return View(await _context.Course.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(string title)
        {
            if (title == null)
            {
                return NotFound();
            }

            var courses = from m in _context.Course
                          select m;
            courses = courses.Where(s => s.Title.Contains(title));
            var students = courses.Select(s => new { students = s.Students });
           
            return View(await students.ToListAsync());
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Credits,Semester,Programe,EducationLevel,FirstTeacherID,SecondTeacherID")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Credits,Semester,Programe,EducationLevel,FirstTeacherID,SecondTeacherID")] Course course)
        {
            if (id != course.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.ID == id);
        }
        [HttpGet]
        private async Task<IActionResult> showStudents()
        {
            var students = from m in _context.Student
                           select m;
            return View(await students.ToListAsync());
        }
    }
}
