using Phonebook.ContactAPI.Dtos;
using Phonebook.Shared.Dtos;

namespace Phonebook.ContactAPI.Services;

public interface IPersonService
{
    Task<Response<List<PersonDto>>> GetAllAsync();
    Task<Response<PersonDto>> CreateAsync(CreatePersonDto input);
    Task<Response<PersonDto>> GetByIdAsync(string id);
    Task<Response<NoContent>> UpdateAsync(UpdatePersonDto input);
    Task<Response<NoContent>> DeleteAsync(string id);
}
