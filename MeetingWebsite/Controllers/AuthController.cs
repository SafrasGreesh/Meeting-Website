//using MeetingWebsite.Entity;
//using MeetingWebsite.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Org.BouncyCastle.Crypto.Generators;

//namespace MeetingWebsite.Controllers
//{
//    public class AuthController : Controller
//    {
//        private readonly ApplicationContext _context;
//        private readonly IConfiguration _configuration;

//        public AuthController(ApplicationContext context, IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register(Users model)
//        {
//            if (ModelState.IsValid)
//            {
//                var userExists = await _context.Users.AnyAsync(u => u.Name == model.Name);
//                if (userExists)
//                {
//                    return Conflict(new { message = "User already exists" });
//                }

//                var user = new Users
//                {
//                    Name = model.Name,
//                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
//                };

//                _context.Users.Add(user);
//                await _context.SaveChangesAsync();

//                return Ok(new { message = "Registration successful" });
//            }

//            return BadRequest(ModelState);
//        }

//    }
//}
