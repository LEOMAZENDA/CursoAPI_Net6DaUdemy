using Flunt.Validations;

namespace IWantoApp_Project2.Domain.Products;

public class Category : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; }


    public Category(string name, string CreatedBy, string EditedBy)
    {
        var contracto = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name", "O Nome é obrigatório")
         .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
         .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contracto);

        Name = name;
        CreatedBy = "Teste";
        CreatedOn = DateTime.Now;
        EditedBy = "Test2";
        EditedOn = DateTime.Now;
        Active = true;
    }
}
