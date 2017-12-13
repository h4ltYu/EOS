using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace NAudio.CoreAudioApi.Interfaces
{
	/// <summary>
	/// from Propidl.h.
	/// http://msdn.microsoft.com/en-us/library/aa380072(VS.85).aspx
	/// contains a union so we have to do an explicit layout
	/// </summary>
	// Token: 0x02000131 RID: 305
	[StructLayout(LayoutKind.Explicit)]
	public struct PropVariant
	{
		/// <summary>
		/// Creates a new PropVariant containing a long value
		/// </summary>
		// Token: 0x060006A8 RID: 1704 RVA: 0x00014A10 File Offset: 0x00012C10
		public static PropVariant FromLong(long value)
		{
			return new PropVariant
			{
				vt = 20,
				hVal = value
			};
		}

		/// <summary>
		/// Helper method to gets blob data
		/// </summary>
		// Token: 0x060006A9 RID: 1705 RVA: 0x00014A38 File Offset: 0x00012C38
		private byte[] GetBlob()
		{
			byte[] array = new byte[this.blobVal.Length];
			Marshal.Copy(this.blobVal.Data, array, 0, array.Length);
			return array;
		}

		/// <summary>
		/// Interprets a blob as an array of structs
		/// </summary>
		// Token: 0x060006AA RID: 1706 RVA: 0x00014A6C File Offset: 0x00012C6C
		public T[] GetBlobAsArrayOf<T>()
		{
			int length = this.blobVal.Length;
			T t = (T)((object)Activator.CreateInstance(typeof(T)));
			int num = Marshal.SizeOf(t);
			if (length % num != 0)
			{
				throw new InvalidDataException(string.Format("Blob size {0} not a multiple of struct size {1}", length, num));
			}
			int num2 = length / num;
			T[] array = new T[num2];
			for (int i = 0; i < num2; i++)
			{
				array[i] = (T)((object)Activator.CreateInstance(typeof(T)));
				Marshal.PtrToStructure(new IntPtr((long)this.blobVal.Data + (long)(i * num)), array[i]);
			}
			return array;
		}

		/// <summary>
		/// Gets the type of data in this PropVariant
		/// </summary>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00014B30 File Offset: 0x00012D30
		public VarEnum DataType
		{
			get
			{
				return (VarEnum)this.vt;
			}
		}

		/// <summary>
		/// Property value
		/// </summary>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00014B38 File Offset: 0x00012D38
		public object Value
		{
			get
			{
				VarEnum dataType = this.DataType;
				VarEnum varEnum = dataType;
				if (varEnum > VarEnum.VT_LPWSTR)
				{
					if (varEnum != VarEnum.VT_BLOB)
					{
						if (varEnum == VarEnum.VT_CLSID)
						{
							return (Guid)Marshal.PtrToStructure(this.pointerValue, typeof(Guid));
						}
						if (varEnum != (VarEnum)4113)
						{
							goto IL_EB;
						}
					}
					return this.GetBlob();
				}
				switch (varEnum)
				{
				case VarEnum.VT_I2:
					return this.iVal;
				case VarEnum.VT_I4:
					return this.lVal;
				default:
					switch (varEnum)
					{
					case VarEnum.VT_I1:
						return this.bVal;
					case VarEnum.VT_UI1:
					case VarEnum.VT_UI2:
						break;
					case VarEnum.VT_UI4:
						return this.ulVal;
					case VarEnum.VT_I8:
						return this.hVal;
					case VarEnum.VT_UI8:
						return this.uhVal;
					case VarEnum.VT_INT:
						return this.iVal;
					default:
						if (varEnum == VarEnum.VT_LPWSTR)
						{
							return Marshal.PtrToStringUni(this.pointerValue);
						}
						break;
					}
					break;
				}
				IL_EB:
				throw new NotImplementedException("PropVariant " + dataType.ToString());
			}
		}

		/// <summary>
		/// allows freeing up memory, might turn this into a Dispose method?
		/// </summary>
		// Token: 0x060006AD RID: 1709 RVA: 0x00014C4A File Offset: 0x00012E4A
		public void Clear()
		{
			PropVariant.PropVariantClear(ref this);
		}

		// Token: 0x060006AE RID: 1710
		[DllImport("ole32.dll")]
		private static extern int PropVariantClear(ref PropVariant pvar);

		// Token: 0x04000724 RID: 1828
		[FieldOffset(0)]
		private short vt;

		// Token: 0x04000725 RID: 1829
		[FieldOffset(2)]
		private short wReserved1;

		// Token: 0x04000726 RID: 1830
		[FieldOffset(4)]
		private short wReserved2;

		// Token: 0x04000727 RID: 1831
		[FieldOffset(6)]
		private short wReserved3;

		// Token: 0x04000728 RID: 1832
		[FieldOffset(8)]
		private sbyte cVal;

		// Token: 0x04000729 RID: 1833
		[FieldOffset(8)]
		private byte bVal;

		// Token: 0x0400072A RID: 1834
		[FieldOffset(8)]
		private short iVal;

		// Token: 0x0400072B RID: 1835
		[FieldOffset(8)]
		private ushort uiVal;

		// Token: 0x0400072C RID: 1836
		[FieldOffset(8)]
		private int lVal;

		// Token: 0x0400072D RID: 1837
		[FieldOffset(8)]
		private uint ulVal;

		// Token: 0x0400072E RID: 1838
		[FieldOffset(8)]
		private int intVal;

		// Token: 0x0400072F RID: 1839
		[FieldOffset(8)]
		private uint uintVal;

		// Token: 0x04000730 RID: 1840
		[FieldOffset(8)]
		private long hVal;

		// Token: 0x04000731 RID: 1841
		[FieldOffset(8)]
		private long uhVal;

		// Token: 0x04000732 RID: 1842
		[FieldOffset(8)]
		private float fltVal;

		// Token: 0x04000733 RID: 1843
		[FieldOffset(8)]
		private double dblVal;

		// Token: 0x04000734 RID: 1844
		[FieldOffset(8)]
		private bool boolVal;

		// Token: 0x04000735 RID: 1845
		[FieldOffset(8)]
		private int scode;

		// Token: 0x04000736 RID: 1846
		[FieldOffset(8)]
		private DateTime date;

		// Token: 0x04000737 RID: 1847
		[FieldOffset(8)]
		private System.Runtime.InteropServices.ComTypes.FILETIME filetime;

		// Token: 0x04000738 RID: 1848
		[FieldOffset(8)]
		private Blob blobVal;

		// Token: 0x04000739 RID: 1849
		[FieldOffset(8)]
		private IntPtr pointerValue;
	}
}
