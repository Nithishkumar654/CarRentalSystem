using CarRental_System.Models;

namespace CarRental_System.Repository
{
    public interface IUserRepository
    {
        public void Add(User user);
        public void Update(User user);
        public void Delete(int id);
        public User GetUserById(int id);
        public User GetUserByEmail(string email);
        public IEnumerable<User> GetAllUsers();

    }
}
