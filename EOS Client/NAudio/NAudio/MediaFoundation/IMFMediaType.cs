using System;
using System.Runtime.InteropServices;
using System.Text;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Represents a description of a media format. 
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/ms704850%28v=vs.85%29.aspx
	/// </summary>
	// Token: 0x02000052 RID: 82
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("44AE0FA8-EA31-4109-8D2E-4CAE4997C555")]
	[ComImport]
	public interface IMFMediaType : IMFAttributes
	{
		/// <summary>
		/// Retrieves the value associated with a key.
		/// </summary>
		// Token: 0x060001E0 RID: 480
		void GetItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [In] [Out] ref PropVariant pValue);

		/// <summary>
		/// Retrieves the data type of the value associated with a key.
		/// </summary>
		// Token: 0x060001E1 RID: 481
		void GetItemType([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int pType);

		/// <summary>
		/// Queries whether a stored attribute value equals a specified PROPVARIANT.
		/// </summary>
		// Token: 0x060001E2 RID: 482
		void CompareItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, IntPtr Value, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);

		/// <summary>
		/// Compares the attributes on this object with the attributes on another object.
		/// </summary>
		// Token: 0x060001E3 RID: 483
		void Compare([MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs, int MatchType, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);

		/// <summary>
		/// Retrieves a UINT32 value associated with a key.
		/// </summary>
		// Token: 0x060001E4 RID: 484
		void GetUINT32([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int punValue);

		/// <summary>
		/// Retrieves a UINT64 value associated with a key.
		/// </summary>
		// Token: 0x060001E5 RID: 485
		void GetUINT64([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out long punValue);

		/// <summary>
		/// Retrieves a double value associated with a key.
		/// </summary>
		// Token: 0x060001E6 RID: 486
		void GetDouble([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out double pfValue);

		/// <summary>
		/// Retrieves a GUID value associated with a key.
		/// </summary>
		// Token: 0x060001E7 RID: 487
		void GetGUID([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out Guid pguidValue);

		/// <summary>
		/// Retrieves the length of a string value associated with a key.
		/// </summary>
		// Token: 0x060001E8 RID: 488
		void GetStringLength([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int pcchLength);

		/// <summary>
		/// Retrieves a wide-character string associated with a key.
		/// </summary>
		// Token: 0x060001E9 RID: 489
		void GetString([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszValue, int cchBufSize, out int pcchLength);

		/// <summary>
		/// Retrieves a wide-character string associated with a key. This method allocates the memory for the string.
		/// </summary>
		// Token: 0x060001EA RID: 490
		void GetAllocatedString([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue, out int pcchLength);

		/// <summary>
		/// Retrieves the length of a byte array associated with a key.
		/// </summary>
		// Token: 0x060001EB RID: 491
		void GetBlobSize([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int pcbBlobSize);

		/// <summary>
		/// Retrieves a byte array associated with a key.
		/// </summary>
		// Token: 0x060001EC RID: 492
		void GetBlob([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pBuf, int cbBufSize, out int pcbBlobSize);

		/// <summary>
		/// Retrieves a byte array associated with a key. This method allocates the memory for the array.
		/// </summary>
		// Token: 0x060001ED RID: 493
		void GetAllocatedBlob([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out IntPtr ip, out int pcbSize);

		/// <summary>
		/// Retrieves an interface pointer associated with a key.
		/// </summary>
		// Token: 0x060001EE RID: 494
		void GetUnknown([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		/// <summary>
		/// Associates an attribute value with a key.
		/// </summary>
		// Token: 0x060001EF RID: 495
		void SetItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, IntPtr Value);

		/// <summary>
		/// Removes a key/value pair from the object's attribute list.
		/// </summary>
		// Token: 0x060001F0 RID: 496
		void DeleteItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey);

		/// <summary>
		/// Removes all key/value pairs from the object's attribute list.
		/// </summary>
		// Token: 0x060001F1 RID: 497
		void DeleteAllItems();

		/// <summary>
		/// Associates a UINT32 value with a key.
		/// </summary>
		// Token: 0x060001F2 RID: 498
		void SetUINT32([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, int unValue);

		/// <summary>
		/// Associates a UINT64 value with a key.
		/// </summary>
		// Token: 0x060001F3 RID: 499
		void SetUINT64([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, long unValue);

		/// <summary>
		/// Associates a double value with a key.
		/// </summary>
		// Token: 0x060001F4 RID: 500
		void SetDouble([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, double fValue);

		/// <summary>
		/// Associates a GUID value with a key.
		/// </summary>
		// Token: 0x060001F5 RID: 501
		void SetGUID([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidValue);

		/// <summary>
		/// Associates a wide-character string with a key.
		/// </summary>
		// Token: 0x060001F6 RID: 502
		void SetString([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszValue);

		/// <summary>
		/// Associates a byte array with a key.
		/// </summary>
		// Token: 0x060001F7 RID: 503
		void SetBlob([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] pBuf, int cbBufSize);

		/// <summary>
		/// Associates an IUnknown pointer with a key.
		/// </summary>
		// Token: 0x060001F8 RID: 504
		void SetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] [In] object pUnknown);

		/// <summary>
		/// Locks the attribute store so that no other thread can access it.
		/// </summary>
		// Token: 0x060001F9 RID: 505
		void LockStore();

		/// <summary>
		/// Unlocks the attribute store.
		/// </summary>
		// Token: 0x060001FA RID: 506
		void UnlockStore();

		/// <summary>
		/// Retrieves the number of attributes that are set on this object.
		/// </summary>
		// Token: 0x060001FB RID: 507
		void GetCount(out int pcItems);

		/// <summary>
		/// Retrieves an attribute at the specified index.
		/// </summary>
		// Token: 0x060001FC RID: 508
		void GetItemByIndex(int unIndex, out Guid pGuidKey, [In] [Out] ref PropVariant pValue);

		/// <summary>
		/// Copies all of the attributes from this object into another attribute store.
		/// </summary>
		// Token: 0x060001FD RID: 509
		void CopyAllItems([MarshalAs(UnmanagedType.Interface)] [In] IMFAttributes pDest);

		/// <summary>
		/// Retrieves the major type of the format.
		/// </summary>
		// Token: 0x060001FE RID: 510
		void GetMajorType(out Guid pguidMajorType);

		/// <summary>
		/// Queries whether the media type is a compressed format.
		/// </summary>
		// Token: 0x060001FF RID: 511
		void IsCompressedFormat([MarshalAs(UnmanagedType.Bool)] out bool pfCompressed);

		/// <summary>
		/// Compares two media types and determines whether they are identical.
		/// </summary>
		// Token: 0x06000200 RID: 512
		[PreserveSig]
		int IsEqual([MarshalAs(UnmanagedType.Interface)] [In] IMFMediaType pIMediaType, ref int pdwFlags);

		/// <summary>
		/// Retrieves an alternative representation of the media type.
		/// </summary>
		// Token: 0x06000201 RID: 513
		void GetRepresentation([MarshalAs(UnmanagedType.Struct)] [In] Guid guidRepresentation, ref IntPtr ppvRepresentation);

		/// <summary>
		/// Frees memory that was allocated by the GetRepresentation method.
		/// </summary>
		// Token: 0x06000202 RID: 514
		void FreeRepresentation([MarshalAs(UnmanagedType.Struct)] [In] Guid guidRepresentation, [In] IntPtr pvRepresentation);
	}
}
