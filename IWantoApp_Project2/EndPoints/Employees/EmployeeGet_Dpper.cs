using IWantoApp_Project2.Infra.Data.Config;
using Microsoft.AspNetCore.Authorization;

namespace IWantoApp_Project2.EndPoints.Employees;

public class EmployeeGet_Dpper
{
    public static string Template => "/employee_dapper";
    public static string[] Mehods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;


    //Este endpoint está sendo configurado usando o dapper
    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(int? page, int? rows, QuarydapperAllUserWithName quarydapper)
    {
        if (!page.HasValue || !rows.HasValue)
            return Results.NotFound("Não foi passado algum dos dados da paginação (page ou rows)");

        return Results.Ok(quarydapper.Executa(page.Value, rows.Value));
    }
}