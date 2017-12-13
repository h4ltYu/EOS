using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	/// <summary>
	/// Custom marshaller for WaveFormat structures
	/// </summary>
	// Token: 0x020001AA RID: 426
	public sealed class WaveFormatCustomMarshaler : ICustomMarshaler
	{
		/// <summary>
		/// Gets the instance of this marshaller
		/// </summary>
		/// <param name="cookie"></param>
		/// <returns></returns>
		// Token: 0x060008AC RID: 2220 RVA: 0x00019113 File Offset: 0x00017313
		public static ICustomMarshaler GetInstance(string cookie)
		{
			if (WaveFormatCustomMarshaler.marshaler == null)
			{
				WaveFormatCustomMarshaler.marshaler = new WaveFormatCustomMarshaler();
			}
			return WaveFormatCustomMarshaler.marshaler;
		}

		/// <summary>
		/// Clean up managed data
		/// </summary>
		// Token: 0x060008AD RID: 2221 RVA: 0x0001912B File Offset: 0x0001732B
		public void CleanUpManagedData(object ManagedObj)
		{
		}

		/// <summary>
		/// Clean up native data
		/// </summary>
		/// <param name="pNativeData"></param>
		// Token: 0x060008AE RID: 2222 RVA: 0x0001912D File Offset: 0x0001732D
		public void CleanUpNativeData(IntPtr pNativeData)
		{
			Marshal.FreeHGlobal(pNativeData);
		}

		/// <summary>
		/// Get native data size
		/// </summary>        
		// Token: 0x060008AF RID: 2223 RVA: 0x00019135 File Offset: 0x00017335
		public int GetNativeDataSize()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Marshal managed to native
		/// </summary>
		// Token: 0x060008B0 RID: 2224 RVA: 0x0001913C File Offset: 0x0001733C
		public IntPtr MarshalManagedToNative(object ManagedObj)
		{
			return WaveFormat.MarshalToPtr((WaveFormat)ManagedObj);
		}

		/// <summary>
		/// Marshal Native to Managed
		/// </summary>
		// Token: 0x060008B1 RID: 2225 RVA: 0x00019149 File Offset: 0x00017349
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			return WaveFormat.MarshalFromPtr(pNativeData);
		}

		// Token: 0x040009D2 RID: 2514
		private static WaveFormatCustomMarshaler marshaler;
	}
}
