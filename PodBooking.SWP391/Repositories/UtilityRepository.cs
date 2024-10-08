using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PodBooking.SWP391.Base;
using PodBooking.SWP391.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodBooking.SWP391.Repositories
{
    public class UtilityRepository : GenericRepository<Models.Utility>
    {
        public UtilityRepository(Swp391Context context) => _context = context;
        public async Task<List<Models.Utility>> GetAllAsync()
        {
            return await _context.Utilities.Include(p => p.Pods).ToListAsync();
        }

        public async Task<Models.Utility> GetByIdAsync(int id)
        {
            var result = await _context.Utilities.Include(p => p.Pods).FirstAsync(p => p.Id == id);

            return result;
        }
    }
}
