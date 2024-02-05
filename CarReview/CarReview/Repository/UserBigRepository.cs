using CarReview.Data;
using CarReview.Dto;
using CarReview.Interfaces;
using CarReview.Models;
using CarReview.UnitOfWork;

namespace CarReview.Repository
{
    public class UserBigRepository : IUserBigRepository
    {
        private readonly DataContext _context;
        //public IUnitOfWork _unitOfWork { get; set; }
        public UserBigRepository(DataContext context)
        {
            _context = context;
            //_unitOfWork = unitOfWork;
        }

        public bool CreateUser(User user)
        {

            _context.Add(user);
            return Save();
        }

        /*public async Task DeleteUser(User user)
        {
            _context.Remove(user);
            await _unitOfWork.SaveAsync();
        }*/
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

        /*public async Task UpdateUser(User user)
        {
            _context.Update(user);
            await _unitOfWork.SaveAsync();
        }*/
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
