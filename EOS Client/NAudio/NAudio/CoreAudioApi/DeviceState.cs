using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Device State
	/// </summary>
	// Token: 0x02000120 RID: 288
	[Flags]
	public enum DeviceState
	{
		/// <summary>
		/// DEVICE_STATE_ACTIVE
		/// </summary>
		// Token: 0x040006FA RID: 1786
		Active = 1,
		/// <summary>
		/// DEVICE_STATE_DISABLED
		/// </summary>
		// Token: 0x040006FB RID: 1787
		Disabled = 2,
		/// <summary>
		/// DEVICE_STATE_NOTPRESENT 
		/// </summary>
		// Token: 0x040006FC RID: 1788
		NotPresent = 4,
		/// <summary>
		/// DEVICE_STATE_UNPLUGGED
		/// </summary>
		// Token: 0x040006FD RID: 1789
		Unplugged = 8,
		/// <summary>
		/// DEVICE_STATEMASK_ALL
		/// </summary>
		// Token: 0x040006FE RID: 1790
		All = 15
	}
}
