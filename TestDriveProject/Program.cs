// ReSharper disable ArgumentsStyleNamedExpression

namespace TestDriveProject
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            int[] ints = { 8, 2, 1, 6, 4, 5, 3, 4, 5 };

            Console.Out.WriteLine("ints = {0}", arg0: ints);
            Console.Out.WriteLine("ints = {0}", ints);

            string name = "Alex";
        }
    }
}