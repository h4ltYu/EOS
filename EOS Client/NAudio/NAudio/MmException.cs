using System;

namespace NAudio
{
	/// <summary>
	/// Summary description for MmException.
	/// </summary>
	// Token: 0x0200017D RID: 381
	public class MmException : Exception
	{
		/// <summary>
		/// Creates a new MmException
		/// </summary>
		/// <param name="result">The result returned by the Windows API call</param>
		/// <param name="function">The name of the Windows API that failed</param>
		// Token: 0x060007E5 RID: 2021 RVA: 0x00017225 File Offset: 0x00015425
		public MmException(MmResult result, string function) : base(MmException.ErrorMessage(result, function))
		{
			this.result = result;
			this.function = function;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00017242 File Offset: 0x00015442
		private static string ErrorMessage(MmResult result, string function)
		{
			return string.Format("{0} calling {1}", result, function);
		}

		/// <summary>
		/// Helper function to automatically raise an exception on failure
		/// </summary>
		/// <param name="result">The result of the API call</param>
		/// <param name="function">The API function name</param>
		// Token: 0x060007E7 RID: 2023 RVA: 0x00017255 File Offset: 0x00015455
		public static void Try(MmResult result, string function)
		{
			if (result != MmResult.NoError)
			{
				throw new MmException(result, function);
			}
		}

		/// <summary>
		/// Returns the Windows API result
		/// </summary>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00017262 File Offset: 0x00015462
		public MmResult Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x040008EF RID: 2287
		private MmResult result;

		// Token: 0x040008F0 RID: 2288
		private string function;
	}
}
