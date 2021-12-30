using Extreme.Net;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using OpenBullet.Views.Main;
namespace OpenBullet.Views.Dialogs
{
    public partial class DialogAddProxies : Page
    {
        public static string PSHTTP;

        public static string PSSOCKS4;

        public static string PSSOCKS5;

        public object Caller
        {
            get;
            set;
        }

        static DialogAddProxies()
        {
            DialogAddProxies.PSHTTP = "0 minutes ago";
            DialogAddProxies.PSSOCKS4 = "0 minutes ago";
            DialogAddProxies.PSSOCKS5 = "0 minutes ago";
        }

        public DialogAddProxies(object caller)
        {
            this.InitializeComponent();
            this.Caller = caller;
            string[] names = Enum.GetNames(typeof(ProxyType));
            for (int i = 0; i < (int)names.Length; i++)
            {
                string str = names[i];
                if (str != "Chain")
                {
                    this.proxyTypeCombobox.Items.Add(str);
                }
            }
            this.proxyTypeCombobox.SelectedIndex = 0;
            this.CheckLaestPSupdates();
            this.HTTPupdate.Content = DialogAddProxies.PSHTTP;
            this.SOCKS4update.Content = DialogAddProxies.PSSOCKS4;
            this.SOCKS5update.Content = DialogAddProxies.PSSOCKS4;
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Caller.GetType() == typeof(ProxyManager))
            {
                ((ProxyManager)this.Caller).AddProxies(locationTextbox.Text, (ProxyType)Enum.Parse(typeof(ProxyType), this.proxyTypeCombobox.Text), this.proxiesBox.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList<string>());
            }
            ((MainDialog)base.Parent).Close();
        }

        private void CheckLaestPSupdates()
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.proxyscrape.com/?request=lastupdated&proxytype=http");
                httpWebRequest.Proxy = null;
                httpWebRequest.Timeout = 5000;
                httpWebRequest.ReadWriteTimeout = 5000;
                using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            DialogAddProxies.PSHTTP = streamReader.ReadToEnd();
                        }
                    }
                }
                HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("https://api.proxyscrape.com/?request=lastupdated&proxytype=socks4");
                httpWebRequest1.Proxy = null;
                httpWebRequest1.Timeout = 5000;
                httpWebRequest1.ReadWriteTimeout = 5000;
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest1.GetResponse())
                {
                    using (Stream stream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader streamReader1 = new StreamReader(stream))
                        {
                            DialogAddProxies.PSSOCKS4 = streamReader1.ReadToEnd();
                        }
                    }
                }
                HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create("https://api.proxyscrape.com/?request=lastupdated&proxytype=socks5");
                httpWebRequest2.Proxy = null;
                httpWebRequest2.Timeout = 5000;
                httpWebRequest2.ReadWriteTimeout = 5000;
                using (HttpWebResponse response1 = (HttpWebResponse)httpWebRequest2.GetResponse())
                {
                    using (Stream responseStream1 = response1.GetResponseStream())
                    {
                        using (StreamReader streamReader2 = new StreamReader(responseStream1))
                        {
                            DialogAddProxies.PSSOCKS5 = streamReader2.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
                DialogAddProxies.PSHTTP = "Error Connecting";
                DialogAddProxies.PSSOCKS4 = "Error Connecting";
                DialogAddProxies.PSSOCKS5 = "Error Connecting";
            }
        }

        private void HTTPButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string end = "0";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.proxyscrape.com/?request=getproxies&proxytype=http&timeout=10000&country=all&ssl=all&anonymity=all");
                httpWebRequest.Timeout = 5000;
                httpWebRequest.ReadWriteTimeout = 5000;
                using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            end = streamReader.ReadToEnd();
                        }
                    }
                }
                this.proxiesBox.Text = end;
                this.proxyTypeCombobox.SelectedIndex = 0;
            }
            catch
            {
                OB.Logger.LogError(Components.ConfigManager, "Could not scrape proxies from the host", false);
            }
        }

        private void loadProxiesButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Proxy files | *.txt",
                FilterIndex = 1
            };
            openFileDialog.ShowDialog();
            this.locationTextbox.Text = openFileDialog.FileName;
        }

        private void ScrapeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.APIUrl.Text))
                {
                    string end = "0";
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.APIUrl.Text);
                    httpWebRequest.Proxy = null;
                    using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (StreamReader streamReader = new StreamReader(responseStream))
                            {
                                end = streamReader.ReadToEnd();
                            }
                        }
                    }
                    this.proxiesBox.Text = end;
                }
            }
            catch
            {
                OB.Logger.LogError(Components.ConfigManager, "Could not scrape proxies from the host", false);
            }
        }

        private void SOCKS4Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string end = "0";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.proxyscrape.com/?request=getproxies&proxytype=socks4&timeout=10000&country=all");
                httpWebRequest.Proxy = null;
                httpWebRequest.Timeout = 5000;
                httpWebRequest.ReadWriteTimeout = 5000;
                using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            end = streamReader.ReadToEnd();
                        }
                    }
                }
                this.proxiesBox.Text = end;
                this.proxyTypeCombobox.SelectedIndex = 1;
            }
            catch
            {
                OB.Logger.LogError(Components.ConfigManager, "Could not scrape proxies from the host", false);
            }
        }

        private void SOCKS5Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string end = "0";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.proxyscrape.com/?request=getproxies&proxytype=socks5&timeout=10000&country=all");
                httpWebRequest.Proxy = null;
                httpWebRequest.Timeout = 5000;
                httpWebRequest.ReadWriteTimeout = 5000;
                using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            end = streamReader.ReadToEnd();
                        }
                    }
                }
                this.proxiesBox.Text = end;
                this.proxyTypeCombobox.SelectedIndex = 3;
            }
            catch
            {
                OB.Logger.LogError(Components.ConfigManager, "Could not scrape proxies from the host", false);
            }
        }
    }
}
