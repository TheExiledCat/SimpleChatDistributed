using System;

namespace SimpleChatShared.DTOS;

public class MessageDTO
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int RoomId { get; set; }
    public string Contents { get; set; }
    public DateTime SentAt { get; set; }
    public bool Deleted { get; set; }
}
