using System;
using System.IO;

namespace NAudio.SoundFont
{
	// Token: 0x020000C2 RID: 194
	internal class SampleDataChunk
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x0000E75C File Offset: 0x0000C95C
		public SampleDataChunk(RiffChunk chunk)
		{
			string text = chunk.ReadChunkID();
			if (text != "sdta")
			{
				throw new InvalidDataException(string.Format("Not a sample data chunk ({0})", text));
			}
			this.sampleData = chunk.GetData();
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		public byte[] SampleData
		{
			get
			{
				return this.sampleData;
			}
		}

		// Token: 0x04000517 RID: 1303
		private byte[] sampleData;
	}
}
