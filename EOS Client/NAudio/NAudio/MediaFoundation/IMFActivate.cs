using System;
using System.Runtime.InteropServices;
using System.Text;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// IMFActivate, defined in mfobjects.h
	/// </summary>
	// Token: 0x02000047 RID: 71
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("7FEE9E9A-4A89-47a6-899C-B6A53A70FB67")]
	[ComImport]
	public interface IMFActivate : IMFAttributes
	{
		/// <summary>
		/// Retrieves the value associated with a key.
		/// </summary>
		// Token: 0x06000148 RID: 328
		void GetItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [In] [Out] ref PropVariant pValue);

		/// <summary>
		/// Retrieves the data type of the value associated with a key.
		/// </summary>
		// Token: 0x06000149 RID: 329
		void GetItemType([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int pType);

		/// <summary>
		/// Queries whether a stored attribute value equals a specified PROPVARIANT.
		/// </summary>
		// Token: 0x0600014A RID: 330
		void CompareItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, IntPtr Value, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);

		/// <summary>
		/// Compares the attributes on this object with the attributes on another object.
		/// </summary>
		// Token: 0x0600014B RID: 331
		void Compare([MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs, int MatchType, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);

		/// <summary>
		/// Retrieves a UINT32 value associated with a key.
		/// </summary>
		// Token: 0x0600014C RID: 332
		void GetUINT32([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int punValue);

		/// <summary>
		/// Retrieves a UINT64 value associated with a key.
		/// </summary>
		// Token: 0x0600014D RID: 333
		void GetUINT64([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out long punValue);

		/// <summary>
		/// Retrieves a double value associated with a key.
		/// </summary>
		// Token: 0x0600014E RID: 334
		void GetDouble([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out double pfValue);

		/// <summary>
		/// Retrieves a GUID value associated with a key.
		/// </summary>
		// Token: 0x0600014F RID: 335
		void GetGUID([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out Guid pguidValue);

		/// <summary>
		/// Retrieves the length of a string value associated with a key.
		/// </summary>
		// Token: 0x06000150 RID: 336
		void GetStringLength([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int pcchLength);

		/// <summary>
		/// Retrieves a wide-character string associated with a key.
		/// </summary>
		// Token: 0x06000151 RID: 337
		void GetString([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszValue, int cchBufSize, out int pcchLength);

		/// <summary>
		/// Retrieves a wide-character string associated with a key. This method allocates the memory for the string.
		/// </summary>
		// Token: 0x06000152 RID: 338
		void GetAllocatedString([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue, out int pcchLength);

		/// <summary>
		/// Retrieves the length of a byte array associated with a key.
		/// </summary>
		// Token: 0x06000153 RID: 339
		void GetBlobSize([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out int pcbBlobSize);

		/// <summary>
		/// Retrieves a byte array associated with a key.
		/// </summary>
		// Token: 0x06000154 RID: 340
		void GetBlob([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pBuf, int cbBufSize, out int pcbBlobSize);

		/// <summary>
		/// Retrieves a byte array associated with a key. This method allocates the memory for the array.
		/// </summary>
		// Token: 0x06000155 RID: 341
		void GetAllocatedBlob([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, out IntPtr ip, out int pcbSize);

		/// <summary>
		/// Retrieves an interface pointer associated with a key.
		/// </summary>
		// Token: 0x06000156 RID: 342
		void GetUnknown([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		/// <summary>
		/// Associates an attribute value with a key.
		/// </summary>
		// Token: 0x06000157 RID: 343
		void SetItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, IntPtr Value);

		/// <summary>
		/// Removes a key/value pair from the object's attribute list.
		/// </summary>
		// Token: 0x06000158 RID: 344
		void DeleteItem([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey);

		/// <summary>
		/// Removes all key/value pairs from the object's attribute list.
		/// </summary>
		// Token: 0x06000159 RID: 345
		void DeleteAllItems();

		/// <summary>
		/// Associates a UINT32 value with a key.
		/// </summary>
		// Token: 0x0600015A RID: 346
		void SetUINT32([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, int unValue);

		/// <summary>
		/// Associates a UINT64 value with a key.
		/// </summary>
		// Token: 0x0600015B RID: 347
		void SetUINT64([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, long unValue);

		/// <summary>
		/// Associates a double value with a key.
		/// </summary>
		// Token: 0x0600015C RID: 348
		void SetDouble([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, double fValue);

		/// <summary>
		/// Associates a GUID value with a key.
		/// </summary>
		// Token: 0x0600015D RID: 349
		void SetGUID([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidValue);

		/// <summary>
		/// Associates a wide-character string with a key.
		/// </summary>
		// Token: 0x0600015E RID: 350
		void SetString([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszValue);

		/// <summary>
		/// Associates a byte array with a key.
		/// </summary>
		// Token: 0x0600015F RID: 351
		void SetBlob([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidKey, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] pBuf, int cbBufSize);

		/// <summary>
		/// Associates an IUnknown pointer with a key.
		/// </summary>
		// Token: 0x06000160 RID: 352
		void SetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.IUnknown)] [In] object pUnknown);

		/// <summary>
		/// Locks the attribute store so that no other thread can access it.
		/// </summary>
		// Token: 0x06000161 RID: 353
		void LockStore();

		/// <summary>
		/// Unlocks the attribute store.
		/// </summary>
		// Token: 0x06000162 RID: 354
		void UnlockStore();

		/// <summary>
		/// Retrieves the number of attributes that are set on this object.
		/// </summary>
		// Token: 0x06000163 RID: 355
		void GetCount(out int pcItems);

		/// <summary>
		/// Retrieves an attribute at the specified index.
		/// </summary>
		// Token: 0x06000164 RID: 356
		void GetItemByIndex(int unIndex, out Guid pGuidKey, [In] [Out] ref PropVariant pValue);

		/// <summary>
		/// Copies all of the attributes from this object into another attribute store.
		/// </summary>
		// Token: 0x06000165 RID: 357
		void CopyAllItems([MarshalAs(UnmanagedType.Interface)] [In] IMFAttributes pDest);

		/// <summary>
		/// Creates the object associated with this activation object. 
		/// </summary>
		// Token: 0x06000166 RID: 358
		void ActivateObject([MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

		/// <summary>
		/// Shuts down the created object.
		/// </summary>
		// Token: 0x06000167 RID: 359
		void ShutdownObject();

		/// <summary>
		/// Detaches the created object from the activation object.
		/// </summary>
		// Token: 0x06000168 RID: 360
		void DetachObject();
	}
}
