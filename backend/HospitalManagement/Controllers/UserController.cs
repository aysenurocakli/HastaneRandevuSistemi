
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HospitalManagement.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HospitalManagement.Controllers;

[ApiController]
[EnableCors("AllowSpecificOrigin")]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserController> _logger;


    public UserController(ApplicationDbContext context, ILogger<UserController> logger, IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(User user)
    {
        // Check if username or password is empty
        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
        {
            return BadRequest(new { Error = "Username and password cannot be empty" });
        }

        var dbUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == user.Username);

        if (dbUser == null)
        {
            return Unauthorized();
        }
        if (dbUser.Role == "PATIENT")
{
    return Ok(new { Role = "PATIENT" }); // Kullanıcı rolü "patient" ise "PATIENT" rolünü döndür
}
else if (dbUser.Role == "ADMIN")
{
    return Ok(new { Role = "ADMIN" }); // Kullanıcı rolü "admin" ise "ADMIN" rolünü döndür
}
else
{
    return Unauthorized(); // Diğer durumlar için yetkisiz erişim hatası döndür
}
        // Check if the hashed password matches the one in the database
        /*if (dbUser.Password != HashPassword(user.Password))
        {
            return Unauthorized();
        }*/

        // Generate JWT here
        var tokenString = GenerateJwtToken(dbUser);

        // Return the token
        return Ok(new { Token = tokenString });
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(type: ClaimTypes.Name, user.Username),
                new Claim(type: ClaimTypes.Role, user.Role) // Add role claim
                // Add other claims as needed
            }),
            Expires = DateTime.UtcNow.AddDays(7), // Set token to expire in 7 days
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(User user)
    {
        // Check if username or password is empty
        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
        {
            return BadRequest("Username and password cannot be empty.");
        }

        // Hash the password
        user.Password = user.Password;

        // Set the role to PATIENT
        user.Role = "PATIENT";

        // Set the CreatedDate to today
        user.CreatedDate = DateTime.Today;

        // Save the user to the database
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user.Id); // Return the ID of the new user
    }

    /*private string HashPassword(string password)
    {
        using (var md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }*/
}