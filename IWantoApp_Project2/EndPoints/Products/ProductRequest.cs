using IWantoApp_Project2.Domain.Products;

namespace IWantoApp_Project2.EndPoints.Products;

public record ProductRequest ( string Name, Guid CategoryId, string Desciption, bool HasStock, bool Active);
