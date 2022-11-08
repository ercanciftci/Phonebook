using System.ComponentModel.DataAnnotations;

namespace Phonebook.ContactAPI.Dtos;

public class UpdatePersonDto
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public string CompanyName { get; set; }
}
