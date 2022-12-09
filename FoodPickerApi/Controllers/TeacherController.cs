
using FoodPickerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    public class TeacherController : ControllerBase
    {
        private readonly foodpickerdbContext DBContext;

        public TeacherController(foodpickerdbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Teacher>>> Get()
        {
            var List = await DBContext.Teachers.Select(
                s => new Teacher
                {
                    Id = s.Id,
                    MiddleName = s.MiddleName,
                    Name = s.Name,
                    Surname = s.Surname,
                    Username = s.Username,
                    Password = s.Password
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
        public async Task<ActionResult<Teacher>> GetTeacherById(int Id)
        {
            Teacher teacher = await DBContext.Teachers.Select(s => new Teacher
            {
                Id = s.Id,
                MiddleName = s.MiddleName,
                Name = s.Name,
                Surname = s.Surname,
                Username = s.Username,
                Password = s.Password
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (teacher == null)
            {
                return NotFound();
            }
            else
            {
                return teacher;
            }
        }


        [HttpPost("")]
        public async Task<HttpStatusCode> InsertTeacher(Teacher teacher)
        {
            var entity = new Teacher()
            {
                MiddleName = teacher.MiddleName,
                Name = teacher.Name,
                Surname = teacher.Surname,
                Username = teacher.Username,
                Password = teacher.Password
            };
            DBContext.Teachers.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }


        [HttpPut("")]
        public async Task<HttpStatusCode> UpdateTeacher(Teacher teacher)
        {
            var entity = await DBContext.Teachers.FirstOrDefaultAsync(s => s.Id == teacher.Id);
            entity.MiddleName = teacher.MiddleName;
            entity.Name = teacher.Name;
            entity.Surname = teacher.Surname;
            entity.Username = teacher.Username;
            entity.Password = teacher.Password;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("")]
        public async Task<HttpStatusCode> DeleteTeacher(int Id)
        {
            var entity = new Teacher()
            {
                Id = Id
            };
            DBContext.Teachers.Attach(entity);
            DBContext.Teachers.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
