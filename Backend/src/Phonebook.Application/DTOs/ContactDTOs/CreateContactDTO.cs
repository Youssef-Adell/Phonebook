using System.ComponentModel.DataAnnotations;

namespace Phonebook.Application.DTOs.ContactDTOs;

public class CreateContactDTO
{
    [Required]
    public required string Name { get; set; }

    [Required]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public required string PhoneNumber { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public required string Email { get; set; }
}
