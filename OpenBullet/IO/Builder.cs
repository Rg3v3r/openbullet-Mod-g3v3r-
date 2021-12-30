using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.IO;

namespace OpenBullet.IO
{
    public class Builder
    {
        // Token: 0x0600001D RID: 29 RVA: 0x00003144 File Offset: 0x00001344
        public static void Build(string CrackerName)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "Server\\Crackers\\" + CrackerName + "\\Build\\Build.bat",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            process.Start();
            File.Copy("Server\\Resources\\DLLs\\RestSharp.dll", "Server\\Crackers\\" + CrackerName + "\\Build\\Cracker\\RestSharp.dll", true);
            process.WaitForExit(1000);
        }
    }
}
