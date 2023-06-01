namespace IWantoApp_Project2.Domain.Products;

public class Category : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; }


    //public Categoria(string name)
    //{
    //    var contracto = new Contract<Categoria>()
    //        .IsNotNullOrEmpty(name, "Name","O Nome é obrigatório");
    //    AddNotifications(contracto);

    //    name = name;
    //    Active = true;
    //}
}
