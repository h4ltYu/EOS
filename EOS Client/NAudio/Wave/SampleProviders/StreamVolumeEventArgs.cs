using System;

namespace NAudio.Wave.SampleProviders
{
    public class StreamVolumeEventArgs : EventArgs
    {
        public float[] MaxSampleValues { get; set; }
    }
}
