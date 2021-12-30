using OpenBullet.Views.Main.Tools;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace OpenBullet.Views.Main
{
    public partial class ToolsSection : Page
    {
        private ListGenerator ListGenerator;

        private SeleniumTools SeleniumTools;

        private Database Database;

        private ComboSuite ComboSuiteTools;

        private LolixDecrypt LolixTools;

        private TessDataDownloads TessDataTools;

        public ToolsSection()
        {
            this.InitializeComponent();
            this.ListGenerator = new ListGenerator();
            this.SeleniumTools = new SeleniumTools();
            this.ComboSuiteTools = new ComboSuite();
            this.LolixTools = new LolixDecrypt();
            this.TessDataTools = new TessDataDownloads();
            this.Database = new Database();
            this.menuOptionListGenerator_MouseDown(this, null);
        }

        private void menuOptionComboSuite_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Main.Content = this.ComboSuiteTools;
            this.menuOptionSelected(this.menuOptionComboSuite);
        }

        private void MenuOptionDatabase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Main.Content = this.Database;
            this.menuOptionSelected(this.menuOptionDatabase);
        }

        private void menuOptionListGenerator_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Main.Content = this.ListGenerator;
            this.menuOptionSelected(this.menuOptionListGenerator);
        }

        private void menuOptionLolixDecrypt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Main.Content = this.LolixTools;
            this.menuOptionSelected(this.menuOptionLolixDecrypt);
        }

        private void menuOptionSelected(object sender)
        {
            foreach (object child in this.topMenu.Children)
            {
                try
                {
                    ((Label)child).Foreground = Utils.GetBrush("ForegroundMain");
                }
                catch
                {
                }
            }
            ((Label)sender).Foreground = Utils.GetBrush("ForegroundCustom");
        }

        private void menuOptionSeleniumUtilities_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Main.Content = this.SeleniumTools;
            this.menuOptionSelected(this.menuOptionSeleniumTools);
        }

        private void menuOptionTessData_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Main.Content = this.TessDataTools;
            this.menuOptionSelected(this.menuOptionTessDataDownloads);
        }
    }
}
