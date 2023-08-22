using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.Data
{
    public static class DbManagerExtensions
    {
        public static void ExecuteAction(this DbManager db, Action<IDbCommand> commandAction)
        {
            db.ExecuteCommand(command =>
            {
                commandAction(command);
                return 0;
            });
        }

        public static T QueryOne<T>(this DbManager db, string query,Func<IDataReader,T> extractor)
        {
            var result = db.Query(query, extractor,0,1);
            if (result.Count() > 0)
                return result[0];
            else
                throw new EntityNotFoundException();
        }

        public static T QueryScalar<T>(this DbManager db, string query)
        {
            return db.ExecuteCommand(command =>
            {
                command.CommandText = query;
                return (T)command.ExecuteScalar();
            });
        }
    
    }

    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string? message) : base(message)
        {
        }

        public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
