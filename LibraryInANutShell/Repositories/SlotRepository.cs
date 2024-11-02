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
    public class SlotRepository : GenericRepository<Slot>
    {
        public SlotRepository(Swp391Context context) => _context = context;

        public async Task<Slot> GetByIdAsync(int id)
        {
            var result = await _context.Slots.FirstAsync(p => p.Id == id);

            return result;
        }

        public async Task<bool> RemoveAsync(Slot slot)
        {
            _context.Remove(slot);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
