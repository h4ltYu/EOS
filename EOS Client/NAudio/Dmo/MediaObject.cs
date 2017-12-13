using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NAudio.Utils;
using NAudio.Wave;

namespace NAudio.Dmo
{
    public class MediaObject : IDisposable
    {
        internal MediaObject(IMediaObject mediaObject)
        {
            this.mediaObject = mediaObject;
            mediaObject.GetStreamCount(out this.inputStreams, out this.outputStreams);
        }

        public int InputStreamCount
        {
            get
            {
                return this.inputStreams;
            }
        }

        public int OutputStreamCount
        {
            get
            {
                return this.outputStreams;
            }
        }

        public DmoMediaType? GetInputType(int inputStream, int inputTypeIndex)
        {
            try
            {
                DmoMediaType value;
                if (this.mediaObject.GetInputType(inputStream, inputTypeIndex, out value) == 0)
                {
                    DmoInterop.MoFreeMediaType(ref value);
                    return new DmoMediaType?(value);
                }
            }
            catch (COMException exception)
            {
                if (exception.GetHResult() != -2147220986)
                {
                    throw;
                }
            }
            return null;
        }

        public DmoMediaType? GetOutputType(int outputStream, int outputTypeIndex)
        {
            try
            {
                DmoMediaType value;
                if (this.mediaObject.GetOutputType(outputStream, outputTypeIndex, out value) == 0)
                {
                    DmoInterop.MoFreeMediaType(ref value);
                    return new DmoMediaType?(value);
                }
            }
            catch (COMException exception)
            {
                if (exception.GetHResult() != -2147220986)
                {
                    throw;
                }
            }
            return null;
        }

        public DmoMediaType GetOutputCurrentType(int outputStreamIndex)
        {
            DmoMediaType result;
            int outputCurrentType = this.mediaObject.GetOutputCurrentType(outputStreamIndex, out result);
            if (outputCurrentType == 0)
            {
                DmoInterop.MoFreeMediaType(ref result);
                return result;
            }
            if (outputCurrentType == -2147220989)
            {
                throw new InvalidOperationException("Media type was not set.");
            }
            throw Marshal.GetExceptionForHR(outputCurrentType);
        }

        public IEnumerable<DmoMediaType> GetInputTypes(int inputStreamIndex)
        {
            int typeIndex = 0;
            for (; ; )
            {
                DmoMediaType? mediaType;
                DmoMediaType? dmoMediaType = mediaType = this.GetInputType(inputStreamIndex, typeIndex);
                if (dmoMediaType == null)
                {
                    break;
                }
                yield return mediaType.Value;
                typeIndex++;
            }
            yield break;
        }

        public IEnumerable<DmoMediaType> GetOutputTypes(int outputStreamIndex)
        {
            int typeIndex = 0;
            for (; ; )
            {
                DmoMediaType? mediaType;
                DmoMediaType? dmoMediaType = mediaType = this.GetOutputType(outputStreamIndex, typeIndex);
                if (dmoMediaType == null)
                {
                    break;
                }
                yield return mediaType.Value;
                typeIndex++;
            }
            yield break;
        }

        public bool SupportsInputType(int inputStreamIndex, DmoMediaType mediaType)
        {
            return this.SetInputType(inputStreamIndex, mediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
        }

        private bool SetInputType(int inputStreamIndex, DmoMediaType mediaType, DmoSetTypeFlags flags)
        {
            int num = this.mediaObject.SetInputType(inputStreamIndex, ref mediaType, flags);
            if (num == 0)
            {
                return true;
            }
            if (num == -2147220991)
            {
                throw new ArgumentException("Invalid stream index");
            }
            return false;
        }

        public void SetInputType(int inputStreamIndex, DmoMediaType mediaType)
        {
            if (!this.SetInputType(inputStreamIndex, mediaType, DmoSetTypeFlags.None))
            {
                throw new ArgumentException("Media Type not supported");
            }
        }

        public void SetInputWaveFormat(int inputStreamIndex, WaveFormat waveFormat)
        {
            DmoMediaType mediaType = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
            bool flag = this.SetInputType(inputStreamIndex, mediaType, DmoSetTypeFlags.None);
            DmoInterop.MoFreeMediaType(ref mediaType);
            if (!flag)
            {
                throw new ArgumentException("Media Type not supported");
            }
        }

        public bool SupportsInputWaveFormat(int inputStreamIndex, WaveFormat waveFormat)
        {
            DmoMediaType mediaType = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
            bool result = this.SetInputType(inputStreamIndex, mediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
            DmoInterop.MoFreeMediaType(ref mediaType);
            return result;
        }

        private DmoMediaType CreateDmoMediaTypeForWaveFormat(WaveFormat waveFormat)
        {
            DmoMediaType result = default(DmoMediaType);
            int formatBlockBytes = Marshal.SizeOf(waveFormat);
            DmoInterop.MoInitMediaType(ref result, formatBlockBytes);
            result.SetWaveFormat(waveFormat);
            return result;
        }

        public bool SupportsOutputType(int outputStreamIndex, DmoMediaType mediaType)
        {
            return this.SetOutputType(outputStreamIndex, mediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
        }

        public bool SupportsOutputWaveFormat(int outputStreamIndex, WaveFormat waveFormat)
        {
            DmoMediaType mediaType = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
            bool result = this.SetOutputType(outputStreamIndex, mediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
            DmoInterop.MoFreeMediaType(ref mediaType);
            return result;
        }

        private bool SetOutputType(int outputStreamIndex, DmoMediaType mediaType, DmoSetTypeFlags flags)
        {
            int num = this.mediaObject.SetOutputType(outputStreamIndex, ref mediaType, flags);
            if (num == -2147220987)
            {
                return false;
            }
            if (num == 0)
            {
                return true;
            }
            throw Marshal.GetExceptionForHR(num);
        }

        public void SetOutputType(int outputStreamIndex, DmoMediaType mediaType)
        {
            if (!this.SetOutputType(outputStreamIndex, mediaType, DmoSetTypeFlags.None))
            {
                throw new ArgumentException("Media Type not supported");
            }
        }

        public void SetOutputWaveFormat(int outputStreamIndex, WaveFormat waveFormat)
        {
            DmoMediaType mediaType = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
            bool flag = this.SetOutputType(outputStreamIndex, mediaType, DmoSetTypeFlags.None);
            DmoInterop.MoFreeMediaType(ref mediaType);
            if (!flag)
            {
                throw new ArgumentException("Media Type not supported");
            }
        }

        public MediaObjectSizeInfo GetInputSizeInfo(int inputStreamIndex)
        {
            int size;
            int maxLookahead;
            int alignment;
            Marshal.ThrowExceptionForHR(this.mediaObject.GetInputSizeInfo(inputStreamIndex, out size, out maxLookahead, out alignment));
            return new MediaObjectSizeInfo(size, maxLookahead, alignment);
        }

        public MediaObjectSizeInfo GetOutputSizeInfo(int outputStreamIndex)
        {
            int size;
            int alignment;
            Marshal.ThrowExceptionForHR(this.mediaObject.GetOutputSizeInfo(outputStreamIndex, out size, out alignment));
            return new MediaObjectSizeInfo(size, 0, alignment);
        }

        public void ProcessInput(int inputStreamIndex, IMediaBuffer mediaBuffer, DmoInputDataBufferFlags flags, long timestamp, long duration)
        {
            Marshal.ThrowExceptionForHR(this.mediaObject.ProcessInput(inputStreamIndex, mediaBuffer, flags, timestamp, duration));
        }

        public void ProcessOutput(DmoProcessOutputFlags flags, int outputBufferCount, DmoOutputDataBuffer[] outputBuffers)
        {
            int num;
            Marshal.ThrowExceptionForHR(this.mediaObject.ProcessOutput(flags, outputBufferCount, outputBuffers, out num));
        }

        public void AllocateStreamingResources()
        {
            Marshal.ThrowExceptionForHR(this.mediaObject.AllocateStreamingResources());
        }

        public void FreeStreamingResources()
        {
            Marshal.ThrowExceptionForHR(this.mediaObject.FreeStreamingResources());
        }

        public long GetInputMaxLatency(int inputStreamIndex)
        {
            long result;
            Marshal.ThrowExceptionForHR(this.mediaObject.GetInputMaxLatency(inputStreamIndex, out result));
            return result;
        }

        public void Flush()
        {
            Marshal.ThrowExceptionForHR(this.mediaObject.Flush());
        }

        public void Discontinuity(int inputStreamIndex)
        {
            Marshal.ThrowExceptionForHR(this.mediaObject.Discontinuity(inputStreamIndex));
        }

        public bool IsAcceptingData(int inputStreamIndex)
        {
            DmoInputStatusFlags dmoInputStatusFlags;
            int inputStatus = this.mediaObject.GetInputStatus(inputStreamIndex, out dmoInputStatusFlags);
            Marshal.ThrowExceptionForHR(inputStatus);
            return (dmoInputStatusFlags & DmoInputStatusFlags.DMO_INPUT_STATUSF_ACCEPT_DATA) == DmoInputStatusFlags.DMO_INPUT_STATUSF_ACCEPT_DATA;
        }

        public void Dispose()
        {
            if (this.mediaObject != null)
            {
                Marshal.ReleaseComObject(this.mediaObject);
                this.mediaObject = null;
            }
        }

        private IMediaObject mediaObject;

        private int inputStreams;

        private int outputStreams;
    }
}
