using System;

namespace SimpleChatShared;

public class ChatGroups
{
    public int Id { get; set; }
    public ChatRoom Room { get; set; }
    public ChatUser User { get; set; }
}
