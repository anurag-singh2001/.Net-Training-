using Microsoft.Azure.Cosmos;
using StudentMgmtSys.Common;
using StudentMgmtSys.CosmosDB;
using StudentMgmtSys.Interface;
using StudentMgmtSys.Services;

var builder = WebApplication.CreateBuilder(args);

// Register CosmosClient
builder.Services.AddSingleton(s =>
{
    var options = new CosmosClientOptions
    {
        ConnectionMode = ConnectionMode.Gateway,
        HttpClientFactory = () =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (req, cert, chain, errors) => true
            };
            return new HttpClient(handler);
        }
    };

    return new CosmosClient(
        builder.Configuration["CosmosDb:Account"],
        builder.Configuration["CosmosDb:Key"],
        options
    );
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<ICosmosDBService, CosmosDBService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
