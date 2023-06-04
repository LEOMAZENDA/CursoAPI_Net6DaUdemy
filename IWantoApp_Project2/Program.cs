using IWantoApp_Project2.EndPoints.Categories;
using IWantoApp_Project2.EndPoints.Employees;
using IWantoApp_Project2.Infra.Data.Config;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<IWantDBContext>(builder.Configuration["ConnectionStrings:IWandDataBase"]);
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    //Desactivando alguns padroes de segurança do Identity
    options.Password.RequireNonAlphanumeric = false;//Reitirar a obrigatoriedade de password Alfa numérico
    options.Password.RequireUppercase = false; //Obrigatoriedade de uma letra maiuscula
    options.Password.RequireLowercase = false; //Obrigatoriedade de uma letra minuscula
    options.Password.RequiredLength = 6; //Tamanho minimo de caracteres da password (6)

})
    .AddEntityFrameworkStores<IWantDBContext>();

builder.Services.AddScoped<QuarydapperAllUserWithName>();

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

app.MapMethods(EmployeePost.Template, EmployeePost.Mehods, EmployeePost.Handle);
app.MapMethods(EmployeeGet.Template, EmployeeGet.Mehods, EmployeeGet.Handle);
app.MapMethods(EmployeeGet_Dpper.Template, EmployeeGet_Dpper.Mehods, EmployeeGet_Dpper.Handle);


app.Run();