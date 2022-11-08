using Phonebook.Shared.Enums;

namespace Phonebook.ContactAPI.Dtos;

public class ContactInformationDto
{
    public string Id { get; set; }
    public ContactTypes ContactType { get; set; }
    public string ContactData { get; set; }
}
