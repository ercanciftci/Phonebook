using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Phonebook.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.ContactAPI.Dtos;

public class UpdateContactInformationDto
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string ContactData { get; set; }
}
