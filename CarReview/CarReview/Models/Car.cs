namespace CarReview.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<CarCategory> CarCategories { get; set; }

    }
}
