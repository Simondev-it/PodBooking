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
    public class PaymentRepository : GenericRepository<Payment>
    {
        public PaymentRepository(Swp391Context context) => _context = context;

        public async Task<Payment> GetByIdAsync(int id)
        {
            var result = await _context.Payments.FirstAsync(p => p.Id == id);

            return result;
        }

        public async Task<bool> RemoveAsync(Payment payment)
        {
            _context.Remove(payment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
