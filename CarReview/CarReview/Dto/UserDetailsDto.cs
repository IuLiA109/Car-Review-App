namespace CarReview.Dto
{
    public class UserDetailsDto
    {
        public int UserId { get; set; }
        public string Language { get; set; }
        public bool ReceiveNotifications { get; set; } = true;
    }
}
