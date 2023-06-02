using Flunt.Validations;

namespace IWantoApp_Project2.Domain.Products;

public class Category : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; }


    public Category(string name, string createdBy, string editedBy)
    {
        var contracto = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name", "O Nome é obrigatório")
            .IsGreaterOrEqualsThan(name, 3, "Name", "A categoria deve ter no minimo 3 ou mais caracteres")
            .IsNotNullOrEmpty(createdBy, "CreatedBy")
            .IsNotNullOrEmpty(editedBy, "EditedBy");

        AddNotifications(contracto);

        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;
    }
}
