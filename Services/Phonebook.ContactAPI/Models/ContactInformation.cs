using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Phonebook.Shared.Enums;

namespace Phonebook.ContactAPI.Models;

public class ContactInformation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public ContactTypes ContactType { get; set; }

    public string ContactData { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string PersonId { get; set; }

    [BsonIgnore]
    public Person Person { get; set; }
}
