using System;

namespace NAudio
{
	/// <summary>
	/// Windows multimedia error codes from mmsystem.h.
	/// </summary>
	// Token: 0x0200017E RID: 382
	public enum MmResult
	{
		/// <summary>no error, MMSYSERR_NOERROR</summary>
		// Token: 0x040008F2 RID: 2290
		NoError,
		/// <summary>unspecified error, MMSYSERR_ERROR</summary>
		// Token: 0x040008F3 RID: 2291
		UnspecifiedError,
		/// <summary>device ID out of range, MMSYSERR_BADDEVICEID</summary>
		// Token: 0x040008F4 RID: 2292
		BadDeviceId,
		/// <summary>driver failed enable, MMSYSERR_NOTENABLED</summary>
		// Token: 0x040008F5 RID: 2293
		NotEnabled,
		/// <summary>device already allocated, MMSYSERR_ALLOCATED</summary>
		// Token: 0x040008F6 RID: 2294
		AlreadyAllocated,
		/// <summary>device handle is invalid, MMSYSERR_INVALHANDLE</summary>
		// Token: 0x040008F7 RID: 2295
		InvalidHandle,
		/// <summary>no device driver present, MMSYSERR_NODRIVER</summary>
		// Token: 0x040008F8 RID: 2296
		NoDriver,
		/// <summary>memory allocation error, MMSYSERR_NOMEM</summary>
		// Token: 0x040008F9 RID: 2297
		MemoryAllocationError,
		/// <summary>function isn't supported, MMSYSERR_NOTSUPPORTED</summary>
		// Token: 0x040008FA RID: 2298
		NotSupported,
		/// <summary>error value out of range, MMSYSERR_BADERRNUM</summary>
		// Token: 0x040008FB RID: 2299
		BadErrorNumber,
		/// <summary>invalid flag passed, MMSYSERR_INVALFLAG</summary>
		// Token: 0x040008FC RID: 2300
		InvalidFlag,
		/// <summary>invalid parameter passed, MMSYSERR_INVALPARAM</summary>
		// Token: 0x040008FD RID: 2301
		InvalidParameter,
		/// <summary>handle being used simultaneously on another thread (eg callback),MMSYSERR_HANDLEBUSY</summary>
		// Token: 0x040008FE RID: 2302
		HandleBusy,
		/// <summary>specified alias not found, MMSYSERR_INVALIDALIAS</summary>
		// Token: 0x040008FF RID: 2303
		InvalidAlias,
		/// <summary>bad registry database, MMSYSERR_BADDB</summary>
		// Token: 0x04000900 RID: 2304
		BadRegistryDatabase,
		/// <summary>registry key not found, MMSYSERR_KEYNOTFOUND</summary>
		// Token: 0x04000901 RID: 2305
		RegistryKeyNotFound,
		/// <summary>registry read error, MMSYSERR_READERROR</summary>
		// Token: 0x04000902 RID: 2306
		RegistryReadError,
		/// <summary>registry write error, MMSYSERR_WRITEERROR</summary>
		// Token: 0x04000903 RID: 2307
		RegistryWriteError,
		/// <summary>registry delete error, MMSYSERR_DELETEERROR</summary>
		// Token: 0x04000904 RID: 2308
		RegistryDeleteError,
		/// <summary>registry value not found, MMSYSERR_VALNOTFOUND</summary>
		// Token: 0x04000905 RID: 2309
		RegistryValueNotFound,
		/// <summary>driver does not call DriverCallback, MMSYSERR_NODRIVERCB</summary>
		// Token: 0x04000906 RID: 2310
		NoDriverCallback,
		/// <summary>more data to be returned, MMSYSERR_MOREDATA</summary>
		// Token: 0x04000907 RID: 2311
		MoreData,
		/// <summary>unsupported wave format, WAVERR_BADFORMAT</summary>
		// Token: 0x04000908 RID: 2312
		WaveBadFormat = 32,
		/// <summary>still something playing, WAVERR_STILLPLAYING</summary>
		// Token: 0x04000909 RID: 2313
		WaveStillPlaying,
		/// <summary>header not prepared, WAVERR_UNPREPARED</summary>
		// Token: 0x0400090A RID: 2314
		WaveHeaderUnprepared,
		/// <summary>device is synchronous, WAVERR_SYNC</summary>
		// Token: 0x0400090B RID: 2315
		WaveSync,
		/// <summary>Conversion not possible (ACMERR_NOTPOSSIBLE)</summary>
		// Token: 0x0400090C RID: 2316
		AcmNotPossible = 512,
		/// <summary>Busy (ACMERR_BUSY)</summary>
		// Token: 0x0400090D RID: 2317
		AcmBusy,
		/// <summary>Header Unprepared (ACMERR_UNPREPARED)</summary>
		// Token: 0x0400090E RID: 2318
		AcmHeaderUnprepared,
		/// <summary>Cancelled (ACMERR_CANCELED)</summary>
		// Token: 0x0400090F RID: 2319
		AcmCancelled,
		/// <summary>invalid line (MIXERR_INVALLINE)</summary>
		// Token: 0x04000910 RID: 2320
		MixerInvalidLine = 1024,
		/// <summary>invalid control (MIXERR_INVALCONTROL)</summary>
		// Token: 0x04000911 RID: 2321
		MixerInvalidControl,
		/// <summary>invalid value (MIXERR_INVALVALUE)</summary>
		// Token: 0x04000912 RID: 2322
		MixerInvalidValue
	}
}
