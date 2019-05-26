<%@ Page Language="VB" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>代码生成器</title>
</head>
<body>

<div style="width:100%;">
    <form id="frmtablename" action="code.aspx" method="get" />
        <div style="width:190px;">表名：<input type="text" name="tablename" id="tablename" value="<% =Request.QueryString("tablename")%>" style="width:180px;" /></div>
        <div style="width:190px;">实体名：<input type="text" name="modlename" id="modlename" value="<% =Request.QueryString("modlename")%>" style="width:180px;" /></div>
        <div style="width:190px;">函数名：<input type="text" name="functionname" id="functionname" value="<% =Request.QueryString("functionname")%>" style="width:180px;" /></div>
        <div style="width:190px;">主键：<input type="text" name="primarykey" id="primarykey" value="<% =Request.QueryString("primarykey")%>" style="width:180px;" /></div>
        <div style="width:190px;"><input type="submit" id="btnsub" name="btnsub" value="提交" /></div>
        </div>
    </form>
</div>


<hr />实体<hr />

<%
    Dim tableName, modleName, functionName, primaryKey As String
    
    tableName = Request.QueryString("tablename")
    modleName = Request.QueryString("modlename")
    functionName = Request.QueryString("functionname")
    primaryKey = Request.QueryString("primaryKey")
    
    Dim ObjConn As New SqlConnection(ConfigurationManager.ConnectionStrings("connstr").ToString())
    Dim ObjAdr As New SqlDataAdapter("SELECT top 0 * FROM " & tableName, ObjConn)
    Dim ObjDt As New DataTable("A")
    
    Try
        ObjAdr.FillSchema(ObjDt,SchemaType.Mapped)
    Catch ex As Exception
        'Throw ex
    Finally
        ObjAdr.Dispose()
        ObjAdr = Nothing
        ObjConn = Nothing
    End Try
    
    Dim ObjDc As DataColumn = Nothing
    
    Dim dStr As String
    
    Response.Write("<textarea name=""a"" rows=""10"" cols=""100"">")
    
    Response.Write("public class " & modleName & " { " & Chr(10))
    
    For Each ObjDc In ObjDt.Columns
        
        dStr = "private "
        
        Select Case ObjDc.DataType.ToString
            Case System.Type.GetType("System.String").ToString
                dStr &= " string _" & ObjDc.ToString & ";" & Chr(10)
            Case System.Type.GetType("System.Int32").ToString
                dStr &= " int _" & ObjDc.ToString & ";" & Chr(10)
            Case System.Type.GetType("System.DateTime").ToString
                dStr &= " DateTime _" & ObjDc.ToString & ";" & Chr(10)
            Case System.Type.GetType("System.Double").ToString, System.Type.GetType("System.Decimal").ToString
                dStr &= " double _" & ObjDc.ToString & ";" & Chr(10)

        End Select
        
        Response.Write(dStr)
        dStr = Nothing
        ObjDc = Nothing
    Next
    
    Response.Write(Chr(10))
    
    For Each ObjDc In ObjDt.Columns
        
        dStr = "public "
        
        Select Case ObjDc.DataType.ToString
            Case System.Type.GetType("System.String").ToString
  
                
                dStr &= " string " & ObjDc.ToString & "{ " & Chr(10)
                dStr &= "get { return _" & ObjDc.ToString & "; } " & Chr(10)
                
                If ObjDc.MaxLength = 1073741823 Or ObjDc.MaxLength = 2147483647 Then
                    dStr &= "set { _" & ObjDc.ToString & " = value; } " & Chr(10)
                Else
                    dStr &= "set { _" & ObjDc.ToString & " = value.Length > " & ObjDc.MaxLength & " ? value.Substring(0, " & ObjDc.MaxLength & ") : value.ToString(); } " & Chr(10)
                End If
                
                dStr &= "} " & Chr(10)
                
            Case System.Type.GetType("System.Int32").ToString
                
                dStr &= " int " & ObjDc.ToString & " { " & Chr(10)
                dStr &= "get { return _" & ObjDc.ToString & "; } " & Chr(10)
                dStr &= "set { _" & ObjDc.ToString & " = value; } " & Chr(10)
                dStr &= "} " & Chr(10)
                
            Case System.Type.GetType("System.DateTime").ToString
                
                dStr &= " DateTime " & ObjDc.ToString & " { " & Chr(10)
                dStr &= "get { return _" & ObjDc.ToString & "; } " & Chr(10)
                dStr &= "set { _" & ObjDc.ToString & " = value; } " & Chr(10)
                dStr &= "} " & Chr(10)
                
            Case System.Type.GetType("System.Double").ToString, System.Type.GetType("System.Decimal").ToString
                
                dStr &= " double " & ObjDc.ToString & " { " & Chr(10)
                dStr &= "get { return _" & ObjDc.ToString & "; } " & Chr(10)
                dStr &= "set { _" & ObjDc.ToString & " = value; } " & Chr(10)
                dStr &= "} " & Chr(10)
                
        End Select
        
        Response.Write(dStr & Chr(10))
        ObjDc = Nothing
    Next
    
    Response.Write("}</textarea>")
    
%>

<hr />Maping<hr />
<textarea name="a" rows="10" cols="100">
<%
    Response.Write("//" & tableName & Chr(10))
    For Each ObjDc In ObjDt.Columns
        dStr = "Relation[""" & modleName & "." & ObjDc.ToString & """] = """ & tableName & "." & ObjDc.ToString & """;"
        Response.Write(dStr & Chr(10))
        ObjDc = Nothing
    Next
%>
</textarea>

<hr />DAL接口<hr />

<textarea name="a" rows="10" cols="100">
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;

namespace Glaer.Trade.B2C.DAL.命名空间
{
     public interface I<% =functionName%> 
     {
        bool Add<% =functionName%> (<% =modleName%> entity);

        bool Edit<% =functionName%> (<% =modleName%> entity);
        
        int Del<% =functionName%>(int ID);

        <% =modleName%> Get<% =functionName%>ByID(int ID);

        IList<<% =modleName%>> Get<% =functionName%>s(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
</textarea>

<hr />DAL接口实现<hr />
<textarea name="a" rows="10" cols="100">
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.命名空间
{
    public class <% =functionName%> : I<% =functionName%> 
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public <% =functionName%>() {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool Add<% =functionName%> (<% =modleName%> entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM <% =tableName%>";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            
            <%
                For Each ObjDc In ObjDt.Columns
                    dStr = "DrAdd[""" & ObjDc.ToString & """] = entity." & ObjDc.ToString & ";"
                    Response.Write(dStr & Chr(10))
                    dStr = Nothing
                    ObjDc = Nothing
                Next
            %>
            DtAdd.Rows.Add(DrAdd);
            try {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        public virtual bool Edit<% =functionName%> (<% =modleName%> entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM <% =tableName%> WHERE <% =primaryKey%> = " + entity.<% =primaryKey%>;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    <%
                        For Each ObjDc In ObjDt.Columns
                            dStr = "DrAdd[""" & ObjDc.ToString & """] = entity." & ObjDc.ToString & ";"
                            Response.Write(dStr & Chr(10))
                            dStr = Nothing
                            ObjDc = Nothing
                        Next
                    %>
                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else  {
                    return false;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
            return true;
        
        }

        public virtual int Del<% =functionName%> (int ID) {
            string SqlAdd = "DELETE FROM <% =tableName%> WHERE <% =primaryKey%> = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        
       public virtual <% =modleName%> Get<% =functionName%>ByID(int ID)
        {
            <% =modleName%> entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM <% =tableName%> WHERE <% =primaryKey%> = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new <% =modleName%>();
                    
                    <%
                        For Each ObjDc In ObjDt.Columns
                            dStr = ""
                            Select Case ObjDc.DataType.ToString
                                Case System.Type.GetType("System.String").ToString
                                    dStr = " entity." & ObjDc.ToString & " = Tools.NullStr(RdrList[""" & ObjDc.ToString & """]);"
                                Case System.Type.GetType("System.Int32").ToString
                                    dStr = " entity." & ObjDc.ToString & " = Tools.NullInt(RdrList[""" & ObjDc.ToString & """]);"
                                Case System.Type.GetType("System.DateTime").ToString
                                    dStr = " entity." & ObjDc.ToString & " = Tools.NullDate(RdrList[""" & ObjDc.ToString & """]);"
                                Case System.Type.GetType("System.Double").ToString, System.Type.GetType("System.Decimal").ToString
                                    dStr = " entity." & ObjDc.ToString & " = Tools.NullDbl(RdrList[""" & ObjDc.ToString & """]);"
                            End Select
                            
                            Response.Write(dStr & Chr(10))
                            ObjDc = Nothing
                        Next
                    %>
                }
                
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }
        
        public virtual IList<<% =modleName%>> Get<% =functionName%>s(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<<% =modleName%>> entitys = null;
            <% =modleName%> entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable; 
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "<% =tableName%>";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<<% =modleName%>>();
                    while (RdrList.Read())
                    {
                       entity = new <% =modleName%>();
                        <%
                            dStr = ""
                            For Each ObjDc In ObjDt.Columns
                                Select Case ObjDc.DataType.ToString
                                    Case System.Type.GetType("System.String").ToString
                                        dStr &= " entity." & ObjDc.ToString & " = Tools.NullStr(RdrList[""" & ObjDc.ToString & """]);" & Chr(10)
                                    Case System.Type.GetType("System.Int32").ToString
                                        dStr &= " entity." & ObjDc.ToString & " = Tools.NullInt(RdrList[""" & ObjDc.ToString & """]);" & Chr(10)
                                    Case System.Type.GetType("System.DateTime").ToString
                                        dStr &= " entity." & ObjDc.ToString & " = Tools.NullDate(RdrList[""" & ObjDc.ToString & """]);" & Chr(10)
                                    Case System.Type.GetType("System.Double").ToString, System.Type.GetType("System.Decimal").ToString
                                        dStr &= " entity." & ObjDc.ToString & " = Tools.NullDbl(RdrList[""" & ObjDc.ToString & """]);" & Chr(10)
                                End Select
                                ObjDc = Nothing
                            Next
                            
                            Response.Write(dStr)
                        %>
                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try
            {
                Page = new PageInfo();
                SqlTable = "<% =tableName%>";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(<% =primaryKey%>) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize= Query.PageSize;

                return Page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
</textarea>

<hr />DAL工厂<hr />
<textarea name="a" rows="10" cols="100">
using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.命名空间
{
    public class <% =functionName%>Factory
    {
        public static I<% =functionName%> Create<% =functionName%>() {
            string path = ConfigurationManager.AppSettings["DAL<% =functionName%>"];
            string classname = path + ".<% =functionName%>";
            return (I<% =functionName%>)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
</textarea>

<hr />BLL接口<hr />

<textarea name="a" rows="10" cols="100">
using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.命名空间
{
    public interface I<% =functionName%>
    {
        bool Add<% =functionName%> (<% =modleName%> entity);

        bool Edit<% =functionName%> (<% =modleName%> entity);
        
        int Del<% =functionName%>(int ID);

        <% =modleName%> Get<% =functionName%>ByID(int ID);

        IList<<% =modleName%>> Get<% =functionName%>s(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
</textarea>

<hr />BLL接口实现<hr />
<textarea name="a" rows="10" cols="100">
using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.命名空间 {
    public class <% =functionName%> : I<% =functionName%> {
        protected DAL.Product.I<% =functionName%> MyDAL;
        protected IRBAC RBAC;

        public <% =functionName%>() {
            MyDAL = DAL.Product.<% =functionName%>Factory.Create<% =functionName%>();
            RBAC = RBACFactory.CreateRBAC();
        } 

        public virtual bool Add<% =functionName%> (<% =modleName%> entity)  {
            return MyDAL.Add<% =functionName%>(entity);
        }

        public virtual bool Edit<% =functionName%> (<% =modleName%> entity) {
            return MyDAL.Edit<% =functionName%>(entity);
        }

        public virtual int Del<% =functionName%> (int ID) {
            return MyDAL.Del<% =functionName%>(ID);
        }
        
        public virtual <% =modleName%> Get<% =functionName%>ByID(int ID) {
            return MyDAL.Get<% =functionName%>ByID(ID);
        }
        
        public virtual IList<<% =modleName%>> Get<% =functionName%>s(QueryInfo Query) {
            return MyDAL.Get<% =functionName%>s(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query) {
            return MyDAL.GetPageInfo(Query);
        }
        
    }
}

</textarea>

<hr />BLL工厂<hr />
<textarea name="a" rows="10" cols="100">
using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.命名空间
{
    public class <% =functionName%>Factory
    {
        public static I<% =functionName%> Create<% =functionName%>() {
            string path = ConfigurationManager.AppSettings["BLL<% =functionName%>"];
            string classname = path + ".<% =functionName%>";
            return (I<% =functionName%>)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
</textarea>

<hr />表现层<hr />
<textarea name="a" rows="10" cols="100">
    public virtual void Add<% =functionName%>()
    {
<%
    Dim pStr As String
    dStr = ""
    pStr = ""
    
    For Each ObjDc In ObjDt.Columns
        Select Case ObjDc.DataType.ToString
            Case System.Type.GetType("System.String").ToString
                pStr &= " string " & ObjDc.ToString & " = tools.CheckStr(Request.Form[""" & ObjDc.ToString & """]);" & Chr(10)
                dStr &= " entity." & ObjDc.ToString & " = " & ObjDc.ToString & ";" & Chr(10)
            Case System.Type.GetType("System.Int32").ToString
                pStr &= " int " & ObjDc.ToString & " = tools.CheckInt(Request.Form[""" & ObjDc.ToString & """]);" & Chr(10)
                dStr &= " entity." & ObjDc.ToString & " = " & ObjDc.ToString & ";" & Chr(10)
            Case System.Type.GetType("System.DateTime").ToString
                pStr &= " DateTime " & ObjDc.ToString & " = tools.CheckInt(Request.Form[""" & ObjDc.ToString & """]);" & Chr(10)
                dStr &= " entity." & ObjDc.ToString & " = " & ObjDc.ToString & ";" & Chr(10)
            Case System.Type.GetType("System.Double").ToString, System.Type.GetType("System.Decimal").ToString
                pStr &= " double " & ObjDc.ToString & " = tools.CheckFloat(Request.Form[""" & ObjDc.ToString & """]);" & Chr(10)
                dStr &= " entity." & ObjDc.ToString & " = " & ObjDc.ToString & ";" & Chr(10)
        End Select
        ObjDc = Nothing
    Next
    
    Response.Write(pStr & Chr(10) & modleName & " entity = new " & modleName & "();" & Chr(10) & dStr)
%>
        if (MyBLL.Add<% =functionName%>(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "<% =tableName%>_add.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }
    
    public virtual void Edit<% =functionName%>() {
    
    <%  Response.Write(pStr & Chr(10) & modleName & " entity = new " & modleName & "();" & Chr(10) & dStr)%>
    
        if (MyBLL.Edit<% =functionName%>(entity)) {
            Public.Msg("positive", "操作成功", "操作成功", true, "<% =tableName%>_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void Del<% =functionName%>() {
        int <% =primaryKey%> = tools.CheckInt(Request.QueryString["<% =primaryKey%>"]);
        if (MyBLL.Del<% =functionName%>(<% =primaryKey%>)> 0) {
            Public.Msg("positive", "操作成功", "操作成功", true, "<% =tableName%>_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual <% =modleName%> Get<% =functionName%>ByID(int cate_id) {
        return MyBLL.Get<% =functionName%>ByID(cate_id);
    }
    
</textarea>

<hr />取出信息<hr />
<textarea name="a" rows="10" cols="100">
<%
    dStr = ""
    
    For Each ObjDc In ObjDt.Columns
        Select Case ObjDc.DataType.ToString
            Case System.Type.GetType("System.String").ToString
                dStr &= ObjDc.ToString & " = entity." & ObjDc.ToString & ";" & Chr(10)
            Case System.Type.GetType("System.Int32").ToString
                dStr &= ObjDc.ToString & " = entity." & ObjDc.ToString & ";" & Chr(10)
            Case System.Type.GetType("System.DateTime").ToString
                dStr &= ObjDc.ToString & " = entity." & ObjDc.ToString & ";" & Chr(10)
            Case System.Type.GetType("System.Double").ToString, System.Type.GetType("System.Decimal").ToString
                dStr &= ObjDc.ToString & " = entity." & ObjDc.ToString & ";" & Chr(10)
        End Select
        ObjDc = Nothing
    Next
    
    Response.Write(dStr)
%>
</textarea>

<hr />修改页面<hr />
<textarea name="a" rows="10" cols="100">

private <%  =functionName%> myApp;
private ITools tools;

<%
    Dim prstr, print, prdbl, prdata As String
    prstr = "private string "
    print = "private int "
    prdbl = "private double "
    prdata = "private DateTime "
    
    For Each ObjDc In ObjDt.Columns
        Select Case ObjDc.DataType.ToString
            Case System.Type.GetType("System.String").ToString
                prstr &= ObjDc.ToString & ","
            Case System.Type.GetType("System.Int32").ToString
                print &= ObjDc.ToString & ","
            Case System.Type.GetType("System.DateTime").ToString
                prdata &= ObjDc.ToString & ","
            Case System.Type.GetType("System.Double").ToString, System.Type.GetType("System.Decimal").ToString
                prdbl &= ObjDc.ToString & ","
        End Select
        ObjDc = Nothing
    Next
    
    Response.Write(prstr & Chr(10) & print & Chr(10) & prdata & Chr(10) & prdbl)
%>

protected void Page_Load(object sender, EventArgs e)
{
    myApp = new <%  =functionName%>();
    tools = ToolsFactory.CreateTools();
    
    <% =primaryKey%> = tools.CheckInt(Request.QueryString["<% =primaryKey%>"]);
    <%  =modleName%> entity = myApp.Get<%  =functionName%>ByID(<% =primaryKey%>);
    if (entity == null) {
        Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
        Response.End();
    } else {
        <%Response.Write(dStr)%>
    }
}
</textarea>

<textarea name="in" rows="10" cols="100">
<%
    dStr = ""
    
    For Each ObjDc In ObjDt.Columns
        dStr &= ", " & ObjDc.ToString
    Next
    
    If dStr.Length > 0 Then
        Response.Write(dStr.Remove(0, 1))
    End If
   
%>
</textarea>


</body>
</html>

