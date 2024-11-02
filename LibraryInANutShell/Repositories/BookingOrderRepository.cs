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
    public class BookingOrderRepository : GenericRepository<BookingOrder>
    {
        public BookingOrderRepository(Swp391Context context) => _context = context;



        public async Task<BookingOrder> GetByIdAsync(int id)
        {
            var result = await _context.BookingOrders.FirstAsync(p => p.Id == id);

            return result;
        }

        public async Task<bool> RemoveAsync(BookingOrder bookingOrder)
        {
            _context.Remove(bookingOrder);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
