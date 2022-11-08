using Phonebook.ReportAPI.Dtos;
using Phonebook.Shared.Dtos;

namespace Phonebook.ReportAPI.Services;

public interface IReportService
{
    Task<Response<List<ReportDto>>> GetAllAsync();
    Task<Response<ReportDto>> GetByIdAsync(string id);
    Task<Response<ReportDto>> CreateAsync(CreateReportDto input);
    Task<Response<NoContent>> UpdateAsync(UpdateReportDto input);
    Task<Response<NoContent>> DeleteAsync(string id);
}
