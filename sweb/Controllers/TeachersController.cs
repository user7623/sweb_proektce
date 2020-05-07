using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWEB_app.Models;
using sweb.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using sweb.ViewModels;

namespace sweb.Controllers
{
    public class TeachersController : Controller
    {
        private readonly swebContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment he;
        private readonly IWebHostEnvironment hostEnvironment;

        public TeachersController(swebContext context, IWebHostEnvironment e, IWebHostEnvironment hostEnvironment)
        {
            he = e;
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }
        [HttpPost]
        public async Task<IActionResult> showPicture(string firstName, string lastName, IFormFile pic)
        {
            ViewData["fname"] = firstName;
            if (pic != null)
            {
                var filename = Path.Combine(he.WebRootPath, Path.GetFileName(pic.FileName));
                pic.CopyTo(new FileStream(filename, FileMode.Create));
                ViewData["filelocation"] = "/" + Path.GetFileName(pic.FileName);
            }
            var selected = await _context.Teacher.Where(s => s.FirstName.Equals(firstName) && s.LastName.Equals(lastName)).FirstOrDefaultAsync();
            TeacherViewModel model = new TeacherViewModel();
            model.Id = selected.ID;
            model.FirstName = selected.FirstName;
            model.LastName = selected.LastName;
            model.Degree = selected.Degree;
            model.AcademicRank = selected.AcademicRank;
            model.HireDate = selected.HireDate;
            model.OfficeNumber = selected.OfficeNumber;

            if (System.IO.File.Exists(Path.GetFileName(pic.FileName)))
            {
                model.filePath = Path.GetFileName(pic.FileName);
            }

            return View(model);
        }
        public async Task<IActionResult> addPicture(int? id)
        {
            return View();
        }
        // GET: Teachers
        public async Task<IActionResult> Index(string searchName, string searchLastName, string searchAcademicRank, string searchEducation)
        {

            var teachers = from m in _context.Teacher
                          select m;

            if (!String.IsNullOrEmpty(searchName))
            {
                teachers = teachers.Where(s => s.FirstName.Contains(searchName));
            }
            if (!String.IsNullOrEmpty(searchLastName))
            {
                teachers = teachers.Where(s => s.LastName.Contains(searchLastName));
            }
            if (!String.IsNullOrEmpty(searchAcademicRank))
            {
                teachers = teachers.Where(s => s.AcademicRank.Contains(searchAcademicRank));
            }
            if (!String.IsNullOrEmpty(searchEducation))
            {
                teachers = teachers.Where(s => s.Degree.Contains(searchEducation));
            }


            return View(await teachers.ToListAsync());

            // return View(await _context.Teacher.ToListAsync());
        }
        public async Task<IActionResult> Enrolled(int? id)
        {

            var enrol = from c in _context.Enrollment
                          select c;

            enrol = enrol.Where(s => s.CourseID == id);

            return View(await enrol.ToListAsync());
        }
            // GET: Teachers/Details/5  Enrolled
            public async Task<IActionResult> Details(int? id)
        {

            var courses = from c in _context.Course
                          select c;

            courses = courses.Where(s => s.FirstTeacherID == id || s.SecondTeacherID == id);

            return View(await courses.ToListAsync());

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var teacher = await _context.Teacher
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (teacher == null)
            //{
            //    return NotFound();
            //}

            //return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Degree,AcademicRank,OfficeNumber,HireDate")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Degree,AcademicRank,OfficeNumber,HireDate")] Teacher teacher)
        {
            if (id != teacher.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.ID))
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
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.ID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.ID == id);
        }
        // new functiuonalities
        //Teacher SubjectsList
        private async Task<IActionResult> CoursesList(int? id)
        {

            var coursesList = from m in _context.Course
                              select m;
            coursesList = coursesList.Where(s => s.FirstTeacherID == id || s.SecondTeacherID == id);

            return View(coursesList);
        }
    }
}
