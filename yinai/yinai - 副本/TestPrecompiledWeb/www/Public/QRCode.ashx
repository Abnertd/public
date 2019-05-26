<%@ WebHandler Language="C#" Class="QRCode" %>

using System;
using System.Web;
using System.Drawing;
using Glaer.Trade.Util.QRCode;

public class QRCode : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {

        string strcode = context.Request.QueryString["code"];
        int isize = Convert.ToInt32(context.Request.QueryString["size"]);

        context.Response.ContentType = "image/gif";
        QRCodeEncoder qrc = new QRCodeEncoder();
        Bitmap bmap = qrc.Encode(strcode, isize);
        bmap.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        qrc = null;
        
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}