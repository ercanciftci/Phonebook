using Phonebook.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.ReportAPI.Dtos;

public class UpdateReportDto
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string ReportFileName { get; set; }

    [Required]
    public ReportStatuses ReportStatus { get; set; }
}
