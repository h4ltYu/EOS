using System;

namespace NAudio.Utils
{
	/// <summary>
	/// Methods for converting between IEEE 80-bit extended double precision
	/// and standard C# double precision.
	/// </summary>
	// Token: 0x0200006A RID: 106
	public static class IEEE
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00007771 File Offset: 0x00005971
		private static double UnsignedToFloat(ulong u)
		{
			return (double)(u - 2147483647UL - 1UL) + 2147483648.0;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007789 File Offset: 0x00005989
		private static double ldexp(double x, int exp)
		{
			return x * Math.Pow(2.0, (double)exp);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000077A0 File Offset: 0x000059A0
		private static double frexp(double x, out int exp)
		{
			exp = (int)Math.Floor(Math.Log(x) / Math.Log(2.0)) + 1;
			return 1.0 - (Math.Pow(2.0, (double)exp) - x) / Math.Pow(2.0, (double)exp);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000077FB File Offset: 0x000059FB
		private static ulong FloatToUnsigned(double f)
		{
			return (ulong)((long)(f - 2147483648.0) + 2147483647L + 1L);
		}

		/// <summary>
		/// Converts a C# double precision number to an 80-bit
		/// IEEE extended double precision number (occupying 10 bytes).
		/// </summary>
		/// <param name="num">The double precision number to convert to IEEE extended.</param>
		/// <returns>An array of 10 bytes containing the IEEE extended number.</returns>
		// Token: 0x0600024D RID: 589 RVA: 0x00007814 File Offset: 0x00005A14
		public static byte[] ConvertToIeeeExtended(double num)
		{
			int num2;
			if (num < 0.0)
			{
				num2 = 32768;
				num *= -1.0;
			}
			else
			{
				num2 = 0;
			}
			int num3;
			ulong num4;
			ulong num5;
			if (num == 0.0)
			{
				num3 = 0;
				num4 = 0UL;
				num5 = 0UL;
			}
			else
			{
				double num6 = IEEE.frexp(num, out num3);
				if (num3 > 16384 || num6 >= 1.0)
				{
					num3 = (num2 | 32767);
					num4 = 0UL;
					num5 = 0UL;
				}
				else
				{
					num3 += 16382;
					if (num3 < 0)
					{
						num6 = IEEE.ldexp(num6, num3);
						num3 = 0;
					}
					num3 |= num2;
					num6 = IEEE.ldexp(num6, 32);
					double num7 = Math.Floor(num6);
					num4 = IEEE.FloatToUnsigned(num7);
					num6 = IEEE.ldexp(num6 - num7, 32);
					num7 = Math.Floor(num6);
					num5 = IEEE.FloatToUnsigned(num7);
				}
			}
			return new byte[]
			{
				(byte)(num3 >> 8),
				(byte)num3,
				(byte)(num4 >> 24),
				(byte)(num4 >> 16),
				(byte)(num4 >> 8),
				(byte)num4,
				(byte)(num5 >> 24),
				(byte)(num5 >> 16),
				(byte)(num5 >> 8),
				(byte)num5
			};
		}

		/// <summary>
		/// Converts an IEEE 80-bit extended precision number to a
		/// C# double precision number.
		/// </summary>
		/// <param name="bytes">The 80-bit IEEE extended number (as an array of 10 bytes).</param>
		/// <returns>A C# double precision number that is a close representation of the IEEE extended number.</returns>
		// Token: 0x0600024E RID: 590 RVA: 0x0000793C File Offset: 0x00005B3C
		public static double ConvertFromIeeeExtended(byte[] bytes)
		{
			if (bytes.Length != 10)
			{
				throw new Exception("Incorrect length for IEEE extended.");
			}
			int num = (int)(bytes[0] & 127) << 8 | (int)bytes[1];
			uint num2 = (uint)((int)bytes[2] << 24 | (int)bytes[3] << 16 | (int)bytes[4] << 8 | (int)bytes[5]);
			uint num3 = (uint)((int)bytes[6] << 24 | (int)bytes[7] << 16 | (int)bytes[8] << 8 | (int)bytes[9]);
			double num4;
			if (num == 0 && num2 == 0u && num3 == 0u)
			{
				num4 = 0.0;
			}
			else if (num == 32767)
			{
				num4 = double.NaN;
			}
			else
			{
				num -= 16383;
				num4 = IEEE.ldexp(IEEE.UnsignedToFloat((ulong)num2), num -= 31);
				num4 += IEEE.ldexp(IEEE.UnsignedToFloat((ulong)num3), num - 32);
			}
			if ((bytes[0] & 128) == 128)
			{
				return -num4;
			}
			return num4;
		}
	}
}
