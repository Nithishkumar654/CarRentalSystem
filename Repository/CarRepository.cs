using CarRental_System.Data;
using CarRental_System.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace CarRental_System.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;
        public CarRepository(AppDbContext appDbContext) { 
            this._context = appDbContext;
        }
        public void Add(Car car)
        {
            try
            {
                _context.Cars.Add(car);
                _context.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int carId)
        {
            var car = GetCarById(carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
            else
            {
                throw new Exception("Not Found");
            }
        }

        public Car GetCarById(int id)
        {
            var car = _context.Cars.FirstOrDefault(x => x.Id == id);
            if (car != null)
            {
                return car;
            }
            else
            {
                throw new Exception("Not Found");
            }
        }

        public IEnumerable<Car> GetCars()
        {
            return _context.Cars.Where(c => c.IsAvailable).ToList();
        }

        public void Update(Car car)
        {
            var existingCar = _context.Cars.SingleOrDefault(c => c.Id == car.Id);
            
            if (existingCar != null)
            {
                existingCar.PricePerDay = car.PricePerDay;
                existingCar.Year = car.Year;
                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.IsAvailable = car.IsAvailable;
            }
            _context.Entry(existingCar).State = EntityState.Modified;

            _context.SaveChanges();
        }

        public void ToggleAvailability(int id)
        {
            var car = GetCarById(id);
            if(car != null)
            {
                car.IsAvailable = !car.IsAvailable;
                _context.SaveChanges();
            }
        }
    }
}
