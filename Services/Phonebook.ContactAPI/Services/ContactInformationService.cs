using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using Phonebook.ContactAPI.Dtos;
using Phonebook.ContactAPI.Models;
using Phonebook.ContactAPI.Settings;
using Phonebook.Shared.Dtos;
using Phonebook.Shared.Enums;
using Phonebook.Shared.Models;

namespace Phonebook.ContactAPI.Services;

public class ContactInformationService : IContactInformationService
{
    private readonly IMongoCollection<ContactInformation> _contactInformationCollection;
    private readonly IMongoCollection<Person> _personCollection;
    private readonly IMapper _mapper;

    public ContactInformationService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _contactInformationCollection = database.GetCollection<ContactInformation>(databaseSettings.ContactInformationCollectionName);
        _personCollection = database.GetCollection<Person>(databaseSettings.PersonCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<List<ContactInformationDto>>> GetAllAsync()
    {
        var contactInformations = await _contactInformationCollection.Find(contact => true).ToListAsync();

        if (contactInformations.Any())
        {
            foreach (var contactInformation in contactInformations)
            {
                contactInformation.Person = await _personCollection.Find(x => x.Id == contactInformation.PersonId).FirstOrDefaultAsync();
            }
        }
        else
        {
            contactInformations = new List<ContactInformation>();
        }

        return Response<List<ContactInformationDto>>.Success(_mapper.Map<List<ContactInformationDto>>(contactInformations), 200);
    }

    public async Task<Response<ContactInformationDto>> GetByIdAsync(string id)
    {
        var contactInformation = await _contactInformationCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

        if (contactInformation == null)
        {
            return Response<ContactInformationDto>.Fail("Contact information not found!", 404);
        }

        contactInformation.Person = await _personCollection.Find(x => x.Id == contactInformation.PersonId).FirstOrDefaultAsync();

        return Response<ContactInformationDto>.Success(_mapper.Map<ContactInformationDto>(contactInformation), 200);
    }

    public async Task<List<LocationDataModel>> GetLocationDataListAsync()
    {
        var dataList = _contactInformationCollection.Aggregate().Match(x => x.ContactType == ContactTypes.Location && !string.IsNullOrEmpty(x.ContactData))
          .Group(
              x => x.ContactData,
              g => new LocationDataModel
              {
                  Location = g.Key,
                  PersonCount = g.Count()
              }).ToList();

        foreach (var item in dataList)
        {
            item.PhoneNumberCount = 0;
            var personIdList = await _contactInformationCollection.Find(x => x.ContactType == ContactTypes.Location && !string.IsNullOrEmpty(x.ContactData) && x.ContactData.Equals(item.Location)).Project(x => x.PersonId).ToListAsync();
            item.PhoneNumberCount += _contactInformationCollection.Find(x => personIdList.Contains(x.PersonId) && x.ContactType == ContactTypes.PhoneNumber && !string.IsNullOrEmpty(x.ContactData)).ToList().Count();
        }

        return dataList;
    }

    public async Task<Response<ContactInformationDto>> CreateAsync(CreateContactInformationDto input)
    {
        var contactInformation = await _contactInformationCollection.Find(s => s.PersonId == input.PersonId && s.ContactType == input.ContactType).FirstOrDefaultAsync();
        
        if(contactInformation != null)
        {
            contactInformation.ContactType = input.ContactType;
            await _contactInformationCollection.ReplaceOneAsync(s => s.Id == contactInformation.Id, contactInformation);
        }
        else
        {
            contactInformation = _mapper.Map<ContactInformation>(input);
            await _contactInformationCollection.InsertOneAsync(contactInformation);
        }

        return Response<ContactInformationDto>.Success(_mapper.Map<ContactInformationDto>(contactInformation), 200);
    }

    public async Task<Response<NoContent>> UpdateAsync(UpdateContactInformationDto input)
    {
        var contactInformation = _mapper.Map<ContactInformation>(input);
        var result = await _contactInformationCollection.FindOneAndReplaceAsync(s => s.Id == input.Id, contactInformation);

        if(result == null)
        {
            return Response<NoContent>.Fail("Contact information not found!", 404);
        }

        return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        var result = await _contactInformationCollection.DeleteOneAsync(s => s.Id == id);

        if (result.DeletedCount > 0)
        {
            return Response<NoContent>.Success(204);
        }

        return Response<NoContent>.Fail("Contact information not found!", 404);
    }
}
