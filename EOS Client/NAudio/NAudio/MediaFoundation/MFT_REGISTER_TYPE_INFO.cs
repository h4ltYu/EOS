using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Contains media type information for registering a Media Foundation transform (MFT). 
	/// </summary>
	// Token: 0x0200005B RID: 91
	[StructLayout(LayoutKind.Sequential)]
	public class MFT_REGISTER_TYPE_INFO
	{
		/// <summary>
		/// The major media type.
		/// </summary>
		// Token: 0x04000302 RID: 770
		public Guid guidMajorType;

		/// <summary>
		/// The Media Subtype
		/// </summary>
		// Token: 0x04000303 RID: 771
		public Guid guidSubtype;
	}
}
