using PodBooking.SWP391.Base;
using PodBooking.SWP391.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodBooking.SWP391.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(Swp391Context context) => _context = context;
    }
}
