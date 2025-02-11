using System;

namespace SimpleChatShared;

public class ChatContact
{
    public int Id { get; set; }
    public ChatUser UserA { get; set; }
    public ChatUser UserB { get; set; }
    public DateTime ContactsSince { get; set; }
}
