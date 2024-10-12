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
    public class UtilityRepository : GenericRepository<Utility>
    {
        public UtilityRepository(Swp391Context context) => _context = context;

        public async Task<Utility> GetByIdAsync(int id)
        {
            var result = await _context.Utilities.FirstAsync(p => p.Id == id);

            return result;
        }

        public async Task<bool> RemoveAsync(Utility utility)
        {
            _context.Remove(utility);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
