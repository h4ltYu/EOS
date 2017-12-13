using System;
using System.Text;

namespace NAudio.Utils
{
    public class ChunkIdentifier
    {
        public static int ChunkIdentifierToInt32(string s)
        {
            if (s.Length != 4)
            {
                throw new ArgumentException("Must be a four character string");
            }
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            if (bytes.Length != 4)
            {
                throw new ArgumentException("Must encode to exactly four bytes");
            }
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
