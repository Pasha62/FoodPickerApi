using FoodPickerApi.DTO;
using FoodPickerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly foodpickerdbContext DBContext;

        public TeacherController(foodpickerdbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetTeachers")]
        public async Task<ActionResult<List<TeacherDTO>>> Get()
        {
            var List = await DBContext.Teachers.Select(
                s => new TeacherDTO
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

        [HttpGet("GetTeacherById")]
        public async Task<ActionResult<TeacherDTO>> GetTeacherById(int Id)
        {
            TeacherDTO teacher = await DBContext.Teachers.Select(s => new TeacherDTO
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


        [HttpPost("InsertTeacher")]
        public async Task<HttpStatusCode> InsertTeacher(TeacherDTO teacher)
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


        [HttpPut("UpdateTeacher")]
        public async Task<HttpStatusCode> UpdateTeacher(TeacherDTO teacher)
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

        [HttpDelete("DeleteTeacher/{Id}")]
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
