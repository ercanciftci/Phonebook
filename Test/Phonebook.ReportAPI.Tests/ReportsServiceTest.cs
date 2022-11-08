using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Phonebook.ReportAPI.Controllers;
using Phonebook.ReportAPI.Dtos;
using Phonebook.ReportAPI.Services;
using Phonebook.Shared.Enums;

namespace Phonebook.ReportAPI.Tests;

public class ReportsServiceTest : BaseControllerTests
{
    private readonly Mock<IReportService> _reportServiceMock;
    private readonly Mock<ISendEndpointProvider> _sendEndpointProviderMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ReportsController _reportsController;
    private readonly List<ReportDto> _reports = new();

    public ReportsServiceTest()
    {
        _reportServiceMock = new Mock<IReportService>();
        _sendEndpointProviderMock = new Mock<ISendEndpointProvider>();
        _mapperMock = new Mock<IMapper>();
        _reportsController = new ReportsController(_reportServiceMock.Object, _sendEndpointProviderMock.Object);

        _reports.Add(new ReportDto()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Location Report",
            CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local).ToUniversalTime(),
            ReportType = ReportTypes.PersonLocation,
            ReportStatus = ReportStatuses.New
        });
    }

    [Fact]
    public async Task ReportNotNullTest()
    {
        _reportServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Shared.Dtos.Response<List<ReportDto>>.Success(_reports, 200));
        var reports = await _reportServiceMock.Object.GetAllAsync();
        Assert.NotNull(reports);
    }
}
