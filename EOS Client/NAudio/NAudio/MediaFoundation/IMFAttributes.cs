using System;
using System.Runtime.InteropServices;
using System.Text;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Provides a generic way to store key/value pairs on an object.
	/// http://msdn.microsoft.com/en-gb/library/windows/desktop/ms704598%28v=vs.85%29.aspx
	/// </summary>
	// Token: 0x02000046 RID: 70
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("2CD2D921-C447-44A7-A13C-4ADABFC247E3")]
	[ComImport]
	public interface IMFAttributes
	{
		/// <summary>
		/// Retrieves the value associated with a key.
		/// </summary>
		// Token: 0x0600012A RID: 298
		void GetItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [In] [Out] ref PropVariant pValue);

		/// <summary>
		/// Retrieves the data type of the value associated with a key.
		/// </summary>
		// Token: 0x0600012B RID: 299
		void GetItemType([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int pType);

		/// <summary>
		/// Queries whether a stored attribute value equals a specified PROPVARIANT.
		/// </summary>
		// Token: 0x0600012C RID: 300
		void CompareItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, IntPtr Value, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);

		/// <summary>
		/// Compares the attributes on this object with the attributes on another object.
		/// </summary>
		// Token: 0x0600012D RID: 301
		void Compare([MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs, int MatchType, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);

		/// <summary>
		/// Retrieves a UINT32 value associated with a key.
		/// </summary>
		// Token: 0x0600012E RID: 302
		void GetUINT32([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int punValue);

		/// <summary>
		/// Retrieves a UINT64 value associated with a key.
		/// </summary>
		// Token: 0x0600012F RID: 303
		void GetUINT64([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out long punValue);

		/// <summary>
		/// Retrieves a double value associated with a key.
		/// </summary>
		// Token: 0x06000130 RID: 304
		void GetDouble([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out double pfValue);

		/// <summary>
		/// Retrieves a GUID value associated with a key.
		/// </summary>
		// Token: 0x06000131 RID: 305
		void GetGUID([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out Guid pguidValue);

		/// <summary>
		/// Retrieves the length of a string value associated with a key.
		/// </summary>
		// Token: 0x06000132 RID: 306
		void GetStringLength([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int pcchLength);

		/// <summary>
		/// Retrieves a wide-character string associated with a key.
		/// </summary>
		// Token: 0x06000133 RID: 307
		void GetString([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszValue, int cchBufSize, out int pcchLength);

		/// <summary>
		/// Retrieves a wide-character string associated with a key. This method allocates the memory for the string.
		/// </summary>
		// Token: 0x06000134 RID: 308
		void GetAllocatedString([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue, out int pcchLength);

		/// <summary>
		/// Retrieves the length of a byte array associated with a key.
		/// </summary>
		// Token: 0x06000135 RID: 309
		void GetBlobSize([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int pcbBlobSize);

		/// <summary>
		/// Retrieves a byte array associated with a key.
		/// </summary>
		// Token: 0x06000136 RID: 310
		void GetBlob([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pBuf, int cbBufSize, out int pcbBlobSize);

		/// <summary>
		/// Retrieves a byte array associated with a key. This method allocates the memory for the array.
		/// </summary>
		// Token: 0x06000137 RID: 311
		void GetAllocatedBlob([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out IntPtr ip, out int pcbSize);

		/// <summary>
		/// Retrieves an interface pointer associated with a key.
		/// </summary>
		// Token: 0x06000138 RID: 312
		void GetUnknown([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		/// <summary>
		/// Associates an attribute value with a key.
		/// </summary>
		// Token: 0x06000139 RID: 313
		void SetItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, IntPtr Value);

		/// <summary>
		/// Removes a key/value pair from the object's attribute list.
		/// </summary>
		// Token: 0x0600013A RID: 314
		void DeleteItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey);

		/// <summary>
		/// Removes all key/value pairs from the object's attribute list.
		/// </summary>
		// Token: 0x0600013B RID: 315
		void DeleteAllItems();

		/// <summary>
		/// Associates a UINT32 value with a key.
		/// </summary>
		// Token: 0x0600013C RID: 316
		void SetUINT32([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, int unValue);

		/// <summary>
		/// Associates a UINT64 value with a key.
		/// </summary>
		// Token: 0x0600013D RID: 317
		void SetUINT64([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, long unValue);

		/// <summary>
		/// Associates a double value with a key.
		/// </summary>
		// Token: 0x0600013E RID: 318
		void SetDouble([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, double fValue);

		/// <summary>
		/// Associates a GUID value with a key.
		/// </summary>
		// Token: 0x0600013F RID: 319
		void SetGUID([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidValue);

		/// <summary>
		/// Associates a wide-character string with a key.
		/// </summary>
		// Token: 0x06000140 RID: 320
		void SetString([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszValue);

		/// <summary>
		/// Associates a byte array with a key.
		/// </summary>
		// Token: 0x06000141 RID: 321
		void SetBlob([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] pBuf, int cbBufSize);

		/// <summary>
		/// Associates an IUnknown pointer with a key.
		/// </summary>
		// Token: 0x06000142 RID: 322
		void SetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] [In] object pUnknown);

		/// <summary>
		/// Locks the attribute store so that no other thread can access it.
		/// </summary>
		// Token: 0x06000143 RID: 323
		void LockStore();

		/// <summary>
		/// Unlocks the attribute store.
		/// </summary>
		// Token: 0x06000144 RID: 324
		void UnlockStore();

		/// <summary>
		/// Retrieves the number of attributes that are set on this object.
		/// </summary>
		// Token: 0x06000145 RID: 325
		void GetCount(out int pcItems);

		/// <summary>
		/// Retrieves an attribute at the specified index.
		/// </summary>
		// Token: 0x06000146 RID: 326
		void GetItemByIndex(int unIndex, out Guid pGuidKey, [In] [Out] ref PropVariant pValue);

		/// <summary>
		/// Copies all of the attributes from this object into another attribute store.
		/// </summary>
		// Token: 0x06000147 RID: 327
		void CopyAllItems([MarshalAs(UnmanagedType.Interface)] [In] IMFAttributes pDest);
	}
}
