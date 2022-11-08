using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Phonebook.ReportAPI.Tests;

public class BaseControllerTests
{
    protected Mock<IHttpContextAccessor> _mockHttpContextAccessor;

    public BaseControllerTests()
    {
        var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", false)
           .Build();

        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var context = new DefaultHttpContext();
        var fakeTenantId = "abcd";
        context.Request.Headers["Tenant-ID"] = fakeTenantId;
        _mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);
    }
}
