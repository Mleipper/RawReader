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
            
            

        }

        static UInt16[] GetInt16File(string fileName, int width, int height)
        {
            byte[] fileBytes = File.ReadAllBytes(fileName);
            int expectedFileSize = height * width;
            int actualFileSize = fileBytes.Count() / sizeof(UInt16); // Could perform an assert here to see if expected meets actual...
            UInt16[] fileArr = new UInt16[actualFileSize];

            for (var index = 0; index < actualFileSize; index++)
            {
                fileArr[index] = BitConverter.ToUInt16(fileBytes, index * sizeof(Int16));
                Console.WriteLine(fileArr[index]);
            }

            return fileArr;
        }
        
    }
}
