using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phonebook.ReportAPI.Dtos;
using Phonebook.ReportAPI.Services;
using Phonebook.Shared.ControllerBases;
using Phonebook.Shared.Messages;

namespace Phonebook.ReportAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : CustomBaseController
{
    private readonly IReportService _reportService;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public ReportsController(IReportService reportService,
        ISendEndpointProvider sendEndpointProvider)
    {
        _reportService = reportService;
        _sendEndpointProvider = sendEndpointProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _reportService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _reportService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReportDto input)
    {
        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-report-service"));

        var response = await _reportService.CreateAsync(input);

        if (response.IsSuccessful)
        {
            var createReportMessageCommand = new CreateReportMessageCommand
            {
                ReportType = response.Data.ReportType,
                ReportId = response.Data.Id
            };

            await sendEndpoint.Send(createReportMessageCommand);
        }

        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateReportDto input)
    {
        var response = await _reportService.UpdateAsync(input);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _reportService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }
}
