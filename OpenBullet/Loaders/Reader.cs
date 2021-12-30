using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Text.RegularExpressions;

namespace OpenBullet.Loaders
{
    public class Reader
    {
        protected static bool success;

        protected static bool failure;

        protected static bool custom;

        protected static bool ban;

        public static int requestCounter;

        public static int randomizer;

        public static void LoadConfig(string configPath, Config Instance)
        {
            File.ReadAllLines(configPath).ToList().ForEach(delegate (string eachLine)
            {
                CreateConfig(eachLine, Instance);
            });
        }

        public static void CreateConfig(string configLine, Config Instance)
        {
            try
            {
                switch (new string[14]
                {
                "\"Name\": \"", "\"LastModified\": \"", "\"AdditionalInfo\": \"", "\"Author\": \"", "\"Version\": \"", "\"NeedsProxies\": ", "\"MaxProxyUses\": ", "REQUEST POST \"", "CONTENT \"", "CONTENTTYPE \"",
                "HEADER \"", "KEYCHAIN", "KEY \"", "\"<SOURCE>\" LR"
                }.FirstOrDefault((string s) => configLine.Contains(s)))
                {
                    case "\"Name\": \"":
                        Instance.ConfigName = Regex.Match(configLine, "\"Name\": \"(.*)\",").Groups[1].ToString();
                        break;
                    case "\"LastModified\": \"":
                        Instance.LastModified = Regex.Match(configLine, "\"LastModified\": \"(.*)T").Groups[1].ToString();
                        break;
                    case "\"AdditionalInfo\": \"":
                        Instance.AdditionalInformation = Regex.Match(configLine, "\"AdditionalInfo\": \"(.*)\",").Groups[1].ToString();
                        break;
                    case "\"Author\": \"":
                        Instance.Author = Regex.Match(configLine, "\"Author\": \"(.*)\",").Groups[1].ToString();
                        break;
                    case "\"Version\": \"":
                        Instance.Version = Regex.Match(configLine, "\"Version\": \"(.*)\",").Groups[1].ToString();
                        break;
                    case "\"NeedsProxies\": ":
                        Instance.NeedsProxies = Convert.ToBoolean(Regex.Match(configLine, "\"NeedsProxies\": (.*),").Groups[1].ToString());
                        break;
                    case "\"MaxProxyUses\": ":
                        Instance.MaxProxyUses = Regex.Match(configLine, "\"MaxProxyUses\": (.*),").Groups[1].ToString();
                        break;
                    case "REQUEST GET \"":
                        {
                            requestCounter++;
                            string key4 = "[GET] " + Regex.Match(configLine, "REQUEST ET \"(.*)\"").Groups[1].ToString();
                            Instance.Requests.Add(key4, requestCounter);
                            break;
                        }
                    case "REQUEST POST \"":
                        {
                            requestCounter++;
                            string key13 = "[POST] " + Regex.Match(configLine, "REQUEST POST \"(.*)\"").Groups[1].ToString();
                            Instance.Requests.Add(key13, requestCounter);
                            break;
                        }
                    case "CONTENT \"":
                        {
                            randomizer++;
                            string key12 = randomizer + "[POST-DATA] " + Regex.Match(configLine, "CONTENT \"(.*)\"").Groups[1].ToString();
                            Instance.Requests.Add(key12, requestCounter);
                            break;
                        }
                    case "CONTENTTYPE \"":
                        {
                            randomizer++;
                            string key11 = randomizer + "[HEADER] Content-Type: " + Regex.Match(configLine, "CONTENTTYPE \"(.*)\"").Groups[1].ToString();
                            Instance.Requests.Add(key11, requestCounter);
                            break;
                        }
                    case "HEADER \"":
                        if (!configLine.Contains("Accept-Encoding"))
                        {
                            randomizer++;
                            string text15 = Regex.Match(configLine, "HEADER \"(.*)\"").Groups[1].ToString();
                            string text16 = text15.Split(new string[1] { ": " }, StringSplitOptions.None)[0];
                            string text17 = text15.Split(new string[1] { ": " }, StringSplitOptions.None)[1];
                            string key9 = randomizer + "[HEADER] " + text16 + ": " + text17;
                            Instance.Requests.Add(key9, requestCounter);
                        }
                        else if (!configLine.Contains("accept-encoding"))
                        {
                            randomizer++;
                            string text18 = Regex.Match(configLine, "HEADER \"(.*)\"").Groups[1].ToString();
                            string text19 = text18.Split(new string[1] { ": " }, StringSplitOptions.None)[0];
                            string text20 = text18.Split(new string[1] { ": " }, StringSplitOptions.None)[1];
                            string key10 = randomizer + "[HEADER] " + text19 + ": " + text20;
                            Instance.Requests.Add(key10, requestCounter);
                        }
                        break;
                    case "KEYCHAIN":
                        {
                            string text21 = Regex.Match(configLine, "KEYCHAIN (.*)").Groups[1].ToString();
                            if (text21.Contains("Failure"))
                            {
                                failure = true;
                                success = false;
                                ban = false;
                                custom = false;
                            }
                            else if (text21.Contains("Success"))
                            {
                                success = true;
                                failure = false;
                                ban = false;
                                custom = false;
                            }
                            else if (text21.Contains("Ban"))
                            {
                                ban = true;
                                success = false;
                                failure = false;
                                custom = false;
                            }
                            else if (text21.Contains("Custom"))
                            {
                                custom = true;
                                ban = false;
                                success = false;
                                failure = false;
                            }
                            break;
                        }
                    case "KEY \"":
                        randomizer++;
                        if (success)
                        {
                            string text11 = Regex.Match(configLine, "KEY \"(.*)\"").Groups[1].ToString();
                            string key5 = randomizer + "[][SUCCESS KEY] " + text11;
                            Instance.Requests.Add(key5, requestCounter);
                        }
                        else if (failure)
                        {
                            string text12 = Regex.Match(configLine, "KEY \"(.*)\"").Groups[1].ToString();
                            string key6 = randomizer + "[][FAILURE KEY] " + text12;
                            Instance.Requests.Add(key6, requestCounter);
                        }
                        else if (ban)
                        {
                            string text13 = Regex.Match(configLine, "KEY \"(.*)\"").Groups[1].ToString();
                            string key7 = randomizer + "[][BAN KEY] " + text13;
                            Instance.Requests.Add(key7, requestCounter);
                        }
                        else if (custom)
                        {
                            string text14 = Regex.Match(configLine, "KEY \"(.*)\"").Groups[1].ToString();
                            string key8 = randomizer + "[][CUSTOM KEY] " + text14;
                            Instance.Requests.Add(key8, requestCounter);
                        }
                        break;
                    case "\"<SOURCE>\" LR":
                        if (configLine.Contains(" -> VAR"))
                        {
                            string text3 = Regex.Match(configLine, "-> VAR \"(.*)\"").Groups[1].ToString();
                            string text4 = Regex.Match(configLine, "<SOURCE>\" LR \"(.*)\" ->").Groups[1].ToString();
                            string text5 = text4.Split(new string[1] { "\" \"" }, StringSplitOptions.None)[0];
                            string text6 = text4.Split(new string[1] { "\" \"" }, StringSplitOptions.None)[1];
                            string key2 = "[PARSE VARIABLE] " + text3 + ": " + text5 + "(.*)" + text6;
                            Instance.varKeys.Add(text3);
                            Instance.Requests.Add(key2, requestCounter);
                        }
                        else if (configLine.Contains(" -> CAP"))
                        {
                            string text7 = Regex.Match(configLine, "-> CAP \"(.*)\"").Groups[1].ToString();
                            string text8 = Regex.Match(configLine, "<SOURCE>\" LR \"(.*)\" ->").Groups[1].ToString();
                            string text9 = text8.Split(new string[1] { "\" \"" }, StringSplitOptions.None)[0];
                            string text10 = text8.Split(new string[1] { "\" \"" }, StringSplitOptions.None)[1];
                            string key3 = "[PARSE CAPTURE] " + text7 + ": " + text9 + "(.*)" + text10;
                            Instance.Requests.Add(key3, requestCounter);
                        }
                        break;
                    case "PARSE \"<COOKIES":
                        {
                            string text = Regex.Match(configLine, " -> VAR \"(.*)\"").Groups[1].ToString();
                            string text2 = Regex.Match(configLine, "<COOKIES(.*)>\"").Groups[1].ToString();
                            text2 = text2.Replace("(", "");
                            text2 = text2.Replace(")", "");
                            string key = "[PARSE COOKIE] " + text + ": " + text2;
                            Instance.varKeys.Add(text);
                            Instance.Requests.Add(key, requestCounter);
                            break;
                        }
                    case "\"<SOURCE>\" JSON":
                        break;
                    case "PARSE \"<SOURCE>\" CSS":
                        break;
                }
            }
            catch (Exception value)
            {
                Console.WriteLine(value);
                Console.ReadKey();
            }
        }

    }
}
