using System;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000122 RID: 290
	internal struct Blob
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x000149EC File Offset: 0x00012BEC
		private void FixCS0649()
		{
			this.Length = 0;
			this.Data = IntPtr.Zero;
		}

		// Token: 0x04000703 RID: 1795
		public int Length;

		// Token: 0x04000704 RID: 1796
		public IntPtr Data;
	}
}
