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
    public class BookingRepository : GenericRepository<Booking>
    {
        public BookingRepository(Swp391Context context) => _context = context;

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings.Include(p => p.BookingOrders).ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            var result = await _context.Bookings.Include(p => p.BookingOrders).FirstAsync(p => p.Id == id);

            return result;
        }
    }
}
