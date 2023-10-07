using Sample.Start;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Éú³É´úÂë
await Start.Generator(app.Services.GetRequiredService<IHostEnvironment>().ContentRootPath);

//app.Run();