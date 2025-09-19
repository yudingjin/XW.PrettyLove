namespace XW.PrettyLove.Core
{
    public static class StreamExtensions
    {
        /// <summary>
        /// 从流中读取字节数据
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] ReadData(this Stream s)
        {
            if (s.CanSeek) s.Position = 0;
            List<byte> data = new List<byte>();
            var d = new byte[1024];
            while (true)
            {
                var length = s.Read(d, 0, d.Length);
                if (length <= 0) break;
                data.AddRange(d.Take(length));
            }
            return data.ToArray();
        }

        /// <summary>
        /// 从流中读取字节数据
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static async Task<byte[]> ReadDataAsync(this Stream s)
        {
            if (s.CanSeek)
            {
                s.Position = 0;
            }
            List<byte> list = new List<byte>();
            byte[] array = new byte[1024];
            while (true)
            {
                int num = await s.ReadAsync(array, 0, array.Length);
                if (num <= 0)
                {
                    break;
                }

                list.AddRange(array.Take(num));
            }

            return list.ToArray();
        }
    }
}
