using System;
using System.Runtime.InteropServices;
using NAudio.Utils;
using NAudio.Wave;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// An abstract base class for simplifying working with Media Foundation Transforms
	/// You need to override the method that actually creates and configures the transform
	/// </summary>
	// Token: 0x020001D7 RID: 471
	public abstract class MediaFoundationTransform : IWaveProvider, IDisposable
	{
		/// <summary>
		/// Constructs a new MediaFoundationTransform wrapper
		/// Will read one second at a time
		/// </summary>
		/// <param name="sourceProvider">The source provider for input data to the transform</param>
		/// <param name="outputFormat">The desired output format</param>
		// Token: 0x06000A4E RID: 2638 RVA: 0x0001DE44 File Offset: 0x0001C044
		public MediaFoundationTransform(IWaveProvider sourceProvider, WaveFormat outputFormat)
		{
			this.outputWaveFormat = outputFormat;
			this.sourceProvider = sourceProvider;
			this.sourceBuffer = new byte[sourceProvider.WaveFormat.AverageBytesPerSecond];
			this.outputBuffer = new byte[this.outputWaveFormat.AverageBytesPerSecond + this.outputWaveFormat.BlockAlign];
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001DEA0 File Offset: 0x0001C0A0
		private void InitializeTransformForStreaming()
		{
			this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_COMMAND_FLUSH, IntPtr.Zero);
			this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_NOTIFY_BEGIN_STREAMING, IntPtr.Zero);
			this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_NOTIFY_START_OF_STREAM, IntPtr.Zero);
			this.initializedForStreaming = true;
		}

		/// <summary>
		/// To be implemented by overriding classes. Create the transform object, set up its input and output types,
		/// and configure any custom properties in here
		/// </summary>
		/// <returns>An object implementing IMFTrasform</returns>
		// Token: 0x06000A50 RID: 2640
		protected abstract IMFTransform CreateTransform();

		/// <summary>
		/// Disposes this MediaFoundation transform
		/// </summary>
		// Token: 0x06000A51 RID: 2641 RVA: 0x0001DEEF File Offset: 0x0001C0EF
		protected virtual void Dispose(bool disposing)
		{
			if (this.transform != null)
			{
				Marshal.ReleaseComObject(this.transform);
			}
		}

		/// <summary>
		/// Disposes this Media Foundation Transform
		/// </summary>
		// Token: 0x06000A52 RID: 2642 RVA: 0x0001DF05 File Offset: 0x0001C105
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Destructor
		/// </summary>
		// Token: 0x06000A53 RID: 2643 RVA: 0x0001DF24 File Offset: 0x0001C124
		~MediaFoundationTransform()
		{
			this.Dispose(false);
		}

		/// <summary>
		/// The output WaveFormat of this Media Foundation Transform
		/// </summary>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0001DF54 File Offset: 0x0001C154
		public WaveFormat WaveFormat
		{
			get
			{
				return this.outputWaveFormat;
			}
		}

		/// <summary>
		/// Reads data out of the source, passing it through the transform
		/// </summary>
		/// <param name="buffer">Output buffer</param>
		/// <param name="offset">Offset within buffer to write to</param>
		/// <param name="count">Desired byte count</param>
		/// <returns>Number of bytes read</returns>
		// Token: 0x06000A55 RID: 2645 RVA: 0x0001DF5C File Offset: 0x0001C15C
		public int Read(byte[] buffer, int offset, int count)
		{
			if (this.transform == null)
			{
				this.transform = this.CreateTransform();
				this.InitializeTransformForStreaming();
			}
			int i = 0;
			if (this.outputBufferCount > 0)
			{
				i += this.ReadFromOutputBuffer(buffer, offset, count - i);
			}
			while (i < count)
			{
				IMFSample imfsample = this.ReadFromSource();
				if (imfsample == null)
				{
					this.EndStreamAndDrain();
					i += this.ReadFromOutputBuffer(buffer, offset + i, count - i);
					break;
				}
				if (!this.initializedForStreaming)
				{
					this.InitializeTransformForStreaming();
				}
				this.transform.ProcessInput(0, imfsample, 0);
				Marshal.ReleaseComObject(imfsample);
				this.ReadFromTransform();
				i += this.ReadFromOutputBuffer(buffer, offset + i, count - i);
			}
			return i;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001E000 File Offset: 0x0001C200
		private void EndStreamAndDrain()
		{
			this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_NOTIFY_END_OF_STREAM, IntPtr.Zero);
			this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_COMMAND_DRAIN, IntPtr.Zero);
			int num;
			do
			{
				num = this.ReadFromTransform();
			}
			while (num > 0);
			this.outputBufferCount = 0;
			this.outputBufferOffset = 0;
			this.inputPosition = 0L;
			this.outputPosition = 0L;
			this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_NOTIFY_END_STREAMING, IntPtr.Zero);
			this.initializedForStreaming = false;
		}

		/// <summary>
		/// Attempts to read from the transform
		/// Some useful info here:
		/// http://msdn.microsoft.com/en-gb/library/windows/desktop/aa965264%28v=vs.85%29.aspx#process_data
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000A57 RID: 2647 RVA: 0x0001E078 File Offset: 0x0001C278
		private int ReadFromTransform()
		{
			MFT_OUTPUT_DATA_BUFFER[] array = new MFT_OUTPUT_DATA_BUFFER[1];
			IMFSample imfsample = MediaFoundationApi.CreateSample();
			IMFMediaBuffer imfmediaBuffer = MediaFoundationApi.CreateMemoryBuffer(this.outputBuffer.Length);
			imfsample.AddBuffer(imfmediaBuffer);
			imfsample.SetSampleTime(this.outputPosition);
			array[0].pSample = imfsample;
			_MFT_PROCESS_OUTPUT_STATUS mft_PROCESS_OUTPUT_STATUS;
			int num = this.transform.ProcessOutput(_MFT_PROCESS_OUTPUT_FLAGS.None, 1, array, out mft_PROCESS_OUTPUT_STATUS);
			if (num == -1072861838)
			{
				Marshal.ReleaseComObject(imfmediaBuffer);
				Marshal.ReleaseComObject(imfsample);
				return 0;
			}
			if (num != 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			IMFMediaBuffer imfmediaBuffer2;
			array[0].pSample.ConvertToContiguousBuffer(out imfmediaBuffer2);
			IntPtr source;
			int num2;
			int num3;
			imfmediaBuffer2.Lock(out source, out num2, out num3);
			this.outputBuffer = BufferHelpers.Ensure(this.outputBuffer, num3);
			Marshal.Copy(source, this.outputBuffer, 0, num3);
			this.outputBufferOffset = 0;
			this.outputBufferCount = num3;
			imfmediaBuffer2.Unlock();
			this.outputPosition += MediaFoundationTransform.BytesToNsPosition(this.outputBufferCount, this.WaveFormat);
			Marshal.ReleaseComObject(imfmediaBuffer);
			Marshal.ReleaseComObject(imfsample);
			Marshal.ReleaseComObject(imfmediaBuffer2);
			return num3;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001E188 File Offset: 0x0001C388
		private static long BytesToNsPosition(int bytes, WaveFormat waveFormat)
		{
			return 10000000L * (long)bytes / (long)waveFormat.AverageBytesPerSecond;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		private IMFSample ReadFromSource()
		{
			int num = this.sourceProvider.Read(this.sourceBuffer, 0, this.sourceBuffer.Length);
			if (num == 0)
			{
				return null;
			}
			IMFMediaBuffer imfmediaBuffer = MediaFoundationApi.CreateMemoryBuffer(num);
			IntPtr destination;
			int num2;
			int num3;
			imfmediaBuffer.Lock(out destination, out num2, out num3);
			Marshal.Copy(this.sourceBuffer, 0, destination, num);
			imfmediaBuffer.Unlock();
			imfmediaBuffer.SetCurrentLength(num);
			IMFSample imfsample = MediaFoundationApi.CreateSample();
			imfsample.AddBuffer(imfmediaBuffer);
			imfsample.SetSampleTime(this.inputPosition);
			long num4 = MediaFoundationTransform.BytesToNsPosition(num, this.sourceProvider.WaveFormat);
			imfsample.SetSampleDuration(num4);
			this.inputPosition += num4;
			Marshal.ReleaseComObject(imfmediaBuffer);
			return imfsample;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001E254 File Offset: 0x0001C454
		private int ReadFromOutputBuffer(byte[] buffer, int offset, int needed)
		{
			int num = Math.Min(needed, this.outputBufferCount);
			Array.Copy(this.outputBuffer, this.outputBufferOffset, buffer, offset, num);
			this.outputBufferOffset += num;
			this.outputBufferCount -= num;
			if (this.outputBufferCount == 0)
			{
				this.outputBufferOffset = 0;
			}
			return num;
		}

		/// <summary>
		/// Indicate that the source has been repositioned and completely drain out the transforms buffers
		/// </summary>
		// Token: 0x06000A5B RID: 2651 RVA: 0x0001E2AE File Offset: 0x0001C4AE
		public void Reposition()
		{
			if (this.initializedForStreaming)
			{
				this.EndStreamAndDrain();
				this.InitializeTransformForStreaming();
			}
		}

		/// <summary>
		/// The Source Provider
		/// </summary>
		// Token: 0x04000B4D RID: 2893
		protected readonly IWaveProvider sourceProvider;

		/// <summary>
		/// The Output WaveFormat
		/// </summary>
		// Token: 0x04000B4E RID: 2894
		protected readonly WaveFormat outputWaveFormat;

		// Token: 0x04000B4F RID: 2895
		private readonly byte[] sourceBuffer;

		// Token: 0x04000B50 RID: 2896
		private byte[] outputBuffer;

		// Token: 0x04000B51 RID: 2897
		private int outputBufferOffset;

		// Token: 0x04000B52 RID: 2898
		private int outputBufferCount;

		// Token: 0x04000B53 RID: 2899
		private IMFTransform transform;

		// Token: 0x04000B54 RID: 2900
		private bool disposed;

		// Token: 0x04000B55 RID: 2901
		private long inputPosition;

		// Token: 0x04000B56 RID: 2902
		private long outputPosition;

		// Token: 0x04000B57 RID: 2903
		private bool initializedForStreaming;
	}
}
