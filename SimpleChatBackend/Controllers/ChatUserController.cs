using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleChatShared;
using SimpleChatShared.DTOS;

namespace SimpleChatBackend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class ChatUserController(ChatDbContext context) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register(string name, string email, string password)
        {
            if (context.ChatUsers.Any(u => u.Email == email))
            {
                return Conflict("Email already in use");
            }
            ChatUser user = new ChatUser(name, email, password);
            context.ChatUsers.Add(user);
            context.SaveChanges();
            return Ok("user registered succesfully");
        }

        [HttpGet("login")]
        public IActionResult Login(string email, string password)
        {
            password = Hasher.Hash(password);
            ChatUser? user = context.ChatUsers.FirstOrDefault(u =>
                u.Email == email && u.Password == password
            );
            if (user == null)
            {
                return Unauthorized("incorrect email or password");
            }
            return Ok(new LoginDTO { Id = user.Id, Token = user.Token });
        }

        [HttpGet("validate")]
        public IActionResult Validate(int id, string token)
        {
            ChatUser? user = context.ChatUsers.FirstOrDefault(u => u.Id == id && u.Token == token);
            if (user == null)
            {
                return Unauthorized("invalid token");
            }
            return Ok();
        }

        [HttpGet("id/{id}")]
        [RequireLogin]
        public IActionResult Get(int id, string token)
        {
            ChatUser? user = context.ChatUsers.FirstOrDefault(u => u.Id == id && u.Token == token);
            return user == null ? BadRequest("user id invalid") : Ok(user);
        }

        [HttpPost("add/{email}")]
        [RequireLogin]
        public IActionResult AddContact(string email, int id, string token)
        {
            ChatUser? current = context.ChatUsers.FirstOrDefault(c =>
                c.Id == id && c.Token == token
            );
            if (current == null)
            {
                return Unauthorized("You are not logged in or your user account was deleted");
            }
            ChatUser? contact = context.ChatUsers.FirstOrDefault(c => c.Email == email);
            if (contact == null)
            {
                return StatusCode(409, "Contact email not found");
            }
        }
    }
}
