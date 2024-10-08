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
    }
}
