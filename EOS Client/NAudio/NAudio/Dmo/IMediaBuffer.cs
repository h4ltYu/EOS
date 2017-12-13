using System;
using System.Runtime.InteropServices;
using System.Security;

namespace NAudio.Dmo
{
	/// <summary>
	/// IMediaBuffer Interface
	/// </summary>
	// Token: 0x0200008E RID: 142
	[SuppressUnmanagedCodeSecurity]
	[Guid("59eff8b9-938c-4a26-82f2-95cb84cdc837")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IMediaBuffer
	{
		/// <summary>
		/// Set Length
		/// </summary>
		/// <param name="length">Length</param>
		/// <returns>HRESULT</returns>
		// Token: 0x06000306 RID: 774
		[PreserveSig]
		int SetLength(int length);

		/// <summary>
		/// Get Max Length
		/// </summary>
		/// <param name="maxLength">Max Length</param>
		/// <returns>HRESULT</returns>
		// Token: 0x06000307 RID: 775
		[PreserveSig]
		int GetMaxLength(out int maxLength);

		/// <summary>
		/// Get Buffer and Length
		/// </summary>
		/// <param name="bufferPointerPointer">Pointer to variable into which to write the Buffer Pointer </param>
		/// <param name="validDataLengthPointer">Pointer to variable into which to write the Valid Data Length</param>
		/// <returns>HRESULT</returns>
		// Token: 0x06000308 RID: 776
		[PreserveSig]
		int GetBufferAndLength(IntPtr bufferPointerPointer, IntPtr validDataLengthPointer);
	}
}
