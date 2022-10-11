using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entity
{
    public class clsUserEntity : BaseEntity<int>
    {
        public string userName { get; private set; }
    }
}
