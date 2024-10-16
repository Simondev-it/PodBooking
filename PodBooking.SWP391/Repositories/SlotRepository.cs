﻿using Microsoft.EntityFrameworkCore;
using PodBooking.SWP391.Base;
using PodBooking.SWP391.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodBooking.SWP391.Repositories
{
    public class SlotRepository : GenericRepository<Slot>
    {
        public SlotRepository(Swp391Context context) => _context = context;

        public async Task<List<Slot>> GetAllAsync()
        {
            return await _context.Slots.Include(p => p.Bookings).ToListAsync();
        }

        public async Task<Slot> GetByIdAsync(int id)
        {
            var result = await _context.Slots.Include(p => p.Bookings).FirstAsync(p => p.Id == id);

            return result;
        }
        //public async Task<List<Slot>> GetByBookingIdAsync(int bookingId)
        //{
        //    return await _context.Slots
        //        .Where(sb => sb.BookingId == bookingId)
        //        .ToListAsync();
        //}
    }
}
