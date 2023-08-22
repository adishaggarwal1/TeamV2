using System.Data;

namespace ConceptArchitect.Data
{
    public class DbManager
    {
        public Func<IDbConnection> ConnectionFactory { get; set; }

        public DbManager(Func<IDbConnection> connectionFactory)
        {
            this.ConnectionFactory = connectionFactory;
        }

        public T ExecuteCommand<T>( Func<IDbCommand, T>  commandExecutor)
        {
            IDbConnection connection = null;
            try
            {
                connection = ConnectionFactory();
                var command = connection.CreateCommand();
                return commandExecutor(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw; //throw for the client
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        /// <summary>
        /// The method gets a list of records based on the query
        /// </summary>
        /// <typeparam name="T">generic parameter of record type</typeparam>
        /// <param name="query">an standard sql select query</param>
        /// <param name="extractor">a method to convert sql data (reader) to c# object</param>
        /// <param name="skip">how many initial records to skip</param>
        /// <param name="take">how many record to take. 0 indicates take all</param>
        /// <returns></returns>
        public List<T> Query<T>(string query, Func<IDataReader,T> extractor, int skip=0, int take=0)
        {
            return ExecuteCommand<List<T>>(command =>
           {
               var result = new List<T>();
               command.CommandText = query;
               var reader = command.ExecuteReader();
               int rec = 0;
               int taken = 0;
               while(reader.Read())
               {
                   var item=extractor(reader);
                   rec++;
                   if (rec < skip)
                       continue;

                   result.Add(item);
                   taken++;
                   if (take != 0 && take == taken)
                       break;
               }

               return result;
           });
        }


        /// <summary>
        /// This method is useful for executing insert/update/delete
        /// </summary>
        /// <param name="query">standard non-select query</param>
        /// <returns>rows affected</returns>
        public int ExecuteUpdate(string query)
        {
            return ExecuteCommand(command =>
            {
                command.CommandText = query;
                return command.ExecuteNonQuery();
            });
        }
    }
}