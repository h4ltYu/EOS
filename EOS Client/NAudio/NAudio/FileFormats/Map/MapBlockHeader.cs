using System;
using System.IO;

namespace NAudio.FileFormats.Map
{
	// Token: 0x020000A2 RID: 162
	internal class MapBlockHeader
	{
		// Token: 0x0600038D RID: 909 RVA: 0x0000C31C File Offset: 0x0000A51C
		public static MapBlockHeader Read(BinaryReader reader)
		{
			return new MapBlockHeader
			{
				length = reader.ReadInt32(),
				value2 = reader.ReadInt32(),
				value3 = reader.ReadInt16(),
				value4 = reader.ReadInt16()
			};
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000C360 File Offset: 0x0000A560
		public override string ToString()
		{
			return string.Format("{0} {1:X8} {2:X4} {3:X4}", new object[]
			{
				this.length,
				this.value2,
				this.value3,
				this.value4
			});
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000C3B7 File Offset: 0x0000A5B7
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x04000452 RID: 1106
		private int length;

		// Token: 0x04000453 RID: 1107
		private int value2;

		// Token: 0x04000454 RID: 1108
		private short value3;

		// Token: 0x04000455 RID: 1109
		private short value4;
	}
}
