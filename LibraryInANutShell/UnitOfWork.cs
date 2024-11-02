using LibraryInANutShell.Models;
using LibraryInANutShell.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInANutShell
{
    public class UnitOfWork
    {
        private Swp391Context _context;

        private PodRepository _podRepository;
        private BookingRepository _bookingRepository;
        private BookingOrderRepository _bookingOrderRepository;
        private CategoryRepository _categoryRepository;
        private PaymentRepository _paymentRepository;
        private ProductRepository _productRepository;
        private SlotRepository _slotRepository;
        private StoreRepository _storeRepository;
        private TypeRepository _typeRepository;
        private UserRepository _userRepository;
        private UtilityRepository _utilityRepository;

        public UnitOfWork() => _context = new Swp391Context();

        public PodRepository PodRepository
        {
            get { return _podRepository ??= new PodRepository(_context); }
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

        public ProductRepository ProductRepository
        {
            get { return _productRepository ??= new ProductRepository(_context); }
        }

        public SlotRepository SlotRepository
        {
            get { return _slotRepository ??= new SlotRepository(_context); }
        }

        public StoreRepository StoreRepository
        {
            get { return _storeRepository ??= new StoreRepository(_context); }
        }

        public TypeRepository TypeRepository
        {
            get { return _typeRepository ??= new TypeRepository(_context); }
        }

        public UserRepository UserRepository
        {
            get { return _userRepository ??= new UserRepository(_context); }
        }

        public UtilityRepository UtilityRepository
        {
            get { return _utilityRepository ??= new UtilityRepository(_context); }
        }
    }
}
