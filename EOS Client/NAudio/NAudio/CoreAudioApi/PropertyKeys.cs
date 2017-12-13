using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Property Keys
	/// </summary>
	// Token: 0x02000037 RID: 55
	public static class PropertyKeys
	{
		/// <summary>
		/// PKEY_DeviceInterface_FriendlyName
		/// </summary>
		// Token: 0x040000A7 RID: 167
		public static readonly PropertyKey PKEY_DeviceInterface_FriendlyName = new PropertyKey(new Guid(40784238, -18412, 16715, 131, 205, 133, 109, 111, 239, 72, 34), 2);

		/// <summary>
		/// PKEY_AudioEndpoint_FormFactor
		/// </summary>
		// Token: 0x040000A8 RID: 168
		public static readonly PropertyKey PKEY_AudioEndpoint_FormFactor = new PropertyKey(new Guid(497408003, -11118, 20189, 140, 35, 224, 192, byte.MaxValue, 238, 127, 14), 0);

		/// <summary>
		/// PKEY_AudioEndpoint_ControlPanelPageProvider
		/// </summary>
		// Token: 0x040000A9 RID: 169
		public static readonly PropertyKey PKEY_AudioEndpoint_ControlPanelPageProvider = new PropertyKey(new Guid(497408003, -11118, 20189, 140, 35, 224, 192, byte.MaxValue, 238, 127, 14), 1);

		/// <summary>
		/// PKEY_AudioEndpoint_Association
		/// </summary>
		// Token: 0x040000AA RID: 170
		public static readonly PropertyKey PKEY_AudioEndpoint_Association = new PropertyKey(new Guid(497408003, -11118, 20189, 140, 35, 224, 192, byte.MaxValue, 238, 127, 14), 2);

		/// <summary>
		/// PKEY_AudioEndpoint_PhysicalSpeakers
		/// </summary>
		// Token: 0x040000AB RID: 171
		public static readonly PropertyKey PKEY_AudioEndpoint_PhysicalSpeakers = new PropertyKey(new Guid(497408003, -11118, 20189, 140, 35, 224, 192, byte.MaxValue, 238, 127, 14), 3);

		/// <summary>
		/// PKEY_AudioEndpoint_GUID
		/// </summary>
		// Token: 0x040000AC RID: 172
		public static readonly PropertyKey PKEY_AudioEndpoint_GUID = new PropertyKey(new Guid(497408003, -11118, 20189, 140, 35, 224, 192, byte.MaxValue, 238, 127, 14), 4);

		/// <summary>
		/// PKEY_AudioEndpoint_Disable_SysFx 
		/// </summary>
		// Token: 0x040000AD RID: 173
		public static readonly PropertyKey PKEY_AudioEndpoint_Disable_SysFx = new PropertyKey(new Guid(497408003, -11118, 20189, 140, 35, 224, 192, byte.MaxValue, 238, 127, 14), 5);

		/// <summary>
		/// PKEY_AudioEndpoint_FullRangeSpeakers 
		/// </summary>
		// Token: 0x040000AE RID: 174
		public static readonly PropertyKey PKEY_AudioEndpoint_FullRangeSpeakers = new PropertyKey(new Guid(497408003, -11118, 20189, 140, 35, 224, 192, byte.MaxValue, 238, 127, 14), 6);

		/// <summary>
		/// PKEY_AudioEndpoint_Supports_EventDriven_Mode 
		/// </summary>
		// Token: 0x040000AF RID: 175
		public static readonly PropertyKey PKEY_AudioEndpoint_Supports_EventDriven_Mode = new PropertyKey(new Guid(497408003, -11118, 20189, 140, 35, 224, 192, byte.MaxValue, 238, 127, 14), 7);

		/// <summary>
		/// PKEY_AudioEndpoint_JackSubType
		/// </summary>
		// Token: 0x040000B0 RID: 176
		public static readonly PropertyKey PKEY_AudioEndpoint_JackSubType = new PropertyKey(new Guid(497408003, -11118, 20189, 140, 35, 224, 192, byte.MaxValue, 238, 127, 14), 8);

		/// <summary>
		/// PKEY_AudioEngine_DeviceFormat 
		/// </summary>
		// Token: 0x040000B1 RID: 177
		public static readonly PropertyKey PKEY_AudioEngine_DeviceFormat = new PropertyKey(new Guid(-241236403, 2092, 20007, 188, 115, 104, 130, 161, 187, 142, 76), 0);

		/// <summary>
		/// PKEY_AudioEngine_OEMFormat
		/// </summary>
		// Token: 0x040000B2 RID: 178
		public static readonly PropertyKey PKEY_AudioEngine_OEMFormat = new PropertyKey(new Guid(-460911066, 15557, 19666, 186, 70, 202, 10, 154, 112, 237, 4), 3);

		/// <summary>
		/// PKEY _Devie_FriendlyName
		/// </summary>
		// Token: 0x040000B3 RID: 179
		public static readonly PropertyKey PKEY_Device_FriendlyName = new PropertyKey(new Guid(-1537465010, -8420, 20221, 128, 32, 103, 209, 70, 168, 80, 224), 14);

		/// <summary>
		/// PKEY _Device_IconPath
		/// </summary>
		// Token: 0x040000B4 RID: 180
		public static readonly PropertyKey PKEY_Device_IconPath = new PropertyKey(new Guid(630898684, 20647, 18382, 175, 8, 104, 201, 167, 215, 51, 102), 12);
	}
}
