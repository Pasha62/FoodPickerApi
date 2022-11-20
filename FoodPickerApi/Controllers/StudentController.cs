using FoodPickerApi.DTO;
using FoodPickerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly foodpickerdbContext DBContext;

        public StudentController(foodpickerdbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetStudents")]
        public async Task<ActionResult<List<StudentDTO>>> Get()
        {
            var List = await DBContext.Students.Select(
                s => new StudentDTO
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

        [HttpGet("GetStudentById")]
        public async Task<ActionResult<StudentDTO>> GetStudentById(int Id)
        {
            StudentDTO student = await DBContext.Students.Select(s => new StudentDTO
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


        [HttpPost("InsertStudent")]
        public async Task<HttpStatusCode> InsertStudent(StudentDTO student)
        {
            var entity = new Student()
            {
                MiddleName = student.MiddleName,
                Name = student.Name,
                Surname = student.Surname,
                GradeId = student.GradeId
            };
            DBContext.Students.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }


        [HttpPut("UpdateStudent")]
        public async Task<HttpStatusCode> UpdateStudent(StudentDTO student)
        {
            var entity = await DBContext.Students.FirstOrDefaultAsync(s => s.Id == student.Id);
            entity.MiddleName = student.MiddleName;
            entity.Name = student.Name;
            entity.Surname = student.Surname;
            entity.GradeId = student.GradeId;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteStudent/{Id}")]
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
