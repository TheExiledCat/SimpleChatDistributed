using System;

namespace SimpleChatShared;

public class ChatMessage
{
    public int Id { get; set; }
    public ChatUser Sender { get; set; }
    public ChatRoom Room { get; set; }
    public string Contents { get; set; }
    public DateTime SentAt { get; set; }
    public bool Deleted { get; set; }
}
