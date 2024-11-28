using CarRental_System.Models;

namespace CarRental_System.Repository
{
    public interface ICarRepository
    {
        public IEnumerable<Car> GetCars();
        public Car GetCarById(int carId);
        public void Add(Car car);
        public void Update(Car car);
        public void Delete(int carId);
        public void ToggleAvailability(int id);
    }
}
