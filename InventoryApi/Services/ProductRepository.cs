using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using InventoryApi.Helpers;

namespace InventoryApi.Services
{
    public class ProductRepository : IProductsRepository
    {
        private readonly string _connectionString;
        private bool disposed = false;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("connectionStrings").GetSection("InventoryDbConnectionString").Value;
        }


        public IEnumerable<Product> GetAll()
        {

            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                string query = "Select Id, Name, ShelfId from Products";

                return db.Query<Product>(query).ToList();
            }
            
        }

        public PagedResult<Product> Get(int pageNumber, int pageSize, string fields, string orderBy, string orderDirection, string searchQuery)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                //string itemsQry = string.Format(@"Select Products.Id, 
                //                                        Products.Name, 
                //                                        Products.ShelfId, 
                //                                        Shelves.Id, 
                //                                        Shelves.Name, 
                //                                        Shelves.ShelfCode 
                //                                        from Products as Products
                //                                            join Shelves as Shelves on Products.ShelfId  = Shelves.Id 
                //                                Order by Products.{0} {1} 
                //                                Offset (@RowsPerPage * (@PageNumber - 1)) rows Fetch next @RowsPerPage rows only", orderBy, orderDirection);

                string itemsQry = string.Format(@"Select p.*,
		                                            s.Id as Id, 
		                                            s.Name, 
		                                            s.ShelfCode,
		                                            pa.Id as Id,
		                                            pa.ActivityType,
		                                            pa.ProductId,
		                                            pa.Total,
		                                            pa.AuditById,
		                                            pa.Date
		                                                from Products as p
		                                                left join Shelves as s on p.ShelfId  = s.Id
		                                                left join ProductActivities as pa on p.Id = pa.ProductId
                                                Order by p.{0} {1} 
                                                Offset (@RowsPerPage * (@PageNumber - 1)) rows Fetch next @RowsPerPage rows only", orderBy, orderDirection);

                string countQry = @"SELECT count(*) FROM Products";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("RowsPerPage", pageSize);
                dynamicParameters.Add("PageNumber", pageNumber);
                dynamicParameters.Add("TotalCount", ParameterDirection.Output);

                var products = db.Query<Product, Shelf, ProductActivity, Product>(itemsQry, 
                    (p, s, pa) => {
                        p.Shelf = s;

                        if (p.ProductActivities == null)
                            p.ProductActivities = new List<ProductActivity>();

                        if(pa != null)
                            p.ProductActivities.Add(pa);
                            
                        return p;
                    }, 
                    dynamicParameters)
                        .ToList();
                var totalCount = db.Query<int>(countQry, dynamicParameters).Single();

                return PagedResult<Product>.Create(products, totalCount, pageNumber, pageSize);
            }
        }

        public Product GetById(int productId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                string query = "Select Id, Name, ShelfId from Products where Id = @id";

                return db.Query<Product>(query, new { @id = productId }).FirstOrDefault();
            }
        }

        public long New(Product obj)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var product = obj as Product;

                db.Open();
                /*string query = "Insert into Products (Name, ShelfId)Values (@name, @shelfId);" +
                    "SELECT CAST(SCOPE_IDENTITY() as int);";*/

                //var test = db.Query<int>(query, new { @name = product.Name, @shelfId = product.ShelfId }).Single();

                // another test - inserting shelf into product.. see if the write is going to affect it
                // product.Shelf = new Shelf { Name = "Test Shelf" };
                var test = db.Insert<Product>(product);
                return test;
            }
        }

        public void Delete(Product obj)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var product = obj as Product;

                db.Open();
                var test = db.Delete<Product>(product);
            }
        }

        public bool Update(Product obj)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var product = obj as Product;

                db.Open();

                var test = db.Update<Product>(product);
                
                return test;
            }
        }

        public bool Exists(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                string query = "select count(1) from Products where Id = @id";
                return db.ExecuteScalar<bool>(query, new { id });
            }
        }
        public bool Save()
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //_context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
