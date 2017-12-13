using System;

namespace NAudio.SoundFont
{
	/// <summary>
	/// Generator types
	/// </summary>
	// Token: 0x020000B4 RID: 180
	public enum GeneratorEnum
	{
		/// <summary>Start address offset</summary>
		// Token: 0x04000499 RID: 1177
		StartAddressOffset,
		/// <summary>End address offset</summary>
		// Token: 0x0400049A RID: 1178
		EndAddressOffset,
		/// <summary>Start loop address offset</summary>
		// Token: 0x0400049B RID: 1179
		StartLoopAddressOffset,
		/// <summary>End loop address offset</summary>
		// Token: 0x0400049C RID: 1180
		EndLoopAddressOffset,
		/// <summary>Start address coarse offset</summary>
		// Token: 0x0400049D RID: 1181
		StartAddressCoarseOffset,
		/// <summary>Modulation LFO to pitch</summary>
		// Token: 0x0400049E RID: 1182
		ModulationLFOToPitch,
		/// <summary>Vibrato LFO to pitch</summary>
		// Token: 0x0400049F RID: 1183
		VibratoLFOToPitch,
		/// <summary>Modulation envelope to pitch</summary>
		// Token: 0x040004A0 RID: 1184
		ModulationEnvelopeToPitch,
		/// <summary>Initial filter cutoff frequency</summary>
		// Token: 0x040004A1 RID: 1185
		InitialFilterCutoffFrequency,
		/// <summary>Initial filter Q</summary>
		// Token: 0x040004A2 RID: 1186
		InitialFilterQ,
		/// <summary>Modulation LFO to filter Cutoff frequency</summary>
		// Token: 0x040004A3 RID: 1187
		ModulationLFOToFilterCutoffFrequency,
		/// <summary>Modulation envelope to filter cutoff frequency</summary>
		// Token: 0x040004A4 RID: 1188
		ModulationEnvelopeToFilterCutoffFrequency,
		/// <summary>End address coarse offset</summary>
		// Token: 0x040004A5 RID: 1189
		EndAddressCoarseOffset,
		/// <summary>Modulation LFO to volume</summary>
		// Token: 0x040004A6 RID: 1190
		ModulationLFOToVolume,
		/// <summary>Unused</summary>
		// Token: 0x040004A7 RID: 1191
		Unused1,
		/// <summary>Chorus effects send</summary>
		// Token: 0x040004A8 RID: 1192
		ChorusEffectsSend,
		/// <summary>Reverb effects send</summary>
		// Token: 0x040004A9 RID: 1193
		ReverbEffectsSend,
		/// <summary>Pan</summary>
		// Token: 0x040004AA RID: 1194
		Pan,
		/// <summary>Unused</summary>
		// Token: 0x040004AB RID: 1195
		Unused2,
		/// <summary>Unused</summary>
		// Token: 0x040004AC RID: 1196
		Unused3,
		/// <summary>Unused</summary>
		// Token: 0x040004AD RID: 1197
		Unused4,
		/// <summary>Delay modulation LFO</summary>
		// Token: 0x040004AE RID: 1198
		DelayModulationLFO,
		/// <summary>Frequency modulation LFO</summary>
		// Token: 0x040004AF RID: 1199
		FrequencyModulationLFO,
		/// <summary>Delay vibrato LFO</summary>
		// Token: 0x040004B0 RID: 1200
		DelayVibratoLFO,
		/// <summary>Frequency vibrato LFO</summary>
		// Token: 0x040004B1 RID: 1201
		FrequencyVibratoLFO,
		/// <summary>Delay modulation envelope</summary>
		// Token: 0x040004B2 RID: 1202
		DelayModulationEnvelope,
		/// <summary>Attack modulation envelope</summary>
		// Token: 0x040004B3 RID: 1203
		AttackModulationEnvelope,
		/// <summary>Hold modulation envelope</summary>
		// Token: 0x040004B4 RID: 1204
		HoldModulationEnvelope,
		/// <summary>Decay modulation envelope</summary>
		// Token: 0x040004B5 RID: 1205
		DecayModulationEnvelope,
		/// <summary>Sustain modulation envelop</summary>
		// Token: 0x040004B6 RID: 1206
		SustainModulationEnvelope,
		/// <summary>Release modulation envelope</summary>
		// Token: 0x040004B7 RID: 1207
		ReleaseModulationEnvelope,
		/// <summary>Key number to modulation envelope hold</summary>
		// Token: 0x040004B8 RID: 1208
		KeyNumberToModulationEnvelopeHold,
		/// <summary>Key number to modulation envelope decay</summary>
		// Token: 0x040004B9 RID: 1209
		KeyNumberToModulationEnvelopeDecay,
		/// <summary>Delay volume envelope</summary>
		// Token: 0x040004BA RID: 1210
		DelayVolumeEnvelope,
		/// <summary>Attack volume envelope</summary>
		// Token: 0x040004BB RID: 1211
		AttackVolumeEnvelope,
		/// <summary>Hold volume envelope</summary>
		// Token: 0x040004BC RID: 1212
		HoldVolumeEnvelope,
		/// <summary>Decay volume envelope</summary>
		// Token: 0x040004BD RID: 1213
		DecayVolumeEnvelope,
		/// <summary>Sustain volume envelope</summary>
		// Token: 0x040004BE RID: 1214
		SustainVolumeEnvelope,
		/// <summary>Release volume envelope</summary>
		// Token: 0x040004BF RID: 1215
		ReleaseVolumeEnvelope,
		/// <summary>Key number to volume envelope hold</summary>
		// Token: 0x040004C0 RID: 1216
		KeyNumberToVolumeEnvelopeHold,
		/// <summary>Key number to volume envelope decay</summary>
		// Token: 0x040004C1 RID: 1217
		KeyNumberToVolumeEnvelopeDecay,
		/// <summary>Instrument</summary>
		// Token: 0x040004C2 RID: 1218
		Instrument,
		/// <summary>Reserved</summary>
		// Token: 0x040004C3 RID: 1219
		Reserved1,
		/// <summary>Key range</summary>
		// Token: 0x040004C4 RID: 1220
		KeyRange,
		/// <summary>Velocity range</summary>
		// Token: 0x040004C5 RID: 1221
		VelocityRange,
		/// <summary>Start loop address coarse offset</summary>
		// Token: 0x040004C6 RID: 1222
		StartLoopAddressCoarseOffset,
		/// <summary>Key number</summary>
		// Token: 0x040004C7 RID: 1223
		KeyNumber,
		/// <summary>Velocity</summary>
		// Token: 0x040004C8 RID: 1224
		Velocity,
		/// <summary>Initial attenuation</summary>
		// Token: 0x040004C9 RID: 1225
		InitialAttenuation,
		/// <summary>Reserved</summary>
		// Token: 0x040004CA RID: 1226
		Reserved2,
		/// <summary>End loop address coarse offset</summary>
		// Token: 0x040004CB RID: 1227
		EndLoopAddressCoarseOffset,
		/// <summary>Coarse tune</summary>
		// Token: 0x040004CC RID: 1228
		CoarseTune,
		/// <summary>Fine tune</summary>
		// Token: 0x040004CD RID: 1229
		FineTune,
		/// <summary>Sample ID</summary>
		// Token: 0x040004CE RID: 1230
		SampleID,
		/// <summary>Sample modes</summary>
		// Token: 0x040004CF RID: 1231
		SampleModes,
		/// <summary>Reserved</summary>
		// Token: 0x040004D0 RID: 1232
		Reserved3,
		/// <summary>Scale tuning</summary>
		// Token: 0x040004D1 RID: 1233
		ScaleTuning,
		/// <summary>Exclusive class</summary>
		// Token: 0x040004D2 RID: 1234
		ExclusiveClass,
		/// <summary>Overriding root key</summary>
		// Token: 0x040004D3 RID: 1235
		OverridingRootKey,
		/// <summary>Unused</summary>
		// Token: 0x040004D4 RID: 1236
		Unused5,
		/// <summary>Unused</summary>
		// Token: 0x040004D5 RID: 1237
		UnusedEnd
	}
}
