using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000126 RID: 294
	[Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IAudioEndpointVolume
	{
		// Token: 0x0600067A RID: 1658
		int RegisterControlChangeNotify(IAudioEndpointVolumeCallback pNotify);

		// Token: 0x0600067B RID: 1659
		int UnregisterControlChangeNotify(IAudioEndpointVolumeCallback pNotify);

		// Token: 0x0600067C RID: 1660
		int GetChannelCount(out int pnChannelCount);

		// Token: 0x0600067D RID: 1661
		int SetMasterVolumeLevel(float fLevelDB, Guid pguidEventContext);

		// Token: 0x0600067E RID: 1662
		int SetMasterVolumeLevelScalar(float fLevel, Guid pguidEventContext);

		// Token: 0x0600067F RID: 1663
		int GetMasterVolumeLevel(out float pfLevelDB);

		// Token: 0x06000680 RID: 1664
		int GetMasterVolumeLevelScalar(out float pfLevel);

		// Token: 0x06000681 RID: 1665
		int SetChannelVolumeLevel(uint nChannel, float fLevelDB, Guid pguidEventContext);

		// Token: 0x06000682 RID: 1666
		int SetChannelVolumeLevelScalar(uint nChannel, float fLevel, Guid pguidEventContext);

		// Token: 0x06000683 RID: 1667
		int GetChannelVolumeLevel(uint nChannel, out float pfLevelDB);

		// Token: 0x06000684 RID: 1668
		int GetChannelVolumeLevelScalar(uint nChannel, out float pfLevel);

		// Token: 0x06000685 RID: 1669
		int SetMute([MarshalAs(UnmanagedType.Bool)] bool bMute, Guid pguidEventContext);

		// Token: 0x06000686 RID: 1670
		int GetMute(out bool pbMute);

		// Token: 0x06000687 RID: 1671
		int GetVolumeStepInfo(out uint pnStep, out uint pnStepCount);

		// Token: 0x06000688 RID: 1672
		int VolumeStepUp(Guid pguidEventContext);

		// Token: 0x06000689 RID: 1673
		int VolumeStepDown(Guid pguidEventContext);

		// Token: 0x0600068A RID: 1674
		int QueryHardwareSupport(out uint pdwHardwareSupportMask);

		// Token: 0x0600068B RID: 1675
		int GetVolumeRange(out float pflVolumeMindB, out float pflVolumeMaxdB, out float pflVolumeIncrementdB);
	}
}
