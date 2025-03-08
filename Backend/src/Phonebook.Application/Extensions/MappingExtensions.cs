using Phonebook.Application.DTOs.ContactDTOs;
using Phonebook.Domain.Entities;

namespace Phonebook.Application.Extensions;

public static class MappingExtensions
{
    public static ContactDTO ToDTO(this Contact contact)
    {
        return new ContactDTO
        {
            Id = contact.Id,
            Name = contact.Name,
            PhoneNumber = contact.PhoneNumber,
            Email = contact.Email,
        };
    }

    public static Contact ToEntity(this CreateContactDTO contactToCreate)
    {
        return new Contact
        {
            Name = contactToCreate.Name,
            PhoneNumber = contactToCreate.PhoneNumber,
            Email = contactToCreate.Email,
        };
    }

    public static Contact ToEntity(this UpdateContactDTO updatedContact, int id)
    {
        return new Contact
        {
            Id = id,
            Name = updatedContact.Name,
            PhoneNumber = updatedContact.PhoneNumber,
            Email = updatedContact.Email,
        };
    }
}
