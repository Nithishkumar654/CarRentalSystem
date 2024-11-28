using CarRental_System.Data;
using CarRental_System.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental_System.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = GetUserById(id);
            if(user != null)
            {
                _context.Remove(user);
            }
            else
            {
                throw new Exception("User Not Found");
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            if(user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("No User Found");
            }
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("No User Found");
            }
        }

        public void Update(User user)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.Id == user.Id);

            if(existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.CarId = user.CarId;
                _context.Entry(existingUser).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void RentCar(int id, User user)
        {
            user.CarId = id;
            _context.SaveChanges();
        }
    }
}
