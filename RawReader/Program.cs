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
            //Int16Image image1 = new Int16Image("sino800_540x1200.RAW", 1200, 500);
            //Int16Image image2 = new Int16Image("sino801_540x1200.RAW", 1200, 500);
            //var image3 = image1.CompareImages(image2);
            //image3.OutPutTofile();
            var image1= new Int16Image("sino800_540x1200.RAW", 1200, 500);
            var image2 = new Int16Image("sino801_540x1200.RAW", 1200, 500);
            image1.LoadUint16File2d("sino800_540x1200.RAW", 1200, 500);
            image2.LoadUint16File2d("sino801_540x1200.RAW", 1200, 500);
            var image3 = image1.CompareImages2d(image2);
            image3.Output2dToFile();
        }


    }
}
