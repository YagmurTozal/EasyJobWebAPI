using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using EasyJobWebAPI.Model.AuthenticationAPIModel;

namespace EasyJobWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(ILogger<AuthenticationController> logger)
    {
        _logger = logger;
    }
    public static User user = new User();

    [HttpPost(Name = "Register")]
    public ActionResult<User> Register(UserDTO request)
    {
        GenerateHash(request.Password, out byte[] hash, out byte[] salt);

        user.Email = request.Email;
        user.UserHash = hash;
        user.UserSalt = salt;

        return Ok(user);
    }

    [HttpPost(Name = "Login")]
    public ActionResult<User> Login(UserDTO request)
    {

        if (request.Email != user.Email)
            return BadRequest("User Not Found");

        if (user.UserHash != null && user.UserSalt != null)
        {
            var verify = VerifyPasswordHash(request.Password, user.UserHash, user.UserSalt);

            if (!verify)
                return BadRequest("Password is incorrect.");

            return Ok("My Crazy Token");

        }
        return BadRequest("User not registered.");
    }

    private void GenerateHash(string password, out byte[] hash, out byte[] salt)
    {
        using (var hmac = new HMACSHA512())
        {
            hash = hmac.Key;
            salt = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
    {
        using (var hmac = new HMACSHA512(salt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(hash);
        }
    }
}
