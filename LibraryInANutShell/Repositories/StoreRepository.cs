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
    public class StoreRepository : GenericRepository<Store>
    {
        public StoreRepository(Swp391Context context) => _context = context;

        public async Task<Store> GetByIdAsync(int id)
        {
            var result = await _context.Stores.FirstAsync(p => p.Id == id);

            return result;
        }

        public async Task<bool> RemoveAsync(Store store)
        {
            _context.Remove(store);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
