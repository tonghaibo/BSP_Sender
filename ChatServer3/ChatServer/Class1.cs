using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatServer
{
    class Class1
    {
        static void Main(string args)
        {
            string hexString = "7E0100002D9161227001100001002C012C373035303348543631314130303030303030303030303030300207000001010001D4C1423132333435A77E";
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            Console.WriteLine( returnBytes);
        }
        

    }
}
