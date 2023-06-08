using IWantoApp_Project2.Domain.Products;
using IWantoApp_Project2.Infra.Data.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IWantoApp_Project2.EndPoints.Products;

public class ProductPost
{
    public static string Template => "/products";
    public static string[] Mehods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    //[Authorize("EmployeePolicy")]
    public static async Task<IResult> Action(ProductRequest productRequest,HttpContext httpCont ,IWantDBContext context)
    {
        //Obter o user Identity que fez a operação
        var userId = httpCont.User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == productRequest.CategoryId);
        var product = new Product(productRequest.Name, category, productRequest.Desciption, productRequest.Preco, productRequest.HasStock, userId);

        if (!product.IsValid)
        {
            return Results.ValidationProblem(product.Notifications.ConvertToProblemDetails());
        }

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        return Results.Created($"/products/{product.Id}", product.Id);// Retornar o Id Salvo
    }
}
