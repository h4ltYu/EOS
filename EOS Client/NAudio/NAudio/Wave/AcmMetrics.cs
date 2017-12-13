using System;

namespace NAudio.Wave
{
	// Token: 0x02000173 RID: 371
	internal enum AcmMetrics
	{
		/// <summary>ACM_METRIC_COUNT_DRIVERS</summary>
		// Token: 0x0400081D RID: 2077
		CountDrivers = 1,
		/// <summary>ACM_METRIC_COUNT_CODECS</summary>
		// Token: 0x0400081E RID: 2078
		CountCodecs,
		/// <summary>ACM_METRIC_COUNT_CONVERTERS</summary>
		// Token: 0x0400081F RID: 2079
		CountConverters,
		/// <summary>ACM_METRIC_COUNT_FILTERS</summary>
		// Token: 0x04000820 RID: 2080
		CountFilters,
		/// <summary>ACM_METRIC_COUNT_DISABLED</summary>
		// Token: 0x04000821 RID: 2081
		CountDisabled,
		/// <summary>ACM_METRIC_COUNT_HARDWARE</summary>
		// Token: 0x04000822 RID: 2082
		CountHardware,
		/// <summary>ACM_METRIC_COUNT_LOCAL_DRIVERS</summary>
		// Token: 0x04000823 RID: 2083
		CountLocalDrivers = 20,
		/// <summary>ACM_METRIC_COUNT_LOCAL_CODECS</summary>
		// Token: 0x04000824 RID: 2084
		CountLocalCodecs,
		/// <summary>ACM_METRIC_COUNT_LOCAL_CONVERTERS</summary>
		// Token: 0x04000825 RID: 2085
		CountLocalConverters,
		/// <summary>ACM_METRIC_COUNT_LOCAL_FILTERS</summary>
		// Token: 0x04000826 RID: 2086
		CountLocalFilters,
		/// <summary>ACM_METRIC_COUNT_LOCAL_DISABLED</summary>
		// Token: 0x04000827 RID: 2087
		CountLocalDisabled,
		/// <summary>ACM_METRIC_HARDWARE_WAVE_INPUT</summary>
		// Token: 0x04000828 RID: 2088
		HardwareWaveInput = 30,
		/// <summary>ACM_METRIC_HARDWARE_WAVE_OUTPUT</summary>
		// Token: 0x04000829 RID: 2089
		HardwareWaveOutput,
		/// <summary>ACM_METRIC_MAX_SIZE_FORMAT</summary>
		// Token: 0x0400082A RID: 2090
		MaxSizeFormat = 50,
		/// <summary>ACM_METRIC_MAX_SIZE_FILTER</summary>
		// Token: 0x0400082B RID: 2091
		MaxSizeFilter,
		/// <summary>ACM_METRIC_DRIVER_SUPPORT</summary>
		// Token: 0x0400082C RID: 2092
		DriverSupport = 100,
		/// <summary>ACM_METRIC_DRIVER_PRIORITY</summary>
		// Token: 0x0400082D RID: 2093
		DriverPriority
	}
}
