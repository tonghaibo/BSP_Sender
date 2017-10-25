using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace BSP_Sender.Util
{
    public class MyTraceListener : TraceListener
    {
        public string FilePath { get; private set; }

        public MyTraceListener(string filepath)
        {
            FilePath = filepath;
        }

        public override void Write(string message)
        {
            File.AppendAllText(FilePath, message);
        }

        public override void WriteLine(string message)
        {
            File.AppendAllText(FilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss    ") + message + Environment.NewLine);
        }

        public override void Write(object o, string category)
        {
            string msg = "";

            if (string.IsNullOrWhiteSpace(category) == false) //category参数不为空
            {
                msg = category + " : ";
            }

            if (o is Exception) //如果参数o是异常类,输出异常消息+堆栈,否则输出o.ToString()
            {
                var ex = (Exception)o;
                msg += ex.Message + Environment.NewLine;
                msg += ex.StackTrace;
            }
            else if (o != null)
            {
                msg = o.ToString();
            }

            WriteLine(msg);
        }
    }
}
