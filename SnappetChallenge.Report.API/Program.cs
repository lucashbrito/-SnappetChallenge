using SnappetChallenge.Report.API.AutoMapperConfig;
using SnappetChallenge.Report.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var keyvault = Environment.GetEnvironmentVariable("Azure:KeyVault:Address");

await builder.Services.AddReportDependencyInjection(keyvault);

var mapper = AutoMapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
