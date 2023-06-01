using IWantoApp_Project2.Infra.Data.Config;

namespace IWantoApp_Project2.EndPoints.Categories;

public class CategoryGetAll
{
    public static string Template => "/cateroies";
    public static string[] Mehods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;


    public static IResult Action(IWantDBContext context)
    {
        var categories = context.Categories.ToList();
        var responde = categories.Select(c => new CategoryResponse { Id = c.Id, Name = c.Name, Active = c.Active });
        return Results.Ok(responde);
    }
}
