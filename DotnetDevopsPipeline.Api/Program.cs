using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

// ✅ KRİTİK DÜZELTME — GROUP KULLANMIYORUZ
app.MapControllers();                 // /api/v1/logs artık %100 çalışacak
app.MapHealthChecks("/api/v1/health"); // versiyonlu health
app.MapHealthChecks("/health");        // root health (Render için)

app.Run();
