using System;
using System.Collections.Generic;

namespace FoodPickerApi.Entities
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? MiddleName { get; set; }
        public int? GradeId { get; set; }
    }
}
