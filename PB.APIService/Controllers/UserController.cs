using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodBooking.SWP391;
using PodBooking.SWP391.Models;

namespace PB.APIService.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public UserController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }
        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                await _unitOfWork.UserRepository.CreateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.UserRepository.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _unitOfWork.UserRepository.RemoveAsync(user);

            return NoContent();
        }
        private bool UserExists(int id)
        {
            return _unitOfWork.UserRepository.GetByIdAsync(id) != null;
        }
    }
}
