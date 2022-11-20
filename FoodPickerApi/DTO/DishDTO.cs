namespace FoodPickerApi.DTO
{
    public class DishDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public int Price { get; set; }
        public int Proteins { get; set; }
        public int Carbs { get; set; }
        public int Fats { get; set; }
        public int WeightGrams { get; set; }
        public string Ingredients { get; set; } = null!;
        public int Calories { get; set; }
        public string Type { get; set; } = null!;
    }
}
