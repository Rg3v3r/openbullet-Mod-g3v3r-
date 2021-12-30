using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;

namespace OpenBullet.Views.Main.Tools
{
	public partial class TessDataDownloads : Page
	{
		private WebClient loadSite = new WebClient();

		public string url = "https://github.com/tesseract-ocr/tessdata/tree/3.04.00";

		public string siteResponse;

		public string language;

		private Regex lang = new Regex("title=\"(.*).traineddata\" id=\"");

		public TessDataDownloads()
		{
			this.InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.DownloadList.Items.Add(this.LanguageList.SelectedItem);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			this.DownloadList.Items.Remove(this.DownloadList.SelectedItem);
		}

		private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (System.Windows.Forms.MessageBox.Show("This button will add ALL languages to the download list", "ALERT", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				foreach (string item in (IEnumerable)this.LanguageList.Items)
				{
					this.DownloadList.Items.Add(item);
				}
			}
		}

		private void Button_MouseRightButtonDown_1(object sender, MouseButtonEventArgs e)
		{
			if (System.Windows.Forms.MessageBox.Show("This button will remove ALL languages to the download list", "ALERT", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				this.DownloadList.Items.Clear();
			}
		}

		private void DownloadBtn_Click(object sender, RoutedEventArgs e)
		{
			this.LogsText.Clear();
			int num = 0;
			System.Windows.Controls.TextBox logsText = this.LogsText;
			logsText.Text = string.Concat(logsText.Text, "Downloading tessdata files...", Environment.NewLine);
			foreach (string item in (IEnumerable)this.DownloadList.Items)
			{
				num++;
				System.Windows.Controls.TextBox textBox = this.LogsText;
				textBox.Text = string.Concat(textBox.Text, string.Format("{0}/{1} | Downloading: {2}..", num, this.DownloadList.Items.Count, item));
				System.Windows.Forms.Application.DoEvents();
				try
				{
					this.DownloadLanguage(num, item.ToString());
				}
				catch
				{
					System.Windows.Forms.MessageBox.Show("Please create a folder named 'tessdata' in your Openbullet directory", "FOLDER MISSING", MessageBoxButtons.OK);
				}
			}
			System.Windows.Controls.TextBox logsText1 = this.LogsText;
			logsText1.Text = string.Concat(logsText1.Text, "Your chosen languages have been downloaded");
		}

		public void DownloadLanguage(int i, string language)
		{
			language = string.Concat(language, ".traineddata");
			this.loadSite.DownloadFile(string.Concat("https://github.com/tesseract-ocr/tessdata/raw/3.04.00/", language), string.Concat(AppDomain.CurrentDomain.BaseDirectory, "/tessdata/", language));
			System.Windows.Controls.TextBox logsText = this.LogsText;
			logsText.Text = string.Concat(logsText.Text, "\t\t\t\t| Finished!", Environment.NewLine);
			System.Windows.Forms.Application.DoEvents();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			this.siteResponse = this.loadSite.DownloadString(this.url);
			foreach (Match match in this.lang.Matches(this.siteResponse))
			{
				this.LanguageList.Items.Add(match.Groups[1].Value);
			}
		}
	}
}
