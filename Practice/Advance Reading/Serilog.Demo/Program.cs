using Serilog;
using Serilog.Demo.Services;
using Serilog.Events;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(new JsonFormatter(), "logs/serilog-demo-.json")
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Serilog Demo application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration.ReadFrom.Configuration(context.Configuration)
                     .ReadFrom.Services(services)
                     .Enrich.FromLogContext()
                     .Enrich.WithProperty("Application", "Serilog.Demo")
                     .Enrich.WithProperty("Environtment", context.HostingEnvironment.EnvironmentName)
                     .WriteTo.File(new JsonFormatter(), "logs/serilog-demo-.json");
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new()
        {
            Title = "Serilog Demo API",
            Version = "v1",
            Description = "Comprehensive structured logging demonstration with Seroilog"
        });
    });

    builder.Services.AddScoped<IUserService, UserService>();
    // builder.Services.AddScoped<IOrderService, OrderService>();
    // builder.Services.AddScoped<IPaymentService, PaymentService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Serilog Demo API v1");
            c.RoutePrefix = string.Empty;
        });
    }

    app.UseHttpsRedirection();

    // Add request logging with enrichment
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
        options.GetLevel = (httpContext, elapsed, ex) => ex != null
            ? LogEventLevel.Error
            : LogEventLevel.Information;

        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());
            diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress?.ToString());
            diagnosticContext.Set("RequestId", httpContext.TraceIdentifier);
        };
    });

    app.UseAuthorization();
    app.MapControllers();

    // Add health check endpoint
    app.MapGet("/health", () =>
    {
        Log.Information("Health check endpoint accessed");
        return new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Environment = app.Environment.EnvironmentName,
            Application = "Serilog.Demo"
        };
    });

    Log.Information("Serilog Demo application configured successfully");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Serilog Demo application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}