using Microsoft.OpenApi.Models;
using ProviderService.Services;

var builder = WebApplication.CreateBuilder(args);

// �������� ��� ��������� ��� ���������
builder.Services.AddTransient<DiscountService>();
builder.Services.AddControllers();

// �������� �� Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProviderService", Version = "v1" });
});

var app = builder.Build();

// ������������ �� middleware ��� �� ���������� �� Swagger JSON endpoints
app.UseSwagger();

// ������������ �� middleware ��� �� ���������� �� swagger-ui
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProviderService V1");
    c.RoutePrefix = string.Empty; // ������� ��� Swagger UI ���� ���� ��� ���������
});

// ������ ��� endpoints
app.UseRouting();
app.UseAuthorization(); // ��������� ���� �� �������������� Authorization
app.MapControllers();

app.Run();
