using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi.Entities;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Dapper.Contrib.Extensions;
using InventoryApi.Helpers;

namespace InventoryApi.Services
{
    public class ShelvesRepository : IShelvesRepository, IDisposable
    {

        private readonly string _connectionString;
        private bool disposed = false;
        
        public ShelvesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("connectionStrings").GetSection("InventoryDbConnectionString").Value;
        }
        public void Delete(Shelf obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Shelf> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                string query = "Select Id, Name, ShelfCode from Shelves";

                return db.Query<Shelf>(query).ToList();
            }

        }

        public PagedResult<Shelf> Get(int pageNumber, int pageSize, string fields, string orderBy, string orderDirection, string searchQuery)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                db.Open();

                string itemsQry = string.Format(@"Select * from Shelves Order by {0} {1} 
                                                Offset (@RowsPerPage * (@PageNumber - 1)) rows Fetch next @RowsPerPage rows only", orderBy, orderDirection);

                string countQry = @"SELECT count(*) FROM Shelves";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("RowsPerPage", pageSize);
                dynamicParameters.Add("PageNumber", pageNumber);
                dynamicParameters.Add("TotalCount", ParameterDirection.Output);

                var shelves = db.Query<Shelf>(itemsQry, dynamicParameters).ToList();
                var totalCount = db.Query<int>(countQry, dynamicParameters).Single();

                return PagedResult<Shelf>.Create(shelves, totalCount, pageNumber, pageSize);
            }
        }

        public Shelf GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                string query = "Select Id, Name, ShelfCode from Shelves where Id = @id";

                return db.Query<Shelf>(query, new { @id = id }).FirstOrDefault();
            }
        }

        public long New(Shelf obj)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }
        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Shelf obj)
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
