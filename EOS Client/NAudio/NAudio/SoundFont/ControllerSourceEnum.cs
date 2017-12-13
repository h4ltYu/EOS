using System;

namespace NAudio.SoundFont
{
	/// <summary>
	/// Controller Sources
	/// </summary>
	// Token: 0x020000BB RID: 187
	public enum ControllerSourceEnum
	{
		/// <summary>
		/// No Controller
		/// </summary>
		// Token: 0x040004EE RID: 1262
		NoController,
		/// <summary>
		/// Note On Velocity
		/// </summary>
		// Token: 0x040004EF RID: 1263
		NoteOnVelocity = 2,
		/// <summary>
		/// Note On Key Number
		/// </summary>
		// Token: 0x040004F0 RID: 1264
		NoteOnKeyNumber,
		/// <summary>
		/// Poly Pressure
		/// </summary>
		// Token: 0x040004F1 RID: 1265
		PolyPressure = 10,
		/// <summary>
		/// Channel Pressure
		/// </summary>
		// Token: 0x040004F2 RID: 1266
		ChannelPressure = 13,
		/// <summary>
		/// Pitch Wheel
		/// </summary>
		// Token: 0x040004F3 RID: 1267
		PitchWheel,
		/// <summary>
		/// Pitch Wheel Sensitivity
		/// </summary>
		// Token: 0x040004F4 RID: 1268
		PitchWheelSensitivity = 16
	}
}
