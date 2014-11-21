using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateCSV
{
    public class Program
    {
        public static int Main(string[] args)
        {
            IExample example = new Example01();
            example.Start();

            return 0;
        }
    }
}
