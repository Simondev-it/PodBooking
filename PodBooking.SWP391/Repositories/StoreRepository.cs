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
    public class StoreRepository : GenericRepository<Store>
    {
        public StoreRepository(Swp391Context context) => _context = context;

        public async Task<List<Store>> GetAllAsync()
        {
            return await _context.Stores.Include(p => p.Pods).ToListAsync();
        }

        public async Task<Store> GetByIdAsync(int id)
        {
            var result = await _context.Stores.Include(p => p.Pods).FirstAsync(p => p.Id == id);

            return result;
        }
    }
}
