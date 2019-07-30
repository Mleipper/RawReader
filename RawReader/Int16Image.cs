using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace RawReader
{
    public class Int16Image
    {
        private UInt16[] _fileArr;
        private int _width;
        private int _height;
        private int _actualFileSize;
        private string _fileName;
        public Int16Image()
        {

        }
        //CONSTRUCTOR USED IN THE CREATION OF A THIRD IMAGE 
        public Int16Image(string fileName, int width,int height,UInt16[] image,int actualFileSize)
        {
            _height = height;
            _width = width;
            _fileName = fileName;
            _actualFileSize = actualFileSize;
            _fileArr = image;

        }

        public Int16Image(string fileName, int width, int height) {
            _height = height;
            _width = width;
            _fileName = fileName;
            LoadUint16File(fileName, _width, _height);

        }
        /// <summary>
        /// Produces local Uint16 to represent the current file. 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
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


        public string GetFileName()
        {
            return _fileName;
        }

        //can be made async thruouh use of a 2 d array
        public Int16Image CompareImages(Int16Image int16Array)
        {
            var secondImage = int16Array.Getbytes();
            UInt16[] newImage = new UInt16[_actualFileSize];
            for (var index = 0; index < _actualFileSize; index++)
            {
                newImage[index] = (UInt16)(_fileArr[index] - secondImage[index]);
                //Console.WriteLine(newImage[index]);
            }
            var newInt16 = new Int16Image("comparison of" + _fileName + " " + int16Array.GetFileName(), _width, _height, newImage, _actualFileSize);
            return newInt16;
        }

        public void OutPutTofile() {

            byte[] result = new byte[_fileArr.Length * sizeof(UInt16)];
            Buffer.BlockCopy(_fileArr, 0, result, 0, result.Length);
            Console.WriteLine(result);
            File.WriteAllBytes(_fileName, result);
        }


    }
}
