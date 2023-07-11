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
            if (System.Environment.OSVersion.Platform == PlatformID.Unix){
                var p = new Process();
                var psi = new ProcessStartInfo(){
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName =$"/bin/bash",
                    WorkingDirectory= "/mnt",
                    Arguments = $"-c \"gimp {path}\"" ,
                      RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    UseShellExecute=false
                };
                p.StartInfo = psi;
                p.Start();
            }else{
                Process.Start(@"C:\Users\jaison.jacob\AppData\Local\Programs\GIMP 2\bin\gimp-2.10.exe", path);
            }
        }
    }
}
