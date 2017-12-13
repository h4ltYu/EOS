using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace NAudio.MediaFoundation
{
    public static class MediaFoundationApi
    {
        public static void Startup()
        {
            if (!MediaFoundationApi.initialized)
            {
                int num = 2;
                OperatingSystem osversion = Environment.OSVersion;
                if (osversion.Version.Major == 6 && osversion.Version.Minor == 0)
                {
                    num = 1;
                }
                MediaFoundationInterop.MFStartup(num << 16 | 112, 0);
                MediaFoundationApi.initialized = true;
            }
        }

        public static IEnumerable<IMFActivate> EnumerateTransforms(Guid category)
        {
            IntPtr interfacesPointer;
            int interfaceCount;
            MediaFoundationInterop.MFTEnumEx(category, _MFT_ENUM_FLAG.MFT_ENUM_FLAG_ALL, null, null, out interfacesPointer, out interfaceCount);
            IMFActivate[] interfaces = new IMFActivate[interfaceCount];
            for (int j = 0; j < interfaceCount; j++)
            {
                IntPtr pUnk = Marshal.ReadIntPtr(new IntPtr(interfacesPointer.ToInt64() + (long)(j * Marshal.SizeOf(interfacesPointer))));
                interfaces[j] = (IMFActivate)Marshal.GetObjectForIUnknown(pUnk);
            }
            foreach (IMFActivate i in interfaces)
            {
                yield return i;
            }
            Marshal.FreeCoTaskMem(interfacesPointer);
            yield break;
        }

        public static void Shutdown()
        {
            if (MediaFoundationApi.initialized)
            {
                MediaFoundationInterop.MFShutdown();
                MediaFoundationApi.initialized = false;
            }
        }

        public static IMFMediaType CreateMediaType()
        {
            IMFMediaType result;
            MediaFoundationInterop.MFCreateMediaType(out result);
            return result;
        }

        public static IMFMediaType CreateMediaTypeFromWaveFormat(WaveFormat waveFormat)
        {
            IMFMediaType imfmediaType = MediaFoundationApi.CreateMediaType();
            try
            {
                MediaFoundationInterop.MFInitMediaTypeFromWaveFormatEx(imfmediaType, waveFormat, Marshal.SizeOf(waveFormat));
            }
            catch (Exception)
            {
                Marshal.ReleaseComObject(imfmediaType);
                throw;
            }
            return imfmediaType;
        }

        public static IMFMediaBuffer CreateMemoryBuffer(int bufferSize)
        {
            IMFMediaBuffer result;
            MediaFoundationInterop.MFCreateMemoryBuffer(bufferSize, out result);
            return result;
        }

        public static IMFSample CreateSample()
        {
            IMFSample result;
            MediaFoundationInterop.MFCreateSample(out result);
            return result;
        }

        public static IMFAttributes CreateAttributes(int initialSize)
        {
            IMFAttributes result;
            MediaFoundationInterop.MFCreateAttributes(out result, initialSize);
            return result;
        }

        public static IMFByteStream CreateByteStream(object stream)
        {
            IMFByteStream result;
            MediaFoundationInterop.MFCreateMFByteStreamOnStreamEx(stream, out result);
            return result;
        }

        public static IMFSourceReader CreateSourceReaderFromByteStream(IMFByteStream byteStream)
        {
            IMFSourceReader result;
            MediaFoundationInterop.MFCreateSourceReaderFromByteStream(byteStream, null, out result);
            return result;
        }

        private static bool initialized;
    }
}
