using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


/// <summary>
/// UnicodeFontFactory 的摘要说明
/// </summary>
public class UnicodeFontFactory : FontFactoryImp
{
    //private static readonly string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
    //     "arialuni.ttf");//arial unicode MS是完整的unicode字型。
    //private static readonly string 标楷体Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
    //  "simkai.ttf");//标楷体


    public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color,
      bool cached)
    {
        
        //可用Arial或标楷体，自己选一个
        BaseFont baseFont = BaseFont.CreateFont(@"c:\windows\fonts\simkai.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        return new Font(baseFont, size, style, color);
    }
}