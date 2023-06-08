namespace IWantoApp_Project2.EndPoints.Products;

public record ProductResponse (string Name, string categoryName, string Description, bool HasStock, decimal preco, bool Active);