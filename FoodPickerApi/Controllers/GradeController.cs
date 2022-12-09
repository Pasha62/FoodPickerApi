
using FoodPickerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/grades")]
    public class GradeController : ControllerBase
    {
        private readonly foodpickerdbContext DBContext;

        public GradeController(foodpickerdbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Grade>>> Get()
        {
            var List = await DBContext.Grades.Select(
                s => new Grade
                {
                    Id = s.Id,
                    Letter = s.Letter,
                    Number = s.Number,
                    TeacherId = s.TeacherId
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
        public async Task<ActionResult<Grade>> GetGradeById(int Id)
        {
            Grade grade = await DBContext.Grades.Select(s => new Grade
            {
                Id = s.Id,
                Letter = s.Letter,
                Number = s.Number,
                TeacherId = s.TeacherId
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (grade == null)
            {
                return NotFound();
            }
            else
            {
                return grade;
            }
        }


        [HttpPost("")]
        public async Task<HttpStatusCode> InsertGrade(Grade grade)
        {
            var entity = new Grade()
            {
                Letter = grade.Letter,
                Number = grade.Number,
                TeacherId = DBContext.Teachers.FirstOrDefaultAsync(g => g.Id == grade.TeacherId) != null ? grade.TeacherId : null
            };
            DBContext.Grades.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }


        [HttpPut("")]
        public async Task<HttpStatusCode> UpdateGrade(Grade grade)
        {
            var entity = await DBContext.Grades.FirstOrDefaultAsync(s => s.Id == grade.Id);
            entity.Letter = grade.Letter;
            entity.Number = grade.Number;
            entity.TeacherId = DBContext.Teachers.FirstOrDefaultAsync(g => g.Id == grade.TeacherId) != null ? grade.TeacherId : null;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("")]
        public async Task<HttpStatusCode> DeleteGrade(int Id)
        {
            var entity = new Grade()
            {
                Id = Id
            };
            DBContext.Grades.Attach(entity);
            DBContext.Grades.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
