using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodBooking.SWP391.Models
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public string OrderId { get; set; } // ID giao dịch từ ZaloPay
        public int Amount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedDate { get; set; } // Ngày tạo giao dịch
        public DateTime? UpdatedDate { get; set; } // Ngày cập nhật giao dịch (nullable)
        public int BookingId { get; set; }

        // Các thuộc tính khác nếu cần
    }
}
