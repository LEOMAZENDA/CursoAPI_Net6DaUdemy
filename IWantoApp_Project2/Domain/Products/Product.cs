using Flunt.Validations;

namespace IWantoApp_Project2.Domain.Products;

public class Product : Entity
{
    public string Name { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public string Desciption { get; private set; }
    public bool HasStock { get; private set; }
    public decimal Preco { get; private set; }
    public bool  Active { get; private set; } = true;

    private Product() { }

    public Product(string name, Category category, string desciption,decimal preco, bool hasDtock, string createdBy)
    {
        Name = name;
        Category = category;
        Desciption = desciption;
        HasStock = hasDtock;
        Preco = preco;

        CreatedBy = createdBy;
        EditedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        var contracto = new Contract<Product>()
            .IsNotNullOrEmpty(Name, "Name", "O Nome é obrigatório")
            .IsGreaterOrEqualsThan(Name, 3, "Name", "O Nome deve ter no minimo 3 ou mais caracteres")
            .IsNotNull(Category, "Category", "A categoria passada não existe no sistema")
            //.IsNullOrEmpty(Desciption, "Desciption")
            //.IsGreaterOrEqualsThan(Desciption, 3, "Desciption")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            .IsGreaterOrEqualsThan(Preco, 1, "Preco", "O preço deve ser maior ou igual a zero")
            .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contracto);
    }
}
