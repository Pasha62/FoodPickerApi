using System;
using System.Collections.Generic;

namespace FoodPickerApi.Entities
{
    public partial class ParentStudent
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? ParentId { get; set; }
    }
}
