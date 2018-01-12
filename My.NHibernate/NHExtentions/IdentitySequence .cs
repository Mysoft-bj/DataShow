using NHibernate.Engine;
using NHibernate.Id;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.NHibernate.NHExtentions
{
    public class IdentitySequence : TableGenerator
    {


        
        public override object Generate(ISessionImplementor session, object obj)
        {
           
          //  int counter = Convert.ToInt32(base.Generate(session, obj));
            return 0;
        }
    }
}
