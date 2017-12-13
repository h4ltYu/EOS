using System;

namespace NAudio.Codecs
{
	/// <summary>
	/// A-law encoder
	/// </summary>
	// Token: 0x02000003 RID: 3
	public static class ALawEncoder
	{
		/// <summary>
		/// Encodes a single 16 bit sample to a-law
		/// </summary>
		/// <param name="sample">16 bit PCM sample</param>
		/// <returns>a-law encoded byte</returns>
		// Token: 0x06000004 RID: 4 RVA: 0x00002284 File Offset: 0x00000484
		public static byte LinearToALawSample(short sample)
		{
			int num = ~sample >> 8 & 128;
			if (num == 0)
			{
				sample = -sample;
			}
			if (sample > 32635)
			{
				sample = 32635;
			}
			byte b;
			if (sample >= 256)
			{
				int num2 = (int)ALawEncoder.ALawCompressTable[sample >> 8 & 127];
				int num3 = sample >> num2 + 3 & 15;
				b = (byte)(num2 << 4 | num3);
			}
			else
			{
				b = (byte)(sample >> 4);
			}
			return b ^ (byte)(num ^ 85);
		}

		// Token: 0x04000002 RID: 2
		private const int cBias = 132;

		// Token: 0x04000003 RID: 3
		private const int cClip = 32635;

		// Token: 0x04000004 RID: 4
		private static readonly byte[] ALawCompressTable = new byte[]
		{
			1,
			1,
			2,
			2,
			3,
			3,
			3,
			3,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7
		};
	}
}
