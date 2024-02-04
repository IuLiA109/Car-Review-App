using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace CarReview.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public ICollection<Review> Reviews { get; set; }

        [JsonIgnore]
        public UserDetails UserDetails { get; set; }

        //[JsonIgnore]
        public byte[] PasswordHash { get; set; }
        //[JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public Role Role {  get; set; }
    }
}
