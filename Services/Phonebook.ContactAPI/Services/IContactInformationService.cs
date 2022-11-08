using Phonebook.ContactAPI.Dtos;
using Phonebook.Shared.Dtos;
using Phonebook.Shared.Models;

namespace Phonebook.ContactAPI.Services;

public interface IContactInformationService
{
    Task<Response<List<ContactInformationDto>>> GetAllAsync();
    Task<Response<ContactInformationDto>> GetByIdAsync(string id);
    Task<List<LocationDataModel>> GetLocationDataListAsync();
    Task<Response<ContactInformationDto>> CreateAsync(CreateContactInformationDto input);
    Task<Response<NoContent>> UpdateAsync(UpdateContactInformationDto input);
    Task<Response<NoContent>> DeleteAsync(string id);
}
