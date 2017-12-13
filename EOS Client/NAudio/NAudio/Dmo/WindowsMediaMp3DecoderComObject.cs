using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	/// <summary>
	/// implements IMediaObject  (DirectX Media Object)
	/// implements IMFTransform (Media Foundation Transform)
	/// On Windows XP, it is always an MM (if present at all)
	/// </summary>
	// Token: 0x02000096 RID: 150
	[Guid("bbeea841-0a63-4f52-a7ab-a9b3a84ed38a")]
	[ComImport]
	internal class WindowsMediaMp3DecoderComObject
	{
		// Token: 0x0600033B RID: 827
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern WindowsMediaMp3DecoderComObject();
	}
}
