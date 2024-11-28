using CarRental_System.Models;

namespace CarRental_System.Services
{
    public interface ICarRentalService
    {
        public void RentCar(int id);
        public void ReturnCar(int id);
        public bool CheckCarAvailability(int id);
        public IEnumerable<Car> GetCars();
        public Car GetCarById(int carId);
        public void Add(Car car);
        public void Update(Car car);
        public void Delete(int carId);
    }
}
