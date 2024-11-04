using Microsoft.OpenApi.Models;
using ProviderService.Services;

var builder = WebApplication.CreateBuilder(args);

// Πρόσθεσε τις υπηρεσίες στο κοντέινερ
builder.Services.AddTransient<DiscountService>();
builder.Services.AddControllers();

// Πρόσθεσε το Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProviderService", Version = "v1" });
});

var app = builder.Build();

// Ενεργοποίησε το middleware για να εξυπηρετεί τα Swagger JSON endpoints
app.UseSwagger();

// Ενεργοποίησε το middleware για να εξυπηρετεί το swagger-ui
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProviderService V1");
    c.RoutePrefix = string.Empty; // Ορισμός του Swagger UI στην ρίζα της εφαρμογής
});

// Χάρτης των endpoints
app.UseRouting();
app.UseAuthorization(); // Προσθέστε αυτό αν χρησιμοποιείτε Authorization
app.MapControllers();

app.Run();
