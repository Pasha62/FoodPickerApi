
using FoodPickerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Linq;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/dishes")]
    public class DishController : ControllerBase
    {

        private readonly foodpickerdbContext DBContext;

        public DishController(foodpickerdbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Dish>>> Get()
        {
            var List = await DBContext.Dishes.Select(
                s => new Dish
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

        [HttpGet("{Id}")]
        public async Task<ActionResult<Dish>> GetDishById(int Id)
        {
            Dish dish = await DBContext.Dishes.Select(s => new Dish
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


        [HttpPost("")]
        public async Task<HttpStatusCode> InsertDish(Dish dish)
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
                Calories = dish.Calories
            };
            if (dish.Type == Dish.DishType.PRIMARY) entity.Type = Dish.DishType.PRIMARY;
            if (dish.Type == Dish.DishType.SIDE) entity.Type = Dish.DishType.SIDE;
            if (dish.Type == Dish.DishType.SECONDARY) entity.Type = Dish.DishType.SECONDARY;
            if (dish.Type == Dish.DishType.DRINK) entity.Type = Dish.DishType.DRINK;
            if (dish.Type == Dish.DishType.EXTRA) entity.Type = Dish.DishType.EXTRA;
            DBContext.Dishes.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }


        [HttpPut("")]
        public async Task<HttpStatusCode> UpdateDish(Dish dish)
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

        [HttpDelete("")]
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
