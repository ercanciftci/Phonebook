using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phonebook.ContactAPI.Dtos;
using Phonebook.ContactAPI.Services;
using Phonebook.Shared.ControllerBases;

namespace Phonebook.ContactAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : CustomBaseController
{
    private readonly IPersonService _personService;

    public PersonsController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _personService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _personService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonDto input)
    {
        var response = await _personService.CreateAsync(input);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdatePersonDto input)
    {
        var response = await _personService.UpdateAsync(input);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _personService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }
}
