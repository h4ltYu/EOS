using System;
using System.IO;

namespace NAudio.Wave
{
	/// <summary>
	/// Represents an MP3 Frame
	/// </summary>
	// Token: 0x020000A7 RID: 167
	public class Mp3Frame
	{
		/// <summary>
		/// Reads an MP3 frame from a stream
		/// </summary>
		/// <param name="input">input stream</param>
		/// <returns>A valid MP3 frame, or null if none found</returns>
		// Token: 0x060003A2 RID: 930 RVA: 0x0000CA43 File Offset: 0x0000AC43
		public static Mp3Frame LoadFromStream(Stream input)
		{
			return Mp3Frame.LoadFromStream(input, true);
		}

		/// <summary>Reads an MP3Frame from a stream</summary>
		/// <remarks>http://mpgedit.org/mpgedit/mpeg_format/mpeghdr.htm has some good info
		/// also see http://www.codeproject.com/KB/audio-video/mpegaudioinfo.aspx
		/// </remarks>
		/// <returns>A valid MP3 frame, or null if none found</returns>
		// Token: 0x060003A3 RID: 931 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		public static Mp3Frame LoadFromStream(Stream input, bool readData)
		{
			Mp3Frame mp3Frame = new Mp3Frame();
			mp3Frame.FileOffset = input.Position;
			byte[] array = new byte[4];
			int num = input.Read(array, 0, array.Length);
			if (num < array.Length)
			{
				return null;
			}
			while (!Mp3Frame.IsValidHeader(array, mp3Frame))
			{
				array[0] = array[1];
				array[1] = array[2];
				array[2] = array[3];
				num = input.Read(array, 3, 1);
				if (num < 1)
				{
					return null;
				}
				mp3Frame.FileOffset += 1L;
			}
			int num2 = mp3Frame.FrameLength - 4;
			if (readData)
			{
				mp3Frame.RawData = new byte[mp3Frame.FrameLength];
				Array.Copy(array, mp3Frame.RawData, 4);
				num = input.Read(mp3Frame.RawData, 4, num2);
				if (num < num2)
				{
					throw new EndOfStreamException("Unexpected end of stream before frame complete");
				}
			}
			else
			{
				input.Position += (long)num2;
			}
			return mp3Frame;
		}

		/// <summary>
		/// Constructs an MP3 frame
		/// </summary>
		// Token: 0x060003A4 RID: 932 RVA: 0x0000CB18 File Offset: 0x0000AD18
		private Mp3Frame()
		{
		}

		/// <summary>
		/// checks if the four bytes represent a valid header,
		/// if they are, will parse the values into Mp3Frame
		/// </summary>
		// Token: 0x060003A5 RID: 933 RVA: 0x0000CB20 File Offset: 0x0000AD20
		private static bool IsValidHeader(byte[] headerBytes, Mp3Frame frame)
		{
			if (headerBytes[0] != 255 || (headerBytes[1] & 224) != 224)
			{
				return false;
			}
			frame.MpegVersion = (MpegVersion)((headerBytes[1] & 24) >> 3);
			if (frame.MpegVersion == MpegVersion.Reserved)
			{
				return false;
			}
			frame.MpegLayer = (MpegLayer)((headerBytes[1] & 6) >> 1);
			if (frame.MpegLayer == MpegLayer.Reserved)
			{
				return false;
			}
			int num = (frame.MpegLayer == MpegLayer.Layer1) ? 0 : ((frame.MpegLayer == MpegLayer.Layer2) ? 1 : 2);
			frame.CrcPresent = ((headerBytes[1] & 1) == 0);
			frame.BitRateIndex = (headerBytes[2] & 240) >> 4;
			if (frame.BitRateIndex == 15)
			{
				return false;
			}
			int num2 = (frame.MpegVersion == MpegVersion.Version1) ? 0 : 1;
			frame.BitRate = Mp3Frame.bitRates[num2, num, frame.BitRateIndex] * 1000;
			if (frame.BitRate == 0)
			{
				return false;
			}
			int num3 = (headerBytes[2] & 12) >> 2;
			if (num3 == 3)
			{
				return false;
			}
			if (frame.MpegVersion == MpegVersion.Version1)
			{
				frame.SampleRate = Mp3Frame.sampleRatesVersion1[num3];
			}
			else if (frame.MpegVersion == MpegVersion.Version2)
			{
				frame.SampleRate = Mp3Frame.sampleRatesVersion2[num3];
			}
			else
			{
				frame.SampleRate = Mp3Frame.sampleRatesVersion25[num3];
			}
			bool flag = (headerBytes[2] & 2) == 2;
			byte b = headerBytes[2];
			frame.ChannelMode = (ChannelMode)((headerBytes[3] & 192) >> 6);
			frame.ChannelExtension = (headerBytes[3] & 48) >> 4;
			if (frame.ChannelExtension != 0 && frame.ChannelMode != ChannelMode.JointStereo)
			{
				return false;
			}
			frame.Copyright = ((headerBytes[3] & 8) == 8);
			byte b2 = headerBytes[3];
			byte b3 = headerBytes[3];
			int num4 = flag ? 1 : 0;
			frame.SampleCount = Mp3Frame.samplesPerFrame[num2, num];
			int num5 = frame.SampleCount / 8;
			if (frame.MpegLayer == MpegLayer.Layer1)
			{
				frame.FrameLength = (num5 * frame.BitRate / frame.SampleRate + num4) * 4;
			}
			else
			{
				frame.FrameLength = num5 * frame.BitRate / frame.SampleRate + num4;
			}
			return frame.FrameLength <= 16384;
		}

		/// <summary>
		/// Sample rate of this frame
		/// </summary>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000CD0F File Offset: 0x0000AF0F
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000CD17 File Offset: 0x0000AF17
		public int SampleRate { get; private set; }

		/// <summary>
		/// Frame length in bytes
		/// </summary>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000CD20 File Offset: 0x0000AF20
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000CD28 File Offset: 0x0000AF28
		public int FrameLength { get; private set; }

		/// <summary>
		/// Bit Rate
		/// </summary>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000CD31 File Offset: 0x0000AF31
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000CD39 File Offset: 0x0000AF39
		public int BitRate { get; private set; }

		/// <summary>
		/// Raw frame data (includes header bytes)
		/// </summary>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000CD42 File Offset: 0x0000AF42
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0000CD4A File Offset: 0x0000AF4A
		public byte[] RawData { get; private set; }

		/// <summary>
		/// MPEG Version
		/// </summary>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000CD53 File Offset: 0x0000AF53
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0000CD5B File Offset: 0x0000AF5B
		public MpegVersion MpegVersion { get; private set; }

		/// <summary>
		/// MPEG Layer
		/// </summary>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000CD64 File Offset: 0x0000AF64
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		public MpegLayer MpegLayer { get; private set; }

		/// <summary>
		/// Channel Mode
		/// </summary>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000CD75 File Offset: 0x0000AF75
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000CD7D File Offset: 0x0000AF7D
		public ChannelMode ChannelMode { get; private set; }

		/// <summary>
		/// The number of samples in this frame
		/// </summary>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000CD86 File Offset: 0x0000AF86
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000CD8E File Offset: 0x0000AF8E
		public int SampleCount { get; private set; }

		/// <summary>
		/// The channel extension bits
		/// </summary>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000CD97 File Offset: 0x0000AF97
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000CD9F File Offset: 0x0000AF9F
		public int ChannelExtension { get; private set; }

		/// <summary>
		/// The bitrate index (directly from the header)
		/// </summary>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000CDA8 File Offset: 0x0000AFA8
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		public int BitRateIndex { get; private set; }

		/// <summary>
		/// Whether the Copyright bit is set
		/// </summary>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000CDB9 File Offset: 0x0000AFB9
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000CDC1 File Offset: 0x0000AFC1
		public bool Copyright { get; private set; }

		/// <summary>
		/// Whether a CRC is present
		/// </summary>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000CDCA File Offset: 0x0000AFCA
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000CDD2 File Offset: 0x0000AFD2
		public bool CrcPresent { get; private set; }

		/// <summary>
		/// Not part of the MP3 frame itself - indicates where in the stream we found this header
		/// </summary>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000CDDB File Offset: 0x0000AFDB
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000CDE3 File Offset: 0x0000AFE3
		public long FileOffset { get; private set; }

		// Token: 0x04000463 RID: 1123
		private const int MaxFrameLength = 16384;

		// Token: 0x04000464 RID: 1124
		private static readonly int[,,] bitRates = new int[,,]
		{
			{
				{
					0,
					32,
					64,
					96,
					128,
					160,
					192,
					224,
					256,
					288,
					320,
					352,
					384,
					416,
					448
				},
				{
					0,
					32,
					48,
					56,
					64,
					80,
					96,
					112,
					128,
					160,
					192,
					224,
					256,
					320,
					384
				},
				{
					0,
					32,
					40,
					48,
					56,
					64,
					80,
					96,
					112,
					128,
					160,
					192,
					224,
					256,
					320
				}
			},
			{
				{
					0,
					32,
					48,
					56,
					64,
					80,
					96,
					112,
					128,
					144,
					160,
					176,
					192,
					224,
					256
				},
				{
					0,
					8,
					16,
					24,
					32,
					40,
					48,
					56,
					64,
					80,
					96,
					112,
					128,
					144,
					160
				},
				{
					0,
					8,
					16,
					24,
					32,
					40,
					48,
					56,
					64,
					80,
					96,
					112,
					128,
					144,
					160
				}
			}
		};

		// Token: 0x04000465 RID: 1125
		private static readonly int[,] samplesPerFrame = new int[,]
		{
			{
				384,
				1152,
				1152
			},
			{
				384,
				1152,
				576
			}
		};

		// Token: 0x04000466 RID: 1126
		private static readonly int[] sampleRatesVersion1 = new int[]
		{
			44100,
			48000,
			32000
		};

		// Token: 0x04000467 RID: 1127
		private static readonly int[] sampleRatesVersion2 = new int[]
		{
			22050,
			24000,
			16000
		};

		// Token: 0x04000468 RID: 1128
		private static readonly int[] sampleRatesVersion25 = new int[]
		{
			11025,
			12000,
			8000
		};
	}
}
