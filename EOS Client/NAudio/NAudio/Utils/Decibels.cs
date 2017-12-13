using System;

namespace NAudio.Utils
{
	/// <summary>
	/// A util class for conversions
	/// </summary>
	// Token: 0x02000117 RID: 279
	public class Decibels
	{
		/// <summary>
		/// linear to dB conversion
		/// </summary>
		/// <param name="lin">linear value</param>
		/// <returns>decibel value</returns>
		// Token: 0x0600062F RID: 1583 RVA: 0x0001431C File Offset: 0x0001251C
		public static double LinearToDecibels(double lin)
		{
			return Math.Log(lin) * 8.6858896380650368;
		}

		/// <summary>
		/// dB to linear conversion
		/// </summary>
		/// <param name="dB">decibel value</param>
		/// <returns>linear value</returns>
		// Token: 0x06000630 RID: 1584 RVA: 0x0001432E File Offset: 0x0001252E
		public static double DecibelsToLinear(double dB)
		{
			return Math.Exp(dB * 0.11512925464970228);
		}

		// Token: 0x040006CB RID: 1739
		private const double LOG_2_DB = 8.6858896380650368;

		// Token: 0x040006CC RID: 1740
		private const double DB_2_LOG = 0.11512925464970228;
	}
}
