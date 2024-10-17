using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleWeb.Data.Exceptions
{
    public class NotFoundInDatabaseException : Exception
    {
        public NotFoundInDatabaseException(string message) : base(message) { }

    }
}
