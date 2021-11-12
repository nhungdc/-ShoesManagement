using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetProductsByName(String name) => ProductDAO.Instance.GetProductsByName(name);
        public Product GetProductByName(String name) => ProductDAO.Instance.GetProductByName(name);
        public IEnumerable<Product> GetProducts() => ProductDAO.Instance.GetProducts();
        public Product GetProductByID(int id) => ProductDAO.Instance.GetProductByID(id);
        public void InsertProduct(Product pro) => ProductDAO.Instance.AddNew(pro);
        public void UpdateProduct(Product pro) => ProductDAO.Instance.Update(pro);
        public void DeleteProduct(int id) => ProductDAO.Instance.Remove(id);
    }
}
