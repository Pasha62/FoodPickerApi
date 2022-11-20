using System;
using System.Collections.Generic;

namespace FoodPickerApi.Entities
{
    public partial class Teacher
    {
        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
