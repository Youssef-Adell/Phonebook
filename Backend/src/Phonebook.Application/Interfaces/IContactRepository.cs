using Phonebook.Application.DTOs.Common;
using Phonebook.Application.DTOs.ContactDTOs;
using Phonebook.Domain.Entities;

namespace Phonebook.Application.Interfaces;

public interface IContactRepository
{
    Task<Page<Contact>> GetContacts(ContactQueryParams queryParams);
    Task<Contact?> GetContact(int id);
    Task<Contact> CreateContact(Contact contact);
    Task<bool> UpdateContact(Contact contact);
    Task<bool> DeleteContact(int id);
}
