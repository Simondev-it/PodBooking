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
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(Swp391Context context) => _context = context;

        public async Task<Category> GetByIdAsync(int id)
        {
            var result = await _context.Categories.FirstAsync(p => p.Id == id);

            return result;
        }

        public async Task<bool> RemoveAsync(Category category)
        {
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
