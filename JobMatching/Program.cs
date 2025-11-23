using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using dashmottu.API.Infrastructure.Data.Repositories;
using JobMatching.Application.Interfaces;
using JobMatching.Application.UseCase;
using JobMatching.Config;
using JobMatching.Domain.Interfaces;
using JobMatching.Infrastructure.Data.AppData;
using JobMatching.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Revisao.Infra.Data.HealthCheck;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(x => {
    x.UseOracle(builder.Configuration.GetConnectionString("Oracle"));
});

builder.Services.AddTransient<IJobRepository, JobRepository>();
builder.Services.AddTransient<IJobUseCase, JobUseCase>();
builder.Services.AddTransient<IApplicationRepository, ApplicationRepository>();
builder.Services.AddTransient<IApplicationUseCase, ApplicationUseCase>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserUseCase, UserUseCase>();

// Health Checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" }) // Liveness
    .AddCheck<OracleHealthCheck>("oracle_query", tags: new[] { "ready" }); // Readness

// Response Compression
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.Providers.Add<BrotliCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

// Rate Limiter
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "rateLimitePolicy", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromSeconds(10);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
    options.AddFixedWindowLimiter(policyName: "rateLimitePolicy2", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromSeconds(10);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Configuração do Versionamento da API
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;

    // Versionamento pela URL (ex: /api/v1/products)    
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";  // Formato: v1, v2    
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // Configurando o swagger para exibir as versões de API disponiveis    
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        // Cria um endpoint do Swagger para cada versão de API descoberta
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"API {description.GroupName.ToUpper()}");
        }
    });
}

app.UseMiddleware<TracingMiddleware>();

app.UseResponseCompression();

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }