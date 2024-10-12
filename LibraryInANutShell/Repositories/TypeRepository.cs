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
    public class TypeRepository : GenericRepository<Models.Type>
    {
        public TypeRepository(Swp391Context context) => _context = context;

        public async Task<Models.Type> GetByIdAsync(int id)
        {
            var result = await _context.Types.FirstAsync(p => p.Id == id);

            return result;
        }

        public async Task<bool> RemoveAsync(Models.Type type)
        {
            _context.Remove(type);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
