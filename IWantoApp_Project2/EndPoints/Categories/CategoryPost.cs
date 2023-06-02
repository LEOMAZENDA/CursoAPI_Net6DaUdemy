using IWantoApp_Project2.Domain.Products;
using IWantoApp_Project2.Infra.Data.Config;

namespace IWantoApp_Project2.EndPoints.Categories;

public class CategoryPost
{
    public static string Template => "/cateroies";
    public static string[] Mehods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;


    public static IResult Action(CategoryRequest categoryRequest, IWantDBContext context)
    {
        var category = new Category(categoryRequest.Name, "TEste", "Teste");

        if (!category.IsValid)
            return Results.BadRequest(category.Notifications);

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/cateroies/{category.Id}", category.Id);// Retorna o Id Salvo
    }
}
