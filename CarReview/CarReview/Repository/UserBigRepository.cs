using CarReview.Data;
using CarReview.Dto;
using CarReview.Interfaces;
using CarReview.Models;

namespace CarReview.Repository
{
    public class UserBigRepository : IUserBigRepository
    {
        private readonly DataContext _context;
        public UserBigRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {

            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.Where(u => u.UserName == username).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool Save()
        {

            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public bool UserExistsByUsername(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }
    }
}
