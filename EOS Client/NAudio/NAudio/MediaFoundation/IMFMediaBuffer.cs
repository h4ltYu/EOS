using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// IMFMediaBuffer
	/// http://msdn.microsoft.com/en-gb/library/windows/desktop/ms696261%28v=vs.85%29.aspx
	/// </summary>
	// Token: 0x02000051 RID: 81
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("045FA593-8799-42b8-BC8D-8968C6453507")]
	[ComImport]
	public interface IMFMediaBuffer
	{
		/// <summary>
		/// Gives the caller access to the memory in the buffer.
		/// </summary>
		// Token: 0x060001DB RID: 475
		void Lock(out IntPtr ppbBuffer, out int pcbMaxLength, out int pcbCurrentLength);

		/// <summary>
		/// Unlocks a buffer that was previously locked.
		/// </summary>
		// Token: 0x060001DC RID: 476
		void Unlock();

		/// <summary>
		/// Retrieves the length of the valid data in the buffer.
		/// </summary>
		// Token: 0x060001DD RID: 477
		void GetCurrentLength(out int pcbCurrentLength);

		/// <summary>
		/// Sets the length of the valid data in the buffer.
		/// </summary>
		// Token: 0x060001DE RID: 478
		void SetCurrentLength(int cbCurrentLength);

		/// <summary>
		/// Retrieves the allocated size of the buffer.
		/// </summary>
		// Token: 0x060001DF RID: 479
		void GetMaxLength(out int pcbMaxLength);
	}
}
