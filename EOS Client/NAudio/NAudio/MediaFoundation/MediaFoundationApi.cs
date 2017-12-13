using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Main interface for using Media Foundation with NAudio
	/// </summary>
	// Token: 0x0200004E RID: 78
	public static class MediaFoundationApi
	{
		/// <summary>
		/// initializes MediaFoundation - only needs to be called once per process
		/// </summary>
		// Token: 0x060001B4 RID: 436 RVA: 0x000070A8 File Offset: 0x000052A8
		public static void Startup()
		{
			if (!MediaFoundationApi.initialized)
			{
				int num = 2;
				OperatingSystem osversion = Environment.OSVersion;
				if (osversion.Version.Major == 6 && osversion.Version.Minor == 0)
				{
					num = 1;
				}
				MediaFoundationInterop.MFStartup(num << 16 | 112, 0);
				MediaFoundationApi.initialized = true;
			}
		}

		/// <summary>
		/// Enumerate the installed MediaFoundation transforms in the specified category
		/// </summary>
		/// <param name="category">A category from MediaFoundationTransformCategories</param>
		/// <returns></returns>
		// Token: 0x060001B5 RID: 437 RVA: 0x00007300 File Offset: 0x00005500
		public static IEnumerable<IMFActivate> EnumerateTransforms(Guid category)
		{
			IntPtr interfacesPointer;
			int interfaceCount;
			MediaFoundationInterop.MFTEnumEx(category, _MFT_ENUM_FLAG.MFT_ENUM_FLAG_ALL, null, null, out interfacesPointer, out interfaceCount);
			IMFActivate[] interfaces = new IMFActivate[interfaceCount];
			for (int j = 0; j < interfaceCount; j++)
			{
				IntPtr pUnk = Marshal.ReadIntPtr(new IntPtr(interfacesPointer.ToInt64() + (long)(j * Marshal.SizeOf(interfacesPointer))));
				interfaces[j] = (IMFActivate)Marshal.GetObjectForIUnknown(pUnk);
			}
			foreach (IMFActivate i in interfaces)
			{
				yield return i;
			}
			Marshal.FreeCoTaskMem(interfacesPointer);
			yield break;
		}

		/// <summary>
		/// uninitializes MediaFoundation
		/// </summary>
		// Token: 0x060001B6 RID: 438 RVA: 0x0000731D File Offset: 0x0000551D
		public static void Shutdown()
		{
			if (MediaFoundationApi.initialized)
			{
				MediaFoundationInterop.MFShutdown();
				MediaFoundationApi.initialized = false;
			}
		}

		/// <summary>
		/// Creates a Media type
		/// </summary>
		// Token: 0x060001B7 RID: 439 RVA: 0x00007334 File Offset: 0x00005534
		public static IMFMediaType CreateMediaType()
		{
			IMFMediaType result;
			MediaFoundationInterop.MFCreateMediaType(out result);
			return result;
		}

		/// <summary>
		/// Creates a media type from a WaveFormat
		/// </summary>
		// Token: 0x060001B8 RID: 440 RVA: 0x0000734C File Offset: 0x0000554C
		public static IMFMediaType CreateMediaTypeFromWaveFormat(WaveFormat waveFormat)
		{
			IMFMediaType imfmediaType = MediaFoundationApi.CreateMediaType();
			try
			{
				MediaFoundationInterop.MFInitMediaTypeFromWaveFormatEx(imfmediaType, waveFormat, Marshal.SizeOf(waveFormat));
			}
			catch (Exception)
			{
				Marshal.ReleaseComObject(imfmediaType);
				throw;
			}
			return imfmediaType;
		}

		/// <summary>
		/// Creates a memory buffer of the specified size
		/// </summary>
		/// <param name="bufferSize">Memory buffer size in bytes</param>
		/// <returns>The memory buffer</returns>
		// Token: 0x060001B9 RID: 441 RVA: 0x0000738C File Offset: 0x0000558C
		public static IMFMediaBuffer CreateMemoryBuffer(int bufferSize)
		{
			IMFMediaBuffer result;
			MediaFoundationInterop.MFCreateMemoryBuffer(bufferSize, out result);
			return result;
		}

		/// <summary>
		/// Creates a sample object
		/// </summary>
		/// <returns>The sample object</returns>
		// Token: 0x060001BA RID: 442 RVA: 0x000073A4 File Offset: 0x000055A4
		public static IMFSample CreateSample()
		{
			IMFSample result;
			MediaFoundationInterop.MFCreateSample(out result);
			return result;
		}

		/// <summary>
		/// Creates a new attributes store
		/// </summary>
		/// <param name="initialSize">Initial size</param>
		/// <returns>The attributes store</returns>
		// Token: 0x060001BB RID: 443 RVA: 0x000073BC File Offset: 0x000055BC
		public static IMFAttributes CreateAttributes(int initialSize)
		{
			IMFAttributes result;
			MediaFoundationInterop.MFCreateAttributes(out result, initialSize);
			return result;
		}

		/// <summary>
		/// Creates a media foundation byte stream based on a stream object
		/// (usable with WinRT streams)
		/// </summary>
		/// <param name="stream">The input stream</param>
		/// <returns>A media foundation byte stream</returns>
		// Token: 0x060001BC RID: 444 RVA: 0x000073D4 File Offset: 0x000055D4
		public static IMFByteStream CreateByteStream(object stream)
		{
			IMFByteStream result;
			MediaFoundationInterop.MFCreateMFByteStreamOnStreamEx(stream, out result);
			return result;
		}

		/// <summary>
		/// Creates a source reader based on a byte stream
		/// </summary>
		/// <param name="byteStream">The byte stream</param>
		/// <returns>A media foundation source reader</returns>
		// Token: 0x060001BD RID: 445 RVA: 0x000073EC File Offset: 0x000055EC
		public static IMFSourceReader CreateSourceReaderFromByteStream(IMFByteStream byteStream)
		{
			IMFSourceReader result;
			MediaFoundationInterop.MFCreateSourceReaderFromByteStream(byteStream, null, out result);
			return result;
		}

		// Token: 0x040002D0 RID: 720
		private static bool initialized;
	}
}
