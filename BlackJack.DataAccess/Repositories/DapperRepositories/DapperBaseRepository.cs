using BlackJack.DataAccess.Entities;
using BlackJack.DataAccess.Repositories.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.DapperRepositories
{
    public class DapperBaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly string _connectionString;
        protected readonly string _table;
        protected IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public DapperBaseRepository(string connectionString, string table)
        {
            _connectionString = connectionString;
            _table = table;
        }

        public virtual async Task Add(T item)
        {
            DapperHack();
            await Connection.InsertAsync(item);
        }

        public virtual async Task<T> Get(Guid id)
        {
            var res = await Connection.QueryFirstOrDefaultAsync<T>("SELECT * FROM "+_table+" WHERE Id=@Id",new { Id=id});
            return res;
        }

        public async Task Remove(T item)
        {
            await Connection.DeleteAsync(item);
        }

        public async Task Update(T item)
        {
            await Connection.UpdateAsync(item);
        }

        private static void DapperHack()
        {
            var cache = typeof(SqlMapperExtensions).GetField("KeyProperties", BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null)
                as ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>;
            cache?.Clear();
        }
    }
}
