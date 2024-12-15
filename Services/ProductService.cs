using Microsoft.EntityFrameworkCore;
using MonsterStop.Models;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateProductAsync(Product product)
{
    var existingProduct = await _context.Products.FindAsync(product.Id);
    if (existingProduct == null)
    {
        // Handle not found, either return or throw exception
        return;
    }

    // Update only the scalar properties and foreign keys.
    // Do not reassign the navigation properties themselves, just their IDs, 
    // to avoid re-attaching already tracked entities.
    existingProduct.Name = product.Name;
    existingProduct.Description = product.Description;
    existingProduct.Price = product.Price;
    existingProduct.CategoryId = product.CategoryId;
    existingProduct.SupplierId = product.SupplierId;

    // Just SaveChanges, no Update call
    await _context.SaveChangesAsync();
}


    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }



    public async Task<IEnumerable<IGrouping<Supplier, Product>>> GetProductsGroupedBySupplierAsync(int categoryId)
    {
        // Query products, filter by category, include suppliers
        var products = await _context.Products
            .Include(p => p.Supplier)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();

        // Group products by their supplier
        var grouped = products.GroupBy(p => p.Supplier);
        return grouped;
    }

}


