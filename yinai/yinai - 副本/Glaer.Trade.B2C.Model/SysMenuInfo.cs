using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SysMenuInfo
    {
        private int _Sys_Menu_ID;
        private int _Sys_Menu_Channel;
        private string _Sys_Menu_Name;
        private int _Sys_Menu_ParentID;
        private string _Sys_Menu_Privilege;
        private string _Sys_Menu_Icon;
        private string _Sys_Menu_Url;
        private int _Sys_Menu_Target;
        private int _Sys_Menu_IsSystem;
        private int _Sys_Menu_IsDefault;
        private int _Sys_Menu_IsCommon;
        private int _Sys_Menu_IsActive;
        private int _Sys_Menu_Sort;
        private string _Sys_Menu_Site;
       

        public int Sys_Menu_ID
        {
            get { return _Sys_Menu_ID; }
            set { _Sys_Menu_ID = value; }
        }

        public int Sys_Menu_Channel
        {
            get { return _Sys_Menu_Channel; }
            set { _Sys_Menu_Channel = value; }
        }

        public string Sys_Menu_Name
        {
            get { return _Sys_Menu_Name; }
            set { _Sys_Menu_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Sys_Menu_ParentID
        {
            get { return _Sys_Menu_ParentID; }
            set { _Sys_Menu_ParentID = value; }
        }

        public string Sys_Menu_Privilege
        {
            get { return _Sys_Menu_Privilege; }
            set { _Sys_Menu_Privilege = value.Length > 300 ? value.Substring(0, 300) : value.ToString(); }
        }

        public string Sys_Menu_Icon
        {
            get { return _Sys_Menu_Icon; }
            set { _Sys_Menu_Icon = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Sys_Menu_Url
        {
            get { return _Sys_Menu_Url; }
            set { _Sys_Menu_Url = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Sys_Menu_Target
        {
            get { return _Sys_Menu_Target; }
            set { _Sys_Menu_Target = value; }
        }

        public int Sys_Menu_IsSystem
        {
            get { return _Sys_Menu_IsSystem; }
            set { _Sys_Menu_IsSystem = value; }
        }

        public int Sys_Menu_IsCommon
        {
            get { return _Sys_Menu_IsCommon; }
            set { _Sys_Menu_IsCommon = value; }
        }

        public int Sys_Menu_IsDefault
        {
            get { return _Sys_Menu_IsDefault; }
            set { _Sys_Menu_IsDefault = value; }
        }

        public int Sys_Menu_IsActive
        {
            get { return _Sys_Menu_IsActive; }
            set { _Sys_Menu_IsActive = value; }
        }

        public int Sys_Menu_Sort
        {
            get { return _Sys_Menu_Sort; }
            set { _Sys_Menu_Sort = value; }
        }

        public string Sys_Menu_Site
        {
            get { return _Sys_Menu_Site; }
            set { _Sys_Menu_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
       

    }
}
