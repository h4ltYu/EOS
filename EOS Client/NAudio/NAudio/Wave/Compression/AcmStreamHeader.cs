using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000176 RID: 374
	internal class AcmStreamHeader : IDisposable
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x00016F64 File Offset: 0x00015164
		public AcmStreamHeader(IntPtr streamHandle, int sourceBufferLength, int destBufferLength)
		{
			this.streamHeader = new AcmStreamHeaderStruct();
			this.sourceBuffer = new byte[sourceBufferLength];
			this.hSourceBuffer = GCHandle.Alloc(this.sourceBuffer, GCHandleType.Pinned);
			this.destBuffer = new byte[destBufferLength];
			this.hDestBuffer = GCHandle.Alloc(this.destBuffer, GCHandleType.Pinned);
			this.streamHandle = streamHandle;
			this.firstTime = true;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00016FCC File Offset: 0x000151CC
		private void Prepare()
		{
			this.streamHeader.cbStruct = Marshal.SizeOf(this.streamHeader);
			this.streamHeader.sourceBufferLength = this.sourceBuffer.Length;
			this.streamHeader.sourceBufferPointer = this.hSourceBuffer.AddrOfPinnedObject();
			this.streamHeader.destBufferLength = this.destBuffer.Length;
			this.streamHeader.destBufferPointer = this.hDestBuffer.AddrOfPinnedObject();
			MmException.Try(AcmInterop.acmStreamPrepareHeader(this.streamHandle, this.streamHeader, 0), "acmStreamPrepareHeader");
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00017060 File Offset: 0x00015260
		private void Unprepare()
		{
			this.streamHeader.sourceBufferLength = this.sourceBuffer.Length;
			this.streamHeader.sourceBufferPointer = this.hSourceBuffer.AddrOfPinnedObject();
			this.streamHeader.destBufferLength = this.destBuffer.Length;
			this.streamHeader.destBufferPointer = this.hDestBuffer.AddrOfPinnedObject();
			MmResult mmResult = AcmInterop.acmStreamUnprepareHeader(this.streamHandle, this.streamHeader, 0);
			if (mmResult != MmResult.NoError)
			{
				throw new MmException(mmResult, "acmStreamUnprepareHeader");
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x000170E1 File Offset: 0x000152E1
		public void Reposition()
		{
			this.firstTime = true;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000170EC File Offset: 0x000152EC
		public int Convert(int bytesToConvert, out int sourceBytesConverted)
		{
			this.Prepare();
			try
			{
				this.streamHeader.sourceBufferLength = bytesToConvert;
				this.streamHeader.sourceBufferLengthUsed = bytesToConvert;
				AcmStreamConvertFlags streamConvertFlags = this.firstTime ? (AcmStreamConvertFlags.BlockAlign | AcmStreamConvertFlags.Start) : AcmStreamConvertFlags.BlockAlign;
				MmException.Try(AcmInterop.acmStreamConvert(this.streamHandle, this.streamHeader, streamConvertFlags), "acmStreamConvert");
				this.firstTime = false;
				sourceBytesConverted = this.streamHeader.sourceBufferLengthUsed;
			}
			finally
			{
				this.Unprepare();
			}
			return this.streamHeader.destBufferLengthUsed;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001717C File Offset: 0x0001537C
		public byte[] SourceBuffer
		{
			get
			{
				return this.sourceBuffer;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00017184 File Offset: 0x00015384
		public byte[] DestBuffer
		{
			get
			{
				return this.destBuffer;
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001718C File Offset: 0x0001538C
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001719B File Offset: 0x0001539B
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.sourceBuffer = null;
				this.destBuffer = null;
				this.hSourceBuffer.Free();
				this.hDestBuffer.Free();
			}
			this.disposed = true;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000171D0 File Offset: 0x000153D0
		~AcmStreamHeader()
		{
			this.Dispose(false);
		}

		// Token: 0x04000836 RID: 2102
		private AcmStreamHeaderStruct streamHeader;

		// Token: 0x04000837 RID: 2103
		private byte[] sourceBuffer;

		// Token: 0x04000838 RID: 2104
		private GCHandle hSourceBuffer;

		// Token: 0x04000839 RID: 2105
		private byte[] destBuffer;

		// Token: 0x0400083A RID: 2106
		private GCHandle hDestBuffer;

		// Token: 0x0400083B RID: 2107
		private IntPtr streamHandle;

		// Token: 0x0400083C RID: 2108
		private bool firstTime;

		// Token: 0x0400083D RID: 2109
		private bool disposed;
	}
}
