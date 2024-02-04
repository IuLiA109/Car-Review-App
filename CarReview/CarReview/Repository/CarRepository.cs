using CarReview.Data;
using CarReview.Interfaces;
using CarReview.Models;

namespace CarReview.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;
        public CarRepository(DataContext context)
        {
            _context = context;
        }

        public bool CarExists(int id)
        {
            return _context.Cars.Any(c => c.Id == id);
        }

        public bool CreateCar(int categoryId, Car car)
        {
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var carCategory = new CarCategory()
            {
                Category = category,
                Car = car,
            };

            _context.Add(carCategory);

            _context.Add(car);

            return Save();
        }

        public bool DeleteCar(Car car)
        {
            _context.Remove(car);
            return Save();
        }

        public Car GetCar(int id)
        {
            return _context.Cars.Where(c => c.Id == id).FirstOrDefault();
        }

        public decimal GetCarRating(int id)
        {
            var review = _context.Reviews.Where(c => c.Car.Id == id);
            if (review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Car> GetCars() 
        { 
            return _context.Cars.OrderBy(c => c.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCar(Car car)
        {
            _context.Update(car);
            return Save();
        }
    }
}
