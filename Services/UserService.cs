using CarRental_System.Data;
using CarRental_System.Models;
using CarRental_System.Repository;
using Microsoft.EntityFrameworkCore;

namespace CarRental_System.Services
{
    public class UserService : IUserService
    {
        private readonly JwtService jwtsrv;
        private readonly IUserRepository _userRepo;

        public UserService(JwtService jwtsrv, IUserRepository userRepo) { 
            this.jwtsrv = jwtsrv;
            this._userRepo = userRepo;
        }
        public void RegisterUser(User user)
        {    
            user.Password = jwtsrv.HashPassword(user.Password);
            _userRepo.Add(user);
        }

        public string AuthenticateUser(string email, string password)
        {
            var user = _userRepo.GetUserByEmail(email);

            // If user does not exist or password is incorrect
            if (user == null || !jwtsrv.ValidatePassword(password, user.Password))
            {
                return "Invalid credentials.";
            }

            // Generate JWT token
            var token = jwtsrv.GenerateToken(user.Email, user.Role);

            return token;
        }


        public void DeleteUser(int id)
        {
            _userRepo.Delete(id);
        }

        public void UpdateUser(User user)
        {
            _userRepo.Update(user);
        }

        public void RentCar(int carid, User user)
        {
            if(user.CarId == null)
            {
                //if (_carRepo.GetCarById(carid).IsAvailable)
                //{
                //    throw new Exception("Car is Not Available");
                //}
                //_carRepo.ToggleAvailability(carid);
                user.CarId = carid;
                _userRepo.Update(user);
            }
            else
            {
                throw new Exception("Each User is given only one Car at a time.");
            }
        }

        public void ReturnCar(User user)
        {
            if (user.CarId == null) { throw new Exception("No Car Found"); }
            //_carRepo.ToggleAvailability((int)user.CarId);
            user.CarId = null;
            _userRepo.Update(user);
        }

        public User GetUserById(int id)
        {
            try
            {
                return _userRepo.GetUserById(id);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                return _userRepo.GetUserByEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
