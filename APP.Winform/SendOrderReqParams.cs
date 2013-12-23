using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APP.Winform
{
   public class SendOrderReqParams
    {
       public List<string> _keys
       {
           get;
           set;
       }

       public List<string> _values
       {
           get;
           set;
       }


       public SendOrderReqParams(List<string> keys,List<string> values)
       {
           _keys = keys;
           _values = values;
       }
        
       public void Add(string key,string value)
       {
           _keys.Add(key);
           _values.Add(value);

       }
    }
}
