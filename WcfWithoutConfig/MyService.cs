using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WcfWithoutConfig
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService" in both code and config file together.
    public class MyService : IMyService
    {
        public string ConvertString(string str)
        {
            return str.ToUpper();
        }

        public int DoLongWork10Second(int d, int e)
        {
            Thread.Sleep(10000);
            return d + e;
        }
    }
}
