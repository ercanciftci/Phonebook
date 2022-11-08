using AutoMapper;
using Phonebook.ContactAPI.Dtos;
using Phonebook.ContactAPI.Models;

namespace Phonebook.ContactAPI.Mapping;

public class GeneralMapping : Profile
{
	public GeneralMapping()
	{
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<Person, CreatePersonDto>().ReverseMap();
        CreateMap<Person, UpdatePersonDto>().ReverseMap();

        CreateMap<ContactInformation, ContactInformationDto>().ReverseMap();
        CreateMap<ContactInformation, CreateContactInformationDto>().ReverseMap();
        CreateMap<ContactInformation, UpdateContactInformationDto>().ReverseMap();
    }
}
