using Phonebook.Application.DTOs.Common;
using Phonebook.Application.DTOs.ContactDTOs;

namespace Phonebook.Application.Interfaces;

public interface IContactService
{
    Task<Page<ContactDTO>> GetContacts(ContactQueryParams queryParams);
    Task<ContactDTO?> GetContact(int id);
    Task<ContactDTO> CreateContact(CreateContactDTO contactToCreate);
    Task<bool> UpdateContact(int id, UpdateContactDTO updatedContact);
    Task<bool> DeleteContact(int id);
}
