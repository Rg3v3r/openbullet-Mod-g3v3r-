using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;

namespace OpenBullet
{
    [Obfuscation(Exclude = false, Feature = "+koi;-ctrl flow")]
    public partial class SplashWindow : Window
    {
        public string CurrentVersion = "1.4.1".Trim();

        public static string ChangelogURL;

        private bool KEYCHECK;

        private readonly WebClient ChangelogGet = new WebClient();



        public SplashWindow()
        {
            this.InitializeComponent();

        }

        private void agreeButton_Click(object sender, RoutedEventArgs e)
        {
            base.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://t.me/Rg3v3r");
        }

        private void HandleInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void quitImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.ChangeLogBox.Text = this.ChangelogGet.DownloadString(SplashWindow.ChangelogURL);
            }
            catch (Exception exception)
            {

            }
        }


        private void Update(object sender, RoutedEventArgs e)
        {
            Process.Start("https://t.me/Rg3v3r");
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    base.DragMove();
                }
            }
            catch
            {
            }
        }
    }
}
