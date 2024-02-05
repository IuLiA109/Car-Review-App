using CarReview.Data;
using CarReview.Models;

namespace CarReview.Services
{
    public class UserDetailsService : IUserDetailsService
    {
        private readonly DataContext _context;
        public UserDetailsService(DataContext context)
        {
            _context = context;
        }

        public bool CreateUserDetails(UserDetails userdetails)
        {
            _context.Add(userdetails);
            return Save();
        }

        public UserDetails GetUserDetailsByUserId(int userid)
        {
            return _context.UserDetails.Where(u => u.User.Id == userid).FirstOrDefault();
        }

        public ICollection<UserDetails> GetUsersDetails()
        {
            return _context.UserDetails.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
