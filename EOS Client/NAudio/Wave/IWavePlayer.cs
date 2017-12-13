using System;

namespace NAudio.Wave
{
    public interface IWavePlayer : IDisposable
    {
        void Play();

        void Stop();

        void Pause();

        void Init(IWaveProvider waveProvider);

        PlaybackState PlaybackState { get; }

        [Obsolete("Not intending to keep supporting this going forward: set the volume on your input WaveProvider instead")]
        float Volume { get; set; }

        event EventHandler<StoppedEventArgs> PlaybackStopped;
    }
}
