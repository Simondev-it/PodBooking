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
        //public PodRepository PodRepository
        //{
        //    get { return _podRepository ??= new PodRepository(_context); }
        //}



    }
}
