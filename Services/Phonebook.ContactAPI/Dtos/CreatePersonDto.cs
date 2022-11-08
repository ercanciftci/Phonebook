using System.ComponentModel.DataAnnotations;

namespace Phonebook.ContactAPI.Dtos;

public class CreatePersonDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public string CompanyName { get; set; }
}
