using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RawReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Int16Image image1 = new Int16Image("sino800_540x1200.RAW", 1200, 500);
            //Int16Image image2 = new Int16Image("sino801_540x1200.RAW", 1200, 500);
            //var image3 = image1.CompareImages(image2);
            //image3.OutPutTofile();
            var image1= new Int16Image("sino800_540x1200.RAW", 1200, 540);
            var image2 = new Int16Image("sino801_540x1200.RAW", 1200, 540);
            image1.LoadUint16File("sino800_540x1200.RAW", 1200, 540);
            image2.LoadUint16File("sino801_540x1200.RAW", 1200, 540);
            var image3 = image1.CompareImagesAsync(image2).GetAwaiter().GetResult();
            image3.OutputToFile();
        }
    }
}
