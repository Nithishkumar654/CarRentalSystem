using CarRental_System.Models;
using CarRental_System.Repository;

namespace CarRental_System.Services
{
    public class CarRentalService : ICarRentalService
    {
        private readonly ICarRepository _carRepo;
        public CarRentalService(ICarRepository carRepository) {
            this._carRepo = carRepository;
        }
        public void RentCar(int id){
            try
            {
                if (CheckCarAvailability(id))
                {
                    _carRepo.ToggleAvailability(id);
                }
                else
                {
                    throw new Exception("Car Not Available");
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void ReturnCar(int id)
        {
            _carRepo.ToggleAvailability(id);
        }

        public bool CheckCarAvailability(int id)
        {
            var car = _carRepo.GetCarById(id);
            if (car == null) throw new Exception("Car Not Found");
            return car.IsAvailable;
        }

        public IEnumerable<Car> GetCars()
        {
            return _carRepo.GetCars();
        }

        public Car GetCarById(int carId)
        {
            try
            {
                return _carRepo.GetCarById(carId);
            }catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Add(Car car)
        {
            try
            {
                _carRepo.Add(car);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Car car)
        {
            try
            {
                _carRepo.Update(car);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(int carId)
        {
            try
            {
                _carRepo.Delete(carId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
