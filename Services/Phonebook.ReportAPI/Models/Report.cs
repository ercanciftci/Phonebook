using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Phonebook.Shared.Enums;

namespace Phonebook.ReportAPI.Models;

public class Report
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }

    public ReportTypes ReportType { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedDate { get; set; }

    public string ReportFileName{ get; set; }

    public ReportStatuses ReportStatus { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? CompletedDate { get; set; }
}
