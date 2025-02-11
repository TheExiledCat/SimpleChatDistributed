using System.ComponentModel.DataAnnotations;

namespace SimpleChatShared;

public class ChatUser
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }
    public string? Bio { get; set; }
    public string Token { get; set; }

    [Url]
    public Uri? AvatarUri { get; set; }

    public ChatUser() { }

    public ChatUser(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = Hasher.Hash(password);
        GenerateToken();
    }

    public bool VerifyPassword(string plaintext)
    {
        return Hasher.Hash(plaintext) == Password;
    }

    public bool VerifyToken(string token)
    {
        return Token == token;
    }

    public void GenerateToken()
    {
        Token = Hasher.Hash(Email + Password);
    }
}
