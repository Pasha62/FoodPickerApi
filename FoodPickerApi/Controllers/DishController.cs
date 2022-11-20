using FoodPickerApi.DTO;
using FoodPickerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Linq;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishController : ControllerBase
    {

        private readonly foodpickerdbContext DBContext;

        public DishController(foodpickerdbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetDishes")]
        public async Task<ActionResult<List<DishDTO>>> Get()
        {
            var List = await DBContext.Dishes.Select(
                s => new DishDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    ImgUrl = s.ImgUrl,
                    Price = s.Price,
                    Proteins = s.Proteins,
                    Carbs = s.Carbs,
                    Fats = s.Fats,
                    WeightGrams = s.WeightGrams,
                    Ingredients = s.Ingredients,
                    Calories = s.Calories,
                    Type = s.Type
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

        [HttpGet("GetDishById")]
        public async Task<ActionResult<DishDTO>> GetDishById(int Id)
        {
            DishDTO dish = await DBContext.Dishes.Select(s => new DishDTO
            {
                Id = s.Id,
                Name = s.Name,
                ImgUrl = s.ImgUrl,
                Price = s.Price,
                Proteins = s.Proteins,
                Carbs = s.Carbs,
                Fats = s.Fats,
                WeightGrams = s.WeightGrams,
                Ingredients = s.Ingredients,
                Calories = s.Calories,
                Type = s.Type
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (dish == null)
            {
                return NotFound();
            }
            else
            {
                return dish;
            }
        }


        [HttpPost("InsertDish")]
        public async Task<HttpStatusCode> InsertDish(DishDTO dish)
        {
            var entity = new Dish()
            {
                Name = dish.Name,
                ImgUrl = dish.ImgUrl,
                Price = dish.Price,
                Proteins = dish.Proteins,
                Carbs = dish.Carbs,
                Fats = dish.Fats,
                WeightGrams = dish.WeightGrams,
                Ingredients = dish.Ingredients,
                Calories = dish.Calories,
                Type = dish.Type
            };
            DBContext.Dishes.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }


        [HttpPut("UpdateDish")]
        public async Task<HttpStatusCode> UpdateDish(DishDTO dish)
        {
            var entity = await DBContext.Dishes.FirstOrDefaultAsync(s => s.Id == dish.Id);
            entity.Name = dish.Name;
            entity.ImgUrl = dish.ImgUrl;
            entity.Price = dish.Price;
            entity.Proteins = dish.Proteins;
            entity.Carbs = dish.Carbs;
            entity.Fats = dish.Fats;
            entity.WeightGrams = dish.WeightGrams;
            entity.Ingredients = dish.Ingredients;
            entity.Calories = dish.Calories;
            entity.Type = dish.Type;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteDish/{Id}")]
        public async Task<HttpStatusCode> DeleteDish(int Id)
        {
            var entity = new Dish()
            {
                Id = Id
            };
            DBContext.Dishes.Attach(entity);
            DBContext.Dishes.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
