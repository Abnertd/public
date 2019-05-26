using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;


/// <summary>
/// 加密签名
/// </summary>
public class SecurityUtil
{
    #region 系统变量

    public string signKeyPath = string.Empty;
    public string validateKeyPath = string.Empty;

    string erp_private_key, erp_public_key;

    #endregion

    public SecurityUtil()
    {
        erp_private_key = System.Web.Configuration.WebConfigurationManager.AppSettings["ERP_Key"].ToString();
        erp_public_key = System.Web.Configuration.WebConfigurationManager.AppSettings["ERP_Pub_Key"].ToString();
    }

    public string SignDatas(string content,string sign_type)
    {
        string m_strHashbyteSignature = GetHash(content);
        byte[] rgbHash = Convert.FromBase64String(m_strHashbyteSignature);
        RSAParameters paraPub = ConvertFromPrivateKey(erp_private_key);
        RSACryptoServiceProvider key = new RSACryptoServiceProvider();
        key.ImportParameters(paraPub);
        RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
        formatter.SetHashAlgorithm(sign_type);
        byte[] inArray = formatter.CreateSignature(rgbHash);
        return Convert.ToBase64String(inArray);
    }

    /// <summary>
    /// 签名
    /// </summary>
    /// <param name="content">需要签名的内容</param>
    /// <param name="privateKey">私钥</param>
    /// <param name="input_charset">编码格式</param>
    /// <returns></returns>
    public string signData(string content,string sign_type)
    {
        string privateKey = erp_private_key;
        byte[] signData;

        Encoding code = Encoding.GetEncoding("UTF-8");
        byte[] Data = code.GetBytes(content);

        RSAParameters paraPub = ConvertFromPrivateKey(privateKey);
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.ImportParameters(paraPub);

        if (sign_type == "MD5")
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            signData = rsa.SignData(Data, md5);
        }
        else
        {
            SHA1 sh = new SHA1CryptoServiceProvider();
            signData = rsa.SignData(Data, sh);
        }
        return Convert.ToBase64String(signData);
    }

    /// <summary>
    /// 验证签名
    /// </summary>
    /// <param name="content">需要验证的内容</param>
    /// <param name="signByte">签名结果</param>
    /// <returns>验证结果：True或False</returns>
    public bool verifyData(string content, byte[] signByte)
    {
        //string publicKey = File.ReadAllText(validateKeyPath);
        string publicKey = erp_public_key;

        bool result = false;

        Encoding code = Encoding.GetEncoding("GBK");
        byte[] Data = code.GetBytes(content);
        RSAParameters paraPub = ConvertFromPublicKey(publicKey);
        RSACryptoServiceProvider rsaPub = new RSACryptoServiceProvider();
        rsaPub.ImportParameters(paraPub);

        MD5 md5 = new MD5CryptoServiceProvider();
        result = rsaPub.VerifyData(Data, md5, signByte);
        return result;
    }

    #region 解析java生成的pem文件私钥

    private static RSACryptoServiceProvider DecodePemPrivateKey(String pemstr)
    {
        byte[] pkcs8privatekey;
        pkcs8privatekey = Convert.FromBase64String(pemstr);
        if (pkcs8privatekey != null)
        {
            RSACryptoServiceProvider rsa = DecodePrivateKeyInfo(pkcs8privatekey);
            return rsa;
        }
        else
            return null;
    }

    private static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
    {

        byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
        byte[] seq = new byte[15];

        MemoryStream mem = new MemoryStream(pkcs8);
        int lenstream = (int)mem.Length;
        BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
        byte bt = 0;
        ushort twobytes = 0;

        try
        {

            twobytes = binr.ReadUInt16();
            if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                binr.ReadByte();	//advance 1 byte
            else if (twobytes == 0x8230)
                binr.ReadInt16();	//advance 2 bytes
            else
                return null;


            bt = binr.ReadByte();
            if (bt != 0x02)
                return null;

            twobytes = binr.ReadUInt16();

            if (twobytes != 0x0001)
                return null;

            seq = binr.ReadBytes(15);		//read the Sequence OID
            if (!CompareBytearrays(seq, SeqOID))	//make sure Sequence for OID is correct
                return null;

            bt = binr.ReadByte();
            if (bt != 0x04)	//expect an Octet string 
                return null;

            bt = binr.ReadByte();		//read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count
            if (bt == 0x81)
                binr.ReadByte();
            else
                if (bt == 0x82)
                    binr.ReadUInt16();
            //------ at this stage, the remaining sequence should be the RSA private key

            byte[] rsaprivkey = binr.ReadBytes((int)(lenstream - mem.Position));
            RSACryptoServiceProvider rsacsp = DecodeRSAPrivateKey(rsaprivkey);
            return rsacsp;
        }

        catch (Exception)
        {
            return null;
        }

        finally { binr.Close(); }

    }

    private static bool CompareBytearrays(byte[] a, byte[] b)
    {
        if (a.Length != b.Length)
            return false;
        int i = 0;
        foreach (byte c in a)
        {
            if (c != b[i])
                return false;
            i++;
        }
        return true;
    }

    private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
    {
        byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

        // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
        MemoryStream mem = new MemoryStream(privkey);
        BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
        byte bt = 0;
        ushort twobytes = 0;
        int elems = 0;
        try
        {
            twobytes = binr.ReadUInt16();
            if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                binr.ReadByte();	//advance 1 byte
            else if (twobytes == 0x8230)
                binr.ReadInt16();	//advance 2 bytes
            else
                return null;

            twobytes = binr.ReadUInt16();
            if (twobytes != 0x0102)	//version number
                return null;
            bt = binr.ReadByte();
            if (bt != 0x00)
                return null;


            //------  all private key components are Integer sequences ----
            elems = GetIntegerSize(binr);
            MODULUS = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            E = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            D = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            P = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            Q = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            DP = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            DQ = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            IQ = binr.ReadBytes(elems);

            CspParameters RSAParams = new CspParameters();
            RSAParams.Flags = CspProviderFlags.UseMachineKeyStore;

            // ------- create RSACryptoServiceProvider instance and initialize with public key -----
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024, RSAParams);
            RSAParameters RSAparams = new RSAParameters();
            RSAparams.Modulus = MODULUS;
            RSAparams.Exponent = E;
            RSAparams.D = D;
            RSAparams.P = P;
            RSAparams.Q = Q;
            RSAparams.DP = DP;
            RSAparams.DQ = DQ;
            RSAparams.InverseQ = IQ;
            RSA.ImportParameters(RSAparams);
            return RSA;
        }
        catch (Exception)
        {
            return null;
        }
        finally { binr.Close(); }
    }

    private static int GetIntegerSize(BinaryReader binr)
    {
        byte bt = 0;
        byte lowbyte = 0x00;
        byte highbyte = 0x00;
        int count = 0;
        bt = binr.ReadByte();
        if (bt != 0x02)		//expect integer
            return 0;
        bt = binr.ReadByte();

        if (bt == 0x81)
            count = binr.ReadByte();	// data size in next byte
        else
            if (bt == 0x82)
            {
                highbyte = binr.ReadByte();	// data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;		// we already have the data size
            }



        while (binr.ReadByte() == 0x00)
        {	//remove high order zeros in data
            count -= 1;
        }
        binr.BaseStream.Seek(-1, SeekOrigin.Current);		//last ReadByte wasn't a removed zero, so back up a byte
        return count;
    }

    #endregion

    #region 解析.net 生成的Pem

    /// <summary>
    /// 对原始数据进行MD5加密
    /// </summary>
    /// <param name="m_strSource">待加密数据</param>
    /// <returns>返回加密后的数据</returns>
    public string GetHash(string m_strSource)
    {
        HashAlgorithm algorithm = HashAlgorithm.Create("MD5");
        byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(m_strSource);
        byte[] inArray = algorithm.ComputeHash(bytes);
        return Convert.ToBase64String(inArray);
    }

    private static RSAParameters ConvertFromPublicKey(string pemFileConent)
    {

        byte[] keyData = Convert.FromBase64String(pemFileConent);
        if (keyData.Length < 162)
        {
            throw new ArgumentException("pem file content is incorrect.");
        }
        byte[] pemModulus = new byte[128];
        byte[] pemPublicExponent = new byte[3];
        Array.Copy(keyData, 29, pemModulus, 0, 128);
        Array.Copy(keyData, 159, pemPublicExponent, 0, 3);
        RSAParameters para = new RSAParameters();
        para.Modulus = pemModulus;
        para.Exponent = pemPublicExponent;
        return para;
    }

    private static RSAParameters ConvertFromPrivateKey(string pemFileConent)
    {
        byte[] keyData = Convert.FromBase64String(pemFileConent);
        if (keyData.Length < 609)
        {
            throw new ArgumentException("pem file content is incorrect.");
        }

        int index = 11;
        byte[] pemModulus = new byte[128];
        Array.Copy(keyData, index, pemModulus, 0, 128);

        index += 128;
        index += 2;//141
        byte[] pemPublicExponent = new byte[3];
        Array.Copy(keyData, index, pemPublicExponent, 0, 3);

        index += 3;
        index += 4;//148
        byte[] pemPrivateExponent = new byte[128];
        Array.Copy(keyData, index, pemPrivateExponent, 0, 128);

        index += 128;
        index += ((int)keyData[index + 1] == 64 ? 2 : 3);//279
        byte[] pemPrime1 = new byte[64];
        Array.Copy(keyData, index, pemPrime1, 0, 64);

        index += 64;
        index += ((int)keyData[index + 1] == 64 ? 2 : 3);//346
        byte[] pemPrime2 = new byte[64];
        Array.Copy(keyData, index, pemPrime2, 0, 64);

        index += 64;
        index += ((int)keyData[index + 1] == 64 ? 2 : 3);//412/413
        byte[] pemExponent1 = new byte[64];
        Array.Copy(keyData, index, pemExponent1, 0, 64);

        index += 64;
        index += ((int)keyData[index + 1] == 64 ? 2 : 3);//479/480
        byte[] pemExponent2 = new byte[64];
        Array.Copy(keyData, index, pemExponent2, 0, 64);

        index += 64;
        index += ((int)keyData[index + 1] == 64 ? 2 : 3);//545/546
        byte[] pemCoefficient = new byte[64];
        Array.Copy(keyData, index, pemCoefficient, 0, 64);

        RSAParameters para = new RSAParameters();
        para.Modulus = pemModulus;
        para.Exponent = pemPublicExponent;
        para.D = pemPrivateExponent;
        para.P = pemPrime1;
        para.Q = pemPrime2;
        para.DP = pemExponent1;
        para.DQ = pemExponent2;
        para.InverseQ = pemCoefficient;
        return para;
    }

    #endregion


    /// <summary>
    /// DES3 ECB模式加密
    /// </summary>
    /// <param name="key">密钥</param>
    /// <param name="iv">IV(当模式为ECB时，IV无用)</param>
    /// <param name="str">明文的byte数组</param>
    /// <returns>密文的byte数组</returns>
    public byte[] DES3EncodeECB(byte[] key, byte[] data)
    {
        try
        {
            // Create a MemoryStream.
            MemoryStream mStream = new MemoryStream();
            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.ECB;
            tdsp.Padding = PaddingMode.PKCS7;
            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream cStream = new CryptoStream(mStream,
                tdsp.CreateEncryptor(key, key),
                CryptoStreamMode.Write);
            // Write the byte array to the crypto stream and flush it.
            cStream.Write(data, 0, data.Length);
            cStream.FlushFinalBlock();
            // Get an array of bytes from the 
            // MemoryStream that holds the 
            // encrypted data.
            byte[] ret = mStream.ToArray();
            // Close the streams.
            cStream.Close();
            mStream.Close();
            // Return the encrypted buffer.
            return ret;
        }
        catch (CryptographicException e)
        {
            Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            return null;
        }
    }

    /// <summary>
    /// DES3 ECB模式解密
    /// </summary>
    /// <param name="key">密钥</param>
    /// <param name="iv">IV(当模式为ECB时，IV无用)</param>
    /// <param name="str">密文的byte数组</param>
    /// <returns>明文的byte数组</returns>
    public byte[] DES3DecodeECB(byte[] key, byte[] data)
    {
        try
        {
            // Create a new MemoryStream using the passed 
            // array of encrypted data.
            MemoryStream msDecrypt = new MemoryStream(data);
            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.ECB;
            tdsp.Padding = PaddingMode.PKCS7;
            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                tdsp.CreateDecryptor(key, key),
                CryptoStreamMode.Read);
            // Create buffer to hold the decrypted data.
            byte[] fromEncrypt = new byte[data.Length];
            // Read the decrypted data out of the crypto stream
            // and place it into the temporary buffer.
            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
            //Convert the buffer into a string and return it.
            return fromEncrypt;
        }
        catch (CryptographicException e)
        {
            Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            return null;
        }
    }

    /// <summary>
    /// DES加密字符串
    /// </summary>
    /// <param name="encryptString">待加密的字符串
    /// <param name="encryptKey">加密密钥,要求为8位
    /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
    public byte[] DESEncrypt(byte[] data, string encryptKey)
    {
        try
        {
            byte[] rgbKey = ConvertHexToBytes(encryptKey);
            byte[] rgbIV = rgbKey;
            byte[] inputByteArray = data;
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            dCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();

            return mStream.ToArray();
        }
        catch
        {
            return data;
        }
    }

    /// <summary>
    /// DES解密字符串
    /// </summary>
    /// <param name="decryptString">待解密的字符串
    /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同
    /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
    public byte[] DESDecrypt(byte[] inputByteArray, string decryptKey)
    {
        try
        {
            byte[] rgbKey = ConvertHexToBytes(decryptKey);
            byte[] rgbIV = rgbKey;
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            DCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return mStream.ToArray();
        }
        catch
        {
            return inputByteArray;
        }
    }

    public byte[] ConvertHexToBytes(string value)
    {
        int len = value.Length / 2;
        byte[] ret = new byte[len];
        for (int i = 0; i < len; i++)
            ret[i] = (byte)(Convert.ToInt32(value.Substring(i * 2, 2), 16));
        return ret;
    }

}

