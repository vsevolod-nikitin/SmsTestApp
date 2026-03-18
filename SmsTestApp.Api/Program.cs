using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi;
using SmsTestApp.Api.Services;
using SmsTestApp.Api.Services.Grpc;

internal static class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.ConfigureKestrel(options =>
        {
            // Настройка Kestrel для прослушивания на порту 8080 для HTTP/1.1 и порту 50051 для HTTP/2 (gRPC).
            options.ListenAnyIP(8080, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1;
            });
            options.ListenAnyIP(50051, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });

        builder.Services.ConfigureServices();
        builder.Services.AddGrpc();

        builder.Services.AddControllers();

        RegisterOpenApi(builder.Services);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(options =>
            {
                options.OpenApiVersion = OpenApiSpecVersion.OpenApi2_0;
            });
            app.UseSwaggerUI();
        }

        app.MapControllers();
        app.MapGrpcService<SmsTestGrpcService>();

        app.Run();
    }

    /// <summary>
    /// Зарегистрировать OpenAPI для автоматической генерации документации.
    /// </summary>
    /// <param name="services">Функционал построения.</param>
    private static void RegisterOpenApi(IServiceCollection services)
    {
        services.AddApiVersioning()
            .AddMvc()
            .AddApiExplorer((options) =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "SMS Test Api",
                Description = "Реализация тестового Api",
            });

            // Внедряем документацию.
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SmsTestApp.Api.xml"));
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SmsTestApp.Contracts.xml"));
        });
    }
}