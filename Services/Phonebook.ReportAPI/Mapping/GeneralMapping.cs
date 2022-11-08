using AutoMapper;
using Phonebook.ReportAPI.Dtos;
using Phonebook.ReportAPI.Models;

namespace Phonebook.ReportAPI.Mapping;

public class GeneralMapping : Profile
{
	public GeneralMapping()
	{
        CreateMap<Report, ReportDto>().ReverseMap();
        CreateMap<Report, CreateReportDto>().ReverseMap();
        CreateMap<Report, UpdateReportDto>().ReverseMap();
    }
}
