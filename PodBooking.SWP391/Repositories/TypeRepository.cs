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
    public class TypeRepository : GenericRepository<Models.Type>

    {
        public TypeRepository(Swp391Context context) => _context = context;

        public async Task<List<Models.Type>> GetAllAsync()
        {
            return await _context.Types.Include(p => p.Pods).ToListAsync();
        }

        public async Task<Models.Type> GetByIdAsync(int id)
        {
            var result = await _context.Types.Include(p => p.Pods).FirstAsync(p => p.Id == id);

            return result;
        }
    }
}
