using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PanchaMukhiMarbles.API.Data;
using PanchaMukhiMarbles.API.Mappings;
using PanchaMukhiMarbles.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext Injection
builder.Services.AddDbContext<PanchaMukhiMarblesDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PanchaMukhiConnectionString")));

//Repository Injection
builder.Services.AddScoped<IProductRepository,SQLProductRepository>();

//Repository Injection (For Images)
builder.Services.AddScoped<IImageRepository,LocalImageRepository>();

//AutoMapper Injection
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Injecting Middleware For Static Files
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Images")), //This Is The Physical Folder
    RequestPath ="/Images" //Connection From Request Path, Routing To The Physical Folder
});

app.MapControllers();

app.Run();
