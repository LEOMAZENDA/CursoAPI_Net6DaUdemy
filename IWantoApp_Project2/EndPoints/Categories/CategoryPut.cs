using IWantoApp_Project2.Infra.Data.Config;
using Microsoft.AspNetCore.Mvc;

namespace IWantoApp_Project2.EndPoints.Categories;

public class CategoryPut
{
    public static string Template => "/cateroies/{id}";
    public static string[] Mehods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;


    public static IResult Action([FromRoute] Guid id, CategoryRequest categoryRequest, IWantDBContext context)
    {
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        if (category == null)
            return Results.NotFound("Category não encontrada");

        category.Name = categoryRequest.Name;
        category.Active = categoryRequest.Active;

        context.SaveChanges();
        return Results.Ok();
    }
}
