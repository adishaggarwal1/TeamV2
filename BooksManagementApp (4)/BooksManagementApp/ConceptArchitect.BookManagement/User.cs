using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class User
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Profile_Photo { get; set; }

        public override string ToString()
        {
            return $"User[Email={Email} , Name={Name} ]";
        }
    }
}