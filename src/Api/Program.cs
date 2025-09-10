using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
// Add CORS for development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
// Add Swagger for API documentation and testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}


app.UseHttpsRedirection();
app.UseRouting();
if (!app.Environment.IsDevelopment())
{
    // In production, configure CORS as needed
    app.UseCors("AllowAll");
}
app.UseAuthorization();
app.MapControllers();
// app.MapHub<YourHub>("/yourhub"); // For SignalR

app.Run();
