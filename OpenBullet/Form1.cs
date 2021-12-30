using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace OpenBullet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<ChannelData> Channels = new List<ChannelData>();



        public void ProcessLine(string line1, string line2)
        {
            string processingLine = line1;
            ChannelData data = new ChannelData();

            // ID
            string[] separators = { "tvg-", "group-" };
            string[] subLines = processingLine.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            data.ID = subLines[1].Substring(subLines[1].IndexOf("\"")).Replace("\"", "").Trim();
            data.Name = subLines[2].Substring(subLines[2].IndexOf("\"")).Replace("\"", "").Trim();
            data.LogoURL = subLines[3].Substring(subLines[3].IndexOf("\"")).Replace("\"", "").Trim();

            string[] groupSplitted = subLines[4].Substring(subLines[4].IndexOf("\"")).Trim().Split(',');
            data.GroupTitle = groupSplitted[0].Replace("\"", "");
            data.VisibleName = groupSplitted[1];
            data.StreamUrl = line2;
            Channels.Add(data);
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void botonurl_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri(this.url.Text);
            WebClient webClient = new WebClient();
            WebClient myWebClient = webClient;
            string nombreFichero = "ficheroUrl.m3u";
            myWebClient.DownloadFile(uri, nombreFichero);
            string curFile = nombreFichero;

            Channels.Clear();
            string[] lines = System.IO.File.ReadAllLines(curFile);

            for (int i = 0; i < lines.Count() - 1; i++)
            {
                // Detect Start, ignore first line
                if (lines[i].StartsWith("#EXTM3U"))
                {
                    // Do nothing
                }
                if (lines[i].StartsWith("#EXTINF:-1"))
                {
                    ProcessLine(lines[i], lines[i + 1]);
                    i = i + 1;
                }
            }


            // Fill groups
            CBGroups.Items.AddRange((from chn in Channels select chn.GroupTitle).Distinct<String>().ToArray<Object>());
        }

        private void botonm3u_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.Filter = "M3U Playlist (*.m3u)|*.m3u|All files (*.*)|*.*";
            ofd.ShowDialog();
            string curFile = ofd.FileName;

            Channels.Clear();
            string[] lines = System.IO.File.ReadAllLines(curFile);

            for (int i = 0; i < lines.Count() - 1; i++)
            {
                // Detect Start, ignore first line
                if (lines[i].StartsWith("#EXTM3U"))
                {
                    // Do nothing
                }
                if (lines[i].StartsWith("#EXTINF:-1"))
                {
                    ProcessLine(lines[i], lines[i + 1]);
                    i = i + 1;
                }
            }

            // Fill groups
            CBGroups.Items.AddRange((from chn in Channels select chn.GroupTitle).Distinct<String>().ToArray<Object>());
        }
        public bool IsProcessOpen(string name, ref Process proc)
        {
            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                //now we're going to see if any of the running processes
                //match the currently running processes. Be sure to not
                //add the .exe to the name you provide, i.e: NOTEPAD,
                //not NOTEPAD.EXE or false is always returned even if
                //notepad is running.
                //Remember, if you have the process running more than once, 
                //say IE open 4 times the loop thr way it is now will close all 4,
                //if you want it to just close the first one it finds
                //then add a return; after the Kill
                if (clsProcess.ProcessName.Contains(name))
                {
                    //if the process is found to be running then we
                    //return a true
                    proc = clsProcess;
                    return true;
                }
            }
            //otherwise we return a false
            return false;
        }
        private void CBGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbChannels.Items.Clear();
            lbChannels.Items.AddRange((from chn in Channels where chn.GroupTitle == CBGroups.Text select chn).ToArray<Object>());
        }
        private void lbChannels_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChannelData selectedChannel = (ChannelData)lbChannels.SelectedItem;

            Process p = null;
            if (IsProcessOpen("vlc", ref p))
            {
                p.Kill();
            }
            p = new Process();
            p.StartInfo.FileName = @"C:\Program Files\VideoLAN\VLC\vlc.exe";
            p.StartInfo.Arguments = selectedChannel.StreamUrl;
            p.Start();
        }
        public class ChannelData
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string LogoURL { get; set; }
            public string GroupTitle { get; set; }
            public string VisibleName { get; set; }
            public string StreamUrl { get; set; }

            public ChannelData()
            {
            }

            public override string ToString()
            {
                return VisibleName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpClient client;
            client = new HttpClient();
            string server;
            server = TBurl.Text;

            string http4 = "http://";
            try
            {
                //boque try con todas las operaciones

                HttpResponseMessage response4 = client.GetAsync(http4 + server).Result;


                if (response4.StatusCode == HttpStatusCode.OK)
                {
                    string responseStr = response4.Content.ReadAsStringAsync().Result;


                    string ok = $"SERVER OK👍 {Environment.NewLine}";


                    url.AppendText(ok);




                }
                else
                {
                    string nou = $"TU SERVER NO VA 👎 { Environment.NewLine}";


                    url.AppendText(nou);




                }
            }
            catch (Exception ex) //bloque catch para captura de error
            {
                string nou = $"TU SERVER NO VA 👎 { Environment.NewLine}";


                url.AppendText(nou);


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response;

            HttpClient client;
            client = new HttpClient();
            string currenTime = DateTime.Now.ToString("dd/MM/yyyy");
            var url2 = "http://iptvhit.com/freeiptv?";
            response = client.GetAsync(url2 + currenTime).Result;


            string responseStr = response.Content.ReadAsStringAsync().Result;

            Regex rx = new Regex("color:white\">(.*?)/get.php", RegexOptions.Singleline);
            foreach (var match in rx.Matches(responseStr))
            {
                var url = match;

                txtBoxResultado.Text += url + "\n";
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}

