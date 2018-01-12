using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Core
{
    public interface IUnitOfWorkManager {
        IUnitOfWork Begin();
        IUnitOfWork Current{get;}
    }
    public interface IUnitOfWork:IDisposable
    {
        void Complete();
    }
}
