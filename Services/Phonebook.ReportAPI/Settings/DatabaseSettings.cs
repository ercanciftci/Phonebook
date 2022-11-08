namespace Phonebook.ReportAPI.Settings;

public class DatabaseSettings : IDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string ReportCollectionName { get; set; }
}
