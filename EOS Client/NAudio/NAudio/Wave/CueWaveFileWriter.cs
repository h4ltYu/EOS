using System;
using System.IO;

namespace NAudio.Wave
{
	/// <summary>
	/// A wave file writer that adds cue support
	/// </summary>
	// Token: 0x020001B4 RID: 436
	public class CueWaveFileWriter : WaveFileWriter
	{
		/// <summary>
		/// Writes a wave file, including a cues chunk
		/// </summary>
		// Token: 0x06000936 RID: 2358 RVA: 0x0001A9EC File Offset: 0x00018BEC
		public CueWaveFileWriter(string fileName, WaveFormat waveFormat) : base(fileName, waveFormat)
		{
		}

		/// <summary>
		/// Adds a cue to the Wave file
		/// </summary>
		/// <param name="position">Sample position</param>
		/// <param name="label">Label text</param>
		// Token: 0x06000937 RID: 2359 RVA: 0x0001A9F6 File Offset: 0x00018BF6
		public void AddCue(int position, string label)
		{
			if (this.cues == null)
			{
				this.cues = new CueList();
			}
			this.cues.Add(new Cue(position, label));
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001AA20 File Offset: 0x00018C20
		private void WriteCues(BinaryWriter w)
		{
			if (this.cues != null)
			{
				byte[] riffchunks = this.cues.GetRIFFChunks();
				int count = riffchunks.Length;
				w.Seek(0, SeekOrigin.End);
				if (w.BaseStream.Length % 2L == 1L)
				{
					w.Write(0);
				}
				w.Write(this.cues.GetRIFFChunks(), 0, count);
				w.Seek(4, SeekOrigin.Begin);
				w.Write((int)(w.BaseStream.Length - 8L));
			}
		}

		/// <summary>
		/// Updates the header, and writes the cues out
		/// </summary>
		// Token: 0x06000939 RID: 2361 RVA: 0x0001AA98 File Offset: 0x00018C98
		protected override void UpdateHeader(BinaryWriter writer)
		{
			base.UpdateHeader(writer);
			this.WriteCues(writer);
		}

		// Token: 0x04000AA1 RID: 2721
		private CueList cues;
	}
}
