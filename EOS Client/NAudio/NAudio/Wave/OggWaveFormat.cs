using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x020001A7 RID: 423
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	internal class OggWaveFormat : WaveFormat
	{
		// Token: 0x040009CC RID: 2508
		public uint dwVorbisACMVersion;

		// Token: 0x040009CD RID: 2509
		public uint dwLibVorbisVersion;
	}
}
