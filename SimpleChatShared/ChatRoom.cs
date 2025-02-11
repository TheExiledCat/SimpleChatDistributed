using System;

namespace SimpleChatShared;

public class ChatRoom
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
}
