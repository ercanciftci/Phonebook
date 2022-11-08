using AutoMapper;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using MassTransit;
using MongoDB.Driver;
using Newtonsoft.Json;
using Phonebook.ReportAPI.Dtos;
using Phonebook.ReportAPI.Models;
using Phonebook.ReportAPI.Settings;
using Phonebook.Shared.Dtos;
using Phonebook.Shared.Messages;
using Phonebook.Shared.Models;
using RestSharp;
using System.Net;
using System.Text.Json;
using System.Threading;

namespace Phonebook.ReportAPI.Consumers;

public class CreateReportMessageCommandConsumer : IConsumer<CreateReportMessageCommand>
{
    private readonly IMongoCollection<Report> _reportCollection;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;

    public CreateReportMessageCommandConsumer(IHttpClientFactory clientFactory,
        IDatabaseSettings databaseSettings,
        IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _reportCollection = database.GetCollection<Report>(databaseSettings.ReportCollectionName);
        _configuration = configuration;
    }

    public async Task Consume(ConsumeContext<CreateReportMessageCommand> context)
    {
        var reportItem = await _reportCollection.Find(x => x.Id == context.Message.ReportId).FirstOrDefaultAsync();

        if (reportItem == null)
        {
            return;
        }

        var client = new RestClient(_configuration["ContactAPILocationDataListUrl"]);
        var request = new RestRequest();
        request.AddHeader("Accept", "application/json");

        var response = await client.GetAsync(request);

        if (response != null)
        {
            var content = response?.Content;

            if (!string.IsNullOrEmpty(content))
            {
                var result = JsonConvert.DeserializeObject<List<LocationDataModel>>(content);

                if (result != null && result.Count > 0)
                {
                    var wb = new XLWorkbook();
                    var ws1 = wb.Worksheets.Add("Sheet1");

                    ws1.Cell("A1").Value = "Konum";
                    ws1.Cell("B1").Value = "Rehbere Kayıtlı Kişi Sayısı";
                    ws1.Cell("C1").Value = "Rehbere Kayıtlı Telefon Numarası Sayısı";

                    for (int i = 0; i < result.Count; i++)
                    {
                        var item = result[i];
                        ws1.Cell("A" + (i + 2)).Value = item.Location;
                        ws1.Cell("B" + (i + 2)).Value = item.PersonCount;
                        ws1.Cell("C" + (i + 2)).Value = item.PhoneNumberCount;
                    }

                    if (string.IsNullOrEmpty(reportItem.ReportFileName))
                        reportItem.ReportFileName = Guid.NewGuid().ToString() + ".xlsx";

                    var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\reports", reportItem.ReportFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        wb.SaveAs(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        wb.Dispose();
                    }

                    reportItem.ReportStatus = Shared.Enums.ReportStatuses.Completed;
                    reportItem.CompletedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local).ToUniversalTime();
                    await _reportCollection.FindOneAndReplaceAsync(s => s.Id == reportItem.Id, reportItem);
                }
            }
        }
    }
}
