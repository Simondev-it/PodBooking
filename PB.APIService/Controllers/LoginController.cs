using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PB.APIService.RequestModel;
using PodBooking.SWP391;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PB.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UnitOfWork _unitOfWork;

        public LoginController(IConfiguration configuration, UnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            var user = await _unitOfWork.UserRepository.GetUserByCredentialsAsync(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            // Đảm bảo rằng role được lấy từ người dùng
            var token = GenerateJwtToken(user.Email, user.Role);
            return Ok(new { token });
        }

        private string GenerateJwtToken(string username, string role) // Thêm tham số role
        {
            var jwtSettings = _configuration.GetSection("Jwt").Get<JwtSettings>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username), // ID hoặc username của người dùng
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // ID của token
                new Claim(ClaimTypes.Role, role) // Thêm quyền vào claims
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(jwtSettings.ExpireDays),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize(Policy = "User")] // Nếu bạn đã thiết lập policy
        [HttpGet("user-only")]
        public IActionResult GetUserData()
        {
            return Ok("This is user data.");
        }


        [Authorize(Roles = "Admin")] // Chỉ người dùng có quyền "Admin" mới có thể truy cập endpoint này
        [HttpGet("admin-only")]
        public IActionResult GetAdminData()
        {
            return Ok("This is admin data.");
        }

    }
}
