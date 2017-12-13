using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	/// <summary>
	/// Represents a generic collection of IUnknown pointers. 
	/// </summary>
	// Token: 0x02000048 RID: 72
	[Guid("5BC8A76B-869A-46A3-9B03-FA218A66AEBE")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IMFCollection
	{
		/// <summary>
		/// Retrieves the number of objects in the collection.
		/// </summary>
		// Token: 0x06000169 RID: 361
		void GetElementCount(out int pcElements);

		/// <summary>
		/// Retrieves an object in the collection.
		/// </summary>
		// Token: 0x0600016A RID: 362
		void GetElement([In] int dwElementIndex, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnkElement);

		/// <summary>
		/// Adds an object to the collection.
		/// </summary>
		// Token: 0x0600016B RID: 363
		void AddElement([MarshalAs(UnmanagedType.IUnknown)] [In] object pUnkElement);

		/// <summary>
		/// Removes an object from the collection.
		/// </summary>
		// Token: 0x0600016C RID: 364
		void RemoveElement([In] int dwElementIndex, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnkElement);

		/// <summary>
		/// Removes an object from the collection.
		/// </summary>
		// Token: 0x0600016D RID: 365
		void InsertElementAt([In] int dwIndex, [MarshalAs(UnmanagedType.IUnknown)] [In] object pUnknown);

		/// <summary>
		/// Removes all items from the collection.
		/// </summary>
		// Token: 0x0600016E RID: 366
		void RemoveAllElements();
	}
}
