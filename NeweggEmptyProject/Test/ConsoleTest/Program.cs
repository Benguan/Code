using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<int> intArray = new List<int>();
            intArray.Add(1);
            intArray.Add(2);
            intArray.Add(3);
            intArray.Add(4);
            intArray.Add(5);


            Console.WriteLine(intArray.Count);

            for (var i = 0; i < 5; i++)
            {
                intArray.Remove(i);
            }


            Console.WriteLine(intArray.Count);

            Console.Read();
            
        }
    }
}
