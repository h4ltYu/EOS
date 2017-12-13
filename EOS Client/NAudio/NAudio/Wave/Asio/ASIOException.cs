using System;

namespace NAudio.Wave.Asio
{
	/// <summary>
	/// ASIO common Exception.
	/// </summary>
	// Token: 0x02000153 RID: 339
	internal class ASIOException : Exception
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x0001652F File Offset: 0x0001472F
		public ASIOException()
		{
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00016537 File Offset: 0x00014737
		public ASIOException(string message) : base(message)
		{
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00016540 File Offset: 0x00014740
		public ASIOException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0001654A File Offset: 0x0001474A
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x00016552 File Offset: 0x00014752
		public ASIOError Error
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error = value;
				this.Data["ASIOError"] = this.error;
			}
		}

		/// <summary>
		/// Gets the name of the error.
		/// </summary>
		/// <param name="error">The error.</param>
		/// <returns>the name of the error</returns>
		// Token: 0x06000773 RID: 1907 RVA: 0x00016576 File Offset: 0x00014776
		public static string getErrorName(ASIOError error)
		{
			return Enum.GetName(typeof(ASIOError), error);
		}

		// Token: 0x0400077A RID: 1914
		private ASIOError error;
	}
}
