using BichoBet.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationCore(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddAuthorization();

var app = builder.Build();

await app.SeedDatabaseAsync();

app.UseApplicationPipeline();
app.Run();