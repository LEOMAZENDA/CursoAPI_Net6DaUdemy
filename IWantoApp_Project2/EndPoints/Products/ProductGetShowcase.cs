using IWantoApp_Project2.Infra.Data.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace IWantoApp_Project2.EndPoints.Products;

public class ProductGetShowcase
{
    public static string Template => "/products/showcase";
    public static string[] Mehods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(IWantDBContext context)
    {
        var products = context.Products.Include(p => p.Category)
            .Where(p => p.HasStock && p.Category.Active)
            .OrderBy(p => p.Name).ToList();
        var results = products.Select(p => new ProductResponse(p.Name, p.Category.Name, p.Desciption, p.HasStock,p.Preco, p.Active));
        return Results.Ok(results);
    }
}
