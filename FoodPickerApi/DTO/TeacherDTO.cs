namespace FoodPickerApi.DTO
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string? MiddleName { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
