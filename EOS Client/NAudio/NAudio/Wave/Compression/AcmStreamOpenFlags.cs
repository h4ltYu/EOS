using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000179 RID: 377
	[Flags]
	internal enum AcmStreamOpenFlags
	{
		/// <summary>
		/// ACM_STREAMOPENF_QUERY, ACM will be queried to determine whether it supports the given conversion. A conversion stream will not be opened, and no handle will be returned in the phas parameter. 
		/// </summary>
		// Token: 0x0400084E RID: 2126
		Query = 1,
		/// <summary>
		/// ACM_STREAMOPENF_ASYNC, Stream conversion should be performed asynchronously. If this flag is specified, the application can use a callback function to be notified when the conversion stream is opened and closed and after each buffer is converted. In addition to using a callback function, an application can examine the fdwStatus member of the ACMSTREAMHEADER structure for the ACMSTREAMHEADER_STATUSF_DONE flag. 
		/// </summary>
		// Token: 0x0400084F RID: 2127
		Async = 2,
		/// <summary>
		/// ACM_STREAMOPENF_NONREALTIME, ACM will not consider time constraints when converting the data. By default, the driver will attempt to convert the data in real time. For some formats, specifying this flag might improve the audio quality or other characteristics.
		/// </summary>
		// Token: 0x04000850 RID: 2128
		NonRealTime = 4,
		/// <summary>
		/// CALLBACK_TYPEMASK, callback type mask
		/// </summary>
		// Token: 0x04000851 RID: 2129
		CallbackTypeMask = 458752,
		/// <summary>
		/// CALLBACK_NULL, no callback
		/// </summary>
		// Token: 0x04000852 RID: 2130
		CallbackNull = 0,
		/// <summary>
		/// CALLBACK_WINDOW, dwCallback is a HWND
		/// </summary>
		// Token: 0x04000853 RID: 2131
		CallbackWindow = 65536,
		/// <summary>
		/// CALLBACK_TASK, dwCallback is a HTASK
		/// </summary>
		// Token: 0x04000854 RID: 2132
		CallbackTask = 131072,
		/// <summary>
		/// CALLBACK_FUNCTION, dwCallback is a FARPROC
		/// </summary>
		// Token: 0x04000855 RID: 2133
		CallbackFunction = 196608,
		/// <summary>
		/// CALLBACK_THREAD, thread ID replaces 16 bit task
		/// </summary>
		// Token: 0x04000856 RID: 2134
		CallbackThread = 131072,
		/// <summary>
		/// CALLBACK_EVENT, dwCallback is an EVENT Handle
		/// </summary>
		// Token: 0x04000857 RID: 2135
		CallbackEvent = 327680
	}
}
