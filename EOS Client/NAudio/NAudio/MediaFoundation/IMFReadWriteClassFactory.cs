using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Creates an instance of either the sink writer or the source reader.
	/// </summary>
	// Token: 0x02000040 RID: 64
	[Guid("E7FE2E12-661C-40DA-92F9-4F002AB67627")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IMFReadWriteClassFactory
	{
		/// <summary>
		/// Creates an instance of the sink writer or source reader, given a URL.
		/// </summary>
		// Token: 0x06000120 RID: 288
		void CreateInstanceFromURL([MarshalAs(UnmanagedType.LPStruct)] [In] Guid clsid, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwszURL, [MarshalAs(UnmanagedType.Interface)] [In] IMFAttributes pAttributes, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObject);

		/// <summary>
		/// Creates an instance of the sink writer or source reader, given an IUnknown pointer. 
		/// </summary>
		// Token: 0x06000121 RID: 289
		void CreateInstanceFromObject([MarshalAs(UnmanagedType.LPStruct)] [In] Guid clsid, [MarshalAs(UnmanagedType.IUnknown)] [In] object punkObject, [MarshalAs(UnmanagedType.Interface)] [In] IMFAttributes pAttributes, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObject);
	}
}
