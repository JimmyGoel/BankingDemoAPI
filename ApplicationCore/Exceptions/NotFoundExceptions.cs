using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class NotFoundExceptions : Exception
    {
        public NotFoundExceptions(int Id) : base($"Not found with Id {Id}")
        {

        }
        public NotFoundExceptions() : base($"Not found any Records")
        {

        }
    }
}
