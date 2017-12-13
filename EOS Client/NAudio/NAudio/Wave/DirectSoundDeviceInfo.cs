using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Class for enumerating DirectSound devices
	/// </summary>
	// Token: 0x020001C7 RID: 455
	public class DirectSoundDeviceInfo
	{
		/// <summary>
		/// The device identifier
		/// </summary>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0001BDD6 File Offset: 0x00019FD6
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x0001BDDE File Offset: 0x00019FDE
		public Guid Guid { get; set; }

		/// <summary>
		/// Device description
		/// </summary>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0001BDE7 File Offset: 0x00019FE7
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x0001BDEF File Offset: 0x00019FEF
		public string Description { get; set; }

		/// <summary>
		/// Device module name
		/// </summary>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001BDF8 File Offset: 0x00019FF8
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0001BE00 File Offset: 0x0001A000
		public string ModuleName { get; set; }
	}
}
