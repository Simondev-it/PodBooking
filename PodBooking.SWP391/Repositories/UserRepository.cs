using Microsoft.EntityFrameworkCore;
using PodBooking.SWP391.Base;
using PodBooking.SWP391.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodBooking.SWP391.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(Swp391Context context) => _context = context;
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.Include(p => p.Bookings).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var result = await _context.Users.Include(p => p.Bookings).FirstAsync(p => p.Id == id);

            return result;
        }
        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Email == username); // Điều chỉnh theo trường thực tế
        }
        public async Task<User> GetUserByCredentialsAsync(string username, string password)
        {
            // Giả định bạn có phương thức để kiểm tra mật khẩu đã được băm
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == username && u.Password == password);
        }
    }
}
