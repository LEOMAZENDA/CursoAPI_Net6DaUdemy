﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantoApp_Project2.EndPoints.Employees;

public class EmployeePost
{
    public static string Template => "/employee";
    public static string[] Mehods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;


    //Este endpoint adiciona um user Identity
    public static async Task<IResult> Action(EmployeeRequest employeeRequest,HttpContext httpCont, UserManager<IdentityUser> userManager)
    {
        //Obter o user Identity que fez a operação
        //var userId = httpCont.User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;
        var newUser = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };
        var result  = await userManager.CreateAsync(newUser, employeeRequest.Password);


        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name),
             //new Claim("CreatedBy", userId)
        };

        var claimRsult = await userManager.AddClaimsAsync(newUser, userClaims);

        if (!claimRsult.Succeeded)
            return Results.BadRequest(result.Errors.First());

        return Results.Created($"/employee/{newUser.Id}", newUser.Id);// Retornar o Id Salvo
    }
}