using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Topping.Core.Data.Factory
{
    public abstract class ToppingAccessorFactory
    {
        public abstract static IToppingAccessor GetContext();
    }

    public  class ConcreteToppingAccessorFactory : ToppingAccessorFactory
    {

        public override static IToppingAccessor GetContext()
        {
            if (true)
            {
                return new Topping.Core.Data.SQL.ToppingAccessor();
            }
            else 
            {
                return new Topping.Core.Data.Db4o.ToppingAccessor();
            }
        }
    }
}
