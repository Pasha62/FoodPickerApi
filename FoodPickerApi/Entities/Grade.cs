using System;
using System.Collections.Generic;

namespace FoodPickerApi.Entities
{
    public partial class Grade
    {
        public int Id { get; set; }
        public string Letter { get; set; } = null!;
        public int Number { get; set; }
        public int? TeacherId { get; set; }

        public int breakIndex { get; set; } = 0!;
    }
}
