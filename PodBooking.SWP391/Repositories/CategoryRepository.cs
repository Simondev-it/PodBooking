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
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(Swp391Context context) => _context = context;

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(p => p.Products).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            // Sử dụng FirstOrDefaultAsync để tránh lỗi "Sequence contains no elements"
            var result = await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);

            return result;
        }

    }
}
