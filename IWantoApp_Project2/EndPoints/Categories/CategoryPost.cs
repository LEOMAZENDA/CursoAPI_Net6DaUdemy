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
        var category = new Category(categoryRequest.Name, "Teste", "Teste");

        if (!category.IsValid)
        {
            var erros = category.Notifications.GroupBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
            return Results.ValidationProblem(erros);
        }

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/cateroies/{category.Id}", category.Id);// Retornar o Id Salvo
    }
}
