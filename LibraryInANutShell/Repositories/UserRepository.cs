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
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(Swp391Context context) => _context = context;

        public async Task<User> GetByIdAsync(int id)
        {
            var result = await _context.Users.FirstAsync(p => p.Id == id);

            return result;
        }

        public async Task<bool> RemoveAsync(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
