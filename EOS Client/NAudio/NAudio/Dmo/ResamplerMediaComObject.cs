using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	/// <summary>
	/// From wmcodecsdp.h
	/// Implements:
	/// - IMediaObject 
	/// - IMFTransform (Media foundation - we will leave this for now as there is loads of MF stuff)
	/// - IPropertyStore 
	/// - IWMResamplerProps 
	/// Can resample PCM or IEEE
	/// </summary>
	// Token: 0x0200020E RID: 526
	[Guid("f447b69e-1884-4a7e-8055-346f74d6edb3")]
	[ComImport]
	internal class ResamplerMediaComObject
	{
		// Token: 0x06000C05 RID: 3077
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ResamplerMediaComObject();
	}
}
