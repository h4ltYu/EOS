using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NAudio.MediaFoundation;
using NAudio.Utils;

namespace NAudio.Wave
{
    public class MediaFoundationEncoder : IDisposable
    {
        public static int[] GetEncodeBitrates(Guid audioSubtype, int sampleRate, int channels)
        {
            return (from br in (from mt in MediaFoundationEncoder.GetOutputMediaTypes(audioSubtype)
                                where mt.SampleRate == sampleRate && mt.ChannelCount == channels
                                select mt.AverageBytesPerSecond * 8).Distinct<int>()
                    orderby br
                    select br).ToArray<int>();
        }

        public static MediaType[] GetOutputMediaTypes(Guid audioSubtype)
        {
            IMFCollection imfcollection;
            try
            {
                MediaFoundationInterop.MFTranscodeGetAudioOutputAvailableTypes(audioSubtype, _MFT_ENUM_FLAG.MFT_ENUM_FLAG_ALL, null, out imfcollection);
            }
            catch (COMException exception)
            {
                if (exception.GetHResult() == -1072875819)
                {
                    return new MediaType[0];
                }
                throw;
            }
            int num;
            imfcollection.GetElementCount(out num);
            List<MediaType> list = new List<MediaType>(num);
            for (int i = 0; i < num; i++)
            {
                object obj;
                imfcollection.GetElement(i, out obj);
                IMFMediaType mediaType = (IMFMediaType)obj;
                list.Add(new MediaType(mediaType));
            }
            Marshal.ReleaseComObject(imfcollection);
            return list.ToArray();
        }

        public static void EncodeToWma(IWaveProvider inputProvider, string outputFile, int desiredBitRate = 192000)
        {
            MediaType mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_WMAudioV8, inputProvider.WaveFormat, desiredBitRate);
            if (mediaType == null)
            {
                throw new InvalidOperationException("No suitable WMA encoders available");
            }
            using (MediaFoundationEncoder mediaFoundationEncoder = new MediaFoundationEncoder(mediaType))
            {
                mediaFoundationEncoder.Encode(outputFile, inputProvider);
            }
        }

        public static void EncodeToMp3(IWaveProvider inputProvider, string outputFile, int desiredBitRate = 192000)
        {
            MediaType mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_MP3, inputProvider.WaveFormat, desiredBitRate);
            if (mediaType == null)
            {
                throw new InvalidOperationException("No suitable MP3 encoders available");
            }
            using (MediaFoundationEncoder mediaFoundationEncoder = new MediaFoundationEncoder(mediaType))
            {
                mediaFoundationEncoder.Encode(outputFile, inputProvider);
            }
        }

        public static void EncodeToAac(IWaveProvider inputProvider, string outputFile, int desiredBitRate = 192000)
        {
            MediaType mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_AAC, inputProvider.WaveFormat, desiredBitRate);
            if (mediaType == null)
            {
                throw new InvalidOperationException("No suitable AAC encoders available");
            }
            using (MediaFoundationEncoder mediaFoundationEncoder = new MediaFoundationEncoder(mediaType))
            {
                mediaFoundationEncoder.Encode(outputFile, inputProvider);
            }
        }

        public static MediaType SelectMediaType(Guid audioSubtype, WaveFormat inputFormat, int desiredBitRate)
        {
            return (from mt in MediaFoundationEncoder.GetOutputMediaTypes(audioSubtype)
                    where mt.SampleRate == inputFormat.SampleRate && mt.ChannelCount == inputFormat.Channels
                    select new
                    {
                        MediaType = mt,
                        Delta = Math.Abs(desiredBitRate - mt.AverageBytesPerSecond * 8)
                    } into mt
                    orderby mt.Delta
                    select mt.MediaType).FirstOrDefault<MediaType>();
        }

        public MediaFoundationEncoder(MediaType outputMediaType)
        {
            if (outputMediaType == null)
            {
                throw new ArgumentNullException("outputMediaType");
            }
            this.outputMediaType = outputMediaType;
        }

        public void Encode(string outputFile, IWaveProvider inputProvider)
        {
            if (inputProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm && inputProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
            {
                throw new ArgumentException("Encode input format must be PCM or IEEE float");
            }
            MediaType mediaType = new MediaType(inputProvider.WaveFormat);
            IMFSinkWriter imfsinkWriter = MediaFoundationEncoder.CreateSinkWriter(outputFile);
            try
            {
                int num;
                imfsinkWriter.AddStream(this.outputMediaType.MediaFoundationObject, out num);
                imfsinkWriter.SetInputMediaType(num, mediaType.MediaFoundationObject, null);
                this.PerformEncode(imfsinkWriter, num, inputProvider);
            }
            finally
            {
                Marshal.ReleaseComObject(imfsinkWriter);
                Marshal.ReleaseComObject(mediaType.MediaFoundationObject);
            }
        }

        private static IMFSinkWriter CreateSinkWriter(string outputFile)
        {
            IMFAttributes imfattributes = MediaFoundationApi.CreateAttributes(1);
            imfattributes.SetUINT32(MediaFoundationAttributes.MF_READWRITE_ENABLE_HARDWARE_TRANSFORMS, 1);
            IMFSinkWriter result;
            try
            {
                MediaFoundationInterop.MFCreateSinkWriterFromURL(outputFile, null, imfattributes, out result);
            }
            catch (COMException exception)
            {
                if (exception.GetHResult() == -1072875819)
                {
                    throw new ArgumentException("Was not able to create a sink writer for this file extension");
                }
                throw;
            }
            finally
            {
                Marshal.ReleaseComObject(imfattributes);
            }
            return result;
        }

        private void PerformEncode(IMFSinkWriter writer, int streamIndex, IWaveProvider inputProvider)
        {
            int num = inputProvider.WaveFormat.AverageBytesPerSecond * 4;
            byte[] managedBuffer = new byte[num];
            writer.BeginWriting();
            long num2 = 0L;
            long num3;
            do
            {
                num3 = this.ConvertOneBuffer(writer, streamIndex, inputProvider, num2, managedBuffer);
                num2 += num3;
            }
            while (num3 > 0L);
            writer.DoFinalize();
        }

        private static long BytesToNsPosition(int bytes, WaveFormat waveFormat)
        {
            return 10000000L * (long)bytes / (long)waveFormat.AverageBytesPerSecond;
        }

        private long ConvertOneBuffer(IMFSinkWriter writer, int streamIndex, IWaveProvider inputProvider, long position, byte[] managedBuffer)
        {
            long num = 0L;
            IMFMediaBuffer imfmediaBuffer = MediaFoundationApi.CreateMemoryBuffer(managedBuffer.Length);
            int count;
            imfmediaBuffer.GetMaxLength(out count);
            IMFSample imfsample = MediaFoundationApi.CreateSample();
            imfsample.AddBuffer(imfmediaBuffer);
            IntPtr destination;
            int num2;
            imfmediaBuffer.Lock(out destination, out count, out num2);
            int num3 = inputProvider.Read(managedBuffer, 0, count);
            if (num3 > 0)
            {
                num = MediaFoundationEncoder.BytesToNsPosition(num3, inputProvider.WaveFormat);
                Marshal.Copy(managedBuffer, 0, destination, num3);
                imfmediaBuffer.SetCurrentLength(num3);
                imfmediaBuffer.Unlock();
                imfsample.SetSampleTime(position);
                imfsample.SetSampleDuration(num);
                writer.WriteSample(streamIndex, imfsample);
            }
            else
            {
                imfmediaBuffer.Unlock();
            }
            Marshal.ReleaseComObject(imfsample);
            Marshal.ReleaseComObject(imfmediaBuffer);
            return num;
        }

        protected void Dispose(bool disposing)
        {
            Marshal.ReleaseComObject(this.outputMediaType.MediaFoundationObject);
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.disposed = true;
                this.Dispose(true);
            }
            GC.SuppressFinalize(this);
        }

        ~MediaFoundationEncoder()
        {
            this.Dispose(false);
        }

        private readonly MediaType outputMediaType;

        private bool disposed;
    }
}
