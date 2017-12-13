using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NAudio.Dmo;
using NAudio.MediaFoundation;

namespace NAudio.Wave
{
	/// <summary>
	/// The Media Foundation Resampler Transform
	/// </summary>
	// Token: 0x020001D8 RID: 472
	public class MediaFoundationResampler : MediaFoundationTransform
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x0001E2C4 File Offset: 0x0001C4C4
		private static bool IsPcmOrIeeeFloat(WaveFormat waveFormat)
		{
			WaveFormatExtensible waveFormatExtensible = waveFormat as WaveFormatExtensible;
			return waveFormat.Encoding == WaveFormatEncoding.Pcm || waveFormat.Encoding == WaveFormatEncoding.IeeeFloat || (waveFormatExtensible != null && (waveFormatExtensible.SubFormat == AudioSubtypes.MFAudioFormat_PCM || waveFormatExtensible.SubFormat == AudioSubtypes.MFAudioFormat_Float));
		}

		/// <summary>
		/// Creates the Media Foundation Resampler, allowing modifying of sample rate, bit depth and channel count
		/// </summary>
		/// <param name="sourceProvider">Source provider, must be PCM</param>
		/// <param name="outputFormat">Output format, must also be PCM</param>
		// Token: 0x06000A5D RID: 2653 RVA: 0x0001E318 File Offset: 0x0001C518
		public MediaFoundationResampler(IWaveProvider sourceProvider, WaveFormat outputFormat) : base(sourceProvider, outputFormat)
		{
			if (!MediaFoundationResampler.IsPcmOrIeeeFloat(sourceProvider.WaveFormat))
			{
				throw new ArgumentException("Input must be PCM or IEEE float", "sourceProvider");
			}
			if (!MediaFoundationResampler.IsPcmOrIeeeFloat(outputFormat))
			{
				throw new ArgumentException("Output must be PCM or IEEE float", "outputFormat");
			}
			MediaFoundationApi.Startup();
			this.ResamplerQuality = 60;
			object comObject = this.CreateResamplerComObject();
			this.FreeComObject(comObject);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001E37D File Offset: 0x0001C57D
		private void FreeComObject(object comObject)
		{
			if (this.activate != null)
			{
				this.activate.ShutdownObject();
			}
			Marshal.ReleaseComObject(comObject);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001E399 File Offset: 0x0001C599
		private object CreateResamplerComObject()
		{
			return new ResamplerMediaComObject();
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001E3A0 File Offset: 0x0001C5A0
		private object CreateResamplerComObjectUsingActivator()
		{
			IEnumerable<IMFActivate> enumerable = MediaFoundationApi.EnumerateTransforms(MediaFoundationTransformCategories.AudioEffect);
			foreach (IMFActivate imfactivate in enumerable)
			{
				Guid guid;
				imfactivate.GetGUID(MediaFoundationAttributes.MFT_TRANSFORM_CLSID_Attribute, out guid);
				if (guid.Equals(MediaFoundationResampler.ResamplerClsid))
				{
					object result;
					imfactivate.ActivateObject(MediaFoundationResampler.IMFTransformIid, out result);
					this.activate = imfactivate;
					return result;
				}
			}
			return null;
		}

		/// <summary>
		/// Creates a resampler with a specified target output sample rate
		/// </summary>
		/// <param name="sourceProvider">Source provider</param>
		/// <param name="outputSampleRate">Output sample rate</param>
		// Token: 0x06000A61 RID: 2657 RVA: 0x0001E42C File Offset: 0x0001C62C
		public MediaFoundationResampler(IWaveProvider sourceProvider, int outputSampleRate) : this(sourceProvider, MediaFoundationResampler.CreateOutputFormat(sourceProvider.WaveFormat, outputSampleRate))
		{
		}

		/// <summary>
		/// Creates and configures the actual Resampler transform
		/// </summary>
		/// <returns>A newly created and configured resampler MFT</returns>
		// Token: 0x06000A62 RID: 2658 RVA: 0x0001E444 File Offset: 0x0001C644
		protected override IMFTransform CreateTransform()
		{
			object obj = this.CreateResamplerComObject();
			IMFTransform imftransform = (IMFTransform)obj;
			IMFMediaType imfmediaType = MediaFoundationApi.CreateMediaTypeFromWaveFormat(this.sourceProvider.WaveFormat);
			imftransform.SetInputType(0, imfmediaType, _MFT_SET_TYPE_FLAGS.None);
			Marshal.ReleaseComObject(imfmediaType);
			IMFMediaType imfmediaType2 = MediaFoundationApi.CreateMediaTypeFromWaveFormat(this.outputWaveFormat);
			imftransform.SetOutputType(0, imfmediaType2, _MFT_SET_TYPE_FLAGS.None);
			Marshal.ReleaseComObject(imfmediaType2);
			IWMResamplerProps iwmresamplerProps = (IWMResamplerProps)obj;
			iwmresamplerProps.SetHalfFilterLength(this.ResamplerQuality);
			return imftransform;
		}

		/// <summary>
		/// Gets or sets the Resampler quality. n.b. set the quality before starting to resample.
		/// 1 is lowest quality (linear interpolation) and 60 is best quality
		/// </summary>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0001E4B3 File Offset: 0x0001C6B3
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x0001E4BB File Offset: 0x0001C6BB
		public int ResamplerQuality
		{
			get
			{
				return this.resamplerQuality;
			}
			set
			{
				if (value < 1 || value > 60)
				{
					throw new ArgumentOutOfRangeException("Resampler Quality must be between 1 and 60");
				}
				this.resamplerQuality = value;
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001E4D8 File Offset: 0x0001C6D8
		private static WaveFormat CreateOutputFormat(WaveFormat inputFormat, int outputSampleRate)
		{
			WaveFormat result;
			if (inputFormat.Encoding == WaveFormatEncoding.Pcm)
			{
				result = new WaveFormat(outputSampleRate, inputFormat.BitsPerSample, inputFormat.Channels);
			}
			else
			{
				if (inputFormat.Encoding != WaveFormatEncoding.IeeeFloat)
				{
					throw new ArgumentException("Can only resample PCM or IEEE float");
				}
				result = WaveFormat.CreateIeeeFloatWaveFormat(outputSampleRate, inputFormat.Channels);
			}
			return result;
		}

		/// <summary>
		/// Disposes this resampler
		/// </summary>
		// Token: 0x06000A66 RID: 2662 RVA: 0x0001E527 File Offset: 0x0001C727
		protected override void Dispose(bool disposing)
		{
			if (this.activate != null)
			{
				this.activate.ShutdownObject();
				this.activate = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000B58 RID: 2904
		private int resamplerQuality;

		// Token: 0x04000B59 RID: 2905
		private static readonly Guid ResamplerClsid = new Guid("f447b69e-1884-4a7e-8055-346f74d6edb3");

		// Token: 0x04000B5A RID: 2906
		private static readonly Guid IMFTransformIid = new Guid("bf94c121-5b05-4e6f-8000-ba598961414d");

		// Token: 0x04000B5B RID: 2907
		private IMFActivate activate;
	}
}
