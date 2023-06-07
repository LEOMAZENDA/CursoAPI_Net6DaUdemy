using IWantoApp_Project2.Infra.Data.Config;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IWantoApp_Project2.EndPoints.Categories;

public class CategoryPut
{
    public static string Template => "/cateroies/{id:guid}";
    public static string[] Mehods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, CategoryRequest categoryRequest, HttpContext httpCont,  IWantDBContext context)
    {
        var userId = httpCont.User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        if (category == null)
            return Results.NotFound("Category não encontrada");

        category.EditInfo(categoryRequest.Name, categoryRequest.Active, userId);

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        context.SaveChanges();
        return Results.Ok();
    }
}
