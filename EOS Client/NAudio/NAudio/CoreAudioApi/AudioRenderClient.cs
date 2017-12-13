using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Render Client
	/// </summary>
	// Token: 0x0200001A RID: 26
	public class AudioRenderClient : IDisposable
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00004524 File Offset: 0x00002724
		internal AudioRenderClient(IAudioRenderClient audioRenderClientInterface)
		{
			this.audioRenderClientInterface = audioRenderClientInterface;
		}

		/// <summary>
		/// Gets a pointer to the buffer
		/// </summary>
		/// <param name="numFramesRequested">Number of frames requested</param>
		/// <returns>Pointer to the buffer</returns>
		// Token: 0x06000060 RID: 96 RVA: 0x00004534 File Offset: 0x00002734
		public IntPtr GetBuffer(int numFramesRequested)
		{
			IntPtr result;
			Marshal.ThrowExceptionForHR(this.audioRenderClientInterface.GetBuffer(numFramesRequested, out result));
			return result;
		}

		/// <summary>
		/// Release buffer
		/// </summary>
		/// <param name="numFramesWritten">Number of frames written</param>
		/// <param name="bufferFlags">Buffer flags</param>
		// Token: 0x06000061 RID: 97 RVA: 0x00004555 File Offset: 0x00002755
		public void ReleaseBuffer(int numFramesWritten, AudioClientBufferFlags bufferFlags)
		{
			Marshal.ThrowExceptionForHR(this.audioRenderClientInterface.ReleaseBuffer(numFramesWritten, bufferFlags));
		}

		/// <summary>
		/// Release the COM object
		/// </summary>
		// Token: 0x06000062 RID: 98 RVA: 0x00004569 File Offset: 0x00002769
		public void Dispose()
		{
			if (this.audioRenderClientInterface != null)
			{
				Marshal.ReleaseComObject(this.audioRenderClientInterface);
				this.audioRenderClientInterface = null;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0400005C RID: 92
		private IAudioRenderClient audioRenderClientInterface;
	}
}
