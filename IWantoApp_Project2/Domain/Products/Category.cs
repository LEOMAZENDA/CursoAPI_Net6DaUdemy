using Flunt.Validations;

namespace IWantoApp_Project2.Domain.Products;

public class Category : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }


    public Category(string name, string createdBy, string editedBy)
    {
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        var contracto = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "Name", "O Nome é obrigatório")
            .IsGreaterOrEqualsThan(Name, 3, "Name", "A categoria deve ter no minimo 3 ou mais caracteres")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contracto);
    }

    public void EditInfo(string name, bool active)
    {
        Active = active;
        Name = name;
    }
}
