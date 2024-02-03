namespace CarReview.Models
{
    public class Profil
    {
        public int Id { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int ReviewerId { get; set; }
        public Reviewer Reviewer { get; set; }
    }
}
