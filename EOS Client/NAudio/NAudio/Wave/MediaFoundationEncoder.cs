using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NAudio.MediaFoundation;
using NAudio.Utils;

namespace NAudio.Wave
{
	/// <summary>
	/// Media Foundation Encoder class allows you to use Media Foundation to encode an IWaveProvider
	/// to any supported encoding format
	/// </summary>
	// Token: 0x020001B5 RID: 437
	public class MediaFoundationEncoder : IDisposable
	{
		/// <summary>
		/// Queries the available bitrates for a given encoding output type, sample rate and number of channels
		/// </summary>
		/// <param name="audioSubtype">Audio subtype - a value from the AudioSubtypes class</param>
		/// <param name="sampleRate">The sample rate of the PCM to encode</param>
		/// <param name="channels">The number of channels of the PCM to encode</param>
		/// <returns>An array of available bitrates in average bits per second</returns>
		// Token: 0x0600093A RID: 2362 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		public static int[] GetEncodeBitrates(Guid audioSubtype, int sampleRate, int channels)
		{
			return (from br in (from mt in MediaFoundationEncoder.GetOutputMediaTypes(audioSubtype)
			where mt.SampleRate == sampleRate && mt.ChannelCount == channels
			select mt.AverageBytesPerSecond * 8).Distinct<int>()
			orderby br
			select br).ToArray<int>();
		}

		/// <summary>
		/// Gets all the available media types for a particular 
		/// </summary>
		/// <param name="audioSubtype">Audio subtype - a value from the AudioSubtypes class</param>
		/// <returns>An array of available media types that can be encoded with this subtype</returns>
		// Token: 0x0600093B RID: 2363 RVA: 0x0001AB68 File Offset: 0x00018D68
		public static MediaType[] GetOutputMediaTypes(Guid audioSubtype)
		{
			IMFCollection imfcollection;
			try
			{
				MediaFoundationInterop.MFTranscodeGetAudioOutputAvailableTypes(audioSubtype, _MFT_ENUM_FLAG.MFT_ENUM_FLAG_ALL, null, out imfcollection);
			}
			catch (COMException exception)
			{
				if (exception.GetHResult() == -1072875819)
				{
					return new MediaType[0];
				}
				throw;
			}
			int num;
			imfcollection.GetElementCount(out num);
			List<MediaType> list = new List<MediaType>(num);
			for (int i = 0; i < num; i++)
			{
				object obj;
				imfcollection.GetElement(i, out obj);
				IMFMediaType mediaType = (IMFMediaType)obj;
				list.Add(new MediaType(mediaType));
			}
			Marshal.ReleaseComObject(imfcollection);
			return list.ToArray();
		}

		/// <summary>
		/// Helper function to simplify encoding Window Media Audio
		/// Should be supported on Vista and above (not tested)
		/// </summary>
		/// <param name="inputProvider">Input provider, must be PCM</param>
		/// <param name="outputFile">Output file path, should end with .wma</param>
		/// <param name="desiredBitRate">Desired bitrate. Use GetEncodeBitrates to find the possibilities for your input type</param>
		// Token: 0x0600093C RID: 2364 RVA: 0x0001ABFC File Offset: 0x00018DFC
		public static void EncodeToWma(IWaveProvider inputProvider, string outputFile, int desiredBitRate = 192000)
		{
			MediaType mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_WMAudioV8, inputProvider.WaveFormat, desiredBitRate);
			if (mediaType == null)
			{
				throw new InvalidOperationException("No suitable WMA encoders available");
			}
			using (MediaFoundationEncoder mediaFoundationEncoder = new MediaFoundationEncoder(mediaType))
			{
				mediaFoundationEncoder.Encode(outputFile, inputProvider);
			}
		}

		/// <summary>
		/// Helper function to simplify encoding to MP3
		/// By default, will only be available on Windows 8 and above
		/// </summary>
		/// <param name="inputProvider">Input provider, must be PCM</param>
		/// <param name="outputFile">Output file path, should end with .mp3</param>
		/// <param name="desiredBitRate">Desired bitrate. Use GetEncodeBitrates to find the possibilities for your input type</param>
		// Token: 0x0600093D RID: 2365 RVA: 0x0001AC54 File Offset: 0x00018E54
		public static void EncodeToMp3(IWaveProvider inputProvider, string outputFile, int desiredBitRate = 192000)
		{
			MediaType mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_MP3, inputProvider.WaveFormat, desiredBitRate);
			if (mediaType == null)
			{
				throw new InvalidOperationException("No suitable MP3 encoders available");
			}
			using (MediaFoundationEncoder mediaFoundationEncoder = new MediaFoundationEncoder(mediaType))
			{
				mediaFoundationEncoder.Encode(outputFile, inputProvider);
			}
		}

		/// <summary>
		/// Helper function to simplify encoding to AAC
		/// By default, will only be available on Windows 7 and above
		/// </summary>
		/// <param name="inputProvider">Input provider, must be PCM</param>
		/// <param name="outputFile">Output file path, should end with .mp4 (or .aac on Windows 8)</param>
		/// <param name="desiredBitRate">Desired bitrate. Use GetEncodeBitrates to find the possibilities for your input type</param>
		// Token: 0x0600093E RID: 2366 RVA: 0x0001ACAC File Offset: 0x00018EAC
		public static void EncodeToAac(IWaveProvider inputProvider, string outputFile, int desiredBitRate = 192000)
		{
			MediaType mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_AAC, inputProvider.WaveFormat, desiredBitRate);
			if (mediaType == null)
			{
				throw new InvalidOperationException("No suitable AAC encoders available");
			}
			using (MediaFoundationEncoder mediaFoundationEncoder = new MediaFoundationEncoder(mediaType))
			{
				mediaFoundationEncoder.Encode(outputFile, inputProvider);
			}
		}

		/// <summary>
		/// Tries to find the encoding media type with the closest bitrate to that specified
		/// </summary>
		/// <param name="audioSubtype">Audio subtype, a value from AudioSubtypes</param>
		/// <param name="inputFormat">Your encoder input format (used to check sample rate and channel count)</param>
		/// <param name="desiredBitRate">Your desired bitrate</param>
		/// <returns>The closest media type, or null if none available</returns>
		// Token: 0x0600093F RID: 2367 RVA: 0x0001AE7C File Offset: 0x0001907C
		public static MediaType SelectMediaType(Guid audioSubtype, WaveFormat inputFormat, int desiredBitRate)
		{
			return (from mt in MediaFoundationEncoder.GetOutputMediaTypes(audioSubtype)
			where mt.SampleRate == inputFormat.SampleRate && mt.ChannelCount == inputFormat.Channels
			select new
			{
				MediaType = mt,
				Delta = Math.Abs(desiredBitRate - mt.AverageBytesPerSecond * 8)
			} into mt
			orderby mt.Delta
			select mt.MediaType).FirstOrDefault<MediaType>();
		}

		/// <summary>
		/// Creates a new encoder that encodes to the specified output media type
		/// </summary>
		/// <param name="outputMediaType">Desired output media type</param>
		// Token: 0x06000940 RID: 2368 RVA: 0x0001AF0E File Offset: 0x0001910E
		public MediaFoundationEncoder(MediaType outputMediaType)
		{
			if (outputMediaType == null)
			{
				throw new ArgumentNullException("outputMediaType");
			}
			this.outputMediaType = outputMediaType;
		}

		/// <summary>
		/// Encodes a file
		/// </summary>
		/// <param name="outputFile">Output filename (container type is deduced from the filename)</param>
		/// <param name="inputProvider">Input provider (should be PCM, some encoders will also allow IEEE float)</param>
		// Token: 0x06000941 RID: 2369 RVA: 0x0001AF2C File Offset: 0x0001912C
		public void Encode(string outputFile, IWaveProvider inputProvider)
		{
			if (inputProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm && inputProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Encode input format must be PCM or IEEE float");
			}
			MediaType mediaType = new MediaType(inputProvider.WaveFormat);
			IMFSinkWriter imfsinkWriter = MediaFoundationEncoder.CreateSinkWriter(outputFile);
			try
			{
				int num;
				imfsinkWriter.AddStream(this.outputMediaType.MediaFoundationObject, out num);
				imfsinkWriter.SetInputMediaType(num, mediaType.MediaFoundationObject, null);
				this.PerformEncode(imfsinkWriter, num, inputProvider);
			}
			finally
			{
				Marshal.ReleaseComObject(imfsinkWriter);
				Marshal.ReleaseComObject(mediaType.MediaFoundationObject);
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001AFC4 File Offset: 0x000191C4
		private static IMFSinkWriter CreateSinkWriter(string outputFile)
		{
			IMFAttributes imfattributes = MediaFoundationApi.CreateAttributes(1);
			imfattributes.SetUINT32(MediaFoundationAttributes.MF_READWRITE_ENABLE_HARDWARE_TRANSFORMS, 1);
			IMFSinkWriter result;
			try
			{
				MediaFoundationInterop.MFCreateSinkWriterFromURL(outputFile, null, imfattributes, out result);
			}
			catch (COMException exception)
			{
				if (exception.GetHResult() == -1072875819)
				{
					throw new ArgumentException("Was not able to create a sink writer for this file extension");
				}
				throw;
			}
			finally
			{
				Marshal.ReleaseComObject(imfattributes);
			}
			return result;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001B034 File Offset: 0x00019234
		private void PerformEncode(IMFSinkWriter writer, int streamIndex, IWaveProvider inputProvider)
		{
			int num = inputProvider.WaveFormat.AverageBytesPerSecond * 4;
			byte[] managedBuffer = new byte[num];
			writer.BeginWriting();
			long num2 = 0L;
			long num3;
			do
			{
				num3 = this.ConvertOneBuffer(writer, streamIndex, inputProvider, num2, managedBuffer);
				num2 += num3;
			}
			while (num3 > 0L);
			writer.DoFinalize();
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001B080 File Offset: 0x00019280
		private static long BytesToNsPosition(int bytes, WaveFormat waveFormat)
		{
			return 10000000L * (long)bytes / (long)waveFormat.AverageBytesPerSecond;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001B0A0 File Offset: 0x000192A0
		private long ConvertOneBuffer(IMFSinkWriter writer, int streamIndex, IWaveProvider inputProvider, long position, byte[] managedBuffer)
		{
			long num = 0L;
			IMFMediaBuffer imfmediaBuffer = MediaFoundationApi.CreateMemoryBuffer(managedBuffer.Length);
			int count;
			imfmediaBuffer.GetMaxLength(out count);
			IMFSample imfsample = MediaFoundationApi.CreateSample();
			imfsample.AddBuffer(imfmediaBuffer);
			IntPtr destination;
			int num2;
			imfmediaBuffer.Lock(out destination, out count, out num2);
			int num3 = inputProvider.Read(managedBuffer, 0, count);
			if (num3 > 0)
			{
				num = MediaFoundationEncoder.BytesToNsPosition(num3, inputProvider.WaveFormat);
				Marshal.Copy(managedBuffer, 0, destination, num3);
				imfmediaBuffer.SetCurrentLength(num3);
				imfmediaBuffer.Unlock();
				imfsample.SetSampleTime(position);
				imfsample.SetSampleDuration(num);
				writer.WriteSample(streamIndex, imfsample);
			}
			else
			{
				imfmediaBuffer.Unlock();
			}
			Marshal.ReleaseComObject(imfsample);
			Marshal.ReleaseComObject(imfmediaBuffer);
			return num;
		}

		/// <summary>
		/// Disposes this instance
		/// </summary>
		/// <param name="disposing"></param>
		// Token: 0x06000946 RID: 2374 RVA: 0x0001B142 File Offset: 0x00019342
		protected void Dispose(bool disposing)
		{
			Marshal.ReleaseComObject(this.outputMediaType.MediaFoundationObject);
		}

		/// <summary>
		/// Disposes this instance
		/// </summary>
		// Token: 0x06000947 RID: 2375 RVA: 0x0001B155 File Offset: 0x00019355
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.Dispose(true);
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Finalizer
		/// </summary>
		// Token: 0x06000948 RID: 2376 RVA: 0x0001B174 File Offset: 0x00019374
		~MediaFoundationEncoder()
		{
			this.Dispose(false);
		}

		// Token: 0x04000AA2 RID: 2722
		private readonly MediaType outputMediaType;

		// Token: 0x04000AA3 RID: 2723
		private bool disposed;
	}
}
