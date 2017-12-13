using System;
using NAudio.Dmo;

namespace NAudio.Wave
{
	/// <summary>
	/// Wave Stream for converting between sample rates
	/// </summary>
	// Token: 0x020001F8 RID: 504
	public class ResamplerDmoStream : WaveStream
	{
		/// <summary>
		/// WaveStream to resample using the DMO Resampler
		/// </summary>
		/// <param name="inputProvider">Input Stream</param>
		/// <param name="outputFormat">Desired Output Format</param>
		// Token: 0x06000B46 RID: 2886 RVA: 0x00021834 File Offset: 0x0001FA34
		public ResamplerDmoStream(IWaveProvider inputProvider, WaveFormat outputFormat)
		{
			this.inputProvider = inputProvider;
			this.inputStream = (inputProvider as WaveStream);
			this.outputFormat = outputFormat;
			this.dmoResampler = new DmoResampler();
			if (!this.dmoResampler.MediaObject.SupportsInputWaveFormat(0, inputProvider.WaveFormat))
			{
				throw new ArgumentException("Unsupported Input Stream format", "inputStream");
			}
			this.dmoResampler.MediaObject.SetInputWaveFormat(0, inputProvider.WaveFormat);
			if (!this.dmoResampler.MediaObject.SupportsOutputWaveFormat(0, outputFormat))
			{
				throw new ArgumentException("Unsupported Output Stream format", "outputStream");
			}
			this.dmoResampler.MediaObject.SetOutputWaveFormat(0, outputFormat);
			if (this.inputStream != null)
			{
				this.position = this.InputToOutputPosition(this.inputStream.Position);
			}
			this.inputMediaBuffer = new MediaBuffer(inputProvider.WaveFormat.AverageBytesPerSecond);
			this.outputBuffer = new DmoOutputDataBuffer(outputFormat.AverageBytesPerSecond);
		}

		/// <summary>
		/// Stream Wave Format
		/// </summary>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00021928 File Offset: 0x0001FB28
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.outputFormat;
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00021930 File Offset: 0x0001FB30
		private long InputToOutputPosition(long inputPosition)
		{
			double num = (double)this.outputFormat.AverageBytesPerSecond / (double)this.inputProvider.WaveFormat.AverageBytesPerSecond;
			long num2 = (long)((double)inputPosition * num);
			if (num2 % (long)this.outputFormat.BlockAlign != 0L)
			{
				num2 -= num2 % (long)this.outputFormat.BlockAlign;
			}
			return num2;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00021988 File Offset: 0x0001FB88
		private long OutputToInputPosition(long outputPosition)
		{
			double num = (double)this.outputFormat.AverageBytesPerSecond / (double)this.inputProvider.WaveFormat.AverageBytesPerSecond;
			long num2 = (long)((double)outputPosition / num);
			if (num2 % (long)this.inputProvider.WaveFormat.BlockAlign != 0L)
			{
				num2 -= num2 % (long)this.inputProvider.WaveFormat.BlockAlign;
			}
			return num2;
		}

		/// <summary>
		/// Stream length in bytes
		/// </summary>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x000219E8 File Offset: 0x0001FBE8
		public override long Length
		{
			get
			{
				if (this.inputStream == null)
				{
					throw new InvalidOperationException("Cannot report length if the input was an IWaveProvider");
				}
				return this.InputToOutputPosition(this.inputStream.Length);
			}
		}

		/// <summary>
		/// Stream position in bytes
		/// </summary>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00021A0E File Offset: 0x0001FC0E
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x00021A18 File Offset: 0x0001FC18
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				if (this.inputStream == null)
				{
					throw new InvalidOperationException("Cannot set position if the input was not a WaveStream");
				}
				this.inputStream.Position = this.OutputToInputPosition(value);
				this.position = this.InputToOutputPosition(this.inputStream.Position);
				this.dmoResampler.MediaObject.Discontinuity(0);
			}
		}

		/// <summary>
		/// Reads data from input stream
		/// </summary>
		/// <param name="buffer">buffer</param>
		/// <param name="offset">offset into buffer</param>
		/// <param name="count">Bytes required</param>
		/// <returns>Number of bytes read</returns>
		// Token: 0x06000B4D RID: 2893 RVA: 0x00021A74 File Offset: 0x0001FC74
		public override int Read(byte[] buffer, int offset, int count)
		{
			int i = 0;
			while (i < count)
			{
				if (this.dmoResampler.MediaObject.IsAcceptingData(0))
				{
					int num = (int)this.OutputToInputPosition((long)(count - i));
					byte[] array = new byte[num];
					int num2 = this.inputProvider.Read(array, 0, num);
					if (num2 == 0)
					{
						break;
					}
					this.inputMediaBuffer.LoadData(array, num2);
					this.dmoResampler.MediaObject.ProcessInput(0, this.inputMediaBuffer, DmoInputDataBufferFlags.None, 0L, 0L);
					this.outputBuffer.MediaBuffer.SetLength(0);
					this.outputBuffer.StatusFlags = DmoOutputDataBufferFlags.None;
					this.dmoResampler.MediaObject.ProcessOutput(DmoProcessOutputFlags.None, 1, new DmoOutputDataBuffer[]
					{
						this.outputBuffer
					});
					if (this.outputBuffer.Length == 0)
					{
						break;
					}
					this.outputBuffer.RetrieveData(buffer, offset + i);
					i += this.outputBuffer.Length;
				}
			}
			this.position += (long)i;
			return i;
		}

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="disposing">True if disposing (not from finalizer)</param>
		// Token: 0x06000B4E RID: 2894 RVA: 0x00021B7D File Offset: 0x0001FD7D
		protected override void Dispose(bool disposing)
		{
			if (this.inputMediaBuffer != null)
			{
				this.inputMediaBuffer.Dispose();
				this.inputMediaBuffer = null;
			}
			this.outputBuffer.Dispose();
			if (this.dmoResampler != null)
			{
				this.dmoResampler = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000BCB RID: 3019
		private readonly IWaveProvider inputProvider;

		// Token: 0x04000BCC RID: 3020
		private readonly WaveStream inputStream;

		// Token: 0x04000BCD RID: 3021
		private readonly WaveFormat outputFormat;

		// Token: 0x04000BCE RID: 3022
		private DmoOutputDataBuffer outputBuffer;

		// Token: 0x04000BCF RID: 3023
		private DmoResampler dmoResampler;

		// Token: 0x04000BD0 RID: 3024
		private MediaBuffer inputMediaBuffer;

		// Token: 0x04000BD1 RID: 3025
		private long position;
	}
}
