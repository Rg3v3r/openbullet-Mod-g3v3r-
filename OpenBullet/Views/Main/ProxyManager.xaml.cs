using Extreme.Net;
using Microsoft.Win32;
using OpenBullet.ViewModels;
using RuriLib.Models;
using RuriLib.Runner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using RuriLib;
using OpenBullet.Views.Dialogs;
using LiteDB;
using System.Collections;
using System.Windows.Media;

namespace OpenBullet.Views.Main
{
    /// <summary>
    /// Logica di interazione per ProxyManager.xaml
    /// </summary>
    public partial class ProxyManager : Page
    {
        public ProxyManagerViewModel vm = new ProxyManagerViewModel();
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private WorkerStatus Status = WorkerStatus.Idle;
        private CancellationTokenSource cts = new CancellationTokenSource();

        public ProxyManager()
        {
            InitializeComponent();
            DataContext = vm;

            vm.RefreshList();
            vm.UpdateProperties();
        }

        #region Start Button
        private void checkButton_Click(object sender, RoutedEventArgs e)
        {
            switch (this.Status)
            {
                case WorkerStatus.Idle:
                    OB.Logger.LogInfo(Components.ProxyManager, "Disabling the UI and starting the checker", false);
                    this.checkButton.Content = (object)"ABORT";
                    this.botsSlider.IsEnabled = false;
                    this.Status = WorkerStatus.Running;

                    _ = CheckProxiesAsync(vm.ProxyList, vm.BotsNumber, 200);

                    break;
                case WorkerStatus.Running:
                    OB.Logger.LogWarning(Components.ProxyManager, "Abort signal sent", false);
                    this.checkButton.Content = (object)"HARD ABORT";
                    this.Status = WorkerStatus.Stopping;
                    this.cts.Cancel();
                    break;
                case WorkerStatus.Stopping:
                    OB.Logger.LogWarning(Components.ProxyManager, "Hard abort signal sent", false);
                    this.checkButton.Content = (object)"CHECK";
                    this.botsSlider.IsEnabled = true;
                    this.Status = WorkerStatus.Idle;
                    break;
            }
        }
        #endregion

        #region Check
        public async Task CheckProxiesAsync(IEnumerable<CProxy> proxies, int threads, int step)
        {
            ProxyManager proxyManager1 = this;
            List<CProxy> proxiesToCheck = proxyManager1.vm.OnlyUntested ? proxies.ToList<CProxy>() : proxies.Where<CProxy>((Func<CProxy, bool>)(p => p.Working == ProxyWorking.UNTESTED)).ToList<CProxy>();
            ProxyManager proxyManager = proxyManager1;
            using (SemaphoreSlim semaphore = new SemaphoreSlim(threads, threads))
            {
                proxyManager1.cts = new CancellationTokenSource();
                for (int i = 0; i < proxiesToCheck.Count; i += step)
                {
                    Task[] array = proxiesToCheck.Skip<CProxy>(i).Take<CProxy>(Math.Min(proxiesToCheck.Count - i, step)).Select<CProxy, Task>((Func<CProxy, Task>)(p => proxyManager.CheckProxyAsync(p, semaphore, proxyManager.cts.Token))).ToArray<Task>();
                    try
                    {
                        Task task = await Task.WhenAny(Task.WhenAll(array), ProxyManager.AsTask(proxyManager1.cts.Token));
                        proxyManager1.cts.Token.ThrowIfCancellationRequested();
                    }
                    catch
                    {
                        break;
                    }
                }

                App.Current.Dispatcher.Invoke(() =>
                {
                    OB.Logger.LogInfo(Components.ProxyManager, "Check completed, re-enabling the UI");
                    checkButton.Content = "CHECK";
                    botsSlider.IsEnabled = true;
                    Status = WorkerStatus.Idle;
                });
            }
        }

        public static Task AsTask(CancellationToken cancellationToken)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            cancellationToken.Register((Action)(() => tcs.TrySetCanceled()), false);
            return (Task)tcs.Task;
        }

        public async Task CheckProxyAsync(
          CProxy proxy,
          SemaphoreSlim semaphore,
          CancellationToken token)
        {
            await semaphore.WaitAsync(token).ConfigureAwait(false);
            try
            {
                token.ThrowIfCancellationRequested();
                if (this.Status != WorkerStatus.Running)
                    throw new OperationCanceledException();
                await Task.Run((Action)(() =>
                {
                    this.CheckCountry(proxy);
                    this.CheckProxy(proxy);
                    Application.Current.Dispatcher.Invoke((Action)(() => this.vm.UpdateProperties()));
                }));
            }
            catch (OperationCanceledException)
            {
                OB.Logger.LogInfo(Components.ProxyManager, string.Format("{0} - THROWING", (object)proxy), false);
                throw;
            }
            finally
            {
                semaphore.Release();
            }
        }

        private void CheckCountry(CProxy proxy)
        {
            try
            {
                using (HttpRequest httpRequest = new HttpRequest())
                {
                    httpRequest.ConnectTimeout = (int)this.vm.Timeout;
                    string[] strArray = httpRequest.Get("http://ip-api.com/csv/" + proxy.Host, (RequestParams)null).ToString().Split(',');
                    string country = "Unknown";
                    if (strArray[0] == "success")
                        country = strArray[1];
                    Application.Current.Dispatcher.Invoke((Action)(() => proxy.Country = country.Replace("\"", "")));
                    using (LiteDatabase liteDatabase = new LiteDatabase(OB.dataBaseFile, (BsonMapper)null, (Logger)null))
                        liteDatabase.GetCollection<CProxy>("proxies").Update(proxy);
                    OB.Logger.LogInfo(Components.ProxyManager, "Checked country for proxy '" + proxy.Proxy + "' with result '" + proxy.Country + "'", false);
                }
            }
            catch (Exception ex)
            {
                OB.Logger.LogError(Components.ProxyManager, "Failted to check country for proxy '" + proxy.Proxy + "' - " + ex.Message, false);
            }
        }

        private void CheckProxy(CProxy proxy)
        {
            DateTime before = DateTime.Now;
            try
            {
                using (HttpRequest httpRequest = new HttpRequest())
                {
                    httpRequest.Proxy = proxy.GetClient();
                    httpRequest.Proxy.ConnectTimeout = (int)this.vm.Timeout * 1000;
                    httpRequest.Proxy.ReadWriteTimeout = (int)this.vm.Timeout * 1000;
                    httpRequest.ConnectTimeout = (int)this.vm.Timeout * 1000;
                    httpRequest.KeepAliveTimeout = (int)this.vm.Timeout * 1000;
                    httpRequest.ReadWriteTimeout = (int)this.vm.Timeout * 1000;
                    string source = httpRequest.Get(this.vm.TestURL, (RequestParams)null).ToString();
                    Application.Current.Dispatcher.Invoke((Action)(() => proxy.Ping = (int)(DateTime.Now - before).TotalMilliseconds));
                    Application.Current.Dispatcher.Invoke((Action)(() => proxy.Working = source.Contains(this.vm.SuccessKey) ? ProxyWorking.YES : ProxyWorking.NO));
                    OB.Logger.LogInfo(Components.ProxyManager, "Proxy '" + proxy.Proxy + string.Format("' responded in {0} ms", (object)proxy.Ping), false);
                }
            }
            catch (Exception ex)
            {
                OB.Logger.LogInfo(Components.ProxyManager, "Proxy '" + proxy.Proxy + "' failed to respond - " + ex.Message, false);
                Application.Current.Dispatcher.Invoke((Action)(() => proxy.Working = ProxyWorking.NO));
            }
            using (LiteDatabase liteDatabase = new LiteDatabase(OB.dataBaseFile, (BsonMapper)null, (Logger)null))
                liteDatabase.GetCollection<CProxy>("proxies").Update(proxy);
        }
        #endregion

        public void AddProxies(string fileName, ProxyType type, List<string> lines)
        {
            List<string> source1 = new List<string>();
            List<string> source2 = new List<string>();
            if (fileName != "")
            {
                OB.Logger.LogInfo(Components.ProxyManager, "Trying to load from file " + fileName, false);
                source1.AddRange((IEnumerable<string>)((IEnumerable<string>)File.ReadAllLines(fileName)).ToList<string>());
            }
            else
                OB.Logger.LogInfo(Components.ProxyManager, "No file specified, skipping the import from file", false);
            source2.AddRange((IEnumerable<string>)lines);
            OB.Logger.LogInfo(Components.ProxyManager, string.Format("Adding {0} proxies to the database", (object)(source1.Count + source2.Count)), false);
            List<CProxy> cproxyList = new List<CProxy>();
            foreach (string proxy in source1.Where<string>((Func<string, bool>)(p => !string.IsNullOrEmpty(p))).Distinct<string>().ToList<string>())
            {
                try
                {
                    CProxy cproxy = new CProxy(proxy, type);
                    if (cproxy.IsNumeric)
                    {
                        if (!cproxy.IsValidNumeric)
                            continue;
                    }
                    this.vm.ProxyList.Add(cproxy);
                    cproxyList.Add(cproxy);
                }
                catch
                {
                }
            }
            foreach (string proxy in source2.Where<string>((Func<string, bool>)(p => !string.IsNullOrEmpty(p))).Distinct<string>().ToList<string>())
            {
                try
                {
                    CProxy cproxy = new CProxy();
                    cproxy.Parse(proxy, type);
                    if (cproxy.IsNumeric)
                    {
                        if (!cproxy.IsValidNumeric)
                            continue;
                    }
                    this.vm.ProxyList.Add(cproxy);
                    cproxyList.Add(cproxy);
                }
                catch
                {
                }
            }
            using (LiteDatabase liteDatabase = new LiteDatabase(OB.dataBaseFile, (BsonMapper)null, (Logger)null))
                liteDatabase.GetCollection<CProxy>("proxies").InsertBulk((IEnumerable<CProxy>)cproxyList, 5000);
            this.vm.UpdateProperties();
        }

        #region GUI Controls
        private void botsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.vm.BotsNumber = (int)e.NewValue;
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File |*.txt";
            saveFileDialog.Title = "Export proxies";
            saveFileDialog.ShowDialog();
            if (!(saveFileDialog.FileName != ""))
                return;
            if (this.proxiesListView.Items.Count > 0)
            {
                OB.Logger.LogInfo(Components.ProxyManager, string.Format("Exporting {0} proxies", (object)this.proxiesListView.Items.Count), false);
                List<string> stringList = new List<string>();
                foreach (CProxy selectedItem in (IEnumerable)this.proxiesListView.SelectedItems)
                    stringList.Add(selectedItem.Proxy);
                File.WriteAllLines(saveFileDialog.FileName, (IEnumerable<string>)stringList);
            }
            else
            {
                int num = (int)System.Windows.MessageBox.Show("No proxies selected!");
                OB.Logger.LogWarning(Components.ProxyManager, "No proxies selected", false);
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            OB.Logger.LogInfo(Components.ProxyManager, string.Format("Deleting {0} proxies", (object)this.proxiesListView.SelectedItems.Count), false);
            List<CProxy> proxies = new List<CProxy>();
            foreach (CProxy selectedItem in (IEnumerable)this.proxiesListView.SelectedItems)
                proxies.Add(selectedItem);
            this.DeleteProxies(proxies);
            OB.Logger.LogInfo(Components.ProxyManager, "Proxies deleted successfully", false);
        }

        private void DeleteProxies(List<CProxy> proxies)
        {
            OB.Logger.LogInfo(Components.ProxyManager, "Deleting selected proxies", false);
            using (LiteDatabase liteDatabase = new LiteDatabase(OB.dataBaseFile, (BsonMapper)null, (Logger)null))
            {
                IEnumerable<CProxy> source = this.proxiesListView.SelectedItems.Cast<CProxy>();
                while (source.Count<CProxy>() > 0)
                {
                    liteDatabase.GetCollection<CProxy>(nameof(proxies)).Delete((BsonValue)source.First<CProxy>().Id);
                    this.vm.ProxyList.Remove(source.First<CProxy>());
                }
            }
            this.vm.UpdateProperties();
        }

        private void deleteAllButton_Click(object sender, RoutedEventArgs e)
        {
            OB.Logger.LogWarning(Components.ProxyManager, "Purging all proxies", false);
            using (LiteDatabase liteDatabase = new LiteDatabase(OB.dataBaseFile, (BsonMapper)null, (Logger)null))
                liteDatabase.DropCollection("proxies");
            this.vm.ProxyList.Clear();
            this.vm.UpdateProperties();
        }

        private void deleteNotWorkingButton_Click(object sender, RoutedEventArgs e)
        {
            OB.Logger.LogInfo(Components.ProxyManager, "Deleting all non working proxies", false);
            using (LiteDatabase liteDatabase = new LiteDatabase(OB.dataBaseFile, (BsonMapper)null, (Logger)null))
            {
                liteDatabase.GetCollection<CProxy>("proxies").Delete((System.Linq.Expressions.Expression<Func<CProxy, bool>>)(p => (int)p.Working == 1));
                IEnumerable<CProxy> source = this.vm.ProxyList.Where<CProxy>((Func<CProxy, bool>)(p => p.Working == ProxyWorking.NO));
                while (source.Count<CProxy>() > 0)
                    this.vm.ProxyList.Remove(source.First<CProxy>());
            }
            this.vm.UpdateProperties();
        }

        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            new MainDialog((Page)new DialogAddProxies((object)this), "Import Proxies").ShowDialog();
        }

        private void copySelectedProxies_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = "";
                foreach (CProxy selectedItem in (IEnumerable)this.proxiesListView.SelectedItems)
                    text = text + selectedItem.Proxy + Environment.NewLine;
                Clipboard.SetText(text);
                OB.Logger.LogInfo(Components.ProxyManager, string.Format("Copied {0} proxies", (object)this.proxiesListView.SelectedItems.Count), false);
            }
            catch (Exception ex)
            {
                OB.Logger.LogError(Components.ProxyManager, "Failed to copy proxies - " + ex.Message, false);
            }
        }


        private void deleteDuplicatesButton_Click(object sender, RoutedEventArgs e)
        {
            List<CProxy> list = this.vm.ProxyList.GroupBy<CProxy, string>((Func<CProxy, string>)(p => p.Proxy)).Where<IGrouping<string, CProxy>>((Func<IGrouping<string, CProxy>, bool>)(grp => grp.Count<CProxy>() > 1)).Select<IGrouping<string, CProxy>, CProxy>((Func<IGrouping<string, CProxy>, CProxy>)(grp => grp.First<CProxy>())).ToList<CProxy>();
            OB.Logger.LogInfo(Components.ProxyManager, string.Format("Removing {0} duplicate proxies", (object)(this.vm.ProxyList.Count - list.Count)), false);
            using (LiteDatabase liteDatabase = new LiteDatabase(OB.dataBaseFile, (BsonMapper)null, (Logger)null))
            {
                for (int index = 0; index < list.Count; ++index)
                {
                    CProxy cproxy = list[index];
                    this.vm.ProxyList.Remove(cproxy);
                    liteDatabase.GetCollection<CProxy>("proxies").Delete((BsonValue)cproxy.Id);
                }
            }
            this.vm.UpdateProperties();
        }

        private void DeleteUntestedButton_Click(object sender, RoutedEventArgs e)
        {
            OB.Logger.LogInfo(Components.ProxyManager, "Deleting all untested proxies", false);
            using (LiteDatabase liteDatabase = new LiteDatabase(OB.dataBaseFile, (BsonMapper)null, (Logger)null))
            {
                liteDatabase.GetCollection<CProxy>("proxies").Delete((System.Linq.Expressions.Expression<Func<CProxy, bool>>)(p => (int)p.Working == 2));
                IEnumerable<CProxy> source = this.vm.ProxyList.Where<CProxy>((Func<CProxy, bool>)(p => p.Working == ProxyWorking.UNTESTED));
                while (source.Count<CProxy>() > 0)
                    this.vm.ProxyList.Remove(source.First<CProxy>());
            }
            this.vm.UpdateProperties();
        }
        #endregion

        #region ListView
        private void listViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader viewColumnHeader = sender as GridViewColumnHeader;
            string propertyName = viewColumnHeader.Tag.ToString();
            if (this.listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer((Visual)this.listViewSortCol).Remove((Adorner)this.listViewSortAdorner);
                this.proxiesListView.Items.SortDescriptions.Clear();
            }
            ListSortDirection listSortDirection = ListSortDirection.Ascending;
            if (this.listViewSortCol == viewColumnHeader && this.listViewSortAdorner.Direction == listSortDirection)
                listSortDirection = ListSortDirection.Descending;
            this.listViewSortCol = viewColumnHeader;
            this.listViewSortAdorner = new SortAdorner((UIElement)this.listViewSortCol, listSortDirection);
            AdornerLayer.GetAdornerLayer((Visual)this.listViewSortCol).Add((Adorner)this.listViewSortAdorner);
            this.proxiesListView.Items.SortDescriptions.Add(new SortDescription(propertyName, listSortDirection));
        }

        private void ListViewItem_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
        }
        #endregion
    }
}
