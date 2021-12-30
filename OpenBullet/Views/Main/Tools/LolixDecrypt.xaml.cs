using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;

namespace OpenBullet.Views.Main.Tools
{
	public partial class LolixDecrypt : Page
	{
		private static Random randomY;

		private string save;

		public static string FileName;

		private const int Keysize = 256;

		private const int DerivationIterations = 1000;

		static LolixDecrypt()
		{
			LolixDecrypt.randomY = new Random();
		}

		public LolixDecrypt()
		{
			this.InitializeComponent();
			LolixDecrypt.FileName = "";
		}

		[Obfuscation(Exclude=false, Feature="+koi;-ctrl flow")]
		public static string BellaCiao(string helpme, int op)
		{
			string str;
			if (op == 1)
			{
				string base64String = "";
				string str1 = "ay$a5%&jwrtmnh;lasjdf98787OMGFORLAX";
				string str2 = "abc@98797hjkas$&asd(*$%GJMANIGE";
				byte[] bytes = new byte[0];
				bytes = Encoding.UTF8.GetBytes(str2.Substring(0, 8));
				byte[] numArray = new byte[0];
				numArray = Encoding.UTF8.GetBytes(str1.Substring(0, 8));
				byte[] bytes1 = Encoding.UTF8.GetBytes(helpme);
				using (DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider())
				{
					CryptoStream cryptoStream = new CryptoStream(new MemoryStream(), dESCryptoServiceProvider.CreateEncryptor(numArray, bytes), CryptoStreamMode.Write);
					cryptoStream.Write(bytes1, 0, (int)bytes1.Length);
					cryptoStream.FlushFinalBlock();
					base64String = Convert.ToBase64String((new MemoryStream()).ToArray());
					StringBuilder stringBuilder = new StringBuilder();
					byte[] numArray1 = Encoding.UTF8.GetBytes(base64String);
					for (int i = 0; i < (int)numArray1.Length; i++)
					{
						stringBuilder.Append(numArray1[i].ToString("X2"));
					}
					base64String = stringBuilder.ToString();
				}
				return base64String;
			}
			string str3 = "ay$a5%&jwrtmnh;lasjdf98787OMGFORLAX";
			string str4 = "abc@98797hjkas$&asd(*$%GJMANIGE";
			byte[] bytes2 = new byte[0];
			bytes2 = Encoding.UTF8.GetBytes(str4.Substring(0, 8));
			byte[] numArray2 = new byte[0];
			numArray2 = Encoding.UTF8.GetBytes(str3.Substring(0, 8));
			byte[] num = new byte[helpme.Length / 2];
			for (int j = 0; j < (int)num.Length; j++)
			{
				num[j] = Convert.ToByte(helpme.Substring(j * 2, 2), 16);
			}
			helpme = Encoding.UTF8.GetString(num);
			byte[] numArray3 = new byte[helpme.Replace(" ", "+").Length];
			numArray3 = Convert.FromBase64String(helpme.Replace(" ", "+"));
			using (DESCryptoServiceProvider dESCryptoServiceProvider1 = new DESCryptoServiceProvider())
			{
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream1 = new CryptoStream(memoryStream, dESCryptoServiceProvider1.CreateDecryptor(numArray2, bytes2), CryptoStreamMode.Write);
				cryptoStream1.Write(numArray3, 0, (int)numArray3.Length);
				cryptoStream1.FlushFinalBlock();
				str = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return str;
		}

		[Obfuscation(Exclude=false, Feature="+koi;-ctrl flow")]
		private static string DecryptX(string cipherText, string passPhrase)
		{
			string str;
			byte[] numArray = Convert.FromBase64String(cipherText);
			byte[] array = numArray.Take<byte>(32).ToArray<byte>();
			byte[] array1 = numArray.Skip<byte>(32).Take<byte>(32).ToArray<byte>();
			byte[] numArray1 = numArray.Skip<byte>(64).Take<byte>((int)numArray.Length - 64).ToArray<byte>();
			using (Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(passPhrase, array, 1000))
			{
				byte[] bytes = rfc2898DeriveByte.GetBytes(32);
				using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
				{
					rijndaelManaged.BlockSize = 256;
					rijndaelManaged.Mode = CipherMode.CBC;
					rijndaelManaged.Padding = PaddingMode.None;
					using (ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor(bytes, array1))
					{
						using (MemoryStream memoryStream = new MemoryStream(numArray1))
						{
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
							{
								byte[] numArray2 = new byte[(int)numArray1.Length];
								str = Encoding.UTF8.GetString(numArray2, 0, cryptoStream.Read(numArray2, 0, (int)numArray2.Length));
								cryptoStream.Close();
							}
							memoryStream.Close();
						}
					}
				}
			}
			return str;
		}

		[Obfuscation(Exclude=false, Feature="+koi;-ctrl flow")]
		private void LoadFromFileButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				LolixDecrypt.FileName = openFileDialog.FileName;
				this.PathName.Text = LolixDecrypt.FileName;
			}
			if (LolixDecrypt.FileName != "")
			{
				string str = File.ReadAllText(this.PathName.Text);
				try
				{
					if (!str.Contains("Body") || !str.Contains("ID"))
					{
						byte[] numArray = Convert.FromBase64String(str);
						str = LolixDecrypt.DecryptX(Regex.Match(Encoding.UTF8.GetString(numArray), "x0;(.*?)0;x").Groups[1].Value, "0THISISOBmodedByForlaxNIGGAs");
						this.textBox1.Text = str;
						this.save = str;
					}
					else
					{
						byte[] numArray1 = Convert.FromBase64String(LolixDecrypt.BellaCiao(Regex.Match(str, "\"Body\": \"(.*?)\"").Groups[1].Value, 2));
						str = LolixDecrypt.DecryptX(Regex.Match(Encoding.UTF8.GetString(numArray1), "x0;(.*?)0;x").Groups[1].Value, "0THISISOBmodedByForlaxNIGGAs");
						this.textBox1.Text = str;
						this.save = str;
					}
				}
				catch (Exception exception)
				{
					System.Windows.Forms.MessageBox.Show(exception.ToString());
				}
			}
		}

		private void LoadFromManagerButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
		}

		private void SaveConfig_Click(object sender, RoutedEventArgs e)
		{
			File.WriteAllText(string.Concat(this.PathName.Text, "_Rojava.loli"), this.save);
			System.Windows.Forms.MessageBox.Show(string.Concat("Saved to: ", this.PathName.Text, "_Rojava.loli"));
		}
	}
}
