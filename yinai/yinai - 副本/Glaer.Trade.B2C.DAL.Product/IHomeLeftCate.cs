﻿using System;
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
    public interface IHomeLeftCate
    {
        bool AddHomeLeftCate(HomeLeftCateInfo entity);

        bool EditHomeLeftCate(HomeLeftCateInfo entity);

        int DelHomeLeftCate(int ID);

        int DelHomeLeftCateAll();

        HomeLeftCateInfo GetHomeLeftCateByID(int ID);

        HomeLeftCateInfo GetHomeLeftCateByLastID();

        IList<HomeLeftCateInfo> GetHomeLeftCates(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}


