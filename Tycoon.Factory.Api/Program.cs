using Tycoon.Factory.Api;
using Tycoon.Factory.Core;
using Tycoon.Factory.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IExceptionTranslator, ExceptionTranslator>();
builder.Services.AddSingleton<PopulateData>();
builder.Services.AddCore();
builder.Services.AddInfrastructure();

var app = builder.Build();
app.Services.GetService<PopulateData>()!.PopulateRepos().Wait();

// Configure the HTTP request pipeline.
app.UseExceptionHandler(new ExceptionHandlerOptions { ExceptionHandler = ExceptionHandler.HandleException });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
