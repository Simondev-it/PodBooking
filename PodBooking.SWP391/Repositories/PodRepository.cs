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
    public class PodRepository : GenericRepository<Pod>
    {
        public PodRepository(Swp391Context context) => _context = context;

        public async Task<List<Pod>> GetAllAsync()
        {
            return await _context.Pods.Include(p => p.Bookings).ToListAsync();
        }

        public async Task<Pod> GetByIdAsync(int id)
        {
            var result = await _context.Pods.Include(p => p.Bookings).FirstAsync(p => p.Id == id);

            return result;
        }
    }
}
