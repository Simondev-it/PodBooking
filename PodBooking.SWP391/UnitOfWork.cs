using PodBooking.SWP391.Models;
using PodBooking.SWP391.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodBooking.SWP391
{
    public class UnitOfWork
    {
        private Swp391Context _context;
        private ProductRepository _productsRepository;
        private UserRepository _userRepository;
        private BookingRepository _bookingRepository;
        private BookingOrderRepository _bookingOrderRepository;
        private CategoryRepository _categoryRepository;
        private PaymentRepository _paymentRepository;
        //private PodRepository _podRepository;
      
        

        public UnitOfWork() => _context ??= new Swp391Context();

        public ProductRepository ProductsRepository
        {
            get { return _productsRepository ??= new ProductRepository(_context); }
        }
        public UserRepository UserRepository
        {
            get { return _userRepository ??= new UserRepository(_context); }
        }
        public BookingRepository BookingRepository
        {
            get { return _bookingRepository ??= new BookingRepository(_context); }
        }
        public BookingOrderRepository BookingOrderRepository
        {
            get { return _bookingOrderRepository ??= new BookingOrderRepository(_context); }
        }
        public CategoryRepository CategoryRepository
        {
            get { return _categoryRepository ??= new CategoryRepository(_context); }
        }
        public PaymentRepository PaymentRepository
        {
            get { return _paymentRepository ??= new PaymentRepository(_context); }
        }



    }
}
