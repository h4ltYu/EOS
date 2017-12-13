using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000155 RID: 341
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ASIOBufferInfo
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x0001658D File Offset: 0x0001478D
		public IntPtr Buffer(int bufferIndex)
		{
			if (bufferIndex != 0)
			{
				return this.pBuffer1;
			}
			return this.pBuffer0;
		}

		// Token: 0x04000781 RID: 1921
		public bool isInput;

		// Token: 0x04000782 RID: 1922
		public int channelNum;

		// Token: 0x04000783 RID: 1923
		public IntPtr pBuffer0;

		// Token: 0x04000784 RID: 1924
		public IntPtr pBuffer1;
	}
}
