using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x0200017F RID: 383
	[StructLayout(LayoutKind.Explicit)]
	internal struct MmTime
	{
		// Token: 0x04000913 RID: 2323
		public const int TIME_MS = 1;

		// Token: 0x04000914 RID: 2324
		public const int TIME_SAMPLES = 2;

		// Token: 0x04000915 RID: 2325
		public const int TIME_BYTES = 4;

		// Token: 0x04000916 RID: 2326
		[FieldOffset(0)]
		public uint wType;

		// Token: 0x04000917 RID: 2327
		[FieldOffset(4)]
		public uint ms;

		// Token: 0x04000918 RID: 2328
		[FieldOffset(4)]
		public uint sample;

		// Token: 0x04000919 RID: 2329
		[FieldOffset(4)]
		public uint cb;

		// Token: 0x0400091A RID: 2330
		[FieldOffset(4)]
		public uint ticks;

		// Token: 0x0400091B RID: 2331
		[FieldOffset(4)]
		public byte smpteHour;

		// Token: 0x0400091C RID: 2332
		[FieldOffset(5)]
		public byte smpteMin;

		// Token: 0x0400091D RID: 2333
		[FieldOffset(6)]
		public byte smpteSec;

		// Token: 0x0400091E RID: 2334
		[FieldOffset(7)]
		public byte smpteFrame;

		// Token: 0x0400091F RID: 2335
		[FieldOffset(8)]
		public byte smpteFps;

		// Token: 0x04000920 RID: 2336
		[FieldOffset(9)]
		public byte smpteDummy;

		// Token: 0x04000921 RID: 2337
		[FieldOffset(10)]
		public byte smptePad0;

		// Token: 0x04000922 RID: 2338
		[FieldOffset(11)]
		public byte smptePad1;

		// Token: 0x04000923 RID: 2339
		[FieldOffset(4)]
		public uint midiSongPtrPos;
	}
}
