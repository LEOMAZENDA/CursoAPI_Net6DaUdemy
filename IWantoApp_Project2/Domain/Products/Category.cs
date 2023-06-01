using Flunt.Validations;

namespace IWantoApp_Project2.Domain.Products;

public class Category : Entity
{
    public Guid Id { get; set; }
    //public string Email { get; set; }
    public string Name { get; set; }


    public Category(string name/*, string email*/)
    {
        var contracto = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name", "O Nome é obrigatório");
        //.IsNotNullOrEmpty(email, "Email").IsEmail(email, "Email");
        AddNotifications(contracto);


        Name = name;
        Active = true;
    }
}
