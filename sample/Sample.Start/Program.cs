using Sample.Start;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//���ɴ���
await Start.Generator(app.Services.GetRequiredService<IHostEnvironment>().ContentRootPath);

//app.Run();