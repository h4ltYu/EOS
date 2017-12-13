using System;
using System.IO;
using System.Runtime.InteropServices;
using NAudio.Dmo;

namespace NAudio.Wave
{
	/// <summary>
	/// WaveFormatExtensible
	/// http://www.microsoft.com/whdc/device/audio/multichaud.mspx
	/// </summary>
	// Token: 0x020001AC RID: 428
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public class WaveFormatExtensible : WaveFormat
	{
		/// <summary>
		/// Parameterless constructor for marshalling
		/// </summary>
		// Token: 0x060008B4 RID: 2228 RVA: 0x0001915B File Offset: 0x0001735B
		private WaveFormatExtensible()
		{
		}

		/// <summary>
		/// Creates a new WaveFormatExtensible for PCM or IEEE
		/// </summary>
		// Token: 0x060008B5 RID: 2229 RVA: 0x00019164 File Offset: 0x00017364
		public WaveFormatExtensible(int rate, int bits, int channels) : base(rate, bits, channels)
		{
			this.waveFormatTag = WaveFormatEncoding.Extensible;
			this.extraSize = 22;
			this.wValidBitsPerSample = (short)bits;
			for (int i = 0; i < channels; i++)
			{
				this.dwChannelMask |= 1 << i;
			}
			if (bits == 32)
			{
				this.subFormat = AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT;
				return;
			}
			this.subFormat = AudioMediaSubtypes.MEDIASUBTYPE_PCM;
		}

		/// <summary>
		/// WaveFormatExtensible for PCM or floating point can be awkward to work with
		/// This creates a regular WaveFormat structure representing the same audio format
		/// </summary>
		/// <returns></returns>
		// Token: 0x060008B6 RID: 2230 RVA: 0x000191D0 File Offset: 0x000173D0
		public WaveFormat ToStandardWaveFormat()
		{
			if (this.subFormat == AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT && this.bitsPerSample == 32)
			{
				return WaveFormat.CreateIeeeFloatWaveFormat(this.sampleRate, (int)this.channels);
			}
			if (this.subFormat == AudioMediaSubtypes.MEDIASUBTYPE_PCM)
			{
				return new WaveFormat(this.sampleRate, (int)this.bitsPerSample, (int)this.channels);
			}
			throw new InvalidOperationException("Not a recognised PCM or IEEE float format");
		}

		/// <summary>
		/// SubFormat (may be one of AudioMediaSubtypes)
		/// </summary>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0001923F File Offset: 0x0001743F
		public Guid SubFormat
		{
			get
			{
				return this.subFormat;
			}
		}

		/// <summary>
		/// Serialize
		/// </summary>
		/// <param name="writer"></param>
		// Token: 0x060008B8 RID: 2232 RVA: 0x00019248 File Offset: 0x00017448
		public override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.wValidBitsPerSample);
			writer.Write(this.dwChannelMask);
			byte[] array = this.subFormat.ToByteArray();
			writer.Write(array, 0, array.Length);
		}

		/// <summary>
		/// String representation
		/// </summary>
		// Token: 0x060008B9 RID: 2233 RVA: 0x0001928C File Offset: 0x0001748C
		public override string ToString()
		{
			return string.Format("{0} wBitsPerSample:{1} dwChannelMask:{2} subFormat:{3} extraSize:{4}", new object[]
			{
				base.ToString(),
				this.wValidBitsPerSample,
				this.dwChannelMask,
				this.subFormat,
				this.extraSize
			});
		}

		// Token: 0x04000A74 RID: 2676
		private short wValidBitsPerSample;

		// Token: 0x04000A75 RID: 2677
		private int dwChannelMask;

		// Token: 0x04000A76 RID: 2678
		private Guid subFormat;
	}
}
