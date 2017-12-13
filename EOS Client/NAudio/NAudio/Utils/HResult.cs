using System;
using System.Runtime.InteropServices;

namespace NAudio.Utils
{
	/// <summary>
	/// HResult
	/// </summary>
	// Token: 0x02000118 RID: 280
	public static class HResult
	{
		/// <summary>
		/// MAKE_HRESULT macro
		/// </summary>
		// Token: 0x06000632 RID: 1586 RVA: 0x00014348 File Offset: 0x00012548
		public static int MAKE_HRESULT(int sev, int fac, int code)
		{
			return sev << 31 | fac << 16 | code;
		}

		/// <summary>
		/// Helper to deal with the fact that in Win Store apps,
		/// the HResult property name has changed
		/// </summary>
		/// <param name="exception">COM Exception</param>
		/// <returns>The HResult</returns>
		// Token: 0x06000633 RID: 1587 RVA: 0x00014355 File Offset: 0x00012555
		public static int GetHResult(this COMException exception)
		{
			return exception.ErrorCode;
		}

		/// <summary>
		/// S_OK
		/// </summary>
		// Token: 0x040006CD RID: 1741
		public const int S_OK = 0;

		/// <summary>
		/// S_FALSE
		/// </summary>
		// Token: 0x040006CE RID: 1742
		public const int S_FALSE = 1;

		/// <summary>
		/// E_INVALIDARG (from winerror.h)
		/// </summary>
		// Token: 0x040006CF RID: 1743
		public const int E_INVALIDARG = -2147483645;

		// Token: 0x040006D0 RID: 1744
		private const int FACILITY_AAF = 18;

		// Token: 0x040006D1 RID: 1745
		private const int FACILITY_ACS = 20;

		// Token: 0x040006D2 RID: 1746
		private const int FACILITY_BACKGROUNDCOPY = 32;

		// Token: 0x040006D3 RID: 1747
		private const int FACILITY_CERT = 11;

		// Token: 0x040006D4 RID: 1748
		private const int FACILITY_COMPLUS = 17;

		// Token: 0x040006D5 RID: 1749
		private const int FACILITY_CONFIGURATION = 33;

		// Token: 0x040006D6 RID: 1750
		private const int FACILITY_CONTROL = 10;

		// Token: 0x040006D7 RID: 1751
		private const int FACILITY_DISPATCH = 2;

		// Token: 0x040006D8 RID: 1752
		private const int FACILITY_DPLAY = 21;

		// Token: 0x040006D9 RID: 1753
		private const int FACILITY_HTTP = 25;

		// Token: 0x040006DA RID: 1754
		private const int FACILITY_INTERNET = 12;

		// Token: 0x040006DB RID: 1755
		private const int FACILITY_ITF = 4;

		// Token: 0x040006DC RID: 1756
		private const int FACILITY_MEDIASERVER = 13;

		// Token: 0x040006DD RID: 1757
		private const int FACILITY_MSMQ = 14;

		// Token: 0x040006DE RID: 1758
		private const int FACILITY_NULL = 0;

		// Token: 0x040006DF RID: 1759
		private const int FACILITY_RPC = 1;

		// Token: 0x040006E0 RID: 1760
		private const int FACILITY_SCARD = 16;

		// Token: 0x040006E1 RID: 1761
		private const int FACILITY_SECURITY = 9;

		// Token: 0x040006E2 RID: 1762
		private const int FACILITY_SETUPAPI = 15;

		// Token: 0x040006E3 RID: 1763
		private const int FACILITY_SSPI = 9;

		// Token: 0x040006E4 RID: 1764
		private const int FACILITY_STORAGE = 3;

		// Token: 0x040006E5 RID: 1765
		private const int FACILITY_SXS = 23;

		// Token: 0x040006E6 RID: 1766
		private const int FACILITY_UMI = 22;

		// Token: 0x040006E7 RID: 1767
		private const int FACILITY_URT = 19;

		// Token: 0x040006E8 RID: 1768
		private const int FACILITY_WIN32 = 7;

		// Token: 0x040006E9 RID: 1769
		private const int FACILITY_WINDOWS = 8;

		// Token: 0x040006EA RID: 1770
		private const int FACILITY_WINDOWS_CE = 24;
	}
}
