using CarReview.Base;

namespace CarReview.Models
{
    public class UserDetails : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Language { get; set; }
        public bool ReceiveNotifications { get; set; }
    }
}
