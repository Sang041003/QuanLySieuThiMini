namespace QuanLySieuThiMini.Models
{
    public class DBHelper
    {
        ProductDBContext dbContext;
        public DBHelper(ProductDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Product> GetProducts()
        {
            return dbContext.Products.ToList();
        }
        public List<ProductType> GetProductTypes()
        {
            return dbContext.ProductTypes.ToList();
        }
        public Product DetailProduct(int id)
        {
            return dbContext.Products.First(x=>x.proID==id);
        }
        public void InsertProduct(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            dbContext.Products.Update(product);
            dbContext.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            Product pro = DetailProduct(id);
            dbContext.Products.Remove(pro);
            dbContext.SaveChanges();
        }
        public List<ProductVM> GetProductVM()
        {
            List<ProductVM> productVMs = new List<ProductVM>();

            var result = dbContext.Products.Join(dbContext.ProductTypes,
                            p => p.typeID,
                            t => t.typeID,
                            (p, t) => new { product = p, type = t });

            foreach (var item in result)
            {
                productVMs.Add(new ProductVM()
                {
                    proID = item.product.proID,
                    proName = item.product.proName,
                    typeID = item.product.typeID,
                    typeName = item.type.typeName,
                    inventory = item.product.inventory,
                    cost = item.product.cost
                });
            }

            return productVMs;
        }
    
    }
}
