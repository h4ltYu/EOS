using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Stopped Event Args
	/// </summary>
	// Token: 0x020001B7 RID: 439
	public class StoppedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of StoppedEventArgs
		/// </summary>
		/// <param name="exception">An exception to report (null if no exception)</param>
		// Token: 0x0600095F RID: 2399 RVA: 0x0001B325 File Offset: 0x00019525
		public StoppedEventArgs(Exception exception = null)
		{
			this.exception = exception;
		}

		/// <summary>
		/// An exception. Will be null if the playback or record operation stopped
		/// </summary>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001B334 File Offset: 0x00019534
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04000AA9 RID: 2729
		private readonly Exception exception;
	}
}
