using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB.APIService.RequestModel;
using PodBooking.SWP391;
using PodBooking.SWP391.Models;
using PB.APIService.RequestModel;

namespace PB.APIService.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public UserController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }
        // GET: api/User/5
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
        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserRequest userRequest)
        {
            var user = new User
            {
                Id = userRequest.Id,
                Name = userRequest.Name,
                Email = userRequest.Email,
                Image = userRequest.Image,
                Role = userRequest.Role,
                Type = userRequest.Type,
                Point = userRequest.Point,
                PhoneNumber = userRequest.PhoneNumber,
                Description = userRequest.Description,
                Password = userRequest.Password,

                

            };
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
        public async Task<IActionResult> PutUser(int id, UserRequest userRequest)
        {
            // Kiểm tra sản phẩm có tồn tại hay không
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            user.Email = userRequest.Email;
            user.PhoneNumber = userRequest.PhoneNumber;
            user.Password = userRequest.Password;
            user.Name = userRequest.Name;
            user.Image = userRequest.Image;
            user.Role = userRequest.Role;
            user.Type = userRequest.Type;
            user.Point = userRequest.Point;
            user.Description = userRequest.Description;
            try
            {
                await _unitOfWork.UserRepository.UpdateAsync(user);
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
        // DELETE: api/User/5
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
