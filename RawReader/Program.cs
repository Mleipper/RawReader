using System;
using System.IO;
using System.Linq;

namespace RawReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Int16Image image1 = new Int16Image("sino800_540x1200.RAW", 1200, 500);
            Int16Image image2 = new Int16Image("sino801_540x1200.RAW", 1200, 500);
            var image3 = image1.CompareImages(image2);

        }


    }
}
