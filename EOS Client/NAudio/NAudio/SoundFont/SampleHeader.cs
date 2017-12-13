using System;

namespace NAudio.SoundFont
{
	/// <summary>
	/// A SoundFont Sample Header
	/// </summary>
	// Token: 0x020000C3 RID: 195
	public class SampleHeader
	{
		/// <summary>
		/// <see cref="M:System.Object.ToString" />
		/// </summary>
		// Token: 0x0600044D RID: 1101 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		public override string ToString()
		{
			return this.SampleName;
		}

		/// <summary>
		/// The sample name
		/// </summary>
		// Token: 0x04000518 RID: 1304
		public string SampleName;

		/// <summary>
		/// Start offset
		/// </summary>
		// Token: 0x04000519 RID: 1305
		public uint Start;

		/// <summary>
		/// End offset
		/// </summary>
		// Token: 0x0400051A RID: 1306
		public uint End;

		/// <summary>
		/// Start loop point
		/// </summary>
		// Token: 0x0400051B RID: 1307
		public uint StartLoop;

		/// <summary>
		/// End loop point
		/// </summary>
		// Token: 0x0400051C RID: 1308
		public uint EndLoop;

		/// <summary>
		/// Sample Rate
		/// </summary>
		// Token: 0x0400051D RID: 1309
		public uint SampleRate;

		/// <summary>
		/// Original pitch
		/// </summary>
		// Token: 0x0400051E RID: 1310
		public byte OriginalPitch;

		/// <summary>
		/// Pitch correction
		/// </summary>
		// Token: 0x0400051F RID: 1311
		public sbyte PitchCorrection;

		/// <summary>
		/// Sample Link
		/// </summary>
		// Token: 0x04000520 RID: 1312
		public ushort SampleLink;

		/// <summary>
		/// SoundFont Sample Link Type
		/// </summary>
		// Token: 0x04000521 RID: 1313
		public SFSampleLink SFSampleLink;
	}
}
