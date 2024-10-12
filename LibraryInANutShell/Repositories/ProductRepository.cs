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
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(Swp391Context context) => _context = context;

        public async Task<Product> GetByIdAsync(int id)
        {
            var result = await _context.Products.FirstAsync(p => p.Id == id);

            return result;
        }

        public async Task<bool> RemoveAsync(Product product)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
