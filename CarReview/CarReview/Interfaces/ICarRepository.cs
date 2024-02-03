using CarReview.Models;

namespace CarReview.Interfaces
{
    public interface ICarRepository
    {
        ICollection<Car> GetCars();
        Car GetCar(int id);
        decimal GetCarRating(int id);
        bool CarExists (int id);
        bool CreateCar(int categoryId, Car car);
        bool Save();

    }
}
