using Dapper;
using IWantoApp_Project2.EndPoints.Employees;
using Microsoft.Data.SqlClient;

namespace IWantoApp_Project2.Infra.Data.Config;

public class QuarydapperAllUserWithName
{
    IConfiguration configuration;

    public QuarydapperAllUserWithName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IEnumerable<EmployeeResponse> Executa(int page, int rows)
    {
        var db = new SqlConnection(configuration["ConnectionStrings:IWandDataBase"]);
        var query = @"Select Email, ClaimValue as Name from AspNetUsers u 
                    inner join AspNetUserClaims c ON u.Id = c.UserId 
                    and ClaimType = 'Name' order by Name
                    OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
        return db.Query<EmployeeResponse>(
            query,
            new { page, rows });
    }
}
