using FoodPickerApi.DTO;
using FoodPickerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/parents")]
    public class ParentController : ControllerBase
    {
        private readonly foodpickerdbContext DBContext;

        public ParentController(foodpickerdbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<ParentDTO>>> Get()
        {
            var List = await DBContext.Parents.Select(
                s => new ParentDTO
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
        public async Task<ActionResult<ParentDTO>> GetParentById(int Id)
        {
            ParentDTO parent = await DBContext.Parents.Select(s => new ParentDTO
            {
                Id = s.Id,
                MiddleName = s.MiddleName,
                Name = s.Name,
                Surname = s.Surname,
                Username = s.Username,
                Password = s.Password
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (parent == null)
            {
                return NotFound();
            }
            else
            {
                return parent;
            }
        }


        [HttpPost("")]
        public async Task<HttpStatusCode> InsertParent(ParentDTO parent)
        {
            var entity = new Parent()
            {
                MiddleName = parent.MiddleName,
                Name = parent.Name,
                Surname = parent.Surname,
                Username = parent.Username,
                Password = parent.Password
            };
            DBContext.Parents.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }


        [HttpPut("")]
        public async Task<HttpStatusCode> UpdateParent(ParentDTO parent)
        {
            var entity = await DBContext.Parents.FirstOrDefaultAsync(s => s.Id == parent.Id);
            entity.MiddleName = parent.MiddleName;
            entity.Name = parent.Name;
            entity.Surname = parent.Surname;
            entity.Username = parent.Username;
            entity.Password = parent.Password;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("")]
        public async Task<HttpStatusCode> DeleteParent(int Id)
        {
            var entity = new Parent()
            {
                Id = Id
            };
            DBContext.Parents.Attach(entity);
            DBContext.Parents.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
