using ATM.Api.Helpers;
using SwaggerAPITest.Configuration;
using SwaggerAPITest.DataBase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.ConfigureService();

var app = builder.Build();

var context = app.Services.GetRequiredService<BankDbContext>();
BankDbCards.Init(context);

app.UseMiddleware<ErrorHandlerMiddleware>();

app
    .UseRouting()
    .UseSwagger()
    .UseSwaggerUI()
    .UseEndpoints(x => x.MapControllers());


app.Run();