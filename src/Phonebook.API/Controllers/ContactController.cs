using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.DTOs.Common;
using Phonebook.Application.DTOs.ContactDTOs;
using Phonebook.Application.Interfaces;

namespace Phonebook.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }


    [HttpGet]
    [ProducesResponseType(typeof(Page<ContactDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContacts([FromQuery] ContactQueryParams queryParams)
    {
        var contacts = await _contactService.GetContacts(queryParams);

        return Ok(contacts);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ContactDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContact(int id)
    {
        var contact = await _contactService.GetContact(id);

        if (contact is null)
        {
            return NotFound();
        }

        return Ok(contact);
    }


    [HttpPost]
    [ProducesResponseType(typeof(ContactDTO), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactDTO contactToCreate)
    {
        var createdContact = await _contactService.CreateContact(contactToCreate);

        return CreatedAtAction(nameof(GetContact), new { id = createdContact.Id }, createdContact);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateContact(int id, [FromBody] UpdateContactDTO updatedContact)
    {
        var isUpdated = await _contactService.UpdateContact(id, updatedContact);

        if (!isUpdated)
        {
            return NotFound();
        }

        return NoContent();
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContact(int id)
    {
        var isDeleted = await _contactService.DeleteContact(id);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
