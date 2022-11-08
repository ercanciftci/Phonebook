namespace Phonebook.ContactAPI.Settings;

public interface IDatabaseSettings
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    string PersonCollectionName { get; set; }
    string ContactInformationCollectionName { get; set; }
}
