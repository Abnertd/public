using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.ORM
{
    public class PageInfo
    {
        private int _PageSize;
        private int _PageCount;
        private int _RecordCount;
        private int _CurrentPage;

        public PageInfo()
        {
            _PageSize = 10;
            _PageCount = 0;
            _RecordCount = 0;
            _CurrentPage = 1;
        }

        public PageInfo(int PageSize, int PageCount, int RecordCount, int CurrentPage)
        {
            _PageSize = PageSize;
            _PageCount = PageCount;
            _RecordCount = RecordCount;
            _CurrentPage = CurrentPage;
        }

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

        public int PageCount
        {
            get { return _PageCount; }
            set { _PageCount = value; }
        }

        public int RecordCount
        {
            get { return _RecordCount; }
            set { _RecordCount = value; }
        }

        public int CurrentPage
        {
            get { return _CurrentPage; }
            set { _CurrentPage = value; }
        }
    }
}
