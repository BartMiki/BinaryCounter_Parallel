using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryCounter_Parallel
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryCounter bs = new BinaryCounter(16);
            bs.Display();
            Console.ReadLine();
        }
    }
}
