using MonsterStop.Models;

public interface ISupplierService
{
    Task<List<Supplier>> GetAllSuppliersAsync();
    Task<Supplier?> GetSupplierByIdAsync(int id);
    Task<Supplier> AddSupplierAsync(Supplier supplier);
    Task UpdateSupplierAsync(Supplier supplier);
    Task DeleteSupplierAsync(int id);
}
