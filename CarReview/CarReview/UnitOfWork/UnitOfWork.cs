using CarReview.Data;
using CarReview.Interfaces;
using CarReview.Repository;

namespace CarReview.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IReviewRepository _reviewRepository { get; set; }
        public IUserBigRepository _userRepository { get; set; }
        private DataContext _context { get; set; }

        public UnitOfWork(DataContext context, IReviewRepository reviewRepository, IUserBigRepository userBigRepository)
        {
            _context = context;
            _reviewRepository = reviewRepository;
            _userRepository= userBigRepository;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() != 0;
        }
    }
}
