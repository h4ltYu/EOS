using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAudio.Utils;

namespace NAudio.Wave
{
    public class Id3v2Tag
    {
        public static Id3v2Tag ReadTag(Stream input)
        {
            Id3v2Tag result;
            try
            {
                result = new Id3v2Tag(input);
            }
            catch (FormatException)
            {
                result = null;
            }
            return result;
        }

        public static Id3v2Tag Create(IEnumerable<KeyValuePair<string, string>> tags)
        {
            return Id3v2Tag.ReadTag(Id3v2Tag.CreateId3v2TagStream(tags));
        }

        private static byte[] FrameSizeToBytes(int n)
        {
            byte[] bytes = BitConverter.GetBytes(n);
            Array.Reverse(bytes);
            return bytes;
        }

        private static byte[] CreateId3v2Frame(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }
            if (key.Length != 4)
            {
                throw new ArgumentOutOfRangeException("key", "key " + key + " must be 4 characters long");
            }
            byte[] array = new byte[]
            {
                byte.MaxValue,
                254
            };
            byte[] array2 = new byte[3];
            byte[] array3 = array2;
            byte[] array4 = new byte[2];
            byte[] array5 = array4;
            byte[] array6;
            if (key == "COMM")
            {
                array6 = ByteArrayExtensions.Concat(new byte[][]
                {
                    new byte[]
                    {
                        1
                    },
                    array3,
                    array5,
                    array,
                    Encoding.Unicode.GetBytes(value)
                });
            }
            else
            {
                array6 = ByteArrayExtensions.Concat(new byte[][]
                {
                    new byte[]
                    {
                        1
                    },
                    array,
                    Encoding.Unicode.GetBytes(value)
                });
            }
            byte[][] array7 = new byte[4][];
            array7[0] = Encoding.UTF8.GetBytes(key);
            array7[1] = Id3v2Tag.FrameSizeToBytes(array6.Length);
            byte[][] array8 = array7;
            int num = 2;
            byte[] array9 = new byte[2];
            array8[num] = array9;
            array7[3] = array6;
            return ByteArrayExtensions.Concat(array7);
        }

        private static byte[] GetId3TagHeaderSize(int size)
        {
            byte[] array = new byte[4];
            for (int i = array.Length - 1; i >= 0; i--)
            {
                array[i] = (byte)(size % 128);
                size /= 128;
            }
            return array;
        }

        private static byte[] CreateId3v2TagHeader(IEnumerable<byte[]> frames)
        {
            int num = 0;
            foreach (byte[] array in frames)
            {
                num += array.Length;
            }
            byte[][] array2 = new byte[4][];
            array2[0] = Encoding.UTF8.GetBytes("ID3");
            byte[][] array3 = array2;
            int num2 = 1;
            byte[] array4 = new byte[2];
            array4[0] = 3;
            array3[num2] = array4;
            byte[][] array5 = array2;
            int num3 = 2;
            byte[] array6 = new byte[1];
            array5[num3] = array6;
            array2[3] = Id3v2Tag.GetId3TagHeaderSize(num);
            return ByteArrayExtensions.Concat(array2);
        }

        private static Stream CreateId3v2TagStream(IEnumerable<KeyValuePair<string, string>> tags)
        {
            List<byte[]> list = new List<byte[]>();
            foreach (KeyValuePair<string, string> keyValuePair in tags)
            {
                list.Add(Id3v2Tag.CreateId3v2Frame(keyValuePair.Key, keyValuePair.Value));
            }
            byte[] array = Id3v2Tag.CreateId3v2TagHeader(list);
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(array, 0, array.Length);
            foreach (byte[] array2 in list)
            {
                memoryStream.Write(array2, 0, array2.Length);
            }
            memoryStream.Position = 0L;
            return memoryStream;
        }

        private Id3v2Tag(Stream input)
        {
            this.tagStartPosition = input.Position;
            BinaryReader binaryReader = new BinaryReader(input);
            byte[] array = binaryReader.ReadBytes(10);
            if (array.Length >= 3 && array[0] == 73 && array[1] == 68 && array[2] == 51)
            {
                if ((array[5] & 64) == 64)
                {
                    byte[] array2 = binaryReader.ReadBytes(4);
                    int num = (int)array2[0] * 2097152;
                    num += (int)array2[1] * 16384;
                    num += (int)(array2[2] * 128);
                    num += (int)array2[3];
                }
                int num2 = (int)array[6] * 2097152;
                num2 += (int)array[7] * 16384;
                num2 += (int)(array[8] * 128);
                num2 += (int)array[9];
                binaryReader.ReadBytes(num2);
                if ((array[5] & 16) == 16)
                {
                    binaryReader.ReadBytes(10);
                }
                this.tagEndPosition = input.Position;
                input.Position = this.tagStartPosition;
                this.rawData = binaryReader.ReadBytes((int)(this.tagEndPosition - this.tagStartPosition));
                return;
            }
            input.Position = this.tagStartPosition;
            throw new FormatException("Not an ID3v2 tag");
        }

        public byte[] RawData
        {
            get
            {
                return this.rawData;
            }
        }

        private long tagStartPosition;

        private long tagEndPosition;

        private byte[] rawData;
    }
}
