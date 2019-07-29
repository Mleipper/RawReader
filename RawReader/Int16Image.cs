using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RawReader
{
    public class Int16Image
    {
        private UInt16[] _fileArr;
        private int _width;
        private int _height;
        private int _actualFileSize;

        public Int16Image()
        {

        }

        public Int16Image(string fileName, int width, int height) {
            _height = height;
            _width = width;
            LoadUint16File(fileName, _width, _height);

        }

        public void LoadUint16File(string fileName, int width, int height) {
            byte[] fileBytes = File.ReadAllBytes(fileName);
            int expectedFileSize = height * width;
            _actualFileSize = fileBytes.Count() / sizeof(UInt16); // Could perform an assert here to see if expected meets actual...
            _fileArr = new UInt16[_actualFileSize];

            for (var index = 0; index < _actualFileSize; index++)
            {
                _fileArr[index] = BitConverter.ToUInt16(fileBytes, index * sizeof(Int16));
                //Console.WriteLine(_fileArr[index]);
            }

        }

        public UInt16[] Getbytes()
        {
            return _fileArr;
        }

        public UInt16[] Compare(Int16Image int16Array)
        {
            var secondImage = int16Array.Getbytes();
            UInt16[] newImage = new UInt16[_actualFileSize];
            for (var index = 0; index < _actualFileSize; index++)
            {
                newImage[index] = (UInt16)(_fileArr[index] - secondImage[index]);
                Console.WriteLine(newImage[index]);
            }
            return newImage;
        }


    }
}
