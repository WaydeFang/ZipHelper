using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ZipHelper
{
    class PathHelper
    {
        public static void InculdeBackslash(ref string path)
        {
            if (path[path.Length - 1] != '\\')
                path += "\\";
        }

        public static void RemoveBackslash(ref string path)
        {
            if (path[path.Length - 1] == '\\')
                path = path.Substring(0, path.Length - 1);
        }

        public static void CreateDirifnotExist(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);           
        }
    }
}
