using IWantoApp_Project2.EndPoints.Categories;
using IWantoApp_Project2.Infra.Data.Config;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<IWantDBContext>(builder.Configuration["ConnectionStrings:IWandDataBase"]);
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IWantDBContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Passando os EndPoins
app.MapMethods(CategoryPost.Template, CategoryPost.Mehods, CategoryPost.Handle);
app.MapMethods(CategoryGetAll.Template, CategoryGetAll.Mehods, CategoryGetAll.Handle);
app.MapMethods(CategoryPut.Template, CategoryPut.Mehods, CategoryPut.Handle);

app.Run();