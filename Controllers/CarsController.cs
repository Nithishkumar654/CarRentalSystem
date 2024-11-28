using CarRental_System.Filters;
using CarRental_System.Models;
using CarRental_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRentalService _carservice;
        private readonly IUserService _userService;

        private readonly NotificationService _notificationService;
        public CarsController(ICarRentalService _carservice, IUserService userService, NotificationService notification)
        {
            this._carservice = _carservice;
            this._userService = userService;
            this._notificationService = notification;
        }


        [HttpGet]
        public IActionResult GetCars()
        {
            var cars = _carservice.GetCars();
            return Ok(cars);
        }


        [HttpPost]
        [RoleAuthorize("Admin")]
        public IActionResult AddCar(Car car)
        {
            if (car == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            _carservice.Add(car);

            return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car);
        }


        [HttpGet("{id}")]
        public IActionResult GetCarById(int id)
        {
            try
            {
                var car = _carservice.GetCarById(id);

                return Ok(car);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        [RoleAuthorize("Admin")]
        public IActionResult UpdateCar(int id, Car car)
        {
            if (car == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var getcar = _carservice.GetCarById(id);

                _carservice.Update(car);
                return Ok(new { message = "Car details updated successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        [RoleAuthorize("Admin")]
        public IActionResult DeleteCar(int id)
        {
            try
            {
                var car = _carservice.GetCarById(id);

                _carservice.Delete(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(new {message = ex.Message});
            }
        }

        [HttpPost("rent")]
        [RoleAuthorize("User")]
        [JwtValidation]
        public async Task<IActionResult> RentCar(int carId, string email)
        {
            try
            {
                var car = _carservice.GetCarById(carId);
                var user = _userService.GetUserByEmail(email);
                _userService.RentCar(carId, user);
                _carservice.RentCar(carId);
                await _notificationService.SendNotification(user.Email, user.Name, car.Make, car.Model, car.PricePerDay);
                return Ok($"Car Rented Successfully. Price per Day {car.PricePerDay}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("return")]
        public IActionResult ReturnCar(int carId, string email)
        {
            try
            {
                var car = _carservice.GetCarById(carId);
                var user = _userService.GetUserByEmail(email);
                _carservice.ReturnCar(carId);
                _userService.ReturnCar(user);
                return Ok($"Car Returned Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}