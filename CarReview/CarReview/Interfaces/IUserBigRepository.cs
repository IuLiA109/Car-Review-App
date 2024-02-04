using CarReview.Dto;
using CarReview.Models;

namespace CarReview.Interfaces
{
    public interface IUserBigRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUserByUsername(string username);
        bool Save();
        bool CreateUser(User user);
        bool UserExists(int id);
        bool UserExistsByUsername(string username);
        bool UpdateUser(User user);
        bool DeleteUser(User user);


    }
}
