// ReSharper disable ArgumentsStyleNamedExpression

namespace TestDriveProject
{
    using System;

    class Program
    {
        private static void Main(string[] args)
        {
            int[] ints = { 8, 2, 1, 6, 4, 5, 3, 4, 5 };
            var name = "Alex";
            

            Console.Out.WriteLine($"name = {name}");

            var employee = new Employee();

            employee.DoThat();
        }
    }

    class Person
    {
        public string Name { get; set; }

        public void DoThis() { }
    }

    class Employee : Person
    {
        public string IDNP { get; set; }

        public void DoThat()
        {
            DoThis();
        }
    }


}