using OpenBullet;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;

namespace OpenBullet.Views.Main.Tools
{
	public partial class ComboSuite : Page
	{
		public static string FileName;

		public ComboSuite()
		{
			this.InitializeComponent();
			ComboSuite.FileName = "";
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (ComboSuite.FileName == "")
			{
				System.Windows.MessageBox.Show("Please Choose a file first!");
				return;
			}
			int num = 0;
			StreamReader streamReader = new StreamReader(File.OpenRead(ComboSuite.FileName));
			StreamWriter streamWriter = new StreamWriter(File.OpenWrite(string.Concat(OB.Blank, "DeDuped.txt")));
			HashSet<int> nums = new HashSet<int>();
			while (!streamReader.EndOfStream)
			{
				string str = streamReader.ReadLine();
				int hashCode = str.GetHashCode();
				if (nums.Contains(hashCode))
				{
					continue;
				}
				num++;
				nums.Add(hashCode);
				streamWriter.WriteLine(str);
			}
			streamWriter.Flush();
			streamWriter.Close();
			streamReader.Close();
			int length = (int)File.ReadAllLines(ComboSuite.FileName).Length;
			length -= num;
			this.DupesRemoved.Text = string.Concat("Duplicates Removed: ", length.ToString());
			try
			{
				System.Windows.MessageBox.Show("Saved File DeDuped.txt to OpenBullet Root Folder!", "OpenBullet Duplicate Remover");
			}
			catch
			{
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if (ComboSuite.FileName == "")
			{
				System.Windows.MessageBox.Show("Please Choose a file first!");
				return;
			}
			if (this.SplitCount.Text == "0")
			{
				System.Windows.MessageBox.Show("Please use something higher than 0", "Just saved your pc");
				return;
			}
			StreamReader streamReader = File.OpenText(ComboSuite.FileName);
			string str = string.Concat(ComboSuite.FileName, "{0}.txt");
			int num = 1;
			int num1 = Convert.ToInt16(this.SplitCount.Text.Trim());
			while (!streamReader.EndOfStream)
			{
				int num2 = num;
				num = num2 + 1;
				StreamWriter streamWriter = File.CreateText(string.Format(str, num2));
				for (int i = 0; i < num1; i++)
				{
					streamWriter.WriteLine(streamReader.ReadLine());
					if (streamReader.EndOfStream)
					{
						break;
					}
				}
				streamWriter.Close();
			}
			streamReader.Close();
			System.Windows.MessageBox.Show("Save Files Next to original Folder", "OpenBullet Splitter");
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			if (ComboSuite.FileName == "")
			{
				System.Windows.MessageBox.Show("Please Select a File First!");
				return;
			}
			StreamReader streamReader = new StreamReader(ComboSuite.FileName);
			string end = streamReader.ReadToEnd();
			streamReader.Close();
			end = Regex.Replace(end, this.OriginalSep.Text.Trim(), this.NewSep.Text.Trim());
			StreamWriter streamWriter = new StreamWriter(string.Concat(OB.Blank, ComboSuite.FileName, "SepChange.txt"));
			streamWriter.Write(end);
			streamWriter.Close();
			try
			{
				System.Windows.MessageBox.Show("Saved File by Original File!", "OpenBullet Sep Changer");
			}
			catch
			{
			}
		}

		private void LoadFromFileButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				ComboSuite.FileName = openFileDialog.FileName;
				this.PathName.Text = ComboSuite.FileName;
				int length = (int)File.ReadAllLines(ComboSuite.FileName).Length;
			}
		}

		private void LoadFromManagerButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
		}

		private void Merge_Lists_Click(object sender, RoutedEventArgs e)
		{
		}
	}
}
