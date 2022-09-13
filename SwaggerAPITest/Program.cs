using ATM.Api.Helpers;
using SwaggerAPITest.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.ConfigureService();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app
    .UseRouting()
    .UseSwagger()
    .UseSwaggerUI()
    .UseEndpoints(x => x.MapControllers());


app.Run();