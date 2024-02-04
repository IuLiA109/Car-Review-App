using CarReview.Models;

namespace CarReview.Interfaces
{
    public interface IUserDetailsRepository
    {
        UserDetails GetInfoByUser(string username);
    }
}
