using System;
using System.Collections.Generic;

namespace FoodPickerApi.Entities
{
    public partial class Parent
    {
        public int Id { get; set; }
        public string? MiddleName { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
