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
        private UInt16[][] _fileArr;
        private int _width;
        private int _height;
        private int _actualFileSize;
        private string _fileName;

        public Int16Image()
        {

        }

        public Int16Image(string fileName, int width, int height) {
            _height = height;
            _width = width;
            _fileName = fileName;
            InitialiseArr(_width, _height);
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
            InitialiseArr(_width, _height);
            LoadUint16File();
        }                

        private void InitialiseArr(int width, int height)
        {
            _fileArr = new UInt16[height][];
            for(int h = 0; h < height; h++)
            {
                _fileArr[h] = new UInt16[width];
            }
        }

        public void LoadUint16File()
        {
            byte[] fileBytes = File.ReadAllBytes(_fileName);
            int expectedFileSize = _height * _width;
            _actualFileSize = fileBytes.Count() / sizeof(UInt16); // Could perform an assert here to see if expected meets actual...

            for(int h = 0; h < _height; h++)
            {
                for(int w = 0; w < _width; w++)
                {
                    var fileIndex = (h * _width) + w;
                    _fileArr[h][w] = BitConverter.ToUInt16(fileBytes, fileIndex * sizeof(Int16));
                }
            }
        }
        /// <summary>
        /// non async method of compare images 
        /// </summary>
        /// <param name="secImg"></param>
        /// <returns></returns>
        public Int16Image CompareImages(Int16Image secImg)
        {
            if (!IsSameDimension(secImg))
            {
                throw new ArgumentOutOfRangeException("Images must be the same width and height!");
            }
            var newImage = new Int16Image("comparison of" + _fileName + " " + secImg.GetFileName(), _width, _height);

            List<Task> imgTasks = new List<Task>();

            for (var h = 0; h < _height; h++)
            {
                int row = h;
                newImage.CompareRow(row, this, secImg);
            }

            return newImage;
        }
        /// <summary>
        /// async method to compare 
        /// </summary>
        /// <param name="secImg"></param>
        /// <returns></returns>
        public async Task<Int16Image> CompareImagesAsync(Int16Image secImg)
        {
            if (!IsSameDimension(secImg))
            {
                throw new ArgumentOutOfRangeException("Images must be the same width and height!");
            }
            var newImage = new Int16Image("comparison of" + _fileName + " " + secImg.GetFileName(), _width, _height);

            List<Task> imgTasks = new List<Task>();

            for (var h = 0; h < _height; h++)
            {
                int row = h;                
                imgTasks.Add(Task.Run(() => newImage.CompareRow(row, this, secImg)));
            }
            
            while (imgTasks.Any())
            {
                Task finished = await Task.WhenAny(imgTasks);
                Console.WriteLine("Row complete");
                imgTasks.Remove(finished);
            }

            return newImage;
        }


        private void CompareRow(int row, Int16Image firstImg, Int16Image secImg)
        {
            var firstImgRow = firstImg.GetRow(row);
            var secImgRow = secImg.GetRow(row);
            for (int w = 0; w < _width; w++)
            {
                _fileArr[row][w] = (UInt16)(firstImgRow[w] - secImgRow[w]);
            }
        }

        public bool IsSameDimension(Int16Image otherImg)
        {
            return _width == otherImg._width && _height == otherImg._height;
        }

        public UInt16[] GetRow(int row)
        {
            return _fileArr[row];
        }

        public void SetArr(UInt16[][] SetArr)
        {
            _fileArr = SetArr;
        }

        public UInt16[][] Getbytes()
        {
            return _fileArr;
        }

        public int GetFileSize()
        {
            return _width * _height * sizeof(UInt16);
        }


        public string GetFileName()
        {
            return _fileName;
        }

        public void OutputToFile()
        {
            byte[] result = new byte[GetFileSize()];
            for(int h = 0; h < _height; h++)
            {
                Buffer.BlockCopy(_fileArr[h], 0, result, (h * _width), _width * sizeof(UInt16));
            }
            
            File.WriteAllBytesAsync(_fileName, result);
        } 
    }
}
