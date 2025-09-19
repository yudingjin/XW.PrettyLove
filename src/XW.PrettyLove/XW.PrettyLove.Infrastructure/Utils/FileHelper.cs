using System.Security.Cryptography;
using System.Text;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 读取长度
        /// </summary>
        const int length = 1024;

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileContent"></param>
        /// <param name="fileMode"></param>
        /// <param name="encoding"></param>
        public static void Write(string filePath, string fileContent, FileMode? fileMode = null, Encoding? encoding = null)
        {
            FileInfo fileWithDir = GetFileWithDir(filePath);
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            using (StreamWriter streamWriter = ((!fileWithDir.Exists) ? new StreamWriter(fileWithDir.Open(FileMode.Create, FileAccess.Write), encoding) : new StreamWriter(fileWithDir.Open(fileMode.GetValueOrDefault(FileMode.Truncate), FileAccess.Write), encoding)))
            {
                streamWriter.Write(fileContent);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileData"></param>
        /// <param name="startPosition"></param>
        public static void Write(string filePath, byte[] fileData, int startPosition = -1)
        {
            FileInfo fileWithDir = GetFileWithDir(filePath);
            if (startPosition > 0 && !File.Exists(filePath))
            {
                return;
            }
            using (FileStream fileStream = ((!File.Exists(filePath)) ? fileWithDir.OpenWrite() : fileWithDir.Open(FileMode.OpenOrCreate)))
            {
                if (startPosition > 0)
                {
                    fileStream.Position = startPosition;
                }
                fileStream.Write(fileData, 0, fileData.Length);
                fileStream.Flush();
                fileStream.Close();
            }
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileContent"></param>
        /// <param name="fileMode"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task WriteAsync(string filePath, string fileContent, FileMode? fileMode = null, Encoding? encoding = null)
        {
            FileInfo file = GetFileWithDir(filePath);
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            StreamWriter tw = ((!file.Exists) ? new StreamWriter(file.Open(FileMode.Create, FileAccess.Write), encoding) : new StreamWriter(file.Open(fileMode.GetValueOrDefault(FileMode.Truncate), FileAccess.Write), encoding));
            await tw.WriteAsync(fileContent);
            await tw.FlushAsync();
            tw.Close();
            tw.Dispose();
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileData"></param>
        /// <param name="startPosition"></param>
        /// <returns></returns>
        public static async Task WriteAsync(string filePath, byte[] fileData, int startPosition = -1)
        {
            FileInfo fileInfo = GetFileWithDir(filePath);
            if (startPosition > 0 && !File.Exists(filePath))
            {
                return;
            }
            using (FileStream fs = ((!File.Exists(filePath)) ? fileInfo.OpenWrite() : fileInfo.Open(FileMode.OpenOrCreate)))
            {
                if (startPosition > 0)
                {
                    fs.Position = startPosition;
                }
                await fs.WriteAsync(fileData, 0, fileData.Length);
                await fs.FlushAsync();
                fs.Close();
            }
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileData"></param>
        /// <param name="startPosition"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task WriteAsync(string filePath, Stream fileData, int startPosition = -1)
        {
            FileInfo fileInfo = GetFileWithDir(filePath);
            if (startPosition > 0 && !File.Exists(filePath))
            {
                throw new Exception("要续传的文件不存在,请检查文件是否存在!");
            }

            using FileStream fs = ((!File.Exists(filePath)) ? fileInfo.OpenWrite() : fileInfo.Open(FileMode.OpenOrCreate));
            if (startPosition > 0)
            {
                fs.Position = startPosition;
            }

            byte[] buffer = new byte[1024];
            int offset = 0;
            for (int count = await fileData.ReadAsync(buffer, offset, 1024); count > 0; count = await fileData.ReadAsync(buffer, offset, 1024))
            {
                await fs.WriteAsync(buffer, offset, buffer.Length);
                offset += count;
            }

            await fs.FlushAsync();
            fs.Close();
        }

        /// <summary>
        /// 读取文件字节
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(string filePath)
        {
            using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] array = new byte[1024];
            List<byte> list = new List<byte>();
            int num = 0;
            int num2;
            do
            {
                fileStream.Seek(num, SeekOrigin.Begin);
                num2 = fileStream.Read(array, 0, array.Length);
                list.AddRange(array.Take(num2));
                num += num2;
            }
            while (num2 >= 1024);
            fileStream.Close();
            return list.ToArray();
        }

        /// <summary>
        /// 从指定的文件中读取文本
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Read(string filePath, Encoding? encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            using StreamReader sr = new StreamReader(filePath, encoding);
            string content = sr.ReadToEnd();
            sr.Close();
            return content;
        }

        /// <summary>
        /// 从指定的文件中读取文本
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> ReadAsync(string filePath, Encoding? encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            using StreamReader sr = new StreamReader(filePath, encoding);
            string content = await sr.ReadToEndAsync();
            sr.Close();
            return content;
        }

        /// <summary>
        /// 读取文件字节
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="startPosition"></param>
        /// <param name="readLength"></param>
        /// <returns></returns>
        public static async Task<byte[]> ReadBytesAsync(string filePath, int startPosition = -1, int readLength = -1)
        {
            using FileStream fs = new FileStream(filePath, FileMode.Open);

            if (startPosition >= 0 && readLength > 0)
            {
                byte[] buf = new byte[readLength];
                fs.Seek(startPosition, SeekOrigin.Begin);
                int length = await fs.ReadAsync(buf, 0, buf.Length);
                if (length < buf.Length)
                {
                    return buf.Take(length).ToArray();
                }
                return buf;
            }
            return fs.ReadData();
        }

        /// <summary>
        /// 读取不能超过2G的文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] FileToBytes(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using MemoryStream memoryStream = new MemoryStream();
                byte[] buffer = new byte[1024];
                int dataSize = 0;
                while ((dataSize = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, dataSize);
                }
                var result = memoryStream.ToArray();
                memoryStream.Close();
                fileStream.Close();
                return result;
            }
        }

        /// 读取不能超过2G的文件 异步
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<byte[]> FileToBytesAsync(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using MemoryStream memoryStream = new MemoryStream();
                byte[] buffer = new byte[1024];
                int dataSize = 0;
                while ((dataSize = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await memoryStream.WriteAsync(buffer, 0, dataSize);
                }
                var result = memoryStream.ToArray();
                memoryStream.Close();
                fileStream.Close();
                return result;
            }
        }

        /// <summary>
        /// 将文件转成流
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static MemoryStream FileToStream(string path)
        {
            using FileStream fileStream = new FileStream(path, FileMode.Open);
            MemoryStream memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            fileStream.Close();
            memoryStream.Position = 0;
            return memoryStream;
        }

        /// <summary>
        /// 将文件转成流
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<MemoryStream> FileToStreamAsync(string path)
        {
            using FileStream fileStream = new FileStream(path, FileMode.Open);
            MemoryStream memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            fileStream.Close();
            memoryStream.Position = 0;
            return memoryStream;
        }

        /// <summary>
        /// 将流转成二进制
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream();
            byte[] buffer = new byte[length];
            int intSize = stream.Read(buffer, 0, buffer.Length);
            while (intSize > 0)
            {
                memoryStream.Write(buffer, 0, intSize);
                intSize = stream.Read(buffer, 0, length);
            }
            return memoryStream.ToArray();
        }

        /// <summary>
        /// 将流转成二进制
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<byte[]> StreamToBytesAsync(Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream();
            byte[] buffer = new byte[length];
            int intSize = await stream.ReadAsync(buffer, 0, buffer.Length);
            while (intSize > 0)
            {
                await memoryStream.WriteAsync(buffer, 0, intSize);
                intSize = await stream.ReadAsync(buffer, 0, length);
            }
            return memoryStream.ToArray();
        }

        /// <summary>
        /// 读取文件转化成字符串
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encode"></param>
        /// <returns>字符串</returns>
        public static async Task<string> FileToStringAsync(string filePath, Encoding encode)
        {
            using (StreamReader streamReader = new StreamReader(filePath, encode))
            {
                string content = await streamReader.ReadToEndAsync();
                streamReader.Close();
                return content;
            }
        }

        /// <summary>
        /// 流转化成字符串
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encode"></param>
        /// <returns>字符串</returns>
        public static async Task<string> StreamToStringAsync(Stream stream, Encoding encode)
        {
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }
            using (StreamReader streamReader = new StreamReader(stream, encode))
            {
                string text = await streamReader.ReadToEndAsync();
                streamReader.Close();
                return text;
            }
        }

        /// <summary>
        /// byte数组转化字符串
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static string BytesToString(byte[] bytes, Encoding encode)
        {
            return encode.GetString(bytes);
        }

        /// <summary>
        /// 字符串转化byte数组
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static byte[] StringToBytes(string text, Encoding encode)
        {
            return encode.GetBytes(text);
        }

        /// <summary>
        /// 字符串转化成流
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static Stream StringToStream(string text, Encoding encode)
        {
            var bytes = StringToBytes(text, encode);
            return new MemoryStream(bytes);
        }

        /// <summary>
        /// 检查所给文件的目录是否存在，不存在则创建,并返回FileInfo对象
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FileInfo GetFileWithDir(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!Directory.Exists(fileInfo.DirectoryName))
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
            }
            return fileInfo;
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(filePath))
                {
                    byte[] hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
                }
            }
        }

        /// <summary>
        /// 获取文件所在目录
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetDirectory(string filePath)
        {
            FileInfo fileInfo = GetFileInfo(filePath);
            return fileInfo.Directory.FullName;
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetExtension(string filePath)
        {
            FileInfo fileInfo = GetFileInfo(filePath);
            return fileInfo.Extension;
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FileInfo GetFileInfo(string filePath)
        {
            return new FileInfo(filePath);
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="targetDir"></param>
        public static void DeleteDirectory(string targetDir)
        {
            if (!Directory.Exists(targetDir))
            {
                return;
            }
            string[] files = Directory.GetFiles(targetDir);
            string[] dirs = Directory.GetDirectories(targetDir);

            //删除目录下的文件
            foreach (string file in files)
            {
                File.Delete(file);
            }

            //删除子目录
            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(targetDir);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath))
                return;
            File.Delete(filePath);
        }
    }
}
