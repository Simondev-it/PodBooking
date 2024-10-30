using Microsoft.EntityFrameworkCore;
using PodBooking.SWP391.Base;
using PodBooking.SWP391.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PodBooking.SWP391.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>
    {
        public PaymentRepository(Swp391Context context) => _context = context;

        //public async Task<List<Payment>> GetAllAsync()
        //{
        //    return await _context.Payments.Include(p => p.Products).ToListAsync();
        //}

        //public async Task<Category> GetByIdAsync(int id)
        //{
        //    var result = await _context.Categories.Include(p => p.Products).FirstAsync(p => p.Id == id);

        //    return result;
        //}

        public async Task<Payment> GetFirstOrDefaultAsync(Expression<Func<Payment, bool>> predicate)
        {
            return await _context.Set<Payment>().FirstOrDefaultAsync(predicate);
        }
        public async Task<Payment> GetByBookingIdAsync(int bookingId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.BookingId == bookingId);
        }
        public async Task<Payment> GetByOrderIdAsync(int orderId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.BookingId == orderId); // Giả sử BookingId là OrderId
        }
        public async Task<Payment> GetAsync(Expression<Func<Payment, bool>> predicate)
        {
            return await _context.Payments.FirstOrDefaultAsync(predicate);
        }
    }
}
