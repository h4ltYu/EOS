using System;

namespace NAudio.SoundFont
{
	/// <summary>
	/// SoundFont Version Structure
	/// </summary>
	// Token: 0x020000C7 RID: 199
	public class SFVersion
	{
		/// <summary>
		/// Major Version
		/// </summary>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000E897 File Offset: 0x0000CA97
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000E89F File Offset: 0x0000CA9F
		public short Major
		{
			get
			{
				return this.major;
			}
			set
			{
				this.major = value;
			}
		}

		/// <summary>
		/// Minor Version
		/// </summary>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000E8B0 File Offset: 0x0000CAB0
		public short Minor
		{
			get
			{
				return this.minor;
			}
			set
			{
				this.minor = value;
			}
		}

		// Token: 0x04000530 RID: 1328
		private short major;

		// Token: 0x04000531 RID: 1329
		private short minor;
	}
}
