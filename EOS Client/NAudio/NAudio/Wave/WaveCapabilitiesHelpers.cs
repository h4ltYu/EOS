using System;
using Microsoft.Win32;

namespace NAudio.Wave
{
	// Token: 0x02000185 RID: 389
	internal static class WaveCapabilitiesHelpers
	{
		/// <summary>
		/// The device name from the registry if supported
		/// </summary>
		// Token: 0x06000813 RID: 2067 RVA: 0x00017920 File Offset: 0x00015B20
		public static string GetNameFromGuid(Guid guid)
		{
			string result = null;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\MediaCategories"))
			{
				using (RegistryKey registryKey2 = registryKey.OpenSubKey(guid.ToString("B")))
				{
					if (registryKey2 != null)
					{
						result = (registryKey2.GetValue("Name") as string);
					}
				}
			}
			return result;
		}

		// Token: 0x0400094A RID: 2378
		public static readonly Guid MicrosoftDefaultManufacturerId = new Guid("d5a47fa8-6d98-11d1-a21a-00a0c9223196");

		// Token: 0x0400094B RID: 2379
		public static readonly Guid DefaultWaveOutGuid = new Guid("E36DC310-6D9A-11D1-A21A-00A0C9223196");

		// Token: 0x0400094C RID: 2380
		public static readonly Guid DefaultWaveInGuid = new Guid("E36DC311-6D9A-11D1-A21A-00A0C9223196");
	}
}
