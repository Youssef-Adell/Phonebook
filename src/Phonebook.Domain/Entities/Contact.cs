namespace Phonebook.Domain.Entities;

public class Contact
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
}
