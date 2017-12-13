using System;

namespace NAudio.FileFormats.Map
{
	/// <summary>
	/// Represents an entry in a Cakewalk drum map
	/// </summary>
	// Token: 0x020000A0 RID: 160
	public class CakewalkDrumMapping
	{
		/// <summary>
		/// User customisable note name
		/// </summary>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000BE2E File Offset: 0x0000A02E
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000BE36 File Offset: 0x0000A036
		public string NoteName { get; set; }

		/// <summary>
		/// Input MIDI note number
		/// </summary>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000BE3F File Offset: 0x0000A03F
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000BE47 File Offset: 0x0000A047
		public int InNote { get; set; }

		/// <summary>
		/// Output MIDI note number
		/// </summary>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000BE50 File Offset: 0x0000A050
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000BE58 File Offset: 0x0000A058
		public int OutNote { get; set; }

		/// <summary>
		/// Output port
		/// </summary>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000BE61 File Offset: 0x0000A061
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000BE69 File Offset: 0x0000A069
		public int OutPort { get; set; }

		/// <summary>
		/// Output MIDI Channel
		/// </summary>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000BE72 File Offset: 0x0000A072
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000BE7A File Offset: 0x0000A07A
		public int Channel { get; set; }

		/// <summary>
		/// Velocity adjustment
		/// </summary>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000BE83 File Offset: 0x0000A083
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000BE8B File Offset: 0x0000A08B
		public int VelocityAdjust { get; set; }

		/// <summary>
		/// Velocity scaling - in percent
		/// </summary>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000BE94 File Offset: 0x0000A094
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000BE9C File Offset: 0x0000A09C
		public float VelocityScale { get; set; }

		/// <summary>
		/// Describes this drum map entry
		/// </summary>
		// Token: 0x06000382 RID: 898 RVA: 0x0000BEA8 File Offset: 0x0000A0A8
		public override string ToString()
		{
			return string.Format("{0} In:{1} Out:{2} Ch:{3} Port:{4} Vel+:{5} Vel:{6}%", new object[]
			{
				this.NoteName,
				this.InNote,
				this.OutNote,
				this.Channel,
				this.OutPort,
				this.VelocityAdjust,
				this.VelocityScale * 100f
			});
		}
	}
}
