
using FoodPickerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly foodpickerdbContext DBContext;

        public StudentController(foodpickerdbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Student>>> Get()
        {
            var List = await DBContext.Students.Select(
                s => new Student
                {
                    Id = s.Id,
                    MiddleName = s.MiddleName,
                    Name = s.Name,
                    Surname = s.Surname,
                    GradeId = s.GradeId
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Student>> GetStudentById(int Id)
        {
            Student student = await DBContext.Students.Select(s => new Student
            {
                Id = s.Id,
                MiddleName = s.MiddleName,
                Name = s.Name,
                Surname = s.Surname,
                GradeId = s.GradeId
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                return student;
            }
        }


        [HttpPost("")]
        public async Task<HttpStatusCode> InsertStudent(Student student)
        {
            var entity = new Student()
            {
                MiddleName = student.MiddleName,
                Name = student.Name,
                Surname = student.Surname,
                GradeId = DBContext.Grades.FirstOrDefaultAsync(g => g.Id == student.GradeId) != null ? student.GradeId : null
            };
            if(entity.GradeId != null)
            {
                DBContext.Students.Add(entity);
                await DBContext.SaveChangesAsync();
                return HttpStatusCode.Created;
            }
            else
            {
                return HttpStatusCode.NotFound;
            }
        }


        [HttpPut("")]
        public async Task<HttpStatusCode> UpdateStudent(Student student)
        {
            var entity = await DBContext.Students.FirstOrDefaultAsync(s => s.Id == student.Id);
            entity.MiddleName = student.MiddleName;
            entity.Name = student.Name;
            entity.Surname = student.Surname;
            entity.GradeId = student.GradeId;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("")]
        public async Task<HttpStatusCode> DeleteStudent(int Id)
        {
            var entity = new Student()
            {
                Id = Id
            };
            DBContext.Students.Attach(entity);
            DBContext.Students.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
