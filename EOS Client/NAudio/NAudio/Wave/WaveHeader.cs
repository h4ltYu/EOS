using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// WaveHeader interop structure (WAVEHDR)
	/// http://msdn.microsoft.com/en-us/library/dd743837%28VS.85%29.aspx
	/// </summary>
	// Token: 0x02000180 RID: 384
	[StructLayout(LayoutKind.Sequential)]
	internal class WaveHeader
	{
		/// <summary>pointer to locked data buffer (lpData)</summary>
		// Token: 0x04000924 RID: 2340
		public IntPtr dataBuffer;

		/// <summary>length of data buffer (dwBufferLength)</summary>
		// Token: 0x04000925 RID: 2341
		public int bufferLength;

		/// <summary>used for input only (dwBytesRecorded)</summary>
		// Token: 0x04000926 RID: 2342
		public int bytesRecorded;

		/// <summary>for client's use (dwUser)</summary>
		// Token: 0x04000927 RID: 2343
		public IntPtr userData;

		/// <summary>assorted flags (dwFlags)</summary>
		// Token: 0x04000928 RID: 2344
		public WaveHeaderFlags flags;

		/// <summary>loop control counter (dwLoops)</summary>
		// Token: 0x04000929 RID: 2345
		public int loops;

		/// <summary>PWaveHdr, reserved for driver (lpNext)</summary>
		// Token: 0x0400092A RID: 2346
		public IntPtr next;

		/// <summary>reserved for driver</summary>
		// Token: 0x0400092B RID: 2347
		public IntPtr reserved;
	}
}
