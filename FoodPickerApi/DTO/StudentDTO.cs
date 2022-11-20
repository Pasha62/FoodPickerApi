namespace FoodPickerApi.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? MiddleName { get; set; }
        public int? GradeId { get; set; }
    }
}
