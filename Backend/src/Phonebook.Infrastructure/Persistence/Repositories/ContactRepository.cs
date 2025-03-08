using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Phonebook.Application.DTOs.Common;
using Phonebook.Application.Interfaces;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Persistence.EFConfig;

namespace Phonebook.Infrastructure.Persistence.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly AppDbContext _appDbcontext;

    public ContactRepository(AppDbContext appDbcontext)
    {
        _appDbcontext = appDbcontext;
    }

    public async Task<Page<Contact>> GetContacts(ContactQueryParams queryParams)
    {
        IQueryable<Contact> query = _appDbcontext.Contacts;

        if (!string.IsNullOrEmpty(queryParams.Search))
        {
            query = query.Where(c =>
                EF.Functions.ILike(c.Name, $"%{queryParams.Search}%") ||
                EF.Functions.ILike(c.PhoneNumber, $"%{queryParams.Search}%") ||
                EF.Functions.ILike(c.Email, $"%{queryParams.Search}%"));
        }

        var totalItems = query.Count();

        var items = await query
            .OrderByDescending(c => c.Id)
            .Skip((queryParams.Page - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .AsNoTracking()
            .ToListAsync();

        return new Page<Contact>(items, queryParams.Page, queryParams.PageSize, totalItems);
    }

    public async Task<Contact?> GetContact(int id)
    {
        return await _appDbcontext.Contacts
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Contact> CreateContact(Contact contact)
    {
        await _appDbcontext.Contacts.AddAsync(contact);
        await _appDbcontext.SaveChangesAsync();

        return contact;
    }

    public async Task<bool> UpdateContact(Contact contact)
    {
        var noOfRowsAffected = await _appDbcontext.Contacts
        .Where(c => c.Id == contact.Id)
        .ExecuteUpdateAsync(s => s.SetProperty(c => c.Name, contact.Name)
            .SetProperty(c => c.PhoneNumber, contact.PhoneNumber)
            .SetProperty(c => c.Email, contact.Email)
        );

        return noOfRowsAffected > 0;
    }

    public async Task<bool> DeleteContact(int id)
    {
        var noOfRowsAffected = await _appDbcontext.Contacts
             .Where(c => c.Id == id)
             .ExecuteDeleteAsync();

        return noOfRowsAffected > 0;
    }
}
