using IWantoApp_Project2.Infra.Data.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace IWantoApp_Project2.EndPoints.Products;

public class ProductGetAll
{
    public static string Template => "/products";
    public static string[] Mehods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;


    [Authorize]
    //[Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(IWantDBContext context)
    {
        var products = context.Products.Include(p => p.Category).OrderBy(p => p.Name).ToList();
        var results = products.Select(p => new ProductResponse(p.Name, p.Category.Name, p.Desciption, p.HasStock, p.Active));
        return Results.Ok(results);
    }
}
