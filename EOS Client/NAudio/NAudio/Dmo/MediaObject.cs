using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NAudio.Utils;
using NAudio.Wave;

namespace NAudio.Dmo
{
	/// <summary>
	/// Media Object
	/// </summary>
	// Token: 0x02000090 RID: 144
	public class MediaObject : IDisposable
	{
		/// <summary>
		/// Creates a new Media Object
		/// </summary>
		/// <param name="mediaObject">Media Object COM interface</param>
		// Token: 0x06000313 RID: 787 RVA: 0x0000A333 File Offset: 0x00008533
		internal MediaObject(IMediaObject mediaObject)
		{
			this.mediaObject = mediaObject;
			mediaObject.GetStreamCount(out this.inputStreams, out this.outputStreams);
		}

		/// <summary>
		/// Number of input streams
		/// </summary>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000A355 File Offset: 0x00008555
		public int InputStreamCount
		{
			get
			{
				return this.inputStreams;
			}
		}

		/// <summary>
		/// Number of output streams
		/// </summary>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000A35D File Offset: 0x0000855D
		public int OutputStreamCount
		{
			get
			{
				return this.outputStreams;
			}
		}

		/// <summary>
		/// Gets the input media type for the specified input stream
		/// </summary>
		/// <param name="inputStream">Input stream index</param>
		/// <param name="inputTypeIndex">Input type index</param>
		/// <returns>DMO Media Type or null if there are no more input types</returns>
		// Token: 0x06000316 RID: 790 RVA: 0x0000A368 File Offset: 0x00008568
		public DmoMediaType? GetInputType(int inputStream, int inputTypeIndex)
		{
			try
			{
				DmoMediaType value;
				if (this.mediaObject.GetInputType(inputStream, inputTypeIndex, out value) == 0)
				{
					DmoInterop.MoFreeMediaType(ref value);
					return new DmoMediaType?(value);
				}
			}
			catch (COMException exception)
			{
				if (exception.GetHResult() != -2147220986)
				{
					throw;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the DMO Media Output type
		/// </summary>
		/// <param name="outputStream">The output stream</param>
		/// <param name="outputTypeIndex">Output type index</param>
		/// <returns>DMO Media Type or null if no more available</returns>
		// Token: 0x06000317 RID: 791 RVA: 0x0000A3CC File Offset: 0x000085CC
		public DmoMediaType? GetOutputType(int outputStream, int outputTypeIndex)
		{
			try
			{
				DmoMediaType value;
				if (this.mediaObject.GetOutputType(outputStream, outputTypeIndex, out value) == 0)
				{
					DmoInterop.MoFreeMediaType(ref value);
					return new DmoMediaType?(value);
				}
			}
			catch (COMException exception)
			{
				if (exception.GetHResult() != -2147220986)
				{
					throw;
				}
			}
			return null;
		}

		/// <summary>
		/// retrieves the media type that was set for an output stream, if any
		/// </summary>
		/// <param name="outputStreamIndex">Output stream index</param>
		/// <returns>DMO Media Type or null if no more available</returns>
		// Token: 0x06000318 RID: 792 RVA: 0x0000A430 File Offset: 0x00008630
		public DmoMediaType GetOutputCurrentType(int outputStreamIndex)
		{
			DmoMediaType result;
			int outputCurrentType = this.mediaObject.GetOutputCurrentType(outputStreamIndex, out result);
			if (outputCurrentType == 0)
			{
				DmoInterop.MoFreeMediaType(ref result);
				return result;
			}
			if (outputCurrentType == -2147220989)
			{
				throw new InvalidOperationException("Media type was not set.");
			}
			throw Marshal.GetExceptionForHR(outputCurrentType);
		}

		/// <summary>
		/// Enumerates the supported input types
		/// </summary>
		/// <param name="inputStreamIndex">Input stream index</param>
		/// <returns>Enumeration of input types</returns>
		// Token: 0x06000319 RID: 793 RVA: 0x0000A59C File Offset: 0x0000879C
		public IEnumerable<DmoMediaType> GetInputTypes(int inputStreamIndex)
		{
			int typeIndex = 0;
			for (;;)
			{
				DmoMediaType? mediaType;
				DmoMediaType? dmoMediaType = mediaType = this.GetInputType(inputStreamIndex, typeIndex);
				if (dmoMediaType == null)
				{
					break;
				}
				yield return mediaType.Value;
				typeIndex++;
			}
			yield break;
		}

		/// <summary>
		/// Enumerates the output types
		/// </summary>
		/// <param name="outputStreamIndex">Output stream index</param>
		/// <returns>Enumeration of supported output types</returns>
		// Token: 0x0600031A RID: 794 RVA: 0x0000A6E8 File Offset: 0x000088E8
		public IEnumerable<DmoMediaType> GetOutputTypes(int outputStreamIndex)
		{
			int typeIndex = 0;
			for (;;)
			{
				DmoMediaType? mediaType;
				DmoMediaType? dmoMediaType = mediaType = this.GetOutputType(outputStreamIndex, typeIndex);
				if (dmoMediaType == null)
				{
					break;
				}
				yield return mediaType.Value;
				typeIndex++;
			}
			yield break;
		}

		/// <summary>
		/// Querys whether a specified input type is supported
		/// </summary>
		/// <param name="inputStreamIndex">Input stream index</param>
		/// <param name="mediaType">Media type to check</param>
		/// <returns>true if supports</returns>
		// Token: 0x0600031B RID: 795 RVA: 0x0000A70C File Offset: 0x0000890C
		public bool SupportsInputType(int inputStreamIndex, DmoMediaType mediaType)
		{
			return this.SetInputType(inputStreamIndex, mediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
		}

		/// <summary>
		/// Sets the input type helper method
		/// </summary>
		/// <param name="inputStreamIndex">Input stream index</param>
		/// <param name="mediaType">Media type</param>
		/// <param name="flags">Flags (can be used to test rather than set)</param>
		// Token: 0x0600031C RID: 796 RVA: 0x0000A718 File Offset: 0x00008918
		private bool SetInputType(int inputStreamIndex, DmoMediaType mediaType, DmoSetTypeFlags flags)
		{
			int num = this.mediaObject.SetInputType(inputStreamIndex, ref mediaType, flags);
			if (num == 0)
			{
				return true;
			}
			if (num == -2147220991)
			{
				throw new ArgumentException("Invalid stream index");
			}
			return false;
		}

		/// <summary>
		/// Sets the input type
		/// </summary>
		/// <param name="inputStreamIndex">Input stream index</param>
		/// <param name="mediaType">Media Type</param>
		// Token: 0x0600031D RID: 797 RVA: 0x0000A756 File Offset: 0x00008956
		public void SetInputType(int inputStreamIndex, DmoMediaType mediaType)
		{
			if (!this.SetInputType(inputStreamIndex, mediaType, DmoSetTypeFlags.None))
			{
				throw new ArgumentException("Media Type not supported");
			}
		}

		/// <summary>
		/// Sets the input type to the specified Wave format
		/// </summary>
		/// <param name="inputStreamIndex">Input stream index</param>
		/// <param name="waveFormat">Wave format</param>
		// Token: 0x0600031E RID: 798 RVA: 0x0000A770 File Offset: 0x00008970
		public void SetInputWaveFormat(int inputStreamIndex, WaveFormat waveFormat)
		{
			DmoMediaType mediaType = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
			bool flag = this.SetInputType(inputStreamIndex, mediaType, DmoSetTypeFlags.None);
			DmoInterop.MoFreeMediaType(ref mediaType);
			if (!flag)
			{
				throw new ArgumentException("Media Type not supported");
			}
		}

		/// <summary>
		/// Requests whether the specified Wave format is supported as an input
		/// </summary>
		/// <param name="inputStreamIndex">Input stream index</param>
		/// <param name="waveFormat">Wave format</param>
		/// <returns>true if supported</returns>
		// Token: 0x0600031F RID: 799 RVA: 0x0000A7A8 File Offset: 0x000089A8
		public bool SupportsInputWaveFormat(int inputStreamIndex, WaveFormat waveFormat)
		{
			DmoMediaType mediaType = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
			bool result = this.SetInputType(inputStreamIndex, mediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
			DmoInterop.MoFreeMediaType(ref mediaType);
			return result;
		}

		/// <summary>
		/// Helper function to make a DMO Media Type to represent a particular WaveFormat
		/// </summary>
		// Token: 0x06000320 RID: 800 RVA: 0x0000A7D0 File Offset: 0x000089D0
		private DmoMediaType CreateDmoMediaTypeForWaveFormat(WaveFormat waveFormat)
		{
			DmoMediaType result = default(DmoMediaType);
			int formatBlockBytes = Marshal.SizeOf(waveFormat);
			DmoInterop.MoInitMediaType(ref result, formatBlockBytes);
			result.SetWaveFormat(waveFormat);
			return result;
		}

		/// <summary>
		/// Checks if a specified output type is supported
		/// n.b. you may need to set the input type first
		/// </summary>
		/// <param name="outputStreamIndex">Output stream index</param>
		/// <param name="mediaType">Media type</param>
		/// <returns>True if supported</returns>
		// Token: 0x06000321 RID: 801 RVA: 0x0000A7FE File Offset: 0x000089FE
		public bool SupportsOutputType(int outputStreamIndex, DmoMediaType mediaType)
		{
			return this.SetOutputType(outputStreamIndex, mediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
		}

		/// <summary>
		/// Tests if the specified Wave Format is supported for output
		/// n.b. may need to set the input type first
		/// </summary>
		/// <param name="outputStreamIndex">Output stream index</param>
		/// <param name="waveFormat">Wave format</param>
		/// <returns>True if supported</returns>
		// Token: 0x06000322 RID: 802 RVA: 0x0000A80C File Offset: 0x00008A0C
		public bool SupportsOutputWaveFormat(int outputStreamIndex, WaveFormat waveFormat)
		{
			DmoMediaType mediaType = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
			bool result = this.SetOutputType(outputStreamIndex, mediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
			DmoInterop.MoFreeMediaType(ref mediaType);
			return result;
		}

		/// <summary>
		/// Helper method to call SetOutputType
		/// </summary>
		// Token: 0x06000323 RID: 803 RVA: 0x0000A834 File Offset: 0x00008A34
		private bool SetOutputType(int outputStreamIndex, DmoMediaType mediaType, DmoSetTypeFlags flags)
		{
			int num = this.mediaObject.SetOutputType(outputStreamIndex, ref mediaType, flags);
			if (num == -2147220987)
			{
				return false;
			}
			if (num == 0)
			{
				return true;
			}
			throw Marshal.GetExceptionForHR(num);
		}

		/// <summary>
		/// Sets the output type
		/// n.b. may need to set the input type first
		/// </summary>
		/// <param name="outputStreamIndex">Output stream index</param>
		/// <param name="mediaType">Media type to set</param>
		// Token: 0x06000324 RID: 804 RVA: 0x0000A866 File Offset: 0x00008A66
		public void SetOutputType(int outputStreamIndex, DmoMediaType mediaType)
		{
			if (!this.SetOutputType(outputStreamIndex, mediaType, DmoSetTypeFlags.None))
			{
				throw new ArgumentException("Media Type not supported");
			}
		}

		/// <summary>
		/// Set output type to the specified wave format
		/// n.b. may need to set input type first
		/// </summary>
		/// <param name="outputStreamIndex">Output stream index</param>
		/// <param name="waveFormat">Wave format</param>
		// Token: 0x06000325 RID: 805 RVA: 0x0000A880 File Offset: 0x00008A80
		public void SetOutputWaveFormat(int outputStreamIndex, WaveFormat waveFormat)
		{
			DmoMediaType mediaType = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
			bool flag = this.SetOutputType(outputStreamIndex, mediaType, DmoSetTypeFlags.None);
			DmoInterop.MoFreeMediaType(ref mediaType);
			if (!flag)
			{
				throw new ArgumentException("Media Type not supported");
			}
		}

		/// <summary>
		/// Get Input Size Info
		/// </summary>
		/// <param name="inputStreamIndex">Input Stream Index</param>
		/// <returns>Input Size Info</returns>
		// Token: 0x06000326 RID: 806 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		public MediaObjectSizeInfo GetInputSizeInfo(int inputStreamIndex)
		{
			int size;
			int maxLookahead;
			int alignment;
			Marshal.ThrowExceptionForHR(this.mediaObject.GetInputSizeInfo(inputStreamIndex, out size, out maxLookahead, out alignment));
			return new MediaObjectSizeInfo(size, maxLookahead, alignment);
		}

		/// <summary>
		/// Get Output Size Info
		/// </summary>
		/// <param name="outputStreamIndex">Output Stream Index</param>
		/// <returns>Output Size Info</returns>
		// Token: 0x06000327 RID: 807 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		public MediaObjectSizeInfo GetOutputSizeInfo(int outputStreamIndex)
		{
			int size;
			int alignment;
			Marshal.ThrowExceptionForHR(this.mediaObject.GetOutputSizeInfo(outputStreamIndex, out size, out alignment));
			return new MediaObjectSizeInfo(size, 0, alignment);
		}

		/// <summary>
		/// Process Input
		/// </summary>
		/// <param name="inputStreamIndex">Input Stream index</param>
		/// <param name="mediaBuffer">Media Buffer</param>
		/// <param name="flags">Flags</param>
		/// <param name="timestamp">Timestamp</param>
		/// <param name="duration">Duration</param>
		// Token: 0x06000328 RID: 808 RVA: 0x0000A90E File Offset: 0x00008B0E
		public void ProcessInput(int inputStreamIndex, IMediaBuffer mediaBuffer, DmoInputDataBufferFlags flags, long timestamp, long duration)
		{
			Marshal.ThrowExceptionForHR(this.mediaObject.ProcessInput(inputStreamIndex, mediaBuffer, flags, timestamp, duration));
		}

		/// <summary>
		/// Process Output
		/// </summary>
		/// <param name="flags">Flags</param>
		/// <param name="outputBufferCount">Output buffer count</param>
		/// <param name="outputBuffers">Output buffers</param>
		// Token: 0x06000329 RID: 809 RVA: 0x0000A928 File Offset: 0x00008B28
		public void ProcessOutput(DmoProcessOutputFlags flags, int outputBufferCount, DmoOutputDataBuffer[] outputBuffers)
		{
			int num;
			Marshal.ThrowExceptionForHR(this.mediaObject.ProcessOutput(flags, outputBufferCount, outputBuffers, out num));
		}

		/// <summary>
		/// Gives the DMO a chance to allocate any resources needed for streaming
		/// </summary>
		// Token: 0x0600032A RID: 810 RVA: 0x0000A94A File Offset: 0x00008B4A
		public void AllocateStreamingResources()
		{
			Marshal.ThrowExceptionForHR(this.mediaObject.AllocateStreamingResources());
		}

		/// <summary>
		/// Tells the DMO to free any resources needed for streaming
		/// </summary>
		// Token: 0x0600032B RID: 811 RVA: 0x0000A95C File Offset: 0x00008B5C
		public void FreeStreamingResources()
		{
			Marshal.ThrowExceptionForHR(this.mediaObject.FreeStreamingResources());
		}

		/// <summary>
		/// Gets maximum input latency
		/// </summary>
		/// <param name="inputStreamIndex">input stream index</param>
		/// <returns>Maximum input latency as a ref-time</returns>
		// Token: 0x0600032C RID: 812 RVA: 0x0000A970 File Offset: 0x00008B70
		public long GetInputMaxLatency(int inputStreamIndex)
		{
			long result;
			Marshal.ThrowExceptionForHR(this.mediaObject.GetInputMaxLatency(inputStreamIndex, out result));
			return result;
		}

		/// <summary>
		/// Flushes all buffered data
		/// </summary>
		// Token: 0x0600032D RID: 813 RVA: 0x0000A991 File Offset: 0x00008B91
		public void Flush()
		{
			Marshal.ThrowExceptionForHR(this.mediaObject.Flush());
		}

		/// <summary>
		/// Report a discontinuity on the specified input stream
		/// </summary>
		/// <param name="inputStreamIndex">Input Stream index</param>
		// Token: 0x0600032E RID: 814 RVA: 0x0000A9A3 File Offset: 0x00008BA3
		public void Discontinuity(int inputStreamIndex)
		{
			Marshal.ThrowExceptionForHR(this.mediaObject.Discontinuity(inputStreamIndex));
		}

		/// <summary>
		/// Is this input stream accepting data?
		/// </summary>
		/// <param name="inputStreamIndex">Input Stream index</param>
		/// <returns>true if accepting data</returns>
		// Token: 0x0600032F RID: 815 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		public bool IsAcceptingData(int inputStreamIndex)
		{
			DmoInputStatusFlags dmoInputStatusFlags;
			int inputStatus = this.mediaObject.GetInputStatus(inputStreamIndex, out dmoInputStatusFlags);
			Marshal.ThrowExceptionForHR(inputStatus);
			return (dmoInputStatusFlags & DmoInputStatusFlags.DMO_INPUT_STATUSF_ACCEPT_DATA) == DmoInputStatusFlags.DMO_INPUT_STATUSF_ACCEPT_DATA;
		}

		/// <summary>
		/// Experimental code, not currently being called
		/// Not sure if it is necessary anyway
		/// </summary>
		// Token: 0x06000330 RID: 816 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public void Dispose()
		{
			if (this.mediaObject != null)
			{
				Marshal.ReleaseComObject(this.mediaObject);
				this.mediaObject = null;
			}
		}

		// Token: 0x040003FD RID: 1021
		private IMediaObject mediaObject;

		// Token: 0x040003FE RID: 1022
		private int inputStreams;

		// Token: 0x040003FF RID: 1023
		private int outputStreams;
	}
}
