using Phonebook.Application.DTOs.Common;
using Phonebook.Application.DTOs.ContactDTOs;
using Phonebook.Application.Extensions;
using Phonebook.Application.Interfaces;

namespace Phonebook.Application.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<Page<ContactDTO>> GetContacts(ContactQueryParams queryParams)
    {
        var contacts = await _contactRepository.GetContacts(queryParams);

        return new Page<ContactDTO>(
            data: contacts.Data.Select(c => c.ToDTO()),
            metadata: contacts.Metadata
        );
    }

    public async Task<ContactDTO?> GetContact(int id)
    {
        var contact = await _contactRepository.GetContact(id);

        if (contact == null)
        {
            return null;
        }

        return contact.ToDTO();
    }

    public async Task<ContactDTO> CreateContact(CreateContactDTO contactToCreate)
    {
        var contact = await _contactRepository.CreateContact(contactToCreate.ToEntity());

        return contact.ToDTO();
    }

    public async Task<bool> UpdateContact(int id, UpdateContactDTO updatedContact)
    {
        return await _contactRepository.UpdateContact(updatedContact.ToEntity(id));
    }

    public async Task<bool> DeleteContact(int id)
    {
        return await _contactRepository.DeleteContact(id);
    }

}
