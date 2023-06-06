using IWantoApp_Project2.Domain.Products;
using IWantoApp_Project2.Infra.Data.Config;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IWantoApp_Project2.EndPoints.Categories;

public class CategoryPost
{
    public static string Template => "/cateroies";
    public static string[] Mehods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize("EmployeePolicy")]
    public static IResult Action(CategoryRequest categoryRequest,HttpContext httpCont ,IWantDBContext context)
    {
        //Obter o user Identity que fez a operação
        var userId = httpCont.User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;
        var category = new Category(categoryRequest.Name, userId, userId);

        if (!category.IsValid)
        {
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        }

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/cateroies/{category.Id}", category.Id);// Retornar o Id Salvo
    }
}
