using LibraryInANutShell.Base;
using LibraryInANutShell.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInANutShell.Repositories
{
    public class BookingRepository : GenericRepository<Booking>
    {
        public BookingRepository(Swp391Context context) => _context = context;



        public async Task<Booking> GetByIdAsync(int id)
        {
            var result = await _context.Bookings.FirstAsync(p => p.Id == id);

            return result;
        }

        public async Task<bool> RemoveAsync(Booking booking)
        {
            _context.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
