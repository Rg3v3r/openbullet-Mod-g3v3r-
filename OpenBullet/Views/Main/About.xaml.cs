using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace OpenBullet.Views.Main
{
    public partial class About
    {
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        public About()
        {
            InitializeComponent();
        }

        private void repoButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start("https://github.com/openbullet/openbullet");
        }

        private void docuButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start("https://openbullet.github.io/");
        }

        

     
    }
}
