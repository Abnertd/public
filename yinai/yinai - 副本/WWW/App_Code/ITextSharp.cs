
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;
using System.Collections;
using Org.BouncyCastle.Pkcs;
using Glaer.Trade.Util.Tools;

/// <summary>
/// ITextSharp 的摘要说明
/// </summary>
public class ITextSharp
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    string PDFPath;
    string PDF_CertPath;
    string PDF_CertPassword;

    ITools tools;

	public ITextSharp()
	{
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        PDFPath = ConfigurationManager.AppSettings["PDFPath"];
        PDF_CertPath = ConfigurationManager.AppSettings["PDF_CertPath"];
        PDF_CertPassword = ConfigurationManager.AppSettings["PDF_CertPassword"];

        tools = ToolsFactory.CreateTools();
	}

    /// <summary>
    /// 生成PDF格式的采购合同
    /// </summary>
    /// <param name="savePath"></param>
    /// <param name="strHTML"></param>
    public  void CreatePDFContract(string fileName, string strHTML) 
    {
        string savePath = Server.MapPath("/Download/" + fileName + ".pdf");

        Document document = new Document(PageSize.A4);
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
        document.Open();

        try
        {
            FontFactory.RegisterFamily("宋体 常规", "simsun", @"c:\windows\fonts\simsun.ttc,0");

            HTMLWorker htmlWorker = new HTMLWorker(document);
            htmlWorker.Parse(new StringReader(strHTML));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            document.Close();
        }
    }


    /// <summary>
    /// 将Html文字 输出到PDF档里
    /// </summary>
    /// <param name="htmlText"></param>
    /// <returns></returns>
    public void ConvertHtmlTextToPDF(string fileName,string htmlText)
    {
        if (string.IsNullOrEmpty(htmlText))
        {
            
        }

        string savePath = Server.MapPath("/Download/" + fileName + ".pdf");

        //避免当htmlText无任何html tag标签的纯文字时，转PDF时会挂掉，所以一律加上<p>标签
        htmlText = "<p>" + htmlText + "</p>";

        MemoryStream outputStream = new MemoryStream();//要把PDF写到哪个串流
        byte[] data = Encoding.UTF8.GetBytes(htmlText);//字串转成byte[]
        MemoryStream msInput = new MemoryStream(data);
        Document doc = new Document();//要写PDF的文件，建构子没填的话预设直式A4
        //PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);

        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(savePath, FileMode.Create));

        //指定文件预设开档时的缩放为100%
        PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
        //开启Document文件 
        doc.Open();
        //使用XMLWorkerHelper把Html parse到PDF档里
        XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8, new UnicodeFontFactory());
        //将pdfDest设定的资料写到PDF档
        PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
        writer.SetOpenAction(action);
        doc.Close();
        msInput.Close();
        outputStream.Close();
        //回传PDF档案 
       
    }



    /// <summary>
    /// 对PDF采购合同进行电子签名
    /// </summary>
    /// <param name="inputName">源文件名称</param>
    /// <param name="outputName">签名后输出的文件名称</param>
    public void PDFSign(string inputName,string outputName)
    {
        string inputPath = PDFPath + inputName + ".pdf";
        string outputPath = PDFPath + outputName + ".pdf";

        Cert myCert = new Cert(PDF_CertPath, PDF_CertPassword);

        MetaData MyMD = new MetaData();
        MyMD.Author = tools.NullStr(Application["Site_Name"]);
        MyMD.Title = tools.NullStr(Application["Site_Name"]) + "电子交易合同";
        MyMD.Subject = "";
        MyMD.Keywords = "";
        MyMD.Creator = tools.NullStr(Application["Site_Name"]);
        MyMD.Producer = tools.NullStr(Application["Site_Name"]);

        PDFSigner pdfs = new PDFSigner(inputPath, outputPath, myCert, MyMD);
        pdfs.Sign("签订交易合同", tools.NullStr(Application["Site_Tel"]), tools.NullStr(Application["Site_Name"]), true);
    }
}
