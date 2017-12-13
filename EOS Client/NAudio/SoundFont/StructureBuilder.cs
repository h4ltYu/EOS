using System;
using System.Collections.Generic;
using System.IO;

namespace NAudio.SoundFont
{
    internal abstract class StructureBuilder<T>
    {
        public StructureBuilder()
        {
            this.Reset();
        }

        public abstract T Read(BinaryReader br);

        public abstract void Write(BinaryWriter bw, T o);

        public abstract int Length { get; }

        public void Reset()
        {
            this.data = new List<T>();
        }

        public T[] Data
        {
            get
            {
                return this.data.ToArray();
            }
        }

        protected List<T> data;
    }
}
