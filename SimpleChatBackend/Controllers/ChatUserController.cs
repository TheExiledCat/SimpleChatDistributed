using Dumpify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("valuserIdate")]
        public IActionResult ValuserIdate(int userId, string token)
        {
            ChatUser? user = context.ChatUsers.FirstOrDefault(u =>
                u.Id == userId && u.Token == token
            );
            if (user == null)
            {
                return Unauthorized("invaluserId token");
            }
            return Ok();
        }

        [HttpGet("userId/{userId}")]
        [RequireLogin]
        public IActionResult Get(int userId, string token)
        {
            ChatUser? user = context.ChatUsers.FirstOrDefault(u =>
                u.Id == userId && u.Token == token
            );
            return user == null ? BadRequest("user userId invaluserId") : Ok(user);
        }

        [HttpPost("add/{email}")]
        [RequireLogin]
        public IActionResult AddContact(string email, int userId, string token)
        {
            ChatUser? current = context.ChatUsers.FirstOrDefault(c =>
                c.Id == userId && c.Token == token
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
            if (
                context.ChatContacts.Any(c =>
                    (c.UserA == current && c.UserB == contact)
                    || c.UserA == contact && c.UserB == current
                )
            )
            {
                return StatusCode(409, "Contact already exists");
            }
            ChatContact newContact = new ChatContact() { UserA = current, UserB = contact };
            context.ChatContacts.Add(newContact);
            context.SaveChanges();
            return Ok(new ContactAddedDTO(email));
        }

        [HttpGet("contacts/{userId}")]
        [RequireLogin]
        public IActionResult GetContacts(int userId, string token)
        {
            ChatUser? user = context.ChatUsers.FirstOrDefault(u =>
                u.Id == userId && u.Token == token
            );
            if (user == null)
            {
                return Unauthorized("You are not logged in or your user account was deleted");
            }
            ContactDTO[] contacts = context
                .ChatContacts.Include(c => c.UserA)
                .Include(c => c.UserB)
                .Where(c => c.UserA.Id == user.Id || c.UserB.Id == user.Id)
                .ToArray()
                .Select(c =>
                {
                    ChatUser other = c.UserA.Id == user.Id ? c.UserB : c.UserA;
                    other.Dump();
                    return new ContactDTO
                    {
                        Name = other.Name,
                        Email = other.Email,
                        Bio = other.Bio,
                        AvatarUri = other.AvatarUri,
                        ContactsSince = c.ContactsSince,
                        LastMessages = context
                            .ChatMessages.Where(m => m.Room.Id == c.PrivateRoom.Id)
                            .OrderBy(m => m.SentAt)
                            .ToArray()
                            .TakeLast(25)
                            .ToArray(),
                    };
                })
                .ToArray();

            return Ok(contacts);
        }
    }
}
