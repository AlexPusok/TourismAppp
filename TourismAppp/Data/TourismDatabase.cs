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
            _database.CreateTableAsync<Vacation>().Wait();
            _database.CreateTableAsync<Booking>().Wait();
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

        // Vacation methods
        public Task<List<Vacation>> GetVacationsAsync()
        {
            return _database.Table<Vacation>().ToListAsync();
        }

        public Task<Vacation> GetVacationAsync(int id)
        {
            return _database.Table<Vacation>()
                            .Where(i => i.VacationID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveVacationAsync(Vacation vacation)
        {
            if (vacation.VacationID != 0)
            {
                return _database.UpdateAsync(vacation);
            }
            else
            {
                return _database.InsertAsync(vacation);
            }
        }

        public Task<int> DeleteVacationAsync(Vacation vacation)
        {
            return _database.DeleteAsync(vacation);
        }

        Task<List<Vacation>> GetVacationsByLocationAsync(string location)
        {
            return _database.Table<Vacation>()
                            .Where(v => v.Location == location)
                            .ToListAsync();
        }


        //Booking Methods

        public Task<List<Booking>> GetBookingsAsync()
        {
            return _database.Table<Booking>().ToListAsync();
        }

        public Task<Booking> GetBookingAsync(int id)
        {
            return _database.Table<Booking>()
                            .Where(i => i.BookingID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveBookingAsync(Booking booking)
        {
            if (booking.BookingID != 0)
            {
                return _database.UpdateAsync(booking);
            }
            else
            {
                return _database.InsertAsync(booking);
                
            }
        }

        

        public Task<int> DeleteBookingAsync(Booking booking)
        {
            return _database.DeleteAsync(booking);
        }

        public Task<List<Booking>> GetBookingsByUserAsync(int userID)
        {
            return _database.Table<Booking>()
                            .Where(b => b.UserID == userID)
                            .ToListAsync();
        }

        public Task<List<Booking>> GetAllBookingsAsync()
        {
            return _database.Table<Booking>().ToListAsync();
        }

        public Task<int> UpdateBookingAsync(Booking booking)
        {
            return _database.UpdateAsync(booking);
        }



    }
}
