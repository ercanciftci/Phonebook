namespace Phonebook.ContactAPI.Dtos;

public class PersonDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string CompanyName { get; set; }
    public List<ContactInformationDto> Contacts { get; set; }
}
