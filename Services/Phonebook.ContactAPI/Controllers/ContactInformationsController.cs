using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phonebook.ContactAPI.Dtos;
using Phonebook.ContactAPI.Services;
using Phonebook.Shared.ControllerBases;

namespace Phonebook.ContactAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactInformationsController : CustomBaseController
{
    private readonly IContactInformationService _contactInformationService;

    public ContactInformationsController(IContactInformationService contactInformationService)
    {
        _contactInformationService = contactInformationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _contactInformationService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("GetLocationDataList")]
    public async Task<IActionResult> GetLocationDataList()
    {
        var data = await _contactInformationService.GetLocationDataListAsync();
        return new JsonResult(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _contactInformationService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateContactInformationDto input)
    {
        var response = await _contactInformationService.CreateAsync(input);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateContactInformationDto input)
    {
        var response = await _contactInformationService.UpdateAsync(input);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _contactInformationService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }
}
