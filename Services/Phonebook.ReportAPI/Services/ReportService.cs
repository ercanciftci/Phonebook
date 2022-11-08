using AutoMapper;
using MongoDB.Driver;
using Phonebook.ReportAPI.Dtos;
using Phonebook.ReportAPI.Models;
using Phonebook.ReportAPI.Settings;
using Phonebook.Shared.Dtos;
using Phonebook.Shared.Enums;

namespace Phonebook.ReportAPI.Services;

public class ReportService : IReportService
{
    private readonly IMongoCollection<Report> _reportCollection;
    private readonly IMapper _mapper;

    public ReportService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _reportCollection = database.GetCollection<Report>(databaseSettings.ReportCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<List<ReportDto>>> GetAllAsync()
    {
        var persons = await _reportCollection.Find(report => true).ToListAsync();

        if (!persons.Any())
        {
            persons = new List<Report>();
        }

        return Response<List<ReportDto>>.Success(_mapper.Map<List<ReportDto>>(persons), 200);
    }

    public async Task<Response<ReportDto>> GetByIdAsync(string id)
    {
        var report = await _reportCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

        if (report == null)
        {
            return Response<ReportDto>.Fail("Report not found!", 404);
        }

        return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
    }

    public async Task<Response<ReportDto>> CreateAsync(CreateReportDto input)
    {
        var report = _mapper.Map<Report>(input);
        report.CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local).ToUniversalTime();
        report.ReportStatus = ReportStatuses.New;
        await _reportCollection.InsertOneAsync(report);
        return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
    }

    public async Task<Response<NoContent>> UpdateAsync(UpdateReportDto input)
    {
        var report = _mapper.Map<Report>(input);
        var result = await _reportCollection.FindOneAndReplaceAsync(s => s.Id == input.Id, report);

        if (result == null)
        {
            return Response<NoContent>.Fail("Report not found!", 404);
        }

        return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        var reportItem = await _reportCollection.Find(s => s.Id == id).FirstOrDefaultAsync();
        var fileName = reportItem == null ? "" : reportItem.ReportFileName;
        var result = await _reportCollection.DeleteOneAsync(s => s.Id == id);

        if (result.DeletedCount > 0)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\reports", fileName);

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception)
            {

            }

            return Response<NoContent>.Success(204);
        }

        return Response<NoContent>.Fail("Report not found!", 404);
    }
}
