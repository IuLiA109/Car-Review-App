using CarReview.Models;

namespace CarReview.Services
{
    public interface IUserDetailsService
    {
        ICollection<UserDetails> GetUsersDetails();
        UserDetails GetUserDetailsByUserId(int userid);
        bool Save();
        bool CreateUserDetails(UserDetails user);
    }
}
