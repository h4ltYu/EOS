using System;

namespace NAudio.Wave
{
    public interface IWaveIn : IDisposable
    {
        WaveFormat WaveFormat { get; set; }

        void StartRecording();

        void StopRecording();

        event EventHandler<WaveInEventArgs> DataAvailable;

        event EventHandler<StoppedEventArgs> RecordingStopped;
    }
}
