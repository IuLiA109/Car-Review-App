using System.Runtime;

namespace CarReview.Models
{
    public class Reviewer
    {
        public int Id { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int ProfileId { get; set; }
        public Profil Profile {  get; set; }
        public ICollection<Review> Reviews { get; set;}
    }
}
