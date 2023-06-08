using IWantoApp_Project2.EndPoints.Categories;
using IWantoApp_Project2.EndPoints.Employees;
using IWantoApp_Project2.EndPoints.Products;
using IWantoApp_Project2.EndPoints.TokenSecurity;
using IWantoApp_Project2.Infra.Data.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<IWantDBContext>(builder.Configuration["ConnectionStrings:IWantApp"]);
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    //Desactivando alguns padroes de segurança do Identity
    options.Password.RequireNonAlphanumeric = false;//Reitirar a obrigatoriedade de password Alfa numérico
    options.Password.RequireUppercase = false; //Obrigatoriedade de uma letra maiuscula
    options.Password.RequireLowercase = false; //Obrigatoriedade de uma letra minuscula
    options.Password.RequiredLength = 6; //Tamanho minimo de caracteres da password (6)
}).AddEntityFrameworkStores<IWantDBContext>();

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//      .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
//      .RequireAuthenticatedUser()
//      .Build();
//    //atribuindo politica  de permissão par quem pode usar um metodo
//    options.AddPolicy("EmployeePolicy", p =>
//        p.RequireAuthenticatedUser().RequireClaim("EmployeeCode"));
//    options.AddPolicy("Employee005Policy", p =>
//        p.RequireAuthenticatedUser().RequireClaim("EmployeeCode", "005"));
//});

builder.Services.AddAuthorization();//Adicionado o serviço de autorização
builder.Services.AddAuthentication(x =>
{
    //A baixo, Adicionado o serviço de Autenticação
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,//Sem tempo de bonus do token
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:Secretkey"]))
    };
});
builder.Services.AddScoped<QuarydapperAllUserWithName>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthentication();// sempre no primeiro lugar
app.UseAuthorization(); // sempre no segundo lugar

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Passando os EndPoins
app.MapMethods(TokenPost.Template, TokenPost.Mehods, TokenPost.Handle);

app.MapMethods(CategoryPost.Template, CategoryPost.Mehods, CategoryPost.Handle);
app.MapMethods(CategoryGetAll.Template, CategoryGetAll.Mehods, CategoryGetAll.Handle);
app.MapMethods(CategoryPut.Template, CategoryPut.Mehods, CategoryPut.Handle);

app.MapMethods(EmployeePost.Template, EmployeePost.Mehods, EmployeePost.Handle);
app.MapMethods(EmployeeGetAll.Template, EmployeeGetAll.Mehods, EmployeeGetAll.Handle);
app.MapMethods(EmployeeGet_Dpper.Template, EmployeeGet_Dpper.Mehods, EmployeeGet_Dpper.Handle);

app.MapMethods(ProductGetShowcase.Template, ProductGetShowcase.Mehods, ProductGetShowcase.Handle);
app.MapMethods(ProductGetAll.Template, ProductGetAll.Mehods, ProductGetAll.Handle);
app.MapMethods(ProductPost.Template, ProductPost.Mehods, ProductPost.Handle);

app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext http) =>
{
    var error = http.Features.Get<IExceptionHandlerFeature>()?.Error;
    if (error != null)
    {
        if (error is SqlException)
            return Results.Problem(title: "Erro de Banco de dados");
        else if (error is BadHttpRequestException)
            return Results.Problem(title: "Erro ao converter tipo de dados para tipo diferente");
    }
    return Results.Problem(title: "Ango deu errado", statusCode: 500);
});

app.Run();