using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JerloPH_CSharp
{
    public static class FileHelper
    {
        public static string CreateNewFolder(string root, string folderName)
        {
            try
            {
                string dir = Path.Combine(root, folderName);
                Directory.CreateDirectory(dir);
                return dir;
            }
            catch { throw; }
        }
        public static string ReadFromFile(string filename)
        {
            string content = String.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    content = sr.ReadToEnd();
                }
            }
            catch { throw; }
            return content;
        }
        public static bool WriteFile(string filename, string content)
        {
            try
            {
                if (File.Exists(filename))
                    File.Delete(filename);
            }
            catch { throw new Exception("Cannot overwrite existing file!"); }
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.Write(content);
                }
                return true;
            }
            catch { throw; }
        }
        public static void AppendFile(string file, string content, bool Addline = false)
        {
            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter s = new StreamWriter(fs))
                    {
                        s.Write(content + (Addline ? "\n" : ""));
                        s.Close();
                    }
                    fs.Close();
                }
            }
            catch { throw; }
        }
        public static void PrependFile(string file, string content)
        {
            string prev = ReadFromFile(file);
            WriteFile(file, content);
            AppendFile(file, prev);
        }

        public static bool Compress(string filepath)
        {
            try
            {
                FileInfo fileToCompress = new FileInfo(filepath);
                using (FileStream originalFileStream = fileToCompress.OpenRead())
                {
                    if ((File.GetAttributes(fileToCompress.FullName) &
                       FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                    {
                        using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                        {
                            using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                               CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);
                            }
                        }
                    }
                }
                return true;
            }
            catch { throw; }
        }
        public static string Decompress(string filepath, string newFileName)
        {
            try
            {
                if (File.Exists(newFileName))
                {
                    File.Delete(newFileName);
                }
                FileInfo fileToDecompress = new FileInfo(filepath);
                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    string currentFileName = fileToDecompress.FullName;
                    using (FileStream file = File.Create(newFileName))
                    {
                        using (GZipStream decStream = new GZipStream(originalFileStream, CompressionMode.Decompress, false))
                        {
                            decStream.CopyTo(file);
                            //Console.WriteLine($"Decompressed: {file.Name}");
                        }
                        file.SetLength(file.Position);
                        file.Close();
                    }
                    originalFileStream.Close();
                }
                if (File.Exists(newFileName))
                    return newFileName;
            }
            catch { throw; }
            return "";
        }
    }
}
