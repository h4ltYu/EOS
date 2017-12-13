using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Represents a Xing VBR header
	/// </summary>
	// Token: 0x020000AB RID: 171
	public class XingHeader
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0000D16C File Offset: 0x0000B36C
		private static int ReadBigEndian(byte[] buffer, int offset)
		{
			int num = (int)buffer[offset];
			num <<= 8;
			num |= (int)buffer[offset + 1];
			num <<= 8;
			num |= (int)buffer[offset + 2];
			num <<= 8;
			return num | (int)buffer[offset + 3];
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		private void WriteBigEndian(byte[] buffer, int offset, int value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			for (int i = 0; i < 4; i++)
			{
				buffer[offset + 4 - i] = bytes[i];
			}
		}

		/// <summary>
		/// Load Xing Header
		/// </summary>
		/// <param name="frame">Frame</param>
		/// <returns>Xing Header</returns>
		// Token: 0x060003C9 RID: 969 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		public static XingHeader LoadXingHeader(Mp3Frame frame)
		{
			XingHeader xingHeader = new XingHeader();
			xingHeader.frame = frame;
			int num;
			if (frame.MpegVersion == MpegVersion.Version1)
			{
				if (frame.ChannelMode != ChannelMode.Mono)
				{
					num = 36;
				}
				else
				{
					num = 21;
				}
			}
			else
			{
				if (frame.MpegVersion != MpegVersion.Version2)
				{
					return null;
				}
				if (frame.ChannelMode != ChannelMode.Mono)
				{
					num = 21;
				}
				else
				{
					num = 13;
				}
			}
			if (frame.RawData[num] == 88 && frame.RawData[num + 1] == 105 && frame.RawData[num + 2] == 110 && frame.RawData[num + 3] == 103)
			{
				xingHeader.startOffset = num;
				num += 4;
				XingHeader.XingHeaderOptions xingHeaderOptions = (XingHeader.XingHeaderOptions)XingHeader.ReadBigEndian(frame.RawData, num);
				num += 4;
				if ((xingHeaderOptions & XingHeader.XingHeaderOptions.Frames) != (XingHeader.XingHeaderOptions)0)
				{
					xingHeader.framesOffset = num;
					num += 4;
				}
				if ((xingHeaderOptions & XingHeader.XingHeaderOptions.Bytes) != (XingHeader.XingHeaderOptions)0)
				{
					xingHeader.bytesOffset = num;
					num += 4;
				}
				if ((xingHeaderOptions & XingHeader.XingHeaderOptions.Toc) != (XingHeader.XingHeaderOptions)0)
				{
					xingHeader.tocOffset = num;
					num += 100;
				}
				if ((xingHeaderOptions & XingHeader.XingHeaderOptions.VbrScale) != (XingHeader.XingHeaderOptions)0)
				{
					xingHeader.vbrScale = XingHeader.ReadBigEndian(frame.RawData, num);
					num += 4;
				}
				xingHeader.endOffset = num;
				return xingHeader;
			}
			return null;
		}

		/// <summary>
		/// Sees if a frame contains a Xing header
		/// </summary>
		// Token: 0x060003CA RID: 970 RVA: 0x0000D2D0 File Offset: 0x0000B4D0
		private XingHeader()
		{
		}

		/// <summary>
		/// Number of frames
		/// </summary>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000D317 File Offset: 0x0000B517
		public int Frames
		{
			get
			{
				if (this.framesOffset == -1)
				{
					return -1;
				}
				return XingHeader.ReadBigEndian(this.frame.RawData, this.framesOffset);
			}
			set
			{
				if (this.framesOffset == -1)
				{
					throw new InvalidOperationException("Frames flag is not set");
				}
				this.WriteBigEndian(this.frame.RawData, this.framesOffset, value);
			}
		}

		/// <summary>
		/// Number of bytes
		/// </summary>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000D345 File Offset: 0x0000B545
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000D368 File Offset: 0x0000B568
		public int Bytes
		{
			get
			{
				if (this.bytesOffset == -1)
				{
					return -1;
				}
				return XingHeader.ReadBigEndian(this.frame.RawData, this.bytesOffset);
			}
			set
			{
				if (this.framesOffset == -1)
				{
					throw new InvalidOperationException("Bytes flag is not set");
				}
				this.WriteBigEndian(this.frame.RawData, this.bytesOffset, value);
			}
		}

		/// <summary>
		/// VBR Scale property
		/// </summary>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000D396 File Offset: 0x0000B596
		public int VbrScale
		{
			get
			{
				return this.vbrScale;
			}
		}

		/// <summary>
		/// The MP3 frame
		/// </summary>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000D39E File Offset: 0x0000B59E
		public Mp3Frame Mp3Frame
		{
			get
			{
				return this.frame;
			}
		}

		// Token: 0x04000483 RID: 1155
		private static int[] sr_table = new int[]
		{
			44100,
			48000,
			32000,
			99999
		};

		// Token: 0x04000484 RID: 1156
		private int vbrScale = -1;

		// Token: 0x04000485 RID: 1157
		private int startOffset;

		// Token: 0x04000486 RID: 1158
		private int endOffset;

		// Token: 0x04000487 RID: 1159
		private int tocOffset = -1;

		// Token: 0x04000488 RID: 1160
		private int framesOffset = -1;

		// Token: 0x04000489 RID: 1161
		private int bytesOffset = -1;

		// Token: 0x0400048A RID: 1162
		private Mp3Frame frame;

		// Token: 0x020000AC RID: 172
		[Flags]
		private enum XingHeaderOptions
		{
			// Token: 0x0400048C RID: 1164
			Frames = 1,
			// Token: 0x0400048D RID: 1165
			Bytes = 2,
			// Token: 0x0400048E RID: 1166
			Toc = 4,
			// Token: 0x0400048F RID: 1167
			VbrScale = 8
		}
	}
}
