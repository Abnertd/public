<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.SQLHelper" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Data.SqlClient" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    private ISQLHelper DBHelper;
    private Orders orders;
    private ITools tools;
    string tableName = "";
    string structure = "";
    string sql = ""; SqlDataReader RdrList = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        DBHelper = SQLHelperFactory.CreateSQLHelper();
        tableName = tools.CheckStr(Request["tablename"]);
       
        if (tableName != "")
        {
            try
            {
                sql = "SELECT id=a.colorder, fieldname=a.name, "
                + "tablekey=case   when   exists(SELECT   1   FROM   sysobjects   where   xtype='PK'   and   name   in   (   SELECT   name   FROM   sysindexes   WHERE   indid   in(   SELECT   indid   FROM   sysindexkeys   WHERE   id   =   a.id   AND   colid=a.colid   )))  then   'Yes'   else   'NO'   end,"
                + "type=b.name,  "
                +"lenght=COLUMNPROPERTY(a.id,a.name,'PRECISION'), "
                + "isnull=case   when   a.isnullable=1   then   'Yes'else   'NO'   end, "
                + "defaultvalues=isnull(e.text,'null'), "
                + "description=isnull(g.[value],'') "
                + "FROM   syscolumns   a "
                + "left   join   systypes   b   on   a.xtype=b.xusertype "
                + "inner   join   sysobjects   d   on   a.id=d.id     and   d.xtype='U'   and     d.name<>'dtproperties' "
                + "left   join   syscomments   e   on   a.cdefault=e.id "
                + "left   join   sys.extended_properties g   on   a.id=g.major_id   and   a.colid=g.minor_id "
                + "left   join   sys.extended_properties f   on   d.id=f.major_id   and   f.minor_id   =0"
                + "where   d.name='" + tableName + "'"
                + "order   by   a.id,a.colorder  ";
                RdrList = DBHelper.ExecuteReader(sql);
                if (RdrList.HasRows)
                {
                    structure += tableName + "<br/>";
                    structure += "<table border=\"1\" cellpadding=\"3\" cellspacing=\"0\">";
                    structure += "<tr>";
                    structure += "<td style=\" width:114px; height:32px; line-height:32px; color:#000000;\">字段名</td>";
                    structure += "<td style=\" width:114px; height:32px; line-height:32px; color:#000000;\">类型</td>";
                    structure += "<td style=\" width:114px; height:32px; line-height:32px; color:#000000;\">可为空</td>";
                    structure += "<td style=\" width:114px; height:32px; line-height:32px; color:#000000;\">默认值</td>";
                    structure += "<td style=\" width:114px; height:32px; line-height:32px; color:#000000;\">说明</td>";
                    structure += "</tr>";
                    while (RdrList.Read())
                    {

                        structure += "<tr>";
                        structure += "<td style=\" width:114px; height:32px; line-height:32px; color:#000000;\">" + RdrList["fieldname"] + "</td>";
                        structure += "<td style=\"width:114px; height:32px; line-height:32px;\">" + RdrList["type"] + "";
                        if (tools.NullStr(RdrList["type"]) == "nvarchar" || tools.NullStr(RdrList["type"]) == "varchar")
                        {
                            if (tools.NullInt(RdrList["lenght"]) == -1)
                            {
                                structure += "(MAX)";
                            }
                            else
                            {
                                structure += "(" + RdrList["lenght"] + ")";
                            }
                        }
                        structure += "</td>";
                        structure += "<td style=\"width:114px; height:32px; line-height:32px;\">" + RdrList["isnull"] + "</td>";
                        structure += "<td style=\" width:114px; height:32px; line-height:32px;\">" + RdrList["defaultvalues"] + "</td>";
                        structure += "<td style=\" width:114px; height:32px; line-height:32px;\">" + RdrList["description"] + "</td>";
                        structure += "</tr>";

                    }
                    structure += "</table>";
                    RdrList.Close();
                    RdrList = null;
                }
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
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>表数据结构</title>
</head>
<body>

<div style="width:100%;">
    <form id="frmtablename" action="gettabstructure.aspx" method="get" >
        <div style="width:190px;">表名：<input type="text" name="tablename" id="tablename" value="<% =tableName%>" style="width:180px;" /></div>
        <div style="width:190px;"><input type="submit" id="btnsub" name="btnsub" value="提交" /></div>
        
    </form>
    </div>
    <br />
    <br />
    <div>
    <%=structure%>
    </div>
</body>
</html>