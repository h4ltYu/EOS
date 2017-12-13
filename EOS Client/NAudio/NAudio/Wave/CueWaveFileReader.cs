using System;

namespace NAudio.Wave
{
	/// <summary>
	/// A wave file reader supporting cue reading
	/// </summary>
	// Token: 0x020001ED RID: 493
	public class CueWaveFileReader : WaveFileReader
	{
		/// <summary>
		/// Loads a wavefile and supports reading cues
		/// </summary>
		/// <param name="fileName"></param>
		// Token: 0x06000AF9 RID: 2809 RVA: 0x00020996 File Offset: 0x0001EB96
		public CueWaveFileReader(string fileName) : base(fileName)
		{
		}

		/// <summary>
		/// Cue List (can be null if cues not present)
		/// </summary>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0002099F File Offset: 0x0001EB9F
		public CueList Cues
		{
			get
			{
				if (this.cues == null)
				{
					this.cues = CueList.FromChunks(this);
				}
				return this.cues;
			}
		}

		// Token: 0x04000BA1 RID: 2977
		private CueList cues;
	}
}
