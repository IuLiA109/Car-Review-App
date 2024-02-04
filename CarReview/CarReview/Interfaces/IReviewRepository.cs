using CarReview.Models;

namespace CarReview.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewsOfCar(int carId);
        ICollection<Review> GetReviewsByUser(int userId);
        bool ReviewExists(int id);
        bool CreateReview(Review review);
        bool Save();
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
    }
}
