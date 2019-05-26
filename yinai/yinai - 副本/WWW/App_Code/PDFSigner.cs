using System;
using System.Collections.Generic;
using System.Text;

using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto;

using System.Collections;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.xml.xmp;
using iTextSharp.text.pdf.crypto;
using iTextSharp.text;
using iTextSharp.text.pdf.security;


public class Cert
{
    #region Attributes

    private string path = "";
    private string password = "";
    private AsymmetricKeyParameter akp;
    private X509Certificate[] chain;

    #endregion

    #region Accessors
    public X509Certificate[] Chain
    {
        get { return chain; }
    }

    public AsymmetricKeyParameter Akp
    {
        get { return akp; }
    }

    public string Path
    {
        get { return path; }
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
    }
    #endregion

    #region Helpers

    private void processCert()
    {
        string alias = null;
        Pkcs12Store pk12;

        //First we'll read the certificate file
        pk12 = new Pkcs12Store(new FileStream(this.Path, FileMode.Open, FileAccess.Read), this.password.ToCharArray());

        //then Iterate throught certificate entries to find the private key entry

        IEnumerator i = pk12.Aliases.GetEnumerator();
        while (i.MoveNext())
        {
            alias = ((string)i.Current);
            if (pk12.IsKeyEntry(alias))
            {
                break;
            }
        }

        akp = pk12.GetKey(alias).Key;
        X509CertificateEntry[] ce = pk12.GetCertificateChain(alias);
        this.chain = new X509Certificate[ce.Length];
        for (int k = 0; k < ce.Length; ++k)
        {
            chain[k] = ce[k].Certificate;
        }
    }
    #endregion

    #region Constructors
    public Cert()
    { }
    public Cert(string cpath)
    {
        this.path = cpath;
        this.processCert();
    }
    public Cert(string cpath, string cpassword)
    {
        this.path = cpath;
        this.Password = cpassword;
        this.processCert();
    }
    #endregion

}

/// <summary>
/// This is a holder class for PDF metadata
/// </summary>
public class MetaData
{
    private Dictionary<string, string> info = new Dictionary<string, string>();

    public Dictionary<string, string> Info
    {
        get { return info; }
        set { info = value; }
    }

    public string Author
    {
        get { return (string)info["Author"]; }
        set { info.Add("Author", value); }
    }

    public string Title
    {
        get { return (string)info["Title"]; }
        set { info.Add("Title", value); }
    }

    public string Subject
    {
        get { return (string)info["Subject"]; }
        set { info.Add("Subject", value); }
    }

    public string Keywords
    {
        get { return (string)info["Keywords"]; }
        set { info.Add("Keywords", value); }
    }

    public string Producer
    {
        get { return (string)info["Producer"]; }
        set { info.Add("Producer", value); }
    }

    public string Creator
    {
        get { return (string)info["Creator"]; }
        set { info.Add("Creator", value); }
    }

    public Dictionary<string, string> getMetaData()
    {
        return this.info;
    }

    public byte[] getStreamedMetaData()
    {
        MemoryStream os = new System.IO.MemoryStream();
        XmpWriter xmp = new XmpWriter(os, info);
        xmp.Close();
        return os.ToArray();
    }
}

/// <summary>
/// this is the most important class
/// it uses iTextSharp library to sign a PDF document
/// </summary>
public class PDFSigner
{
    private string inputPDF = "";
    private string outputPDF = "";
    private Cert myCert;
    private MetaData metadata;

    public PDFSigner(string input, string output)
    {
        this.inputPDF = input;
        this.outputPDF = output;
    }

    public PDFSigner(string input, string output, Cert cert)
    {
        this.inputPDF = input;
        this.outputPDF = output;
        this.myCert = cert;
    }

    public PDFSigner(string input, string output, MetaData md)
    {
        this.inputPDF = input;
        this.outputPDF = output;
        this.metadata = md;
    }

    public PDFSigner(string input, string output, Cert cert, MetaData md)
    {
        this.inputPDF = input;
        this.outputPDF = output;
        this.myCert = cert;
        this.metadata = md;
    }

    public void Verify()
    {
    }

    private void DelectFile(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
    }

    public void Sign(string SigReason, string SigContact, string SigLocation, bool visible)
    {
        PdfReader reader = null;
        PdfStamper stamper = null;

        try
        {
            reader = new PdfReader(this.inputPDF);
            //激活多签名
            stamper = PdfStamper.CreateSignature(reader, new FileStream(this.outputPDF, FileMode.Create, FileAccess.Write), '\0', null, true);

            //禁用多签名请取消该行注释，每一个新签名将覆盖旧的签名
            //stamper = PdfStamper.CreateSignature(reader, new FileStream(this.outputPDF, FileMode.Create, FileAccess.Write), '\0');

            stamper.MoreInfo = metadata.getMetaData();
            stamper.XmpMetadata = this.metadata.getStreamedMetaData();

            PdfSignatureAppearance appearance = stamper.SignatureAppearance;

            appearance.Reason = SigReason;
            appearance.Contact = SigContact;
            appearance.Location = SigLocation;
            appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(40, 748, 164, 780), 1, "sig");
            appearance.CertificationLevel = PdfSignatureAppearance.CERTIFIED_NO_CHANGES_ALLOWED;
            appearance.Layer2Font = new Font(BaseFont.CreateFont(@"c:/windows/fonts/simsun.ttc,0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 4);

            IExternalSignature pks = new PrivateKeySignature(this.myCert.Akp, DigestAlgorithms.SHA256);
            MakeSignature.SignDetached(appearance, pks, this.myCert.Chain, null, null, null, 0, CryptoStandard.CMS);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
            DelectFile(this.inputPDF);
        }
    }
}





