using System;
using System.Collections.Generic;

namespace NAudio.Wave
{
	/// <summary>
	/// WaveStream that can mix together multiple 32 bit input streams
	/// (Normally used with stereo input channels)
	/// All channels must have the same number of inputs
	/// </summary>
	// Token: 0x02000200 RID: 512
	public class WaveMixerStream32 : WaveStream
	{
		/// <summary>
		/// Creates a new 32 bit WaveMixerStream
		/// </summary>
		// Token: 0x06000BAE RID: 2990 RVA: 0x00022F70 File Offset: 0x00021170
		public WaveMixerStream32()
		{
			this.AutoStop = true;
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
			this.bytesPerSample = 4;
			this.inputStreams = new List<WaveStream>();
			this.inputsLock = new object();
		}

		/// <summary>
		/// Creates a new 32 bit WaveMixerStream
		/// </summary>
		/// <param name="inputStreams">An Array of WaveStreams - must all have the same format.
		/// Use WaveChannel is designed for this purpose.</param>
		/// <param name="autoStop">Automatically stop when all inputs have been read</param>
		/// <exception cref="T:System.ArgumentException">Thrown if the input streams are not 32 bit floating point,
		/// or if they have different formats to each other</exception>
		// Token: 0x06000BAF RID: 2991 RVA: 0x00022FB0 File Offset: 0x000211B0
		public WaveMixerStream32(IEnumerable<WaveStream> inputStreams, bool autoStop) : this()
		{
			this.AutoStop = autoStop;
			foreach (WaveStream waveStream in inputStreams)
			{
				this.AddInputStream(waveStream);
			}
		}

		/// <summary>
		/// Add a new input to the mixer
		/// </summary>
		/// <param name="waveStream">The wave input to add</param>
		// Token: 0x06000BB0 RID: 2992 RVA: 0x00023008 File Offset: 0x00021208
		public void AddInputStream(WaveStream waveStream)
		{
			if (waveStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Must be IEEE floating point", "waveStream");
			}
			if (waveStream.WaveFormat.BitsPerSample != 32)
			{
				throw new ArgumentException("Only 32 bit audio currently supported", "waveStream");
			}
			if (this.inputStreams.Count == 0)
			{
				int sampleRate = waveStream.WaveFormat.SampleRate;
				int channels = waveStream.WaveFormat.Channels;
				this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
			}
			else if (!waveStream.WaveFormat.Equals(this.waveFormat))
			{
				throw new ArgumentException("All incoming channels must have the same format", "waveStream");
			}
			lock (this.inputsLock)
			{
				this.inputStreams.Add(waveStream);
				this.length = Math.Max(this.length, waveStream.Length);
				waveStream.Position = this.Position;
			}
		}

		/// <summary>
		/// Remove a WaveStream from the mixer
		/// </summary>
		/// <param name="waveStream">waveStream to remove</param>
		// Token: 0x06000BB1 RID: 2993 RVA: 0x00023100 File Offset: 0x00021300
		public void RemoveInputStream(WaveStream waveStream)
		{
			lock (this.inputsLock)
			{
				if (this.inputStreams.Remove(waveStream))
				{
					long val = 0L;
					foreach (WaveStream waveStream2 in this.inputStreams)
					{
						val = Math.Max(val, waveStream2.Length);
					}
					this.length = val;
				}
			}
		}

		/// <summary>
		/// The number of inputs to this mixer
		/// </summary>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00023194 File Offset: 0x00021394
		public int InputCount
		{
			get
			{
				return this.inputStreams.Count;
			}
		}

		/// <summary>
		/// Automatically stop when all inputs have been read
		/// </summary>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x000231A1 File Offset: 0x000213A1
		// (set) Token: 0x06000BB4 RID: 2996 RVA: 0x000231A9 File Offset: 0x000213A9
		public bool AutoStop { get; set; }

		/// <summary>
		/// Reads bytes from this wave stream
		/// </summary>
		/// <param name="buffer">buffer to read into</param>
		/// <param name="offset">offset into buffer</param>
		/// <param name="count">number of bytes required</param>
		/// <returns>Number of bytes read.</returns>
		/// <exception cref="T:System.ArgumentException">Thrown if an invalid number of bytes requested</exception>
		// Token: 0x06000BB5 RID: 2997 RVA: 0x000231B4 File Offset: 0x000213B4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.AutoStop && this.position + (long)count > this.length)
			{
				count = (int)(this.length - this.position);
			}
			if (count % this.bytesPerSample != 0)
			{
				throw new ArgumentException("Must read an whole number of samples", "count");
			}
			Array.Clear(buffer, offset, count);
			int val = 0;
			byte[] array = new byte[count];
			lock (this.inputsLock)
			{
				foreach (WaveStream waveStream in this.inputStreams)
				{
					if (waveStream.HasData(count))
					{
						int num = waveStream.Read(array, 0, count);
						val = Math.Max(val, num);
						if (num > 0)
						{
							WaveMixerStream32.Sum32BitAudio(buffer, offset, array, num);
						}
					}
					else
					{
						val = Math.Max(val, count);
						waveStream.Position += (long)count;
					}
				}
			}
			this.position += (long)count;
			return count;
		}

		/// <summary>
		/// Actually performs the mixing
		/// </summary>
		// Token: 0x06000BB6 RID: 2998 RVA: 0x000232CC File Offset: 0x000214CC
		private unsafe static void Sum32BitAudio(byte[] destBuffer, int offset, byte[] sourceBuffer, int bytesRead)
		{
			fixed (byte* ptr = &destBuffer[offset], ptr2 = &sourceBuffer[0])
			{
				float* ptr3 = (float*)ptr;
				float* ptr4 = (float*)ptr2;
				int num = bytesRead / 4;
				for (int i = 0; i < num; i++)
				{
					ptr3[i] += ptr4[i];
				}
			}
		}

		/// <summary>
		/// <see cref="P:NAudio.Wave.WaveStream.BlockAlign" />
		/// </summary>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0002331E File Offset: 0x0002151E
		public override int BlockAlign
		{
			get
			{
				return this.waveFormat.BlockAlign;
			}
		}

		/// <summary>
		/// Length of this Wave Stream (in bytes)
		/// <see cref="P:System.IO.Stream.Length" />
		/// </summary>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0002332B File Offset: 0x0002152B
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		/// <summary>
		/// Position within this Wave Stream (in bytes)
		/// <see cref="P:System.IO.Stream.Position" />
		/// </summary>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00023333 File Offset: 0x00021533
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0002333C File Offset: 0x0002153C
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				lock (this.inputsLock)
				{
					value = Math.Min(value, this.Length);
					foreach (WaveStream waveStream in this.inputStreams)
					{
						waveStream.Position = Math.Min(value, waveStream.Length);
					}
					this.position = value;
				}
			}
		}

		/// <summary>
		/// <see cref="P:NAudio.Wave.WaveStream.WaveFormat" />
		/// </summary>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x000233D4 File Offset: 0x000215D4
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		/// <summary>
		/// Disposes this WaveStream
		/// </summary>
		// Token: 0x06000BBC RID: 3004 RVA: 0x000233DC File Offset: 0x000215DC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this.inputsLock)
				{
					foreach (WaveStream waveStream in this.inputStreams)
					{
						waveStream.Dispose();
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000C03 RID: 3075
		private readonly List<WaveStream> inputStreams;

		// Token: 0x04000C04 RID: 3076
		private readonly object inputsLock;

		// Token: 0x04000C05 RID: 3077
		private WaveFormat waveFormat;

		// Token: 0x04000C06 RID: 3078
		private long length;

		// Token: 0x04000C07 RID: 3079
		private long position;

		// Token: 0x04000C08 RID: 3080
		private readonly int bytesPerSample;
	}
}
