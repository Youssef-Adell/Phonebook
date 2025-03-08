namespace Phonebook.Application.DTOs.ContactDTOs;

public class ContactDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
}
