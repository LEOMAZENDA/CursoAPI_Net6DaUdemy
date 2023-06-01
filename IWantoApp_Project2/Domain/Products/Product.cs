namespace IWantoApp_Project2.Domain.Products;

public class Product : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Desciption { get; set; }
    public bool HasStock { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}
