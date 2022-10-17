using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
   public interface ITokenServices
    {
        string TokenServices(clsUserEntity clsUser);
    }
}
