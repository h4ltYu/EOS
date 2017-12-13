using System;
using System.Runtime.InteropServices;
using System.Text;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// http://msdn.microsoft.com/en-gb/library/windows/desktop/ms702192%28v=vs.85%29.aspx
	/// </summary>
	// Token: 0x02000053 RID: 83
	[Guid("c40a00f2-b93a-4d80-ae8c-5a1c634f58e4")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IMFSample : IMFAttributes
	{
		/// <summary>
		/// Retrieves the value associated with a key.
		/// </summary>
		// Token: 0x06000203 RID: 515
		void GetItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [In] [Out] ref PropVariant pValue);

		/// <summary>
		/// Retrieves the data type of the value associated with a key.
		/// </summary>
		// Token: 0x06000204 RID: 516
		void GetItemType([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int pType);

		/// <summary>
		/// Queries whether a stored attribute value equals a specified PROPVARIANT.
		/// </summary>
		// Token: 0x06000205 RID: 517
		void CompareItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, IntPtr Value, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);

		/// <summary>
		/// Compares the attributes on this object with the attributes on another object.
		/// </summary>
		// Token: 0x06000206 RID: 518
		void Compare([MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs, int MatchType, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);

		/// <summary>
		/// Retrieves a UINT32 value associated with a key.
		/// </summary>
		// Token: 0x06000207 RID: 519
		void GetUINT32([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int punValue);

		/// <summary>
		/// Retrieves a UINT64 value associated with a key.
		/// </summary>
		// Token: 0x06000208 RID: 520
		void GetUINT64([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out long punValue);

		/// <summary>
		/// Retrieves a double value associated with a key.
		/// </summary>
		// Token: 0x06000209 RID: 521
		void GetDouble([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out double pfValue);

		/// <summary>
		/// Retrieves a GUID value associated with a key.
		/// </summary>
		// Token: 0x0600020A RID: 522
		void GetGUID([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out Guid pguidValue);

		/// <summary>
		/// Retrieves the length of a string value associated with a key.
		/// </summary>
		// Token: 0x0600020B RID: 523
		void GetStringLength([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int pcchLength);

		/// <summary>
		/// Retrieves a wide-character string associated with a key.
		/// </summary>
		// Token: 0x0600020C RID: 524
		void GetString([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszValue, int cchBufSize, out int pcchLength);

		/// <summary>
		/// Retrieves a wide-character string associated with a key. This method allocates the memory for the string.
		/// </summary>
		// Token: 0x0600020D RID: 525
		void GetAllocatedString([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue, out int pcchLength);

		/// <summary>
		/// Retrieves the length of a byte array associated with a key.
		/// </summary>
		// Token: 0x0600020E RID: 526
		void GetBlobSize([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int pcbBlobSize);

		/// <summary>
		/// Retrieves a byte array associated with a key.
		/// </summary>
		// Token: 0x0600020F RID: 527
		void GetBlob([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pBuf, int cbBufSize, out int pcbBlobSize);

		/// <summary>
		/// Retrieves a byte array associated with a key. This method allocates the memory for the array.
		/// </summary>
		// Token: 0x06000210 RID: 528
		void GetAllocatedBlob([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out IntPtr ip, out int pcbSize);

		/// <summary>
		/// Retrieves an interface pointer associated with a key.
		/// </summary>
		// Token: 0x06000211 RID: 529
		void GetUnknown([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		/// <summary>
		/// Associates an attribute value with a key.
		/// </summary>
		// Token: 0x06000212 RID: 530
		void SetItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, IntPtr Value);

		/// <summary>
		/// Removes a key/value pair from the object's attribute list.
		/// </summary>
		// Token: 0x06000213 RID: 531
		void DeleteItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey);

		/// <summary>
		/// Removes all key/value pairs from the object's attribute list.
		/// </summary>
		// Token: 0x06000214 RID: 532
		void DeleteAllItems();

		/// <summary>
		/// Associates a UINT32 value with a key.
		/// </summary>
		// Token: 0x06000215 RID: 533
		void SetUINT32([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, int unValue);

		/// <summary>
		/// Associates a UINT64 value with a key.
		/// </summary>
		// Token: 0x06000216 RID: 534
		void SetUINT64([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, long unValue);

		/// <summary>
		/// Associates a double value with a key.
		/// </summary>
		// Token: 0x06000217 RID: 535
		void SetDouble([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, double fValue);

		/// <summary>
		/// Associates a GUID value with a key.
		/// </summary>
		// Token: 0x06000218 RID: 536
		void SetGUID([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidValue);

		/// <summary>
		/// Associates a wide-character string with a key.
		/// </summary>
		// Token: 0x06000219 RID: 537
		void SetString([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszValue);

		/// <summary>
		/// Associates a byte array with a key.
		/// </summary>
		// Token: 0x0600021A RID: 538
		void SetBlob([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] pBuf, int cbBufSize);

		/// <summary>
		/// Associates an IUnknown pointer with a key.
		/// </summary>
		// Token: 0x0600021B RID: 539
		void SetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] [In] object pUnknown);

		/// <summary>
		/// Locks the attribute store so that no other thread can access it.
		/// </summary>
		// Token: 0x0600021C RID: 540
		void LockStore();

		/// <summary>
		/// Unlocks the attribute store.
		/// </summary>
		// Token: 0x0600021D RID: 541
		void UnlockStore();

		/// <summary>
		/// Retrieves the number of attributes that are set on this object.
		/// </summary>
		// Token: 0x0600021E RID: 542
		void GetCount(out int pcItems);

		/// <summary>
		/// Retrieves an attribute at the specified index.
		/// </summary>
		// Token: 0x0600021F RID: 543
		void GetItemByIndex(int unIndex, out Guid pGuidKey, [In] [Out] ref PropVariant pValue);

		/// <summary>
		/// Copies all of the attributes from this object into another attribute store.
		/// </summary>
		// Token: 0x06000220 RID: 544
		void CopyAllItems([MarshalAs(UnmanagedType.Interface)] [In] IMFAttributes pDest);

		/// <summary>
		/// Retrieves flags associated with the sample.
		/// </summary>
		// Token: 0x06000221 RID: 545
		void GetSampleFlags(out int pdwSampleFlags);

		/// <summary>
		/// Sets flags associated with the sample.
		/// </summary>
		// Token: 0x06000222 RID: 546
		void SetSampleFlags(int dwSampleFlags);

		/// <summary>
		/// Retrieves the presentation time of the sample.
		/// </summary>
		// Token: 0x06000223 RID: 547
		void GetSampleTime(out long phnsSampletime);

		/// <summary>
		/// Sets the presentation time of the sample.
		/// </summary>
		// Token: 0x06000224 RID: 548
		void SetSampleTime(long hnsSampleTime);

		/// <summary>
		/// Retrieves the duration of the sample.
		/// </summary>
		// Token: 0x06000225 RID: 549
		void GetSampleDuration(out long phnsSampleDuration);

		/// <summary>
		/// Sets the duration of the sample.
		/// </summary>
		// Token: 0x06000226 RID: 550
		void SetSampleDuration(long hnsSampleDuration);

		/// <summary>
		/// Retrieves the number of buffers in the sample.
		/// </summary>
		// Token: 0x06000227 RID: 551
		void GetBufferCount(out int pdwBufferCount);

		/// <summary>
		/// Retrieves a buffer from the sample.
		/// </summary>
		// Token: 0x06000228 RID: 552
		void GetBufferByIndex(int dwIndex, out IMFMediaBuffer ppBuffer);

		/// <summary>
		/// Converts a sample with multiple buffers into a sample with a single buffer.
		/// </summary>
		// Token: 0x06000229 RID: 553
		void ConvertToContiguousBuffer(out IMFMediaBuffer ppBuffer);

		/// <summary>
		///  Adds a buffer to the end of the list of buffers in the sample.
		/// </summary>
		// Token: 0x0600022A RID: 554
		void AddBuffer(IMFMediaBuffer pBuffer);

		/// <summary>
		/// Removes a buffer at a specified index from the sample.
		/// </summary>
		// Token: 0x0600022B RID: 555
		void RemoveBufferByIndex(int dwIndex);

		/// <summary>
		/// Removes all buffers from the sample.
		/// </summary>
		// Token: 0x0600022C RID: 556
		void RemoveAllBuffers();

		/// <summary>
		/// Retrieves the total length of the valid data in all of the buffers in the sample.
		/// </summary>
		// Token: 0x0600022D RID: 557
		void GetTotalLength(out int pcbTotalLength);

		/// <summary>
		/// Copies the sample data to a buffer.
		/// </summary>
		// Token: 0x0600022E RID: 558
		void CopyToBuffer(IMFMediaBuffer pBuffer);
	}
}
