using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Phonebook.ContactAPI.Models;

public class Person
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string CompanyName { get; set; }

    public IList<ContactInformation> Contacts { get; set; }

    public Person()
    {
        Contacts = new List<ContactInformation>();
    }
}
