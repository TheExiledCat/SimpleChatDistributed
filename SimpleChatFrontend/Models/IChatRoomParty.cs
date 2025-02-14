using System;
using SimpleChatShared;

namespace SimpleChatFrontend.Models;

public interface IChatRoomParty
{
    public string Name { get; set; }
    public Uri Avatar { get; set; }
}
