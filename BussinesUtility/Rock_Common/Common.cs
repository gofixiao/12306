using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Data;
using System.Linq;
using System.IO;
using System.Xml;
using System.Net;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace RockUtility
{
    public class Common
    {

        #region 属性，字段
        //DEC密匙
        private static string DECKey = "nmf#gfs*fsa@Vhts";
        #endregion

        #region 构造函数
        public Common()
        {

        }
        #endregion


        #region 删除指定名称Cookie
        public static void RemoveCookie(string cookieName)
        {
            HttpCookie hc = HttpContext.Current.Request.Cookies[cookieName]; //XXXX为Cookie的名字
            if (hc != null)
                hc.Expires = DateTime.Now.AddDays(-1); //设置过期时间为过去的某个时间
            HttpContext.Current.Response.Cookies.Add(hc); //覆盖客户端的同名Cookie
        }
        #endregion

        #region 将对象序列化后添加到指定Cookie
        /// <summary>
        /// 添加Cookie对象并序列化
        /// </summary>
        /// <param name="obj">需进行序列化的对象</param>
        /// <param name="obj">cookie名称</param>
        /// <returns>返回二进制字符串</returns>
        public static string BinarySerialize(object obj, string cookiename)
        {
            BinaryFormatter bf = new BinaryFormatter();  //声明一个序列化类
            MemoryStream ms = new MemoryStream();  //声明一个内存流
            bf.Serialize(ms, obj);  //执行序列化操作
            byte[] result = new byte[ms.Length];
            result = ms.ToArray();
            string temp = System.Convert.ToBase64String(result);
            temp = Encrypt(temp);
            HttpCookie cookie = new HttpCookie(cookiename);
            cookie.HttpOnly = true;
            cookie.Values.Add("val", temp);
            if (HttpContext.Current.Response.Cookies[cookiename] == null)
            {
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
            ms.Flush();
            ms.Close();
            return temp;
        }
        #endregion

        #region 获取Cookie对象并反序列化为指定类型
        /// <summary>
        /// 获取Cookie对象并序列化
        /// </summary>
        /// <param name="cookiename">cookie名称</param>
        /// <returns>反序列化后的Object对象</returns>
        public static T BinaryDeserialize<T>(string cookiename)
        {
            if (HttpContext.Current.Request.Cookies[cookiename] != null)
            {
                try
                {
                    string temp = HttpContext.Current.Request.Cookies[cookiename].Values["val"];
                    temp = Decrypt(temp);
                    byte[] b = System.Convert.FromBase64String(temp);  //将得到的字符串根据相同的编码格式分成字节数组
                    MemoryStream msb = new MemoryStream(b, 0, b.Length);  //从字节数组中得到内存流
                    BinaryFormatter bfb = new BinaryFormatter();
                    return (T)bfb.Deserialize(msb);  //返回Object对象
                }
                catch (Exception)
                {
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }
        #endregion

        #region 原DEC 加密过程

        /// <summary>
        /// DEC 加密过程
        /// </summary>
        /// <param name="strValue">被加密的字符串</param> 
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string strValue)
        {
            //访问数据加密标准(DES)算法的加密服务提供程序 (CSP) 版本的包装对象
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = ASCIIEncoding.ASCII.GetBytes(DECKey.Substring(0, 8));　//建立加密对象的密钥和偏移量
            des.IV = ASCIIEncoding.ASCII.GetBytes(DECKey.Insert(0, "w").Substring(0, 8));　 //原文使用ASCIIEncoding.ASCII方法的GetBytes方法

            byte[] inputByteArray = Encoding.Default.GetBytes(strValue);//把字符串放到byte数组中

            MemoryStream ms = new MemoryStream();//创建其支持存储区为内存的流　
            //定义将数据流链接到加密转换的流
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //上面已经完成了把加密后的结果放到内存中去

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        #endregion

        #region  原DEC 解密过程

        /// <summary>
        /// DEC 解密过程
        /// </summary>
        /// <param name="EncValue">被解密的字符串</param>  
        /// <returns>返回被解密的字符串</returns>
        public static string Decrypt(string EncValue)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[EncValue.Length / 2];
            for (int x = 0; x < EncValue.Length / 2; x++)
            {
                int i = (Convert.ToInt32(EncValue.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(DECKey.Substring(0, 8));　//建立加密对象的密钥和偏移量，此值重要，不能修改
            des.IV = ASCIIEncoding.ASCII.GetBytes(DECKey.Insert(0, "w").Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //建立StringBuild对象，createDecrypt使用的是流对象，必须把解密后的文本变成流对象
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        #endregion

        #region 获取用户IP地址
        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            string user_IP = string.Empty;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    user_IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    user_IP = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
            }
            else
            {
                user_IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return user_IP;
        }
        #endregion

        #region  根据ip地址判断其详细地址
        /// <summary>
        /// 读取yodao 接口IP地址
        /// </summary>
        public static string GetstringIpAddress()//strIP为IP
        {
            string strIP = GetIPAddress();
            string sURL = "http://www.youdao.com/smartresult-xml/search.s?type=ip&q=" + strIP + "";//youdao的URL
            string stringIpAddress = "";
            try
            {
                using (XmlReader read = XmlReader.Create(sURL))//获取youdao返回的xml格式文件内容
                {
                    while (read.Read())
                    {
                        switch (read.NodeType)
                        {
                            case XmlNodeType.Text://取xml格式文件当中的文本内容
                                if (string.Format("{0}", read.Value).ToString().Trim() != strIP)//youdao返回的xml格式文件内容一个是IP，另一个是IP地址，如果不是IP那么就是IP地址
                                {
                                    stringIpAddress = string.Format("{0}", read.Value).ToString().Trim();//赋值
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                stringIpAddress = "IP接口出错，未能获取数据";
            }

            return stringIpAddress;
        }
        #endregion  读取yodao 接口IP地址

        #region 利用NPOI组件导入，导出Excel文件

        /// <summary>
        /// 将DataTable转成Xls文件导出
        /// </summary>
        /// <param name="page"></param>
        /// <param name="dt"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static Stream ExportExcelByNPOI(DataTable dt)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();

            //设定文件相关信息
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Data";
            hssfworkbook.DocumentSummaryInformation = dsi;
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Data";
            hssfworkbook.SummaryInformation = si;

            //将DataTable数据写入
            Sheet sheet1 = hssfworkbook.CreateSheet("DataExport_NPOI");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建行
                Row row = sheet1.CreateRow(i);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    //创建单元格
                    row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                }
            }


            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            hssfworkbook.Dispose();
            return file;
        }


        /// <summary>
        /// 将Excel转成DataTable
        /// </summary>
        /// <param name="fileupload">上传控件实例引用</param>
        /// <returns></returns>
        public static DataTable ImportExcelByNPOI(FileStream file)
        {
            DataTable dt = new DataTable();
            using (file)
            {
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(0);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                bool flag = true;
                //将Excel内数据读取到DataTable 中
                while (rows.MoveNext())
                {
                    HSSFRow row = (HSSFRow)rows.Current;
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < row.LastCellNum; i++)
                    {

                        HSSFCell cell = (HSSFCell)row.GetCell(i);
                        if (flag)
                            dt.Columns.Add(i.ToString());

                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }
                    flag = false;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        #endregion

        #region 判断文件夹是否存在 如果不存在，则创建
        /// <summary>
        /// 判断文件夹是否存在 如果不存在，则创建
        /// </summary>
        /// <param name="path">相对路径</param>
        public static string DirIsExists(string path)
        {
            try
            {
                path = HttpContext.Current.Server.MapPath(path);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Directory.CreateDirectory(path);
                return path;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 根据给定键值对构造URL QueryString
        /// <summary>
        /// 根据给定键值对构造URL QueryString
        /// </summary>
        /// <param name="parameters">键值对</param>
        /// <returns></returns>
        public static string BuildQueryString(Dictionary<string, string> parameters)
        {
            List<string> pairs = new List<string>();
            foreach (KeyValuePair<string, string> item in parameters)
            {
                pairs.Add(string.Format("{0}={1}", HttpUtility.UrlEncode(item.Key), HttpUtility.UrlEncode(item.Value)));
            }
            return string.Join("&", pairs.ToArray());
        }
        #endregion

        #region 根据图片文件的RawFormat属性获取图片的文件格式
        public static string GetImageFormat(System.Drawing.Imaging.ImageFormat imgRawFormat)
        {
            string strImgFormat = string.Empty;

            if (imgRawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                strImgFormat = ".jpg";
            else if (imgRawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                strImgFormat = ".gif";
            else if (imgRawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                strImgFormat = ".bmp";
            else if (imgRawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                strImgFormat = ".png";
            else if (imgRawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
                strImgFormat = ".tiff";
            else if (imgRawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
                strImgFormat = ".icon";
            else if (imgRawFormat.Equals(System.Drawing.Imaging.ImageFormat.Wmf))
                strImgFormat = ".wmf";

            return strImgFormat;

        }
        #endregion

        #region 获取虚拟路径
        /// <summary>
        /// 获取虚拟路径
        /// </summary>
        public static string GetVirtualPath(string path)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(path);
            }
            else
            {
                string winformPath = Directory.GetCurrentDirectory();//获取应用程序的当前工作目录
                winformPath = winformPath.Replace("bin\\Debug", "");
                path = winformPath + path;
            }

            return path;
        }

        #endregion

        #region 将图片流保存成图片文件
        public static string GetImageFromStream(HttpWebResponse response, string imgFileSavePath)
        {
            Stream imgStream = response.GetResponseStream();
            if (imgStream != null)
            {
                string imgFormat = Common.GetImageFormat(response.ContentType);
                FileStream imgFile = File.Create(imgFileSavePath + imgFormat);
                int readCounts = 0;//实际读取自己数
                byte[] buffer = new byte[32 * 1024];
                do
                {
                    readCounts = imgStream.Read(buffer, 0, buffer.Length);
                    imgFile.Write(buffer, 0, readCounts);
                }
                while (readCounts > 0);
                System.Drawing.Image img = System.Drawing.Image.FromStream(imgFile);
                imgFile.Close();
                imgStream.Close();
                response.Close();
                return imgFileSavePath + imgFormat;
            }
            return string.Empty;
        }


        /// <summary>
        /// 根据WebResponse.ContentType 是否为图片文件
        /// </summary>
        /// <returns></returns>
        private static bool ValidateImage(string contentType)
        {
            if (contentType.IsNullOrEmpty())
                return false;

            if (contentType.IndexOf("image/") == 0)
                return true;
            return false;
        }


        /// <summary>
        /// 根据ContentType 获取图片格式
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private static string GetImageFormat(string contentType)
        {
            string format = string.Empty;
            if (!Common.ValidateImage(contentType))
                return format;

            format = "." + contentType.Substring(contentType.IndexOf("/") + 1).ToLower();

            if (format == "jpeg")
                format = ".jpg";

            return format;
        }
        #endregion

    }
}
