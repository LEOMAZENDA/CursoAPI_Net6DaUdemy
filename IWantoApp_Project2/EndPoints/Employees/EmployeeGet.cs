using Microsoft.AspNetCore.Identity;

namespace IWantoApp_Project2.EndPoints.Employees;

public class EmployeeGet
{
    public static string Template => "/employee";
    public static string[] Mehods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;


    //Este endpoint adiciona um user Identity
    public static IResult Action(int page, int rows, UserManager<IdentityUser> userManager)
    {
        var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();//Paginacao na lista
        var employees = new List<EmployeeResponse>();

        foreach (var item in users)
        {
            var calims = userManager.GetClaimsAsync(item).Result;
            var claimName = calims.FirstOrDefault(c => c.Type == "Name");
            var username = claimName != null ? claimName.Value : "Sem nome no sistema"/* string.Empty*/;
            employees.Add(new EmployeeResponse(item.Email, username));
        }


        return Results.Ok(employees);
    }
}