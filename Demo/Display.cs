using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Demo
{
    public class Display
    {
        public static void OpenPicture(string path)
        {
            Process.Start(@"C:\Users\jaison.jacob\AppData\Local\Programs\GIMP 2\bin\gimp-2.10.exe", path);
        }
    }
}
