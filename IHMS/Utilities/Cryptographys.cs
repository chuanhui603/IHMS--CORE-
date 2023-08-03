using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

/// <summary>
/// SHA256 演算法為字串加密
/// </summary>
public class Cryptographys : BaseClass
{
    #region 建構子
    public Cryptographys()
    {
        ErrorMessage = "";
    }
    #endregion
    #region 屬性
    /// <summary>
    /// 錯誤訊息
    /// </summary>
    public string ErrorMessage { get; set; }
    #endregion
    #region 函數
    public string SHA256Encode(string source)
    {
        ErrorMessage = "";
        string str_string = "";
        try
        {
            //建立一個 SHA256
            SHA256 sha = SHA256.Create();
            //將字串轉為Byte[]
            byte[] bsource = Encoding.UTF8.GetBytes(source);
            //進行 SHA256 加密
            byte[] crypto = sha.ComputeHash(bsource);
            //把加密後的字串從Byte[]轉為字串
            str_string = Convert.ToBase64String(crypto);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        return str_string;
    }
    #endregion
}
