using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Phonebook.ContactAPI.Controllers;
using Phonebook.ContactAPI.Dtos;
using Phonebook.ContactAPI.Models;
using Phonebook.ContactAPI.Services;
using Phonebook.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.ContactAPI.Tests;

public class PersonServiceTest : BaseControllerTests
{
    private readonly Mock<IPersonService> _personServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly PersonsController _personsController;
    private readonly List<PersonDto> _persons = new();

    public PersonServiceTest()
    {
        _personServiceMock = new Mock<IPersonService>();
        _mapperMock = new Mock<IMapper>();
        _personsController = new PersonsController(_personServiceMock.Object);

        _persons.Add(new PersonDto()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Ercan",
            Surname = "Çiftçi",
            CompanyName = "Rise"
        });

        _persons.Add(new PersonDto()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Kürşat",
            Surname = "Tekin",
            CompanyName = "Rise"
        });
    }

    [Fact]
    public async Task PersonNotNullTest()
    {
        _personServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Shared.Dtos.Response<List<PersonDto>>.Success(_persons, 200));
        var persons = await _personServiceMock.Object.GetAllAsync();
        Assert.NotNull(persons);
    }

    [Fact]
    public async Task CreatePerson_ShouldReturnAsExpected()
    {
        var input = new CreatePersonDto()
        {
            Name = "Mehmet",
            Surname = "Çelik",
            CompanyName = "Rise"
        };

        var model = _mapperMock.Object.Map<PersonDto>(input);

        _personServiceMock.Setup(x => x.CreateAsync(input))
            .ReturnsAsync(Shared.Dtos.Response<PersonDto>.Success(model, 200));

        var result = await _personsController.Create(input);
        Assert.IsType<ObjectResult>(result);
    }
}
