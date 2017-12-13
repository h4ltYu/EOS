using System;
using System.Runtime.InteropServices;
using System.Security;

namespace NAudio.Dmo
{
	/// <summary>
	/// defined in mediaobj.h
	/// </summary>
	// Token: 0x0200020B RID: 523
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[SuppressUnmanagedCodeSecurity]
	[Guid("d8ad0f58-5494-4102-97c5-ec798e59bcf4")]
	[ComImport]
	internal interface IMediaObject
	{
		// Token: 0x06000BF0 RID: 3056
		[PreserveSig]
		int GetStreamCount(out int inputStreams, out int outputStreams);

		// Token: 0x06000BF1 RID: 3057
		[PreserveSig]
		int GetInputStreamInfo(int inputStreamIndex, out InputStreamInfoFlags flags);

		// Token: 0x06000BF2 RID: 3058
		[PreserveSig]
		int GetOutputStreamInfo(int outputStreamIndex, out OutputStreamInfoFlags flags);

		// Token: 0x06000BF3 RID: 3059
		[PreserveSig]
		int GetInputType(int inputStreamIndex, int typeIndex, out DmoMediaType mediaType);

		// Token: 0x06000BF4 RID: 3060
		[PreserveSig]
		int GetOutputType(int outputStreamIndex, int typeIndex, out DmoMediaType mediaType);

		// Token: 0x06000BF5 RID: 3061
		[PreserveSig]
		int SetInputType(int inputStreamIndex, [In] ref DmoMediaType mediaType, DmoSetTypeFlags flags);

		// Token: 0x06000BF6 RID: 3062
		[PreserveSig]
		int SetOutputType(int outputStreamIndex, [In] ref DmoMediaType mediaType, DmoSetTypeFlags flags);

		// Token: 0x06000BF7 RID: 3063
		[PreserveSig]
		int GetInputCurrentType(int inputStreamIndex, out DmoMediaType mediaType);

		// Token: 0x06000BF8 RID: 3064
		[PreserveSig]
		int GetOutputCurrentType(int outputStreamIndex, out DmoMediaType mediaType);

		// Token: 0x06000BF9 RID: 3065
		[PreserveSig]
		int GetInputSizeInfo(int inputStreamIndex, out int size, out int maxLookahead, out int alignment);

		// Token: 0x06000BFA RID: 3066
		[PreserveSig]
		int GetOutputSizeInfo(int outputStreamIndex, out int size, out int alignment);

		// Token: 0x06000BFB RID: 3067
		[PreserveSig]
		int GetInputMaxLatency(int inputStreamIndex, out long referenceTimeMaxLatency);

		// Token: 0x06000BFC RID: 3068
		[PreserveSig]
		int SetInputMaxLatency(int inputStreamIndex, long referenceTimeMaxLatency);

		// Token: 0x06000BFD RID: 3069
		[PreserveSig]
		int Flush();

		// Token: 0x06000BFE RID: 3070
		[PreserveSig]
		int Discontinuity(int inputStreamIndex);

		// Token: 0x06000BFF RID: 3071
		[PreserveSig]
		int AllocateStreamingResources();

		// Token: 0x06000C00 RID: 3072
		[PreserveSig]
		int FreeStreamingResources();

		// Token: 0x06000C01 RID: 3073
		[PreserveSig]
		int GetInputStatus(int inputStreamIndex, out DmoInputStatusFlags flags);

		// Token: 0x06000C02 RID: 3074
		[PreserveSig]
		int ProcessInput(int inputStreamIndex, [In] IMediaBuffer mediaBuffer, DmoInputDataBufferFlags flags, long referenceTimeTimestamp, long referenceTimeDuration);

		// Token: 0x06000C03 RID: 3075
		[PreserveSig]
		int ProcessOutput(DmoProcessOutputFlags flags, int outputBufferCount, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] [Out] DmoOutputDataBuffer[] outputBuffers, out int statusReserved);

		// Token: 0x06000C04 RID: 3076
		[PreserveSig]
		int Lock(bool acquireLock);
	}
}
