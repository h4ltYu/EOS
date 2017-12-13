using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("bf94c121-5b05-4e6f-8000-ba598961414d")]
    [ComImport]
    public interface IMFTransform
    {
        void GetStreamLimits(out int pdwInputMinimum, out int pdwInputMaximum, out int pdwOutputMinimum, out int pdwOutputMaximum);

        void GetStreamCount(out int pcInputStreams, out int pcOutputStreams);

        void GetStreamIds([In] int dwInputIDArraySize, [In] [Out] int[] pdwInputIDs, [In] int dwOutputIDArraySize, [In] [Out] int[] pdwOutputIDs);

        void GetInputStreamInfo([In] int dwInputStreamID, out MFT_INPUT_STREAM_INFO pStreamInfo);

        void GetOutputStreamInfo([In] int dwOutputStreamID, out MFT_OUTPUT_STREAM_INFO pStreamInfo);

        void GetAttributes(out IMFAttributes pAttributes);

        void GetInputStreamAttributes([In] int dwInputStreamID, out IMFAttributes pAttributes);

        void GetOutputStreamAttributes([In] int dwOutputStreamID, out IMFAttributes pAttributes);

        void DeleteInputStream([In] int dwOutputStreamID);

        void AddInputStreams([In] int cStreams, [In] int[] adwStreamIDs);

        void GetInputAvailableType([In] int dwInputStreamID, [In] int dwTypeIndex, out IMFMediaType ppType);

        void GetOutputAvailableType([In] int dwOutputStreamID, [In] int dwTypeIndex, out IMFMediaType ppType);

        void SetInputType([In] int dwInputStreamID, [In] IMFMediaType pType, [In] _MFT_SET_TYPE_FLAGS dwFlags);

        void SetOutputType([In] int dwOutputStreamID, [In] IMFMediaType pType, [In] _MFT_SET_TYPE_FLAGS dwFlags);

        void GetInputCurrentType([In] int dwInputStreamID, out IMFMediaType ppType);

        void GetOutputCurrentType([In] int dwOutputStreamID, out IMFMediaType ppType);

        void GetInputStatus([In] int dwInputStreamID, out _MFT_INPUT_STATUS_FLAGS pdwFlags);

        void GetOutputStatus([In] int dwInputStreamID, out _MFT_OUTPUT_STATUS_FLAGS pdwFlags);

        void SetOutputBounds([In] long hnsLowerBound, [In] long hnsUpperBound);

        void ProcessEvent([In] int dwInputStreamID, [In] IMFMediaEvent pEvent);

        void ProcessMessage([In] MFT_MESSAGE_TYPE eMessage, [In] IntPtr ulParam);

        void ProcessInput([In] int dwInputStreamID, [In] IMFSample pSample, int dwFlags);

        [PreserveSig]
        int ProcessOutput([In] _MFT_PROCESS_OUTPUT_FLAGS dwFlags, [In] int cOutputBufferCount, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] [Out] MFT_OUTPUT_DATA_BUFFER[] pOutputSamples, out _MFT_PROCESS_OUTPUT_STATUS pdwStatus);
    }
}
