using FinalProject.Data;
using FinalProject.Entities;
using FinalProject.Exceptions;
using FinalProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            var exists = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email) != null;
            if (exists)
                throw new UserException($"User with email {user.Email} already exists");
            user.Favourites = new Favourites() { UserId = user.Id, User = user }; 
            await _context.AddAsync(user);
            await _context.AddAsync(user.UserDetails);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.Include(u => u.UserDetails).FirstOrDefaultAsync(u => u.Email == email);
            if(user == null)
                throw new UserException($"Unable to find user with email {email}");
            return user;
        }

        public async Task UpdateUserAsync(int id, User user)
        {
            var originalUser = await _context.Users.FindAsync(id);
            if (originalUser == null)
                throw new UserException($"Unable to find user with id {id}");
            user.Id = originalUser.Id;
            user.PasswordHash = originalUser.PasswordHash;
            _context.Entry(originalUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserPasswordAsync(int id, string passwordHash)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new UserException($"User not found with id {id}");
            user.PasswordHash = passwordHash;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new UserException($"User not found with id {id}");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.Include(u => u.UserDetails).FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
                throw new UserException($"User not found with id {id}");
            return user;
        }
    }
}
