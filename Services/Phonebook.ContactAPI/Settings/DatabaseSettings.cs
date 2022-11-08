namespace Phonebook.ContactAPI.Settings;

public class DatabaseSettings : IDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string PersonCollectionName { get; set; }
    public string ContactInformationCollectionName { get; set; }
}
