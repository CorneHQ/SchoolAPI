using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;
using System.Linq;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : Controller
    {
        private readonly SchoolContext _context;

        public TeachersController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AllTeachers()
        {
            return Ok(_context.Teachers.Include(x => x.Students));
        }

        [HttpGet("{id}")]
        public IActionResult SingleTeacher(int id)
        {
            Teacher teacher = _context.Teachers.Include(t => t.Students).FirstOrDefault(t => t.Id == id);

            if(teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpPost]
        public IActionResult CreateTeacher([FromBody] Teacher newTeacher)
        {

            _context.Teachers.Add(newTeacher);
            _context.SaveChanges();

            return Ok(newTeacher);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeacher(int id, [FromBody] Teacher newValues)
        {
            Teacher teacher = _context.Teachers.Include(t => t.Students).FirstOrDefault(t => t.Id == id);

            if(teacher == null)
            {
                return NotFound();
            }

            teacher.Name = newValues.Name;
            teacher.Course = newValues.Course;
            _context.SaveChanges();

            return Ok(teacher);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            Teacher teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);

            if(teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();

            return Ok(teacher);
        }

        [HttpGet("{id}/students")]
        public IActionResult GetStudents(int id)
        {
            Teacher teacher = _context.Teachers.Include(x => x.Students).FirstOrDefault(t => t.Id == id);

            if(teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher.Students);
        }

        [HttpPost("{id}/students")]
        public IActionResult AddStudent(int id, [FromBody] Student student)
        {
            Teacher teacher = _context.Teachers.Include(x => x.Students).FirstOrDefault(t => t.Id == id);

            if(teacher == null)
            {
                return NotFound();
            }

            teacher.Students.Add(student);
            _context.SaveChanges();

            return Ok(student);
        }
    }
}

// GET -> Ophalen van info
// POST -> Versturen van info
// DELETE -> Verwijderen van een entity
// PUT/PATCH -> Updaten van een enitity
