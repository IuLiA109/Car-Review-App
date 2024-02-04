using CarReview.Data;
using CarReview.Interfaces;
using CarReview.Models;
using Microsoft.EntityFrameworkCore;

namespace CarReview.Repository
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private readonly DataContext _context;
        public UserDetailsRepository(DataContext context)
        {
            _context = context;
        }

        public UserDetails GetInfoByUser(string username)
        {
            return _context.UserDetails.Include(d => d.User).Where(d => d.User.UserName == username).FirstOrDefault();
        }
    }
}
