using MongoDB.Bson.Serialization.Attributes;
using Phonebook.ContactAPI.Models;
using Phonebook.Shared.Enums;

namespace Phonebook.ContactAPI.Dtos;

public class ContactInformationDto
{
    public string Id { get; set; }
    public ContactTypes ContactType { get; set; }
    public string ContactData { get; set; }
    public string PersonId { get; set; }
    public PersonDto Person { get; set; }
}
