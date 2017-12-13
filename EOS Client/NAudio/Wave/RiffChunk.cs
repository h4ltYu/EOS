using System;
using System.Text;

namespace NAudio.Wave
{
    public class RiffChunk
    {
        public RiffChunk(int identifier, int length, long streamPosition)
        {
            this.Identifier = identifier;
            this.Length = length;
            this.StreamPosition = streamPosition;
        }

        public int Identifier { get; private set; }

        public string IdentifierAsString
        {
            get
            {
                return Encoding.UTF8.GetString(BitConverter.GetBytes(this.Identifier));
            }
        }

        public int Length { get; private set; }

        public long StreamPosition { get; private set; }
    }
}
