using AutoMapper;
using MongoDB.Driver;
using Phonebook.ContactAPI.Dtos;
using Phonebook.ContactAPI.Models;
using Phonebook.ContactAPI.Settings;
using Phonebook.Shared.Dtos;

namespace Phonebook.ContactAPI.Services;

public class PersonService : IPersonService
{
    private readonly IMongoCollection<Person> _personCollection;
    private readonly IMongoCollection<ContactInformation> _contactInformationCollection;
    private readonly IMapper _mapper;

    public PersonService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _personCollection = database.GetCollection<Person>(databaseSettings.PersonCollectionName);
        _contactInformationCollection = database.GetCollection<ContactInformation>(databaseSettings.ContactInformationCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<List<PersonDto>>> GetAllAsync()
    {
        var persons = await _personCollection.Find(person => true).ToListAsync();

        if (persons.Any())
        {
            foreach (var person in persons)
            {
                person.Contacts = await _contactInformationCollection.Find(x => x.PersonId == person.Id).ToListAsync();
            }
        }
        else
        {
            persons = new List<Person>();
        }

        return Response<List<PersonDto>>.Success(_mapper.Map<List<PersonDto>>(persons), 200);
    }

    public async Task<Response<PersonDto>> GetByIdAsync(string id)
    {
        var person = await _personCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

        if (person == null)
        {
            return Response<PersonDto>.Fail("Person not found!", 404);
        }

        person.Contacts = await _contactInformationCollection.Find(s => s.PersonId == id).ToListAsync();

        return Response<PersonDto>.Success(_mapper.Map<PersonDto>(person), 200);
    }

    public async Task<Response<PersonDto>> CreateAsync(CreatePersonDto input)
    {
        var person = _mapper.Map<Person>(input);
        await _personCollection.InsertOneAsync(person);
        return Response<PersonDto>.Success(_mapper.Map<PersonDto>(person), 200);
    }

    public async Task<Response<NoContent>> UpdateAsync(UpdatePersonDto input)
    {
        var person = _mapper.Map<Person>(input);
        var result = await _personCollection.FindOneAndReplaceAsync(s => s.Id == input.Id, person);

        if (result == null)
        {
            return Response<NoContent>.Fail("Person not found!", 404);
        }

        return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        var result = await _personCollection.DeleteOneAsync(s => s.Id == id);

        if (result.DeletedCount > 0)
        {
            return Response<NoContent>.Success(204);
        }

        return Response<NoContent>.Fail("Person not found!", 404);
    }
}
