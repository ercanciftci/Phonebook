namespace Phonebook.ReportAPI.Settings;

public interface IDatabaseSettings
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    string ReportCollectionName { get; set; }
}
