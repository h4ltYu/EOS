using System;
using System.Runtime.InteropServices;
using NAudio.Utils;
using NAudio.Wave;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Media Type helper class, simplifying working with IMFMediaType
	/// (will probably change in the future, to inherit from an attributes class)
	/// Currently does not release the COM object, so you must do that yourself
	/// </summary>
	// Token: 0x020001B6 RID: 438
	public class MediaType
	{
		/// <summary>
		/// Wraps an existing IMFMediaType object
		/// </summary>
		/// <param name="mediaType">The IMFMediaType object</param>
		// Token: 0x0600094D RID: 2381 RVA: 0x0001B1A4 File Offset: 0x000193A4
		public MediaType(IMFMediaType mediaType)
		{
			this.mediaType = mediaType;
		}

		/// <summary>
		/// Creates and wraps a new IMFMediaType object
		/// </summary>
		// Token: 0x0600094E RID: 2382 RVA: 0x0001B1B3 File Offset: 0x000193B3
		public MediaType()
		{
			this.mediaType = MediaFoundationApi.CreateMediaType();
		}

		/// <summary>
		/// Creates and wraps a new IMFMediaType object based on a WaveFormat
		/// </summary>
		/// <param name="waveFormat">WaveFormat</param>
		// Token: 0x0600094F RID: 2383 RVA: 0x0001B1C6 File Offset: 0x000193C6
		public MediaType(WaveFormat waveFormat)
		{
			this.mediaType = MediaFoundationApi.CreateMediaTypeFromWaveFormat(waveFormat);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001B1DC File Offset: 0x000193DC
		private int GetUInt32(Guid key)
		{
			int result;
			this.mediaType.GetUINT32(key, out result);
			return result;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001B1F8 File Offset: 0x000193F8
		private Guid GetGuid(Guid key)
		{
			Guid result;
			this.mediaType.GetGUID(key, out result);
			return result;
		}

		/// <summary>
		/// Tries to get a UINT32 value, returning a default value if it doesn't exist
		/// </summary>
		/// <param name="key">Attribute key</param>
		/// <param name="defaultValue">Default value</param>
		/// <returns>Value or default if key doesn't exist</returns>
		// Token: 0x06000952 RID: 2386 RVA: 0x0001B214 File Offset: 0x00019414
		public int TryGetUInt32(Guid key, int defaultValue = -1)
		{
			int result = defaultValue;
			try
			{
				this.mediaType.GetUINT32(key, out result);
			}
			catch (COMException exception)
			{
				if (exception.GetHResult() != -1072875802)
				{
					if (exception.GetHResult() == -1072875843)
					{
						throw new ArgumentException("Not a UINT32 parameter");
					}
					throw;
				}
			}
			return result;
		}

		/// <summary>
		/// The Sample Rate (valid for audio media types)
		/// </summary>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0001B270 File Offset: 0x00019470
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x0001B27D File Offset: 0x0001947D
		public int SampleRate
		{
			get
			{
				return this.GetUInt32(MediaFoundationAttributes.MF_MT_AUDIO_SAMPLES_PER_SECOND);
			}
			set
			{
				this.mediaType.SetUINT32(MediaFoundationAttributes.MF_MT_AUDIO_SAMPLES_PER_SECOND, value);
			}
		}

		/// <summary>
		/// The number of Channels (valid for audio media types)
		/// </summary>
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0001B290 File Offset: 0x00019490
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x0001B29D File Offset: 0x0001949D
		public int ChannelCount
		{
			get
			{
				return this.GetUInt32(MediaFoundationAttributes.MF_MT_AUDIO_NUM_CHANNELS);
			}
			set
			{
				this.mediaType.SetUINT32(MediaFoundationAttributes.MF_MT_AUDIO_NUM_CHANNELS, value);
			}
		}

		/// <summary>
		/// The number of bits per sample (n.b. not always valid for compressed audio types)
		/// </summary>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0001B2B0 File Offset: 0x000194B0
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x0001B2BD File Offset: 0x000194BD
		public int BitsPerSample
		{
			get
			{
				return this.GetUInt32(MediaFoundationAttributes.MF_MT_AUDIO_BITS_PER_SAMPLE);
			}
			set
			{
				this.mediaType.SetUINT32(MediaFoundationAttributes.MF_MT_AUDIO_BITS_PER_SAMPLE, value);
			}
		}

		/// <summary>
		/// The average bytes per second (valid for audio media types)
		/// </summary>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0001B2D0 File Offset: 0x000194D0
		public int AverageBytesPerSecond
		{
			get
			{
				return this.GetUInt32(MediaFoundationAttributes.MF_MT_AUDIO_AVG_BYTES_PER_SECOND);
			}
		}

		/// <summary>
		/// The Media Subtype. For audio, is a value from the AudioSubtypes class
		/// </summary>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001B2DD File Offset: 0x000194DD
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0001B2EA File Offset: 0x000194EA
		public Guid SubType
		{
			get
			{
				return this.GetGuid(MediaFoundationAttributes.MF_MT_SUBTYPE);
			}
			set
			{
				this.mediaType.SetGUID(MediaFoundationAttributes.MF_MT_SUBTYPE, value);
			}
		}

		/// <summary>
		/// The Major type, e.g. audio or video (from the MediaTypes class)
		/// </summary>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0001B2FD File Offset: 0x000194FD
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x0001B30A File Offset: 0x0001950A
		public Guid MajorType
		{
			get
			{
				return this.GetGuid(MediaFoundationAttributes.MF_MT_MAJOR_TYPE);
			}
			set
			{
				this.mediaType.SetGUID(MediaFoundationAttributes.MF_MT_MAJOR_TYPE, value);
			}
		}

		/// <summary>
		/// Access to the actual IMFMediaType object
		/// Use to pass to MF APIs or Marshal.ReleaseComObject when you are finished with it
		/// </summary>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0001B31D File Offset: 0x0001951D
		public IMFMediaType MediaFoundationObject
		{
			get
			{
				return this.mediaType;
			}
		}

		// Token: 0x04000AA8 RID: 2728
		private readonly IMFMediaType mediaType;
	}
}
