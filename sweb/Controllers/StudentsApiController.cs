using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWEB_app.Models;
using sweb.Models;

namespace sweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsApiController : ControllerBase
    {
        private readonly swebContext _context;

        public StudentsApiController(swebContext context)
        {
            _context = context;
        }

        // GET: api/StudentsApi 
        [HttpGet] 
        public List<Student> GetStudent(string firstName, string lastName) 
        { 
            IQueryable<Student> students = _context.Student.AsQueryable(); 
            if (!string.IsNullOrEmpty(lastName))
            { students = students.Where(s => s.LastName.Contains(lastName)); } 
            if (!string.IsNullOrEmpty(firstName)) 
            { students = students.Where(x => x.FirstName == firstName); }
            return students.ToList();
        }
        //TODO: proveri za getCourses
        // GET: api/StudentsApi /id/GetCourses
        [HttpGet("{id}/GetCourses")] 
        public async Task<IActionResult> GetCourses([FromRoute] int id) 
        { 
            var student = await _context.Student.FindAsync(id);
            if (student == null) 
            { 
                return NotFound(); 
            }
            var enrollments = _context.Enrollment.Where(m => m.StudentID.Equals(id.ToString())).ToList();
            List<Course> courses = new List<Course>();
            foreach (var cours in enrollments) 
            { 
                Course newCours = _context.Course.Where(m => m.ID == cours.ID).FirstOrDefault();
                newCours.Students = null;
                courses.Add(newCours); 
            } 
            return Ok(courses);
        }
        // GET: api/StudentsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }
        //TODO:proveri za putstudent
        //api/PutStudent/id/putStudent 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.ID)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.ID }, student);
        }

        // DELETE: api/StudentsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.ID == id);
        }
    }
}
