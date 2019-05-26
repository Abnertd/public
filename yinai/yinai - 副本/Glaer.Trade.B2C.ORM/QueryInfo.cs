using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.ORM
{
    public class QueryInfo
    {
        private int _PageSize;
        private int _CurrentPage;
        IList<ParamInfo> _ParamInfos = new List<ParamInfo>();
        IList<OrderInfo> _OrderInfos = new List<OrderInfo>();

        public QueryInfo()
        {
            _PageSize = 10;
            _CurrentPage = 1;
        }

        public QueryInfo(int PageSize, int CurrentPage, IList<ParamInfo> ParamInfos, IList<OrderInfo> OrderInfos)
        {
            _PageSize = PageSize;
            _CurrentPage = CurrentPage;
            _ParamInfos = ParamInfos;
            _OrderInfos = OrderInfos;
        }

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

        public int CurrentPage
        {
            get { return _CurrentPage; }
            set { _CurrentPage = value; }
        }

        public IList<ParamInfo> ParamInfos
        {
            get { return _ParamInfos; }
            set { _ParamInfos = value; }
        }

        public IList<OrderInfo> OrderInfos
        {
            get { return _OrderInfos; }
            set { _OrderInfos = value; }
        }
    }
}
