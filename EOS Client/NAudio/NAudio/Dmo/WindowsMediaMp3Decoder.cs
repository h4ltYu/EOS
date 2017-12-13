using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.Dmo
{
	/// <summary>
	/// Windows Media MP3 Decoder (as a DMO)
	/// WORK IN PROGRESS - DO NOT USE!
	/// </summary>
	// Token: 0x02000097 RID: 151
	public class WindowsMediaMp3Decoder : IDisposable
	{
		/// <summary>
		/// Creates a new Resampler based on the DMO Resampler
		/// </summary>
		// Token: 0x0600033C RID: 828 RVA: 0x0000ACD7 File Offset: 0x00008ED7
		public WindowsMediaMp3Decoder()
		{
			this.mediaComObject = new WindowsMediaMp3DecoderComObject();
			this.mediaObject = new MediaObject((IMediaObject)this.mediaComObject);
			this.propertyStoreInterface = (IPropertyStore)this.mediaComObject;
		}

		/// <summary>
		/// Media Object
		/// </summary>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000AD11 File Offset: 0x00008F11
		public MediaObject MediaObject
		{
			get
			{
				return this.mediaObject;
			}
		}

		/// <summary>
		/// Dispose code - experimental at the moment
		/// Was added trying to track down why Resampler crashes NUnit
		/// This code not currently being called by ResamplerDmoStream
		/// </summary>
		// Token: 0x0600033E RID: 830 RVA: 0x0000AD1C File Offset: 0x00008F1C
		public void Dispose()
		{
			if (this.propertyStoreInterface != null)
			{
				Marshal.ReleaseComObject(this.propertyStoreInterface);
				this.propertyStoreInterface = null;
			}
			if (this.mediaObject != null)
			{
				this.mediaObject.Dispose();
				this.mediaObject = null;
			}
			if (this.mediaComObject != null)
			{
				Marshal.ReleaseComObject(this.mediaComObject);
				this.mediaComObject = null;
			}
		}

		// Token: 0x04000424 RID: 1060
		private MediaObject mediaObject;

		// Token: 0x04000425 RID: 1061
		private IPropertyStore propertyStoreInterface;

		// Token: 0x04000426 RID: 1062
		private WindowsMediaMp3DecoderComObject mediaComObject;
	}
}
