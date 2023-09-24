using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using myproject.Data;


namespace myproject.Repository
{
    public interface IProductRepository
    {
          Task<List<Product>> ReadProductsDetails();
          Task<List<Product>> ReadProductDetailsById(int productId);
           Task<Product> CreateProduct(Product product);
           Task<Product> UpdateProduct(Product product);
           Task<Product> PartialUpdateProduct(int productId, JsonPatchDocument<Product> patchDocument);
           Task<bool> DeleteProduct(int productId);




    }
}