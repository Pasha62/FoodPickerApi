
using FoodPickerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

namespace FoodPickerApi.Controllers
{
    [ApiController]
    [Route("api/preferences")]
    public class PreferenceController : ControllerBase
    {
        private readonly foodpickerdbContext DBContext;

        public PreferenceController(foodpickerdbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Preference>>> Get()
        {
            var List = await DBContext.Preferences.Select(
                s => new Preference
                {
                    Id = s.Id,
                    DayOfWeek = s.DayOfWeek,
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
        public async Task<ActionResult<Preference>> GetPreferenceById(int Id)
        {
            Preference preference = await DBContext.Preferences.Select(s => new Preference
            {
                Id = s.Id,
                DayOfWeek = s.DayOfWeek,
                DishId = s.DishId,
                StudentId = s.StudentId
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (preference == null)
            {
                return NotFound();
            }
            else
            {
                return preference;
            }
        }


        [HttpPost("")]
        public async Task<HttpStatusCode> InsertPreference(Preference preference)
        {
            var entity = new Preference()
            {
                DayOfWeek = preference.DayOfWeek,
                DishId = DBContext.Dishes.FirstOrDefaultAsync(g => g.Id == preference.DishId) != null ? preference.DishId: null,
                StudentId = DBContext.Students.FirstOrDefaultAsync(g => g.Id == preference.StudentId) != null ? preference.StudentId: null
            };
            DBContext.Preferences.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }


        [HttpPut("")]
        public async Task<HttpStatusCode> UpdatePreference(Preference preference)
        {
            var entity = await DBContext.Preferences.FirstOrDefaultAsync(s => s.Id == preference.Id);
            entity.DayOfWeek = preference.DayOfWeek;
            entity.DishId = DBContext.Dishes.FirstOrDefaultAsync(g => g.Id == preference.DishId) != null ? preference.DishId : null;
            entity.StudentId = DBContext.Students.FirstOrDefaultAsync(g => g.Id == preference.StudentId) != null ? preference.StudentId : null;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("")]
        public async Task<HttpStatusCode> DeletePreference(int Id)
        {
            var entity = new Preference()
            {
                Id = Id
            };
            DBContext.Preferences.Attach(entity);
            DBContext.Preferences.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
