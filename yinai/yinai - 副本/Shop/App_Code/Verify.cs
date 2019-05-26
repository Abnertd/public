using System;
using System.Drawing;
public class Verify
{

    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    //构建构造函数
    public Verify()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;
    }

    private int _Length = 4;
    private string _SystemType = "NA";
    private string[] _CustomTypes = null;
    private string _SessionKey = "Trade_Verify";
    private Color _BgColor = Color.Black;
    private int _ImgW = 60;
    private int _ImgH = 25;

    public int Length
    {
        get { return _Length; }
        set { _Length = value; }
    }

    public string SystemType
    {
        get { return _SystemType; }
        set { _SystemType = value; }
    }

    public string[] CustomTypes
    {
        get { return _CustomTypes; }
        set { _CustomTypes = value; }
    }

    public string SessionKey
    {
        get { return _SessionKey; }
        set { _SessionKey = value; }
    }

    public Color BgColor
    {
        get { return _BgColor; }
        set { _BgColor = value; }
    }

    public int ImgW
    {
        get { return _ImgW; }
        set { _ImgW = value; }
    }

    public int ImgH
    {
        get { return _ImgH; }
        set { _ImgH = value; }
    }

    public void CodeGen()
    {
        //产生字符串
        string VerifyStr = StrGen(_Length, _SystemType, _CustomTypes);
        Bitmap Objbitmap = new Bitmap(_ImgW, _ImgH);
        Graphics objgraphice = Graphics.FromImage(Objbitmap);
        objgraphice.Clear(_BgColor);

        //画图片的边框线
        objgraphice.DrawRectangle(new Pen(Color.Gray), 0, 0, Objbitmap.Width - 1, Objbitmap.Height - 1);

        Random random = new Random();

        int x1, x2, y1, y2;
        for (int i = 0; i < 2; i++)
        {
            x1 = random.Next(Objbitmap.Width);
            x2 = random.Next(Objbitmap.Width);
            y1 = random.Next(Objbitmap.Height);
            y2 = random.Next(Objbitmap.Height);
            objgraphice.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
        }

        //在矩形内绘制字串（字串，字体，画笔颜色，左上x.左上y）
        Font font = new Font("Courier New", 15, FontStyle.Bold);

        //字体颜色
        SolidBrush fcolor = new SolidBrush(Color.White);

        objgraphice.DrawString(VerifyStr, font, fcolor, 2, 3);


        //需要输出图象信息 要修改http头 
        Response.ClearContent();
        Response.ContentType = "image/gif";

        //Display Bitmap
        Objbitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);

        //写入Session
        Session[_SessionKey] = VerifyStr;
    }

    private string StrGen(int bit, string gentype, string[] arrdata)
    {
        switch (gentype)
        {
            ///随机数字类型
            case "N":
                return NumberGen(bit);

            ///随机字母类型
            case "A":
                return AlphaGen(bit);

            ///随机数字字母混合类型
            case "NA":
                return NumberAlphaGen(bit);

            ///随机汉字类型
            case "C":
                return CharacterGen(bit);

            ///随机用户自定义类型
            case "U":
                return UserTypeGen(bit, _CustomTypes);

            ///随机数字类型
            default:
                return NumberGen(bit);
        }

    }

    private string NumberGen(int bit)
    {
        string str = "";
        Random ran = new Random();
        for (int i = 0; i < bit; i++) { str += ran.Next(9); }
        ran = null;
        return str;
    }

    private string AlphaGen(int bit)
    {
        string str = "";
        int i = 1;

        string VChar = "A,B,C,D,E,F,G,H,I,G,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,g,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        string[] vc = VChar.Split(',');
        int Va = vc.GetLength(0);

        Random ran = new Random();
        while (i <= bit) { str += vc[ran.Next(Va)]; i++; }
        ran = null;
        return str;
    }

    private string NumberAlphaGen(int bit)
    {
        string str = "";
        int i = 1;

        string VChar = "a,b,c,d,e,f,g,h,i,g,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        string[] vc = VChar.Split(',');
        int Va = vc.GetLength(0);

        Random ran = new Random();
        while (i <= bit) { str += vc[ran.Next(Va)]; i++; }
        ran = null;
        return str;
    }

    private string CharacterGen(int bit)
    {
        string[] arrbase = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
        string str = "";
        int r1, r2, r3, r4;

        Random ran = new Random();
        for (int i = 0; i < bit; i++)
        {
            r1 = 11 + ran.Next(2);

            if (r1 > 13) { r2 = ran.Next(6); } else { r2 = ran.Next(15); }
            r3 = 10 + ran.Next(5);

            if (Convert.ToBoolean(r3 = 10)) { r4 = 1 + ran.Next(14); }
            else if (Convert.ToBoolean(r3 = 15)) { r4 = ran.Next(14); }
            else { r4 = ran.Next(15); }
            //str += Chr("&H" + arrbase[r1] + arrbase[r2] + arrbase[r3] + arrbase[r4]);
        }
        return str;
    }

    private string UserTypeGen(int bit, string[] arrdata)
    {
        string str = "";
        int arrUpper = arrdata.GetLength(0);
        Random ran = new Random();
        for (int i = 0; i < bit; i++) { str += arrdata[ran.Next(arrUpper)]; }
        return str;
    }
}