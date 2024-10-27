using Alza.DbContexts;
using Alza.Infrastructure.Operations.Transient;
using Alza.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

// Set Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Alza Web API V1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "Alza Web API V2" });

    c.DocInclusionPredicate((version, apiDescription) =>
    {
        var versionAttribute = apiDescription.ActionDescriptor.EndpointMetadata
            .OfType<ApiVersionAttribute>()
            .FirstOrDefault();
        return versionAttribute?.Versions.Any(v => $"v{v}" == version) ?? false;
    });
});

var configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
     options.UseSqlServer(configuration.GetConnectionString("AlzaShopDbContext")));

builder.Services.AddTransient<IProductV1Operation, ProductV1Operation>();
builder.Services.AddTransient<IProductV2Operation, ProductV2Operation>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alza Web API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Alza Web API V2");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
