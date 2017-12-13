using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Capture Client
	/// </summary>
	// Token: 0x0200000A RID: 10
	public class AudioCaptureClient : IDisposable
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00003CEC File Offset: 0x00001EEC
		internal AudioCaptureClient(IAudioCaptureClient audioCaptureClientInterface)
		{
			this.audioCaptureClientInterface = audioCaptureClientInterface;
		}

		/// <summary>
		/// Gets a pointer to the buffer
		/// </summary>
		/// <returns>Pointer to the buffer</returns>
		// Token: 0x06000027 RID: 39 RVA: 0x00003CFC File Offset: 0x00001EFC
		public IntPtr GetBuffer(out int numFramesToRead, out AudioClientBufferFlags bufferFlags, out long devicePosition, out long qpcPosition)
		{
			IntPtr result;
			Marshal.ThrowExceptionForHR(this.audioCaptureClientInterface.GetBuffer(out result, out numFramesToRead, out bufferFlags, out devicePosition, out qpcPosition));
			return result;
		}

		/// <summary>
		/// Gets a pointer to the buffer
		/// </summary>
		/// <param name="numFramesToRead">Number of frames to read</param>
		/// <param name="bufferFlags">Buffer flags</param>
		/// <returns>Pointer to the buffer</returns>
		// Token: 0x06000028 RID: 40 RVA: 0x00003D24 File Offset: 0x00001F24
		public IntPtr GetBuffer(out int numFramesToRead, out AudioClientBufferFlags bufferFlags)
		{
			IntPtr result;
			long num;
			long num2;
			Marshal.ThrowExceptionForHR(this.audioCaptureClientInterface.GetBuffer(out result, out numFramesToRead, out bufferFlags, out num, out num2));
			return result;
		}

		/// <summary>
		/// Gets the size of the next packet
		/// </summary>
		// Token: 0x06000029 RID: 41 RVA: 0x00003D4C File Offset: 0x00001F4C
		public int GetNextPacketSize()
		{
			int result;
			Marshal.ThrowExceptionForHR(this.audioCaptureClientInterface.GetNextPacketSize(out result));
			return result;
		}

		/// <summary>
		/// Release buffer
		/// </summary>
		/// <param name="numFramesWritten">Number of frames written</param>
		// Token: 0x0600002A RID: 42 RVA: 0x00003D6C File Offset: 0x00001F6C
		public void ReleaseBuffer(int numFramesWritten)
		{
			Marshal.ThrowExceptionForHR(this.audioCaptureClientInterface.ReleaseBuffer(numFramesWritten));
		}

		/// <summary>
		/// Release the COM object
		/// </summary>
		// Token: 0x0600002B RID: 43 RVA: 0x00003D7F File Offset: 0x00001F7F
		public void Dispose()
		{
			if (this.audioCaptureClientInterface != null)
			{
				Marshal.ReleaseComObject(this.audioCaptureClientInterface);
				this.audioCaptureClientInterface = null;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000033 RID: 51
		private IAudioCaptureClient audioCaptureClientInterface;
	}
}
