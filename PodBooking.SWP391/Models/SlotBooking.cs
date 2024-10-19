using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodBooking.SWP391.Models
{
    public class SlotBooking
    {
        public int Id { get; set; }

        public int SlotId { get; set; }
        [ForeignKey("SlotId")]
        public virtual Slot Slot { get; set; }

        public int BookingId { get; set; }
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }
    }
}
