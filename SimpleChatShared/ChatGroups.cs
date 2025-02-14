using System;

namespace SimpleChatShared;

public class ChatGroup
{
    public int Id { get; set; }
    public ChatRoom Room { get; set; }
    public List<ChatUser> Users { get; set; }
    public Uri Avatar { get; set; }
}
