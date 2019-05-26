using System;
using System.Web;
using System.Drawing;
using System.IO;

public partial class fileupload_do : System.Web.UI.Page
{
    string app, formname, frmelement, rtvalue, rturl;

    string AllowFileType = ".jpg|.gif|.png|.swf|.rar|.zip|.pdf|.xls|.txt|.doc|.docx";
    int AllowFileLength = 1024 * 1024 * 3; //上传上限为3M

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "multipart/form-data";
        Response.Expires = -1;

        app = Request["app"];
        formname = Request["formname"];
        frmelement = Request["frmelement"];
        rtvalue = Request["rtvalue"];
        rturl = Request["rturl"];

        //定义上传路径
        string path_http = "";
        string path = Request.MapPath("/");

        //上传文件信息
        HttpPostedFile PostedFile = Request.Files["Filedata"];
        if (PostedFile.ContentLength == 0)
        {
            Response.Redirect("fileupload.aspx?" + Request.QueryString.ToString());
        }

        string fileName = MakeFileName();
        string fileExtend = PostedFile.FileName.Substring(PostedFile.FileName.LastIndexOf('.')).ToLower();
        int fileLength = PostedFile.ContentLength;
        string fileFullName = string.Empty;

        if (!FileValidate(fileExtend, AllowFileType))
            return_result("error", "文件格式错误", app, formname, frmelement, rtvalue, rturl);

        if (fileLength > AllowFileLength)
            return_result("error", "上传上限为3M", app, formname, frmelement, rtvalue, rturl);

        string datadirectory = DateTime.Today.ToString("yyyy-MM-dd").Replace("-", "/") + "/";
        switch (app)
        {
            case "BidProduct":
                #region 发布拍卖
                path_http += "/bidproduct/" + datadirectory;
                path += "\\bidproduct\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;
                PostedFile.SaveAs(path + fileFullName);
                CreateMiniature(240, 120, path + fileFullName, path + "s_" + fileFullName);
                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
            #endregion
            case "product":
            case "supplierproduct":
                #region 商品处理
                path_http += "/product/" + datadirectory;
                path += "\\product\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                CreateMiniature(300, 300, path + fileFullName, path + "s_" + fileFullName);

                //addWaterMark(path + "tmp_" + fileFullName, path + fileFullName, Server.MapPath("/watermark.png"));
                //addWaterMark(path + "tmp_s_" + fileFullName, path + "s_" + fileFullName, Server.MapPath("/watermark.png"));

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "productintro":
                #region 商品编辑框
                path_http += "/product/" + datadirectory;
                path += "\\product\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                //addWaterMark(path + "o_" + fileFullName, path + fileFullName, Server.MapPath("/watermark.png"));
                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion

            case "productgifts":
                #region 珠宝精品库
                path_http += "/product/gifts/" + datadirectory;
                path += "\\product\\gifts\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;
                PostedFile.SaveAs(path + fileFullName);
                CreateMiniature(240, 120, path + fileFullName, path + "s_" + fileFullName);
                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "AD":
                #region 广告处理
                path_http += "/XC/" + datadirectory;
                path += "\\XC\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);
                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "preview":
                #region 预览
                path_http += "/preview/" + datadirectory;
                path += "\\preview\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);
                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "category":
                #region 类别处理
                path_http += "/category/" + datadirectory;
                path += "\\category\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + "tmp_" + fileFullName);
                CreateMiniature(100, 100, path + "tmp_" + fileFullName, path + fileFullName);
                DelectFile(path + "tmp_" + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "friendlylink":
                #region 友情链接处理
                path_http += "/friendlylink/" + datadirectory;
                path += "\\friendlylink\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + "tmp_" + fileFullName);
                CreateMiniature(168, 48, path + "tmp_" + fileFullName, path + fileFullName);
                DelectFile(path + "tmp_" + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "brand":
                #region 品牌处理
                path_http += "/brand/" + datadirectory;
                path += "\\brand\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + "tmp_" + fileFullName);
                CreateMiniature(86, 42, path + "tmp_" + fileFullName, path + fileFullName);
                DelectFile(path + "tmp_" + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "type":
                #region 类型处理
                path_http += "/type/" + datadirectory;
                path += "\\type\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + "tmp_" + fileFullName);
                CreateMiniature(80, 80, path + "tmp_" + fileFullName, path + fileFullName);
                DelectFile(path + "tmp_" + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "content":
            case "article":
                #region 编辑框上传
                path_http += "/content/" + datadirectory;
                path += "\\content\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "promotion":
                #region 促销上传
                path_http += "/promotion/" + datadirectory;
                path += "\\promotion\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "seller":
                #region seller
                path_http += "/seller/" + datadirectory;
                path += "\\seller\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + "tmp_" + fileFullName);
                CreateMiniature(140, 140, path + "tmp_" + fileFullName, path + fileFullName);
                DelectFile(path + "tmp_" + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "pextend":
                #region 扩展处理
                path_http += "/pextend/" + datadirectory;
                path += "\\pextend\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "package":
                #region 捆绑处理
                path_http += "/package/" + datadirectory;
                path += "\\package\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);
                CreateMiniature(192, 240, path + fileFullName, path + "s_" + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "shopcert":
                #region 店铺资质
                path_http += "/shopcert/" + datadirectory;
                path += "\\shopcert\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "shopbanner":
                #region 店铺Banner
                path_http += "/shop/banner/" + datadirectory;
                path += "\\shop\\banner\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "shopcss":
                #region 店铺样式
                path_http += "/shop/css/" + datadirectory;
                path += "\\shop\\css\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "shoppages":
                #region 店铺版面
                path_http += "/shop/pages/" + datadirectory;
                path += "\\shop\\pages\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);
                //Response.Write(path_http + fileFullName);
                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "supplier":
                #region 商家相关信息
                path_http += "/supplier/" + datadirectory;
                path += "\\supplier\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "attachment":
                #region 附件上传
                path_http += "/attachment/" + datadirectory;
                path += "\\attachment\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion
            case "sealimg":
                #region 会员公章上传
                path_http += "/sealimg/" + datadirectory;
                path += "\\sealimg\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                //CreateMiniature(300, 300, path + fileFullName, path + "s_" + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion

            case "headimg":
                #region 头像上传
                path_http += "/headimg/" + datadirectory;
                path += "\\headimg\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                //CreateMiniature(300, 300, path + fileFullName, path + "s_" + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion

            case "accompanyingimg":
                path_http += "/accompanying/" + datadirectory;
                path += "\\accompanying\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                //CreateMiniature(300, 300, path + fileFullName, path + "s_" + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
            case "merchants":
                 #region 招商加盟图片处理
                path_http += "/merchants/" + datadirectory;
                path += "\\merchants\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                CreateMiniature(350, 350, path + fileFullName, path + "s_" + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;
                #endregion


            case "Bid":
            #region 招标附件上传
                path_http += "/bid/" + datadirectory;
                path += "\\bid\\" + datadirectory.Replace("/", "\\");
                CreatFolder(path);
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                //CreateMiniature(300, 300, path + fileFullName, path + "s_" + fileFullName);

                return_result("success", path_http + fileFullName, app, formname, frmelement, rtvalue, rturl);
                break;

            #endregion
            default:
                return_result("error", "error_invaildapp", app, formname, frmelement, rtvalue, rturl);
                break;
        }
    }

    /// <summary>
    /// 上传完成后跳转
    /// </summary>
    /// <param name="msgtype">消息类型</param>
    /// <param name="msg">消息内容</param>
    /// <param name="app">请求类型</param>
    /// <param name="formname">表单名</param>
    /// <param name="frmelement">表单元素</param>
    /// <param name="rtvalue">返回类型</param>
    /// <param name="rturl"></param>
    private void return_result(string msgtype, string msg, string app, string formname, string frmelement, string rtvalue, string rturl)
    {
        Response.Redirect(rturl + "?msgtype=" + msgtype + "&msg=" + msg + "&app=" + app + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue);
    }

    /// <summary>
    /// 生成文件名称
    /// </summary>
    /// <returns></returns>
    private string MakeFileName()
    {
        Random ran = new Random();
        int intstr = (int)ran.Next(9);
        return DateTime.Now.ToString("yyyyMMddHHmmss") + intstr.ToString();
    }

    /// <summary>
    /// 检查文件格式
    /// </summary>
    /// <param name="fileExt">文件格式</param>
    /// <returns></returns>
    public bool FileValidate(string fileExt, string allowType)
    {
        bool FileGood = false;
        string[] Exts = allowType.Split('|');
        foreach (string FileType in Exts)
        {
            if (FileType == fileExt) { FileGood = true; break; }
        }
        return FileGood;
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePath"></param>
    private void DelectFile(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
    }

    /// <summary>
    /// 创建文件夹
    /// </summary>
    /// <param name="filePath"></param>
    private void CreatFolder(string filePath)
    {
        if (!System.IO.Directory.Exists(filePath))
        {
            System.IO.Directory.CreateDirectory(filePath);
        }
    }

    /// <summary>
    /// 创建缩略图
    /// </summary>
    /// <param name="MaxWidth">最大宽度</param>
    /// <param name="MaxHeight">最大高度</param>
    /// <param name="OldImg">原始文件</param>
    /// <param name="NewImg">新文件</param>
    public void CreateMiniature(int maxWidth, int maxHeight, string originalPath, string thumbnailPath)
    {
        System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalPath);

        int originalWidth = originalImage.Width;
        int originalHeight = originalImage.Height;

        if (((double)originalWidth / (double)originalHeight) >= ((double)maxWidth / (double)maxHeight))
        {
            if (originalWidth > maxWidth)
            {
                originalHeight = (maxWidth * originalHeight) / originalWidth;
                originalWidth = maxWidth;
            }
        }
        else
        {
            if (originalHeight > maxHeight)
            {
                originalWidth = (originalWidth * maxHeight) / originalHeight;
                originalHeight = maxHeight;
            }
        }
        System.Drawing.Image bitmap = new System.Drawing.Bitmap(maxWidth, maxHeight);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        g.Clear(Color.White);
        g.DrawImage(originalImage, new Rectangle((maxWidth - originalWidth) / 2, (maxHeight - originalHeight) / 2, originalWidth, originalHeight));
        try
        {
            //以jpg格式保存缩略图
            bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        catch (System.Exception e)
        {
            throw e;
        }
        finally
        {
            originalImage.Dispose();
            bitmap.Dispose();
            g.Dispose();
        }
    }

    /// <summary>
    /// 添加图片水印
    /// </summary>
    /// <param name="path">原图片绝对地址</param>
    /// <param name="suiyi">水印文件</param>
    public void addWaterMark(string oldfile, string newfile, string suiyi)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(oldfile);
        Bitmap b = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        Graphics g = Graphics.FromImage(b);
        g.Clear(Color.White);
        g.DrawImage(image, 0, 0, image.Width, image.Height);

        System.Drawing.Image watermark = new Bitmap(suiyi);

        System.Drawing.Imaging.ImageAttributes imageAttributes = new System.Drawing.Imaging.ImageAttributes();
        System.Drawing.Imaging.ColorMap colorMap = new System.Drawing.Imaging.ColorMap();
        colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
        colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
        System.Drawing.Imaging.ColorMap[] remapTable = { colorMap };
        imageAttributes.SetRemapTable(remapTable, System.Drawing.Imaging.ColorAdjustType.Bitmap);
        float[][] colorMatrixElements = {
             new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
             new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
             new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
             new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},//设置透明度0.3f
             new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
            };
        System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(colorMatrixElements);
        imageAttributes.SetColorMatrix(colorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
        int xpos = 0;
        int ypos = 0;

        xpos = (image.Width - watermark.Width) / 2;//水印位置
        ypos = (image.Height - watermark.Height) / 2;//水印位置

        g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);

        watermark.Dispose();
        imageAttributes.Dispose();

        b.Save(newfile);
        b.Dispose();
        image.Dispose();

        if (File.Exists(oldfile))//删除原始文件
        {
            File.Delete(oldfile);
        }
    }
}
