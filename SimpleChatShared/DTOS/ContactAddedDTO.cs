using System;

namespace SimpleChatShared.DTOS;

public class ContactAddedDTO
{
    public ContactAddedDTO(string email)
    {
        Email = email;
    }

    public string Email { get; set; }
}
