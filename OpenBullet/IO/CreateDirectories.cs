using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OpenBullet.IO
{
    public class CreateDirectories
    {
        // Token: 0x0600001F RID: 31 RVA: 0x000031C1 File Offset: 0x000013C1
        public static void BuildAll(string CrackerName)
        {
            CreateDirectories.CreateFolders(CrackerName);
            CreateDirectories.CreateFolders(CrackerName);
            CreateDirectories.CreateFiles(CrackerName);
            CreateDirectories.CreateBatchBuilder(CrackerName);
        }

        // Token: 0x06000020 RID: 32 RVA: 0x000031DC File Offset: 0x000013DC
        public static void CreateFolders(string CrackerName)
        {
            Directory.CreateDirectory("Server\\Crackers\\" + CrackerName);
            Directory.CreateDirectory("Server\\Crackers\\" + CrackerName + "\\Build");
            Directory.CreateDirectory("Server\\Crackers\\" + CrackerName + "\\Build\\Cracker\\");
            Directory.CreateDirectory("Server\\Crackers\\" + CrackerName + "\\Build\\Cracker\\Assets");
            Directory.CreateDirectory("Server\\Crackers\\" + CrackerName + "\\Build\\Cracker\\Results");
            Directory.CreateDirectory("Server\\Crackers\\" + CrackerName + "\\Classes");
        }

        // Token: 0x06000021 RID: 33 RVA: 0x00003268 File Offset: 0x00001468
        public static void CreateFiles(string CrackerName)
        {
            object locker = CreateDirectories.LOCKER;
            lock (locker)
            {
                using (FileStream fileStream = new FileStream("Server\\Crackers\\" + CrackerName + "\\Build\\Cracker\\Assets\\ComboList.txt", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine("");
                        fileStream.Flush();
                    }
                }
            }
            locker = CreateDirectories.LOCKER;
            lock (locker)
            {
                using (FileStream fileStream2 = new FileStream("Server\\Crackers\\" + CrackerName + "\\Build\\Cracker\\Assets\\ProxyList.txt", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter streamWriter2 = new StreamWriter(fileStream2))
                    {
                        streamWriter2.WriteLine("");
                        fileStream2.Flush();
                    }
                }
            }
        }

        // Token: 0x06000022 RID: 34 RVA: 0x0000338C File Offset: 0x0000158C
        public static void CreateBatchBuilder(string CrackerName)
        {
            object locker = CreateDirectories.LOCKER;
            lock (locker)
            {
                using (FileStream fileStream = new FileStream("Server\\Crackers\\" + CrackerName + "\\Build\\Build.bat", FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine("@ECHO OFF");
                        streamWriter.WriteLine(string.Concat(new string[]
                        {
                            "C:\\WINDOWS\\Microsoft.NET\\Framework64\\v4.0.30319\\csc.exe -out:Server\\Crackers\\",
                            CrackerName,
                            "\\Build\\Cracker\\",
                            CrackerName,
                            ".exe Server\\Crackers\\",
                            CrackerName,
                            "\\Classes\\*.cs -reference:Server\\Resources\\DLLs\\RestSharp.dll"
                        }));
                        fileStream.Flush();
                    }
                }
            }
        }

        // Token: 0x04000011 RID: 17
        public static object LOCKER = new object();
    }
}
