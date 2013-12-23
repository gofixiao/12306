using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;
using System.Data;
using System.IO;

namespace RockUtility
{
    public static class OtherExtension
    {
        
        #region 检查DataSet是否为空
        public static bool Check(this DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion


        #region 对指定流进行拷贝
        public static Stream CopyStream(this Stream input)
        {
            byte[] buffer = new byte[32 * 1024];
            MemoryStream ms = new MemoryStream();
            int readCounts = 0;//实际读到字节数k
            do
            {
                readCounts = input.Read(buffer, 0, buffer.Length);
                ms.Write(buffer, 0, readCounts);
            }
            while (readCounts > 0);
            return ms;
        }
        #endregion


    }
}
