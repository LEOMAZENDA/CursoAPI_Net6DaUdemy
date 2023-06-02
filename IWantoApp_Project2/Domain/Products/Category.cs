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
