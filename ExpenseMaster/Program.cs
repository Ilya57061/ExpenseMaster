using ExpenseMaster.Configuration;

var builder = WebApplication.CreateBuilder(args);
ConfigurationService.ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();
ConfigurationService.Configure(app);
app.Run();
