using System;

namespace NAudio.Dmo
{
    public class MediaObjectSizeInfo
    {
        public int Size { get; private set; }

        public int MaxLookahead { get; private set; }

        public int Alignment { get; private set; }

        public MediaObjectSizeInfo(int size, int maxLookahead, int alignment)
        {
            this.Size = size;
            this.MaxLookahead = maxLookahead;
            this.Alignment = alignment;
        }

        public override string ToString()
        {
            return string.Format("Size: {0}, Alignment {1}, MaxLookahead {2}", this.Size, this.Alignment, this.MaxLookahead);
        }
    }
}
