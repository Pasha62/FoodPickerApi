
using FoodPickerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/parentstudents")]
    public class ParentStudentController : ControllerBase
    {
        private readonly foodpickerdbContext DBContext;

        public ParentStudentController(foodpickerdbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<ParentStudent>>> Get()
        {
            var List = await DBContext.ParentStudents.Select(
                s => new ParentStudent
                {
                    Id = s.Id,
                    StudentId = s.StudentId,
                    ParentId = s.ParentId
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
        public async Task<ActionResult<ParentStudent>> GetParentStudentById(int Id)
        {
            ParentStudent parentstudent = await DBContext.ParentStudents.Select(s => new ParentStudent
            {
                Id = s.Id,
                StudentId = s.StudentId,
                ParentId = s.ParentId
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (parentstudent == null)
            {
                return NotFound();
            }
            else
            {
                return parentstudent;
            }
        }


        [HttpPost("")]
        public async Task<HttpStatusCode> InsertParentStudent(ParentStudent parentstudent)
        {
            var entity = new ParentStudent()
            {
                ParentId = DBContext.Parents.FirstOrDefaultAsync(g => g.Id == parentstudent.ParentId) != null ? parentstudent.ParentId : null,
                StudentId = DBContext.Students.FirstOrDefaultAsync(g => g.Id == parentstudent.StudentId) != null ? parentstudent.StudentId : null
            };
            DBContext.ParentStudents.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }


        [HttpPut("")]
        public async Task<HttpStatusCode> UpdateParentStudent(ParentStudent parentstudent)
        {
            var entity = await DBContext.ParentStudents.FirstOrDefaultAsync(s => s.Id == parentstudent.Id);
            entity.ParentId = DBContext.Parents.FirstOrDefaultAsync(g => g.Id == parentstudent.ParentId) != null ? parentstudent.ParentId : null;
            entity.StudentId = DBContext.Students.FirstOrDefaultAsync(g => g.Id == parentstudent.StudentId) != null ? parentstudent.StudentId : null;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("")]
        public async Task<HttpStatusCode> DeleteParentStudents(int Id)
        {
            var entity = new ParentStudent()
            {
                Id = Id
            };
            DBContext.ParentStudents.Attach(entity);
            DBContext.ParentStudents.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
