using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TourismAppp.Models;

namespace TourismAppp.Data
{
    public class TourismDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public TourismDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<User> GetUserAsync(int id)
        {
            return _database.Table<User>()
                            .Where(i => i.userID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _database.Table<User>()
                            .Where(u => u.Email == email)
                            .FirstOrDefaultAsync();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _database.Table<User>()
                            .Where(u => u.Username == username)
                            .FirstOrDefaultAsync();
        }
        public Task<int> SaveUserAsync(User user)
        {
            if (user.userID != 0)
            {
                return _database.UpdateAsync(user);
            }
            else
            {
                return _database.InsertAsync(user);
            }
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }

        public async Task<bool> RegisterUserAsync(string username, string password, string email, string role)
        {
            // Check if username or email already exists
            var existingUser = await GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                return false; // Username already taken
            }

            var existingEmail = await GetUserByEmailAsync(email);
            if (existingEmail != null)
            {
                return false; // Email already registered
            }

            // Create new user
            var newUser = new User
            {
                Username = username,
                Password = password, // No hashing, plain password
                Email = email,
                Role = role
            };

            await SaveUserAsync(newUser);
            return true; // Registration successful
        }
    }
}
