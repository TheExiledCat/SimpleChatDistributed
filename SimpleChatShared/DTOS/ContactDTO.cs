using System;

namespace SimpleChatShared.DTOS;

public class ContactDTO
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string? Bio { get; set; }

    public Uri? AvatarUri { get; set; }
    public DateTime ContactsSince { get; set; }
    public ChatMessage[] LastMessages { get; set; }
}
