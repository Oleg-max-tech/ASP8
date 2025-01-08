using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opt => 
    opt.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ShopOnlineDB1;Integrated Security=True;"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();