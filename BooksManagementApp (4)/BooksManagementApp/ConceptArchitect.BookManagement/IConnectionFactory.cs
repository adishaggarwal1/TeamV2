using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public  interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }

    //public delegate IDbConnection ConnectionFactory();

    //Func<IDbConnection>
}
