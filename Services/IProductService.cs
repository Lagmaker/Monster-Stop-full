using MonsterStop.Models;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task<IEnumerable<IGrouping<Supplier, Product>>> GetProductsGroupedBySupplierAsync(int categoryId);
}
