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
    }
}
