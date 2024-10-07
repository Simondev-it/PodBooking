using Microsoft.EntityFrameworkCore;
using PodBooking.SWP391.Base;
using PodBooking.SWP391.Models;
using PodBooking.SWP391.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodBooking.SWP391.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(Swp391Context context) => _context = context;

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.BookingOrders).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var result = await _context.Products.Include(p => p.BookingOrders).FirstAsync(p => p.Id == id);

            return result;
        }

       
    }

    
}
