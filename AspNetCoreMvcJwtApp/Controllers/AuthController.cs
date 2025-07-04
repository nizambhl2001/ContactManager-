using AspNetCoreMvcJwtApp.Data;
using AspNetCoreMvcJwtApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreMvcJwtApp.Controllers
{
    public class AuthController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(User login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(login);
            }

            var token = GenerateJwtToken(user.Username);

            // Store login info in session
            HttpContext.Session.SetString("IsLoggedIn", "true");
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToAction("Index", "Person"); // Redirect after login
        }


        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }


        private string GenerateJwtToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    
}
