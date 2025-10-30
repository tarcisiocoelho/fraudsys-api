using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using FraudSys.Repositories;
using FraudSys.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonDynamoDB>();

builder.Services.AddSingleton<LimitRepository>();
builder.Services.AddScoped<LimitService>();
builder.Services.AddScoped<PixService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();