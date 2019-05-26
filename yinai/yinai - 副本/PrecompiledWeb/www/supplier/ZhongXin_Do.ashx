<%@ WebHandler Language="C#" Class="ZhongXin_Do" %>

using System;
using System.Web;

public class ZhongXin_Do : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        Supplier supplier = new Supplier();
        supplier.Supplier_AuditLogin_Check("/supplier/ZhongXin.aspx");
        
        supplier.CheckBelongsType(new BelongsTypeEnum[] { BelongsTypeEnum.Finance });
        
        context.Response.ContentType = "text/plain";
        
        switch (context.Request["action"])
        {
            case "getwithdrawfee":
                context.Response.Write(new ZhongXin().WithdrawFee(Convert.ToDecimal(context.Request["withdraw"]), Convert.ToString(context.Request["bankname"])));
                break;
        }
        
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}