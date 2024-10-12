using LibraryInANutShell;
using LibraryInANutShell.Models;
using Microsoft.AspNetCore.Mvc;
using static APIInANutShell.Controllers.TypeController;

namespace APIInANutShell.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public UserController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSlot(UserDTO slot)
        {
            var newUser = new User
            {
                Id = slot.Id,
                Email = slot.Email,
                Password = slot.Password,
                Name    = slot.Name,
                Image = slot.Image,
                Role = slot.Role,
                Type = slot.Type,
                PhoneNumber = slot.PhoneNumber,
                Point = slot.Point,
                Description = slot.Description,
            };

            await _unitOfWork.UserRepository.CreateAsync(newUser);
            await _unitOfWork.UserRepository.SaveAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = slot.Id }, slot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _unitOfWork.UserRepository.RemoveAsync(user);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(500, "Có lỗi xảy ra khi xóa User.");
        }

        public class UserDTO
        {
            public int Id { get; set; }

            public string? Email { get; set; }

            public string? Password { get; set; }

            public string? Name { get; set; }

            public string? Image { get; set; }

            public string? Role { get; set; }

            public string? Type { get; set; }

            public string? PhoneNumber { get; set; }

            public int? Point { get; set; }

            public string? Description { get; set; }
        }
    }
}
