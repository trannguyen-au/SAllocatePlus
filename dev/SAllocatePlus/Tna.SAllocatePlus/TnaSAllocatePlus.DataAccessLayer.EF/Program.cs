using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnaSAllocatePlus.DataAccessLayer.EF.Migrations;

namespace TnaSAllocatePlus.DataAccessLayer.EF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Configuration config = new Configuration();
            config.RunSeed();
        }
    }
}
