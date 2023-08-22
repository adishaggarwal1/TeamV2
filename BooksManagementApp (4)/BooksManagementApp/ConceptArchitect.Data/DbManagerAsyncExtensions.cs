using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.Data
{
    public static class DbManagerAsyncExtensions
    {
        public static async Task<T> ExecuteCommandAsync<T>(this DbManager db, Func<IDbCommand,T> commandExecutor)
        {
            //await Task.Delay(100);
            //await Task.Yield();

            var result= await Task.Factory.StartNew(()=> db.ExecuteCommand(commandExecutor));

            return result; //This result is a "T" will be returned later wrapped as a Task<T>
        }

        public static async Task<List<T>> QueryAsync<T>(this DbManager db, string query, Func<IDataReader,T> extractor)
        {
            return await Task.Factory.StartNew( ()=> db.Query<T>(query, extractor));
        }

        public static async Task<int> ExecuteUpdateAsync(this DbManager db, string query)
        {
            return await Task.Factory.StartNew(() => db.ExecuteUpdate(query));
        }

        public static async Task<T> QueryOneAsync<T>(this DbManager db, string query, Func<IDataReader,T> extractor)
        {
            return await Task.Factory.StartNew(()=> db.QueryOne(query, extractor));
        }
    }
}
