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
        public const int MAX_WIDTH_HEIGHT = 10000;
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
        }
        /// <summary>
        /// Produces local Uint16 to represent the current file. 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void LoadUint16File(string fileName, int width, int height) {
            _height = height;
            _width = width;
            _fileName = fileName;
            LoadUint16File();
        }

        public void LoadUint16File()
        {
            byte[] fileBytes = File.ReadAllBytes(_fileName);
            int expectedFileSize = _height * _width;
            _actualFileSize = fileBytes.Count() / sizeof(UInt16); // Could perform an assert here to see if expected meets actual...
            _fileArr = new UInt16[_actualFileSize];

            for (var index = 0; index < _actualFileSize; index++)
            {
                _fileArr[index] = BitConverter.ToUInt16(fileBytes, index * sizeof(Int16));
                //Console.WriteLine(_fileArr[index]);
            }
        }

        private UInt16[][] _fileArr2d;

        public void InitialiseArr(int width, int height)
        {
            _fileArr2d = new UInt16[height][];
            for(int h = 0; h < height; h++)
            {
                _fileArr2d[h] = new UInt16[width];
            }

        }

        public void LoadUint16File2d(string fileName, int width, int height)
        {
            byte[] fileBytes = File.ReadAllBytes(fileName);
            int expectedFileSize = height * width;
            _actualFileSize = fileBytes.Count() / sizeof(UInt16); // Could perform an assert here to see if expected meets actual...
            InitialiseArr(width, height);

            for(int h = 0; h < height; h++)
            {
                for(int w = 0; w < width; w++)
                {
                    var fileIndex = (h * width) + w;
                    _fileArr2d[h][w] = BitConverter.ToUInt16(fileBytes, fileIndex * sizeof(Int16));
                }
            }
        }

        public Int16Image CompareImages2d(Int16Image int16Array)
        {
            var secondImage = int16Array.Getbytes2d();
            var newImage = new Int16Image("comparison of" + _fileName + " " + int16Array.GetFileName(), _width, _height);
            newImage.InitialiseArr(_width, _height);
            UInt16[][] newImageArr = newImage.Getbytes2d();
            for (var height = 0; height < _height; height++)
            {
                for (int width = 0; width < _width; width++)
                    newImageArr[height][width] = (UInt16)(_fileArr2d[height][width] - secondImage[height][width]);

            }
            newImage.SetArr2d(newImageArr);
            return newImage;
        }

        public UInt16[][] Getbytes2d()
        {
            return _fileArr2d;       
        }

        public void SetArr2d(UInt16[][] SetArr2d)
        {
            _fileArr2d = SetArr2d;
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
  
            }
            var newInt16 = new Int16Image("comparison of" + _fileName + " " + int16Array.GetFileName(), _width, _height, newImage, _actualFileSize);
            return newInt16;
        }

        public void Output2dToFile()
        {
        }

        public  void OutPutTofile()
        {
            byte[] result = new byte[_fileArr.Length * sizeof(UInt16)];
            Buffer.BlockCopy(_fileArr, 0, result, 0, result.Length);
            File.WriteAllBytesAsync(_fileName, result);
        }


    }
}
