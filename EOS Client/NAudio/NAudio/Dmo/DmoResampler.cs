using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.Dmo
{
	/// <summary>
	/// DMO Resampler
	/// </summary>
	// Token: 0x0200020F RID: 527
	public class DmoResampler : IDisposable
	{
		/// <summary>
		/// Creates a new Resampler based on the DMO Resampler
		/// </summary>
		// Token: 0x06000C06 RID: 3078 RVA: 0x00023E20 File Offset: 0x00022020
		public DmoResampler()
		{
			this.mediaComObject = new ResamplerMediaComObject();
			this.mediaObject = new MediaObject((IMediaObject)this.mediaComObject);
			this.propertyStoreInterface = (IPropertyStore)this.mediaComObject;
			this.resamplerPropsInterface = (IWMResamplerProps)this.mediaComObject;
		}

		/// <summary>
		/// Media Object
		/// </summary>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00023E76 File Offset: 0x00022076
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
		// Token: 0x06000C08 RID: 3080 RVA: 0x00023E80 File Offset: 0x00022080
		public void Dispose()
		{
			if (this.propertyStoreInterface != null)
			{
				Marshal.ReleaseComObject(this.propertyStoreInterface);
				this.propertyStoreInterface = null;
			}
			if (this.resamplerPropsInterface != null)
			{
				Marshal.ReleaseComObject(this.resamplerPropsInterface);
				this.resamplerPropsInterface = null;
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

		// Token: 0x04000C4C RID: 3148
		private MediaObject mediaObject;

		// Token: 0x04000C4D RID: 3149
		private IPropertyStore propertyStoreInterface;

		// Token: 0x04000C4E RID: 3150
		private IWMResamplerProps resamplerPropsInterface;

		// Token: 0x04000C4F RID: 3151
		private ResamplerMediaComObject mediaComObject;
	}
}
