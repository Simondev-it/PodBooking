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
    public class PodRepository : GenericRepository<Pod>
    {
        public PodRepository(Swp391Context context) => _context = context;

        public async Task<Pod> GetByIdAsync(int id)
        {
            var result = await _context.Pods.FirstAsync(p => p.Id == id);

            return result;
        }

        public async Task<bool> RemoveAsync(Pod pod)
        {
            _context.Remove(pod);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


//asynchronous