using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadanie_1
{
    class Program
    { 
        static void Main(string[] args)
        {
             Misja.ReadData();
             Console.WriteLine("n={0}", Misja.GetSize()); ;
             Console.WriteLine("Wynik operacji: {0}", Misja.GetResultFaster());
             Console.WriteLine("Licznik: {0}", Misja.GetCounter()) ;
          //  Misja.Write();
             Console.ReadKey();
        }
    }
}
