using CarRental_System.Models;

namespace CarRental_System.Services
{
    public interface IUserService
    {
        public void RegisterUser(User user);
        public string AuthenticateUser(string email, string password);
        public void DeleteUser(int id);
        public void UpdateUser(User user);
        public User GetUserById(int id);
        public User GetUserByEmail(string email);
        public void RentCar(int carid, User user);
        public void ReturnCar(User user);
    }
}
