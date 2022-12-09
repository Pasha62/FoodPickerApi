
using FoodPickerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly foodpickerdbContext DBContext;

        public OrderController(foodpickerdbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Order>>> Get()
        {
            var List = await DBContext.Orders.Select(
                s => new Order
                {
                    Id = s.Id,
                    Date = s.Date,
                    Cost = s.Cost,
                    DishId = s.DishId,
                    StudentId = s.StudentId
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
        public async Task<ActionResult<Order>> GetOrderById(int Id)
        {
            Order order = await DBContext.Orders.Select(s => new Order
            {
                Id = s.Id,
                Date = s.Date,
                Cost = s.Cost,
                DishId = s.DishId,
                StudentId = s.StudentId
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                return order;
            }
        }


        [HttpPost("")]
        public async Task<HttpStatusCode> InsertOrder(Order order)
        {
            var entity = new Order()
            {
                Date = order.Date,
                Cost = order.Cost,
                DishId = DBContext.Dishes.FirstOrDefaultAsync(g => g.Id == order.DishId) != null ? order.DishId : null,
                StudentId = DBContext.Students.FirstOrDefaultAsync(g => g.Id == order.StudentId) != null ? order.StudentId : null
            };
            DBContext.Orders.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }


        [HttpPut("")]
        public async Task<HttpStatusCode> UpdateOrder(Order order)
        {
            var entity = await DBContext.Orders.FirstOrDefaultAsync(s => s.Id == order.Id);
            entity.Date = order.Date;
            entity.Cost = order.Cost;
            entity.DishId = DBContext.Dishes.FirstOrDefaultAsync(g => g.Id == order.DishId) != null ? order.DishId : null;
            entity.StudentId = DBContext.Students.FirstOrDefaultAsync(g => g.Id == order.StudentId) != null ? order.StudentId : null;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("")]
        public async Task<HttpStatusCode> DeleteOrder(int Id)
        {
            var entity = new Order()
            {
                Id = Id
            };
            DBContext.Orders.Attach(entity);
            DBContext.Orders.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
