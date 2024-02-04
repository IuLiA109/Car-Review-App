namespace CarReview.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int Rating { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Car Car { get; set; }

    }
}
