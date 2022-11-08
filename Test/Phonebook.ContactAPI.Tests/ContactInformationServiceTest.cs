using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using Phonebook.ContactAPI.Controllers;
using Phonebook.ContactAPI.Dtos;
using Phonebook.ContactAPI.Services;
using Phonebook.Shared.Enums;

namespace Phonebook.ContactAPI.Tests;

public class ContactInformationServiceTest : BaseControllerTests
{
    private readonly Mock<IContactInformationService> _contactInformationServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ContactInformationsController _contactInformationsController;
    private readonly List<ContactInformationDto> _contactInformations = new();

    public ContactInformationServiceTest()
    {
        _contactInformationServiceMock = new Mock<IContactInformationService>();
        _mapperMock = new Mock<IMapper>();
        _contactInformationsController = new ContactInformationsController(_contactInformationServiceMock.Object);

        var personId = Guid.NewGuid().ToString();

        _contactInformations.Add(new ContactInformationDto()
        {
            Id = Guid.NewGuid().ToString(),
            ContactType = Shared.Enums.ContactTypes.PhoneNumber,
            ContactData = "5551236598",
            PersonId = personId
        });

        _contactInformations.Add(new ContactInformationDto()
        {
            Id = Guid.NewGuid().ToString(),
            ContactType = Shared.Enums.ContactTypes.Location,
            ContactData = "Ankara",
            PersonId = personId
        });

        personId = Guid.NewGuid().ToString();

        _contactInformations.Add(new ContactInformationDto()
        {
            Id = Guid.NewGuid().ToString(),
            ContactType = Shared.Enums.ContactTypes.Location,
            ContactData = "İstanbul",
            PersonId = personId
        });
    }

    [Fact]
    public async Task ContactInformationNotNullTest()
    {
        _contactInformationServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Shared.Dtos.Response<List<ContactInformationDto>>.Success(_contactInformations, 200));
        var contactInformations = await _contactInformationServiceMock.Object.GetAllAsync();
        Assert.NotNull(contactInformations);
    }

    [Fact]
    public async Task CreateContactInformation_ShouldReturnAsExpected()
    {
        var personId = Guid.NewGuid().ToString();

        var input = new CreateContactInformationDto()
        {
            PersonId = personId,
            ContactType = ContactTypes.Location,
            ContactData = "Ankara"
        };

        var model = _mapperMock.Object.Map<ContactInformationDto>(input);

        _contactInformationServiceMock.Setup(x => x.CreateAsync(input))
            .ReturnsAsync(Shared.Dtos.Response<ContactInformationDto>.Success(model, 200));

        var result = await _contactInformationsController.Create(input);
        Assert.IsType<ObjectResult>(result);
    }
}
