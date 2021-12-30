using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBullet.Loaders
{
    public class Config
    {
        // Token: 0x17000001 RID: 1
        // (get) Token: 0x0600000A RID: 10 RVA: 0x00002401 File Offset: 0x00000601
        // (set) Token: 0x0600000B RID: 11 RVA: 0x00002409 File Offset: 0x00000609
        public string ConfigName { get; set; }

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x0600000C RID: 12 RVA: 0x00002412 File Offset: 0x00000612
        // (set) Token: 0x0600000D RID: 13 RVA: 0x0000241A File Offset: 0x0000061A
        public string LastModified { get; set; }

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x0600000E RID: 14 RVA: 0x00002423 File Offset: 0x00000623
        // (set) Token: 0x0600000F RID: 15 RVA: 0x0000242B File Offset: 0x0000062B
        public string AdditionalInformation { get; set; }

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000010 RID: 16 RVA: 0x00002434 File Offset: 0x00000634
        // (set) Token: 0x06000011 RID: 17 RVA: 0x0000243C File Offset: 0x0000063C
        public string Author { get; set; }

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x06000012 RID: 18 RVA: 0x00002445 File Offset: 0x00000645
        // (set) Token: 0x06000013 RID: 19 RVA: 0x0000244D File Offset: 0x0000064D
        public string Version { get; set; }

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000014 RID: 20 RVA: 0x00002456 File Offset: 0x00000656
        // (set) Token: 0x06000015 RID: 21 RVA: 0x0000245E File Offset: 0x0000065E
        public string MaxProxyUses { get; set; }

        // Token: 0x17000007 RID: 7
        // (get) Token: 0x06000016 RID: 22 RVA: 0x00002467 File Offset: 0x00000667
        // (set) Token: 0x06000017 RID: 23 RVA: 0x0000246F File Offset: 0x0000066F
        public bool NeedsProxies { get; set; }

        // Token: 0x06000018 RID: 24 RVA: 0x00002478 File Offset: 0x00000678
        public Config()
        {
            this.ConfigName = this.ConfigName;
            this.LastModified = this.LastModified;
            this.AdditionalInformation = this.AdditionalInformation;
            this.Author = this.Author;
            this.NeedsProxies = this.NeedsProxies;
            this.MaxProxyUses = this.MaxProxyUses;
        }

        // Token: 0x04000009 RID: 9
        public IDictionary<string, int> Requests = new Dictionary<string, int>();

        // Token: 0x0400000A RID: 10
        public List<string> varKeys = new List<string>();
    }
}
