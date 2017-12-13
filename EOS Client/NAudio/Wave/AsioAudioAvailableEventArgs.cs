using System;
using NAudio.Wave.Asio;

namespace NAudio.Wave
{
    public class AsioAudioAvailableEventArgs : EventArgs
    {
        public AsioAudioAvailableEventArgs(IntPtr[] inputBuffers, IntPtr[] outputBuffers, int samplesPerBuffer, AsioSampleType asioSampleType)
        {
            this.InputBuffers = inputBuffers;
            this.OutputBuffers = outputBuffers;
            this.SamplesPerBuffer = samplesPerBuffer;
            this.AsioSampleType = asioSampleType;
        }

        public IntPtr[] InputBuffers { get; private set; }

        public IntPtr[] OutputBuffers { get; private set; }

        public bool WrittenToOutputBuffers { get; set; }

        public int SamplesPerBuffer { get; private set; }

        public unsafe int GetAsInterleavedSamples(float[] samples)
        {
            int num = this.InputBuffers.Length;
            if (samples.Length < this.SamplesPerBuffer * num)
            {
                throw new ArgumentException("Buffer not big enough");
            }
            int num2 = 0;
            if (this.AsioSampleType == AsioSampleType.Int32LSB)
            {
                for (int i = 0; i < this.SamplesPerBuffer; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        samples[num2++] = (float)(*(int*)((byte*)((void*)this.InputBuffers[j]) + i * 4)) / 2.14748365E+09f;
                    }
                }
            }
            else if (this.AsioSampleType == AsioSampleType.Int16LSB)
            {
                for (int k = 0; k < this.SamplesPerBuffer; k++)
                {
                    for (int l = 0; l < num; l++)
                    {
                        samples[num2++] = (float)(*(short*)((byte*)((void*)this.InputBuffers[l]) + k * 2)) / 32767f;
                    }
                }
            }
            else if (this.AsioSampleType == AsioSampleType.Int24LSB)
            {
                for (int m = 0; m < this.SamplesPerBuffer; m++)
                {
                    for (int n = 0; n < num; n++)
                    {
                        byte* ptr = (byte*)((void*)this.InputBuffers[n]) + m * 3;
                        int num3 = (int)(*ptr) | (int)ptr[1] << 8 | (int)((sbyte)ptr[2]) << 16;
                        samples[num2++] = (float)num3 / 8388608f;
                    }
                }
            }
            else
            {
                if (this.AsioSampleType != AsioSampleType.Float32LSB)
                {
                    throw new NotImplementedException(string.Format("ASIO Sample Type {0} not supported", this.AsioSampleType));
                }
                for (int num4 = 0; num4 < this.SamplesPerBuffer; num4++)
                {
                    for (int num5 = 0; num5 < num; num5++)
                    {
                        samples[num2++] = *(float*)((byte*)((void*)this.InputBuffers[num5]) + num4 * 4);
                    }
                }
            }
            return this.SamplesPerBuffer * num;
        }

        public AsioSampleType AsioSampleType { get; private set; }

        [Obsolete("Better performance if you use the overload that takes an array, and reuse the same one")]
        public float[] GetAsInterleavedSamples()
        {
            int num = this.InputBuffers.Length;
            float[] array = new float[this.SamplesPerBuffer * num];
            this.GetAsInterleavedSamples(array);
            return array;
        }
    }
}
