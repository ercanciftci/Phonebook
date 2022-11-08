using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Phonebook.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.ContactAPI.Dtos;

public class CreateContactInformationDto
{
    [Required]
    public ContactTypes ContactType { get; set; }

    [Required]
    public string ContactData { get; set; }

    [Required]
    public string PersonId { get; set; }
}
