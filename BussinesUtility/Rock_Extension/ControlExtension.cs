using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;

namespace RockUtility
{
    public static class ControlExtension
    {
        #region Repeater控件扩展
        /// <summary>
        /// 绑定Repeater，绑定前检查数据源是否为空
        /// </summary>
        /// <param name="rp">repeater对象</param>
        /// <param name="data">数据源</param>
        /// <param name="tip">当数据源为空时要显示的友好提示语</param>
        public static void BindWithCheck(this System.Web.UI.WebControls.Repeater rp, object data, string tip)
        {
            BindWithCheck(rp, data, true, tip);
        }

        /// <summary>
        /// 绑定Repeater，绑定前检查数据源是否为空
        /// 当数据源为空时，默认显示“暂无数据！”的友好提示
        /// </summary>
        /// <param name="rp">repeater对象</param>
        /// <param name="data">数据源</param>
        public static void BindWithCheck(this System.Web.UI.WebControls.Repeater rp, object data)
        {
            BindWithCheck(rp, data, true, null);
        }

        /// <summary>
        /// 绑定Repeater，绑定前检查数据源是否为空
        /// 当数据源为空时，无任何提示语显示
        /// </summary>
        /// <param name="rp">repeater对象</param>
        /// <param name="data">数据源</param>
        public static void BindWithCheckNoTip(this System.Web.UI.WebControls.Repeater rp, object data)
        {
            BindWithCheck(rp, data, false, null);
        }

        /// <summary>
        /// 绑定Repeater，绑定前检查数据源是否为空
        /// </summary>
        /// <param name="rp">repeater对象</param>
        /// <param name="data">数据源</param>
        /// <param name="data">数据源</param>
        /// <param name="tip">当数据源为空时要显示友的好提示语</param>
        private static void BindWithCheck(this System.Web.UI.WebControls.Repeater rp, object data, bool NoDataTip, string tip)
        {
            if ((data is DataSet && ((DataSet)data).Tables.Count <= 0) || data == null)
            {
                if (NoDataTip)
                {
                    Literal lit = new Literal();
                    tip = string.IsNullOrEmpty(tip) ? "暂无数据！" : tip;
                    lit.Text = "<p  style=\"color:red;text-align:center;font-size:14px;\">" + tip + "</p>";
                    rp.Controls.Add(lit);
                }
            }
            else
            {
                rp.DataSource = data;
                rp.DataBind();
            }
        }
        #endregion

        #region  FileUpload控件扩展

        /// <summary>
        /// 上传文件 
        /// 文件格式只能为 .doc|.docx|.rar|.pdf|.xls|.ppt
        /// 文件最大10M
        /// </summary>
        /// <param name="path">文件存放路径</param>
        /// <param name="source">FileUpload控件引用</param>
        /// <returns>返回值 返回文件名 正常 1 文件过大 2 格式不正确 正确格式.doc|.docx|.rar|.pdf|.xls|.ppt</returns>
        public static string UploadFile(this FileUpload source, string path)
        {
            var hpf = source.PostedFile;
            //文件最大为10M，单位为字节
            int maxFile = 1024 * 10000;
            string[] formatArr = ".doc|.docx|.rar|.zip|.pdf|.xls|.ppt".Split('|');
            //判断是否已上传文件
            if (hpf.ContentLength <= 0)
            {
                return string.Empty;
            }

            int FileLength = hpf.ContentLength;
            if (FileLength > maxFile)
            {
                return "1";//文件过大
            }
            string fileName = hpf.FileName.ToLower();

            if (!CheckIndexOfArray(fileName, formatArr))
            {
                return "2";//格式不正确 要输入正确的格式
            }
            string SaveName = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 100000).ToString() + Path.GetExtension(fileName);

            string savepath = path.LastIndexOf("/") == path.Length ? path + SaveName : path + "/" + SaveName;
            hpf.SaveAs(HttpContext.Current.Server.MapPath(savepath));
            return savepath;
        }

        /// <summary>
        /// 上传文件 
        /// 文件格式只能为 .doc|.docx|.rar|.pdf|.xls|.ppt
        /// 文件最大10M
        /// 文件存放路径为网站根目录/File/file
        /// </summary>
        /// <param name="source">FileUpload控件引用</param>
        /// <returns>返回值 返回文件名 正常 1 文件过大 2 格式不正确 正确格式.doc|.docx|.rar|.pdf|.xls|.ppt</returns>
        public static string UploadFile(this FileUpload source)
        {
            return UploadFile(source, "/File/file");
        }

        /// <summary>
        /// 上传图片
        ///图片文件格式为.jpg .jpeg .gif .png .bmp
        ///图片文件最大为5M
        /// </summary>
        /// <param name="path">图片存放路径</param>
        /// <param name="source">FileUpload控件引用</param>
        /// <returns>返回值 返回文件名 正常 1 文件过大 2 格式不正确 正确格式.jpg .jpeg .gif .png .bmp</returns>
        public static string UploadImg(this FileUpload source, string path)
        {
            var hpf = source.PostedFile;
            //图片文件最大问5M
            int maxFile = 1024 * 5000;
            string[] formatArr = ".jpg|.jpeg|.gif|.png|.bmp".Split('|');
            //判断是否已上传文件
            if (hpf.ContentLength <= 0)
            {
                return string.Empty;
            }

            int FileLength = hpf.ContentLength;
            if (FileLength > maxFile)
            {
                return "1";//文件过大
            }
            string fileName = hpf.FileName.ToLower();

            if (!CheckIndexOfArray(fileName, formatArr))
            {
                return "2";//格式不正确 要输入正确的格式
            }

            string SaveName = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 100000).ToString() + Path.GetExtension(fileName);

            string savepath = path.LastIndexOf("/") == path.Length ? path + SaveName : path + "/" + SaveName;
            hpf.SaveAs(HttpContext.Current.Server.MapPath(savepath));

            return savepath;
        }

        /// <summary>
        /// 上传图片
        ///图片文件格式为.jpg .jpeg .gif .png .bmp
        ///图片文件最大为5M
        /// 文件存放路径为网站根目录/File/img
        /// </summary>
        /// <param name="path">图片存放路径</param>
        /// <param name="source">FileUpload控件引用</param>
        /// <returns>返回值 返回文件名 正常 1 文件过大 2 格式不正确 正确格式.jpg .jpeg .gif .png .bmp</returns>
        public static string UploadImg(this FileUpload source)
        {
            return UploadFile(source, "/File/img");
        }

        /// <summary>
        /// 判断字符串数组在Str中是否存在
        /// </summary>
        /// <param name="str">要判断的字符串</param>
        /// <param name="strarray">要判断的数组</param>
        /// <returns>不存在返回true 存在返回false</returns>
        public static bool CheckIndexOfArray(string str, string[] strarray)
        {
            bool b = false;
            foreach (string s in strarray)
            {
                if (str.IndexOf(s) > 0)
                {
                    b = true;
                    break;
                }
            }

            return b;

        }

        #endregion

    }
}
