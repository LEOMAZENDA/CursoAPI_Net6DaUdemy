using IWantoApp_Project2.Infra.Data.Config;
using Microsoft.AspNetCore.Mvc;

namespace IWantoApp_Project2.EndPoints.Categories;

public class CategoryPut
{
    public static string Template => "/cateroies/{id:guid}";
    public static string[] Mehods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;


    public static IResult Action([FromRoute] Guid id, CategoryRequest categoryRequest, IWantDBContext context)
    {
        var cat = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        cat.Name = categoryRequest.Name;
        cat.Active = categoryRequest.Active;

        context.SaveChanges();
        return Results.Ok();


        //if (category == null)
        //    return Results.NotFound("Category não encontrada");
    }
}
