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
    public class CoursesApiController : ControllerBase
    {
        private readonly swebContext _context;

        public CoursesApiController(swebContext context)
        {
            _context = context;
        }

        // GET: api/CoursesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            return await _context.Course.ToListAsync();
        }

        // GET: api/CoursesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }
        //TODO:proveri dali raboti
        // GET: api/CoursesApi/5/getenrollments
        [HttpGet("{id}/GetEnrolledStudents")]
        public async Task<IActionResult> GetEnrolledStudents([FromRoute] int id)
        {
            var course = await _context.Course.FindAsync(id);
            
            if (course == null)
            {
                return NotFound();
            }
            var enrollment = from e in _context.Enrollment
                              select e;
            enrollment = enrollment.Where(e => e.CourseID == id);
            List<Enrollment> enrollments = new List<Enrollment>();
            foreach (var enrol in enrollment)
            {
                Enrollment newEnrol = _context.Enrollment.Where(m => m.ID == enrol.ID).FirstOrDefault();
                enrollments.Add(newEnrol);
            }
            List<Student> students = new List<Student>();
            foreach(var enrol in enrollment)
            {
                Student s = _context.Student.Where(x => x.StudentID.Equals(enrol.StudentID)).FirstOrDefault();
                students.Add(s);
            }
            return Ok(students);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.ID)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.ID }, course);
        }

        // DELETE: api/CoursesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return course;
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.ID == id);
        }
    }
}
