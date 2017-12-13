using System;
using System.Text;

namespace NAudio.Utils
{
	/// <summary>
	/// An encoding for use with file types that have one byte per character
	/// </summary>
	// Token: 0x02000115 RID: 277
	public class ByteEncoding : Encoding
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x0001402A File Offset: 0x0001222A
		private ByteEncoding()
		{
		}

		/// <summary>
		/// <see cref="M:System.Text.Encoding.GetByteCount(System.Char[],System.Int32,System.Int32)" />
		/// </summary>
		// Token: 0x06000621 RID: 1569 RVA: 0x00014032 File Offset: 0x00012232
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return count;
		}

		/// <summary>
		/// <see cref="M:System.Text.Encoding.GetBytes(System.Char[],System.Int32,System.Int32,System.Byte[],System.Int32)" />
		/// </summary>
		// Token: 0x06000622 RID: 1570 RVA: 0x00014038 File Offset: 0x00012238
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			for (int i = 0; i < charCount; i++)
			{
				bytes[byteIndex + i] = (byte)chars[charIndex + i];
			}
			return charCount;
		}

		/// <summary>
		/// <see cref="M:System.Text.Encoding.GetCharCount(System.Byte[],System.Int32,System.Int32)" />
		/// </summary>
		// Token: 0x06000623 RID: 1571 RVA: 0x00014060 File Offset: 0x00012260
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (bytes[index + i] == 0)
				{
					return i;
				}
			}
			return count;
		}

		/// <summary>
		/// <see cref="M:System.Text.Encoding.GetChars(System.Byte[],System.Int32,System.Int32,System.Char[],System.Int32)" />
		/// </summary>
		// Token: 0x06000624 RID: 1572 RVA: 0x00014084 File Offset: 0x00012284
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			for (int i = 0; i < byteCount; i++)
			{
				byte b = bytes[byteIndex + i];
				if (b == 0)
				{
					return i;
				}
				chars[charIndex + i] = (char)b;
			}
			return byteCount;
		}

		/// <summary>
		/// <see cref="M:System.Text.Encoding.GetMaxCharCount(System.Int32)" />
		/// </summary>
		// Token: 0x06000625 RID: 1573 RVA: 0x000140B1 File Offset: 0x000122B1
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}

		/// <summary>
		/// <see cref="M:System.Text.Encoding.GetMaxByteCount(System.Int32)" />
		/// </summary>
		// Token: 0x06000626 RID: 1574 RVA: 0x000140B4 File Offset: 0x000122B4
		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		/// <summary>
		/// The one and only instance of this class
		/// </summary>
		// Token: 0x040006C5 RID: 1733
		public static readonly ByteEncoding Instance = new ByteEncoding();
	}
}
