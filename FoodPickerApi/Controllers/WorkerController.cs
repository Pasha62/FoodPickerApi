
using FoodPickerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/workers")]
    public class WorkerController : ControllerBase
    {
        private readonly foodpickerdbContext DBContext;

        public WorkerController(foodpickerdbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Worker>>> Get()
        {
            var List = await DBContext.Workers.Select(
                s => new Worker
                {
                    Id = s.Id,
                    MiddleName = s.MiddleName,
                    Name = s.Name,
                    Surname = s.Surname,
                    Username = s.Username,
                    Password = s.Password,
                    Role = s.Role
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
        public async Task<ActionResult<Worker>> GetWorkerById(int Id)
        {
            Worker worker = await DBContext.Workers.Select(s => new Worker
            {
                Id = s.Id,
                MiddleName = s.MiddleName,
                Name = s.Name,
                Surname = s.Surname,
                Username = s.Username,
                Password = s.Password,
                Role = s.Role
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (worker == null)
            {
                return NotFound();
            }
            else
            {
                return worker;
            }
        }


        [HttpPost("")]
        public async Task<HttpStatusCode> InsertWorker(Worker worker)
        {
            var entity = new Worker()
            {
                MiddleName = worker.MiddleName,
                Name = worker.Name,
                Surname = worker.Surname,
                Username = worker.Username,
                Password = worker.Password,
                Role = worker.Role
            };
            DBContext.Workers.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }


        [HttpPut("")]
        public async Task<HttpStatusCode> UpdateWorker(Worker worker)
        {
            var entity = await DBContext.Workers.FirstOrDefaultAsync(s => s.Id == worker.Id);
            entity.MiddleName = worker.MiddleName;
            entity.Name = worker.Name;
            entity.Surname = worker.Surname;
            entity.Username = worker.Username;
            entity.Password = worker.Password;
            entity.Role = worker.Role;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("")]
        public async Task<HttpStatusCode> DeleteWorker(int Id)
        {
            var entity = new Worker()
            {
                Id = Id
            };
            DBContext.Workers.Attach(entity);
            DBContext.Workers.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
