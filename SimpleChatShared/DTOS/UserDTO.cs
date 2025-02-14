using System;

namespace SimpleChatShared.DTOS;

public class UserDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string? Bio { get; set; }
    public string Token { get; set; }

    public Uri? AvatarUri { get; set; }
}
