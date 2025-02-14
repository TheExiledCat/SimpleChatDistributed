using System;
using Microsoft.EntityFrameworkCore;

namespace SimpleChatShared;

public class ChatDbContext : DbContext
{
    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<ChatContact> ChatContacts { get; set; }
    public DbSet<ChatUser> ChatUsers { get; set; }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options) { }
}
