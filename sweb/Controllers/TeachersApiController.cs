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
    public class TeachersApiController : ControllerBase
    {
        private readonly swebContext _context;

        public TeachersApiController(swebContext context)
        {
            _context = context;
        }
        // GET: api/TeachersApi 
        [HttpGet]
        public List<Teacher> GetTeacher(string firstName, string lastName)
        {
            IQueryable<Teacher> teachers = _context.Teacher.AsQueryable();
            if (!string.IsNullOrEmpty(lastName))
            { teachers = teachers.Where(s => s.LastName.Contains(lastName)); }
            if (!string.IsNullOrEmpty(firstName))
            { teachers = teachers.Where(x => x.FirstName == firstName); }
            return teachers.ToList();
        }
        // GET: api/TeachersApi/id/getcourses 
        [HttpGet("{id}/GetCourses")]
        public async Task<IActionResult> GetCourses([FromRoute] int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            var courses = from c in _context.Course
                          select c;
            courses = courses.Where(c => c.FirstTeacherID.Equals(id.ToString()) || c.SecondTeacherID.Equals(id.ToString()));
            return Ok(courses);
        }
        
        // GET: api/TeachersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.ID)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            _context.Teacher.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.ID }, teacher);
        }

        // DELETE: api/TeachersApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> DeleteTeacher(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();

            return teacher;
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.ID == id);
        }
    }
}
