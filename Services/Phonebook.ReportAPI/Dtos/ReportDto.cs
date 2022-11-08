using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Phonebook.Shared.Enums;

namespace Phonebook.ReportAPI.Dtos;

public class ReportDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ReportTypes ReportType { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ReportFileName { get; set; }
    public ReportStatuses ReportStatus { get; set; }
    public DateTime CompletedDate { get; set; }
}
