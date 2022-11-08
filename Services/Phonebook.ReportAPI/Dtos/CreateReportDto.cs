using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Phonebook.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.ReportAPI.Dtos;

public class CreateReportDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public ReportTypes ReportType { get; set; }
}
