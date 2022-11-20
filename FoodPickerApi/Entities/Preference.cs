using System;
using System.Collections.Generic;

namespace FoodPickerApi.Entities
{
    public partial class Preference
    {
        public int Id { get; set; }
        public int DayOfWeek { get; set; }
        public int? DishId { get; set; }
        public int? StudentId { get; set; }
    }
}
