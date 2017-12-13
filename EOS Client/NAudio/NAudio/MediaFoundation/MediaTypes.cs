using System;
using NAudio.Utils;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Major Media Types
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/aa367377%28v=vs.85%29.aspx
	/// </summary>
	// Token: 0x02000043 RID: 67
	public static class MediaTypes
	{
		/// <summary>
		/// Default
		/// </summary>
		// Token: 0x04000233 RID: 563
		public static readonly Guid MFMediaType_Default = new Guid("81A412E6-8103-4B06-857F-1862781024AC");

		/// <summary>
		/// Audio
		/// </summary>
		// Token: 0x04000234 RID: 564
		[FieldDescription("Audio")]
		public static readonly Guid MFMediaType_Audio = new Guid("73647561-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Video
		/// </summary>
		// Token: 0x04000235 RID: 565
		[FieldDescription("Video")]
		public static readonly Guid MFMediaType_Video = new Guid("73646976-0000-0010-8000-00aa00389b71");

		/// <summary>
		/// Protected Media
		/// </summary>
		// Token: 0x04000236 RID: 566
		[FieldDescription("Protected Media")]
		public static readonly Guid MFMediaType_Protected = new Guid("7b4b6fe6-9d04-4494-be14-7e0bd076c8e4");

		/// <summary>
		/// Synchronized Accessible Media Interchange (SAMI) captions.
		/// </summary>
		// Token: 0x04000237 RID: 567
		[FieldDescription("SAMI captions")]
		public static readonly Guid MFMediaType_SAMI = new Guid("e69669a0-3dcd-40cb-9e2e-3708387c0616");

		/// <summary>
		/// Script stream
		/// </summary>
		// Token: 0x04000238 RID: 568
		[FieldDescription("Script stream")]
		public static readonly Guid MFMediaType_Script = new Guid("72178c22-e45b-11d5-bc2a-00b0d0f3f4ab");

		/// <summary>
		/// Still image stream.
		/// </summary>
		// Token: 0x04000239 RID: 569
		[FieldDescription("Still image stream")]
		public static readonly Guid MFMediaType_Image = new Guid("72178c23-e45b-11d5-bc2a-00b0d0f3f4ab");

		/// <summary>
		/// HTML stream.
		/// </summary>
		// Token: 0x0400023A RID: 570
		[FieldDescription("HTML stream")]
		public static readonly Guid MFMediaType_HTML = new Guid("72178c24-e45b-11d5-bc2a-00b0d0f3f4ab");

		/// <summary>
		/// Binary stream.
		/// </summary>
		// Token: 0x0400023B RID: 571
		[FieldDescription("Binary stream")]
		public static readonly Guid MFMediaType_Binary = new Guid("72178c25-e45b-11d5-bc2a-00b0d0f3f4ab");

		/// <summary>
		/// A stream that contains data files.
		/// </summary>
		// Token: 0x0400023C RID: 572
		[FieldDescription("File transfer")]
		public static readonly Guid MFMediaType_FileTransfer = new Guid("72178c26-e45b-11d5-bc2a-00b0d0f3f4ab");
	}
}
