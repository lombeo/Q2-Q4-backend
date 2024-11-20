using Api_Project_Prn.Infra.Entities;
using Api_Project_Prn.Services.StudentService;
using Microsoft.AspNetCore.Mvc;

namespace Api_Project_Prn.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : BaseAPIController
    {
        private readonly IStudentService _studentService;
        private readonly List<Student> _students = new List<Student>
        {
            new Student("Nguyen Van A", 10, 9, 8),
            new Student("Tran Thi B", 7, 8, 9),
            new Student("Le Van C", 8, 8, 8),
            new Student("Pham Thi D", 9, 8, 7),
            new Student("Bui Van E", 6, 7, 8)
        };

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("sorted")]
        public IActionResult GetSortedStudents()
        {
            var sortedStudents = _studentService.SortStudents(_students);
            return Ok(sortedStudents);
        }

        [HttpGet("find")]
        public IActionResult FindStudent(double average = 8)
        {
            var student = _studentService.FindStudentWithAverageScore(_students, average);
            if (student != null)
            {
                return Ok(student);
            }

            return NotFound("Student with the specified average score not found.");
        }
    }
}
