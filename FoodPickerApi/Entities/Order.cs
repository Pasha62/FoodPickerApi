using System;
using System.Collections.Generic;

namespace FoodPickerApi.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Cost { get; set; }
        public int? DishId { get; set; }
        public int? StudentId { get; set; }
    }
}
