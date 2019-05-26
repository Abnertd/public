using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Product
{
    public interface IShoppingAsk
    {
        bool AddShoppingAsk(ShoppingAskInfo entity);

        bool EditShoppingAsk(ShoppingAskInfo entity);

        int DelShoppingAsk(int ID);

        ShoppingAskInfo GetShoppingAskByID(int ID);

        IList<ShoppingAskInfo> GetShoppingAsks(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
