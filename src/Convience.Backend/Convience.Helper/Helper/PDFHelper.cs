using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convience.Helper
{
    public class PDFHelper
    {
        static public string ConvertToPDF(string fileName, bool isDelete = true)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception("File not exists: " + fileName);
            }
            string result = fileName;
            switch (Path.GetExtension(fileName).ToLower())
            {
                case ".doc":
                case ".docx":
                    WordHelper word = new WordHelper();
                    return word.MakePdfFile(fileName, isDelete);
            }
            return result;
        }
    }
}
