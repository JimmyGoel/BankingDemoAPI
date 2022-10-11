using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class DuplicateException :Exception
    {
        public DuplicateException(string message):base(message)
        {

        }
    }
}
