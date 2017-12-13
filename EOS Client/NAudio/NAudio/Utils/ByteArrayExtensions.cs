using System;
using System.Text;

namespace NAudio.Utils
{
	/// <summary>
	/// these will become extension methods once we move to .NET 3.5
	/// </summary>
	// Token: 0x02000067 RID: 103
	public static class ByteArrayExtensions
	{
		/// <summary>
		/// Checks if the buffer passed in is entirely full of nulls
		/// </summary>
		// Token: 0x06000243 RID: 579 RVA: 0x00007530 File Offset: 0x00005730
		public static bool IsEntirelyNull(byte[] buffer)
		{
			foreach (byte b in buffer)
			{
				if (b != 0)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Converts to a string containing the buffer described in hex
		/// </summary>
		// Token: 0x06000244 RID: 580 RVA: 0x0000755C File Offset: 0x0000575C
		public static string DescribeAsHex(byte[] buffer, string separator, int bytesPerLine)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (byte b in buffer)
			{
				stringBuilder.AppendFormat("{0:X2}{1}", b, separator);
				if (++num % bytesPerLine == 0)
				{
					stringBuilder.Append("\r\n");
				}
			}
			stringBuilder.Append("\r\n");
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Decodes the buffer using the specified encoding, stopping at the first null
		/// </summary>
		// Token: 0x06000245 RID: 581 RVA: 0x000075C4 File Offset: 0x000057C4
		public static string DecodeAsString(byte[] buffer, int offset, int length, Encoding encoding)
		{
			for (int i = 0; i < length; i++)
			{
				if (buffer[offset + i] == 0)
				{
					length = i;
				}
			}
			return encoding.GetString(buffer, offset, length);
		}

		/// <summary>
		/// Concatenates the given arrays into a single array.
		/// </summary>
		/// <param name="byteArrays">The arrays to concatenate</param>
		/// <returns>The concatenated resulting array.</returns>
		// Token: 0x06000246 RID: 582 RVA: 0x000075F0 File Offset: 0x000057F0
		public static byte[] Concat(params byte[][] byteArrays)
		{
			int num = 0;
			foreach (byte array in byteArrays)
			{
				num += array.Length;
			}
			if (num <= 0)
			{
				return new byte[0];
			}
			byte[] array2 = new byte[num];
			int num2 = 0;
			foreach (byte array3 in byteArrays)
			{
				Array.Copy(array3, 0, array2, num2, array3.Length);
				num2 += array3.Length;
			}
			return array2;
		}
	}
}
