
using MCComputers.InvoiceAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCComputers.InvoiceAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);

        Task DeleteProductAsync(int id);
    }
}

