using MCComputers.InvoiceAPI.Models;

namespace MCComputers.InvoiceAPI.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);

        Task DeleteProductAsync(int id);
    }
}
