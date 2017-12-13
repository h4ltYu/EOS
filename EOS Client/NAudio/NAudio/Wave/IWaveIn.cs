using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Generic interface for wave recording
	/// </summary>
	// Token: 0x02000080 RID: 128
	public interface IWaveIn : IDisposable
	{
		/// <summary>
		/// Recording WaveFormat
		/// </summary>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002AE RID: 686
		// (set) Token: 0x060002AF RID: 687
		WaveFormat WaveFormat { get; set; }

		/// <summary>
		/// Start Recording
		/// </summary>
		// Token: 0x060002B0 RID: 688
		void StartRecording();

		/// <summary>
		/// Stop Recording
		/// </summary>
		// Token: 0x060002B1 RID: 689
		void StopRecording();

		/// <summary>
		/// Indicates recorded data is available 
		/// </summary>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002B2 RID: 690
		// (remove) Token: 0x060002B3 RID: 691
		event EventHandler<WaveInEventArgs> DataAvailable;

		/// <summary>
		/// Indicates that all recorded data has now been received.
		/// </summary>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002B4 RID: 692
		// (remove) Token: 0x060002B5 RID: 693
		event EventHandler<StoppedEventArgs> RecordingStopped;
	}
}
