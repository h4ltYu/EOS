using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace NAudio.CoreAudioApi.Interfaces
{
    [StructLayout(LayoutKind.Explicit)]
    public struct PropVariant
    {
        public static PropVariant FromLong(long value)
        {
            return new PropVariant
            {
                vt = 20,
                hVal = value
            };
        }

        private byte[] GetBlob()
        {
            byte[] array = new byte[this.blobVal.Length];
            Marshal.Copy(this.blobVal.Data, array, 0, array.Length);
            return array;
        }

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

        public VarEnum DataType
        {
            get
            {
                return (VarEnum)this.vt;
            }
        }

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

        public void Clear()
        {
            PropVariant.PropVariantClear(ref this);
        }

        [DllImport("ole32.dll")]
        private static extern int PropVariantClear(ref PropVariant pvar);

        [FieldOffset(0)]
        private short vt;

        [FieldOffset(2)]
        private short wReserved1;

        [FieldOffset(4)]
        private short wReserved2;

        [FieldOffset(6)]
        private short wReserved3;

        [FieldOffset(8)]
        private sbyte cVal;

        [FieldOffset(8)]
        private byte bVal;

        [FieldOffset(8)]
        private short iVal;

        [FieldOffset(8)]
        private ushort uiVal;

        [FieldOffset(8)]
        private int lVal;

        [FieldOffset(8)]
        private uint ulVal;

        [FieldOffset(8)]
        private int intVal;

        [FieldOffset(8)]
        private uint uintVal;

        [FieldOffset(8)]
        private long hVal;

        [FieldOffset(8)]
        private long uhVal;

        [FieldOffset(8)]
        private float fltVal;

        [FieldOffset(8)]
        private double dblVal;

        [FieldOffset(8)]
        private bool boolVal;

        [FieldOffset(8)]
        private int scode;

        [FieldOffset(8)]
        private DateTime date;

        [FieldOffset(8)]
        private System.Runtime.InteropServices.ComTypes.FILETIME filetime;

        [FieldOffset(8)]
        private Blob blobVal;

        [FieldOffset(8)]
        private IntPtr pointerValue;
    }
}
