using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenBullet.Loaders;

namespace OpenBullet.Engine
{
    public class Start
    {
        // Token: 0x06000037 RID: 55 RVA: 0x0000657C File Offset: 0x0000477C
        public static void Initalize(Config Instance)
        {
            string projectNamespace = Start.RandomString(20) + "BetaBuild";
            Core.Runner(Instance, projectNamespace, Start.LOCKER);
        }

        // Token: 0x06000038 RID: 56 RVA: 0x000065A7 File Offset: 0x000047A7
        public static string RandomString(int length)
        {
            return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ", length)
                               select s[Start.random.Next(s.Length)]).ToArray<char>());
        }

        // Token: 0x04000012 RID: 18
        public static Random random = new Random();

        // Token: 0x04000013 RID: 19
        public static object LOCKER = new object();
    }
}
