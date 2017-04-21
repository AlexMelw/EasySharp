using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySharp.NHelpers;
using EasySharp.ReSharperCSharpSourceTemplates;

namespace TestDriveProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] ints = new int[] { 8, 2, 1, 6, 4, 5, 3, 4, 5 };

            foreach (var element in ints)
                Console.WriteLine(element);
        }
    }
}