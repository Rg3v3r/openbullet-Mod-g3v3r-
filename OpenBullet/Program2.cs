using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Threading;
using System.Windows.Forms;
using OpenBullet.IO;
using OpenBullet.Loaders;

namespace OpenBullet
{
    public class Program2
    {
        
        [STAThread]
        public static void main()
        {
          
            Program2.Start();
          
        }

      
        public static void Start()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Input|*.loli";
            openFileDialog.Title = "Load Config";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Reader.LoadConfig(openFileDialog.FileName, Program2.Instance);
            }
            Program2.Print();
            CreateDirectories.BuildAll(Program2.Instance.ConfigName);
            Engine.Start.Initalize(Program2.Instance);
            Program2.BuildProcess(Program2.Instance.ConfigName);
        }

        
        public static void Print()
        {
            string text = "Nombre: " + Program2.Instance.ConfigName;
            string text2 = "Ultima modificacion: " + Program2.Instance.LastModified;
            string text3 = "Athor: " + Program2.Instance.Author;
            string text4 = "Version: " + Program2.Instance.Version;
            string text5 = "Usa Proxies: " + Program2.Instance.NeedsProxies.ToString();
            string text6 = "MaxProxyUses: " + Program2.Instance.MaxProxyUses;
            MessageBox.Show(text + text3);
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine(Program.CustomDisplay("## [INFORMACION] ##\n", 2));
            //         Console.ForegroundColor = ConsoleColor.Green;
            //         Console.ResetColor();
            //Console.WriteLine(Program.CustomDisplay(text, 2));
            //Console.WriteLine(Program.CustomDisplay(text2, 2));
            //Console.WriteLine(Program.CustomDisplay(text3, 2));
            //Console.WriteLine(Program.CustomDisplay(text4, 2));
            //Console.WriteLine(Program.CustomDisplay(text5, 2));
            //Console.WriteLine(Program.CustomDisplay(text6, 2));
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine(" ");
            ////Console.WriteLine(Program.CustomDisplay("## [Request Information] ##", 2));
            //Console.ResetColor();
            //foreach (KeyValuePair<string, int> keyValuePair in Program.Instance.Requests)
            //{
            //	if (keyValuePair.Key.Contains("[GET]"))
            //	{
            //		Console.WriteLine("");
            //	}
            //	else if (keyValuePair.Key.Contains("[POST]"))
            //	{
            //		Console.WriteLine("");
            //	}
            //	Console.WriteLine("[{1}] - {0}", keyValuePair.Key, keyValuePair.Value);
            //}
        }

        // Token: 0x06000004 RID: 4 RVA: 0x0000229C File Offset: 0x0000049C
        public static void BuildProcess(string configName)
        {
            Thread.Sleep(1500);
            Builder.Build(Program2.Instance.ConfigName);
            if (Program2.ExistsOnPath(string.Concat(new string[]
            {
                "Server\\Crackers\\",
                configName,
                "\\Build\\Cracker\\",
                configName,
                ".exe"
            })))
            {
                MessageBox.Show("COMPILACION COMPLETADA");
                //Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine(" ");
                //Console.WriteLine(Program.CustomDisplay("# [ COMPILACION COMPLETADA  ] #\n\n", 0));
                //Console.WriteLine(Program.CustomDisplay("# EL CHECKER LO ENCONTRARAS#", 0));
                //Console.WriteLine(Program.CustomDisplay("#EN LA CARPETA CRACKERS#", 0));
                Directory.Delete("Server\\Crackers\\" + configName + "\\Classes\\", true);
                //Console.ResetColor();
                return;
            }
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine("\n --- [COMPILACION FALLIDA]");
            //Console.ResetColor();
            MessageBox.Show("COMPILACION FALLIDA");
        }

        // Token: 0x06000005 RID: 5 RVA: 0x00002367 File Offset: 0x00000567
        public static bool ExistsOnPath(string fileName)
        {
            return Program2.GetFullPath(fileName);
        }

        // Token: 0x06000006 RID: 6 RVA: 0x0000236F File Offset: 0x0000056F
        public static bool GetFullPath(string fileName)
        {
            return File.Exists(fileName);
        }

        // Token: 0x06000007 RID: 7 RVA: 0x0000237C File Offset: 0x0000057C
        public static string CustomDisplay(string Text, int WidthSpacer)
        {
            //int num = Console.WindowWidth - WidthSpacer;
            int num = WidthSpacer;
            return string.Format(string.Concat(new object[]
            {
                "{0,",
                num / 2 + Text.Length / 2,
                "}{1,",
                num - num / 2 - Text.Length / 2 + 1,
                "}"
            }), Text, "");
        }

        // Token: 0x04000001 RID: 1
        public static Config Instance = new Config();
    }
}
