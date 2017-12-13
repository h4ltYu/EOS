using System;
using System.Runtime.InteropServices;

namespace NAudio.Midi
{
	// Token: 0x020000E0 RID: 224
	internal class MidiInterop
	{
		// Token: 0x06000526 RID: 1318
		[DllImport("winmm.dll")]
		public static extern MmResult midiConnect(IntPtr hMidiIn, IntPtr hMidiOut, IntPtr pReserved);

		// Token: 0x06000527 RID: 1319
		[DllImport("winmm.dll")]
		public static extern MmResult midiDisconnect(IntPtr hMidiIn, IntPtr hMidiOut, IntPtr pReserved);

		// Token: 0x06000528 RID: 1320
		[DllImport("winmm.dll")]
		public static extern MmResult midiInAddBuffer(IntPtr hMidiIn, [MarshalAs(UnmanagedType.Struct)] ref MidiInterop.MIDIHDR lpMidiInHdr, int uSize);

		// Token: 0x06000529 RID: 1321
		[DllImport("winmm.dll")]
		public static extern MmResult midiInClose(IntPtr hMidiIn);

		// Token: 0x0600052A RID: 1322
		[DllImport("winmm.dll", CharSet = CharSet.Auto)]
		public static extern MmResult midiInGetDevCaps(IntPtr deviceId, out MidiInCapabilities capabilities, int size);

		// Token: 0x0600052B RID: 1323
		[DllImport("winmm.dll")]
		public static extern MmResult midiInGetErrorText(int err, string lpText, int uSize);

		// Token: 0x0600052C RID: 1324
		[DllImport("winmm.dll")]
		public static extern MmResult midiInGetID(IntPtr hMidiIn, out int lpuDeviceId);

		// Token: 0x0600052D RID: 1325
		[DllImport("winmm.dll")]
		public static extern int midiInGetNumDevs();

		// Token: 0x0600052E RID: 1326
		[DllImport("winmm.dll")]
		public static extern MmResult midiInMessage(IntPtr hMidiIn, int msg, IntPtr dw1, IntPtr dw2);

		// Token: 0x0600052F RID: 1327
		[DllImport("winmm.dll")]
		public static extern MmResult midiInOpen(out IntPtr hMidiIn, IntPtr uDeviceID, MidiInterop.MidiInCallback callback, IntPtr dwInstance, int dwFlags);

		// Token: 0x06000530 RID: 1328
		[DllImport("winmm.dll", EntryPoint = "midiInOpen")]
		public static extern MmResult midiInOpenWindow(out IntPtr hMidiIn, IntPtr uDeviceID, IntPtr callbackWindowHandle, IntPtr dwInstance, int dwFlags);

		// Token: 0x06000531 RID: 1329
		[DllImport("winmm.dll")]
		public static extern MmResult midiInPrepareHeader(IntPtr hMidiIn, [MarshalAs(UnmanagedType.Struct)] ref MidiInterop.MIDIHDR lpMidiInHdr, int uSize);

		// Token: 0x06000532 RID: 1330
		[DllImport("winmm.dll")]
		public static extern MmResult midiInReset(IntPtr hMidiIn);

		// Token: 0x06000533 RID: 1331
		[DllImport("winmm.dll")]
		public static extern MmResult midiInStart(IntPtr hMidiIn);

		// Token: 0x06000534 RID: 1332
		[DllImport("winmm.dll")]
		public static extern MmResult midiInStop(IntPtr hMidiIn);

		// Token: 0x06000535 RID: 1333
		[DllImport("winmm.dll")]
		public static extern MmResult midiInUnprepareHeader(IntPtr hMidiIn, [MarshalAs(UnmanagedType.Struct)] ref MidiInterop.MIDIHDR lpMidiInHdr, int uSize);

		// Token: 0x06000536 RID: 1334
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutCacheDrumPatches(IntPtr hMidiOut, int uPatch, IntPtr lpKeyArray, int uFlags);

		// Token: 0x06000537 RID: 1335
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutCachePatches(IntPtr hMidiOut, int uBank, IntPtr lpPatchArray, int uFlags);

		// Token: 0x06000538 RID: 1336
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutClose(IntPtr hMidiOut);

		// Token: 0x06000539 RID: 1337
		[DllImport("winmm.dll", CharSet = CharSet.Auto)]
		public static extern MmResult midiOutGetDevCaps(IntPtr deviceNumber, out MidiOutCapabilities caps, int uSize);

		// Token: 0x0600053A RID: 1338
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutGetErrorText(IntPtr err, string lpText, int uSize);

		// Token: 0x0600053B RID: 1339
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutGetID(IntPtr hMidiOut, out int lpuDeviceID);

		// Token: 0x0600053C RID: 1340
		[DllImport("winmm.dll")]
		public static extern int midiOutGetNumDevs();

		// Token: 0x0600053D RID: 1341
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutGetVolume(IntPtr uDeviceID, ref int lpdwVolume);

		// Token: 0x0600053E RID: 1342
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutLongMsg(IntPtr hMidiOut, [MarshalAs(UnmanagedType.Struct)] ref MidiInterop.MIDIHDR lpMidiOutHdr, int uSize);

		// Token: 0x0600053F RID: 1343
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutMessage(IntPtr hMidiOut, int msg, IntPtr dw1, IntPtr dw2);

		// Token: 0x06000540 RID: 1344
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutOpen(out IntPtr lphMidiOut, IntPtr uDeviceID, MidiInterop.MidiOutCallback dwCallback, IntPtr dwInstance, int dwFlags);

		// Token: 0x06000541 RID: 1345
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutPrepareHeader(IntPtr hMidiOut, [MarshalAs(UnmanagedType.Struct)] ref MidiInterop.MIDIHDR lpMidiOutHdr, int uSize);

		// Token: 0x06000542 RID: 1346
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutReset(IntPtr hMidiOut);

		// Token: 0x06000543 RID: 1347
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutSetVolume(IntPtr hMidiOut, int dwVolume);

		// Token: 0x06000544 RID: 1348
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutShortMsg(IntPtr hMidiOut, int dwMsg);

		// Token: 0x06000545 RID: 1349
		[DllImport("winmm.dll")]
		public static extern MmResult midiOutUnprepareHeader(IntPtr hMidiOut, [MarshalAs(UnmanagedType.Struct)] ref MidiInterop.MIDIHDR lpMidiOutHdr, int uSize);

		// Token: 0x06000546 RID: 1350
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamClose(IntPtr hMidiStream);

		// Token: 0x06000547 RID: 1351
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamOpen(out IntPtr hMidiStream, IntPtr puDeviceID, int cMidi, IntPtr dwCallback, IntPtr dwInstance, int fdwOpen);

		// Token: 0x06000548 RID: 1352
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamOut(IntPtr hMidiStream, [MarshalAs(UnmanagedType.Struct)] ref MidiInterop.MIDIHDR pmh, int cbmh);

		// Token: 0x06000549 RID: 1353
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamPause(IntPtr hMidiStream);

		// Token: 0x0600054A RID: 1354
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamPosition(IntPtr hMidiStream, [MarshalAs(UnmanagedType.Struct)] ref MidiInterop.MMTIME lpmmt, int cbmmt);

		// Token: 0x0600054B RID: 1355
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamProperty(IntPtr hMidiStream, IntPtr lppropdata, int dwProperty);

		// Token: 0x0600054C RID: 1356
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamRestart(IntPtr hMidiStream);

		// Token: 0x0600054D RID: 1357
		[DllImport("winmm.dll")]
		public static extern MmResult midiStreamStop(IntPtr hMidiStream);

		// Token: 0x040005B7 RID: 1463
		public const int CALLBACK_FUNCTION = 196608;

		// Token: 0x040005B8 RID: 1464
		public const int CALLBACK_NULL = 0;

		// Token: 0x020000E1 RID: 225
		public enum MidiInMessage
		{
			/// <summary>
			/// MIM_OPEN
			/// </summary>
			// Token: 0x040005BA RID: 1466
			Open = 961,
			/// <summary>
			/// MIM_CLOSE
			/// </summary>
			// Token: 0x040005BB RID: 1467
			Close,
			/// <summary>
			/// MIM_DATA
			/// </summary>
			// Token: 0x040005BC RID: 1468
			Data,
			/// <summary>
			/// MIM_LONGDATA
			/// </summary>
			// Token: 0x040005BD RID: 1469
			LongData,
			/// <summary>
			/// MIM_ERROR
			/// </summary>
			// Token: 0x040005BE RID: 1470
			Error,
			/// <summary>
			/// MIM_LONGERROR
			/// </summary>
			// Token: 0x040005BF RID: 1471
			LongError,
			/// <summary>
			/// MIM_MOREDATA
			/// </summary>
			// Token: 0x040005C0 RID: 1472
			MoreData = 972
		}

		// Token: 0x020000E2 RID: 226
		public enum MidiOutMessage
		{
			/// <summary>
			/// MOM_OPEN
			/// </summary>
			// Token: 0x040005C2 RID: 1474
			Open = 967,
			/// <summary>
			/// MOM_CLOSE
			/// </summary>
			// Token: 0x040005C3 RID: 1475
			Close,
			/// <summary>
			/// MOM_DONE
			/// </summary>
			// Token: 0x040005C4 RID: 1476
			Done
		}

		// Token: 0x020000E3 RID: 227
		// (Invoke) Token: 0x06000550 RID: 1360
		public delegate void MidiInCallback(IntPtr midiInHandle, MidiInterop.MidiInMessage message, IntPtr userData, IntPtr messageParameter1, IntPtr messageParameter2);

		// Token: 0x020000E4 RID: 228
		// (Invoke) Token: 0x06000554 RID: 1364
		public delegate void MidiOutCallback(IntPtr midiInHandle, MidiInterop.MidiOutMessage message, IntPtr userData, IntPtr messageParameter1, IntPtr messageParameter2);

		// Token: 0x020000E5 RID: 229
		public struct MMTIME
		{
			// Token: 0x040005C5 RID: 1477
			public int wType;

			// Token: 0x040005C6 RID: 1478
			public int u;
		}

		// Token: 0x020000E6 RID: 230
		public struct MIDIEVENT
		{
			// Token: 0x040005C7 RID: 1479
			public int dwDeltaTime;

			// Token: 0x040005C8 RID: 1480
			public int dwStreamID;

			// Token: 0x040005C9 RID: 1481
			public int dwEvent;

			// Token: 0x040005CA RID: 1482
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
			public int dwParms;
		}

		// Token: 0x020000E7 RID: 231
		public struct MIDIHDR
		{
			// Token: 0x040005CB RID: 1483
			public IntPtr lpData;

			// Token: 0x040005CC RID: 1484
			public int dwBufferLength;

			// Token: 0x040005CD RID: 1485
			public int dwBytesRecorded;

			// Token: 0x040005CE RID: 1486
			public IntPtr dwUser;

			// Token: 0x040005CF RID: 1487
			public int dwFlags;

			// Token: 0x040005D0 RID: 1488
			public IntPtr lpNext;

			// Token: 0x040005D1 RID: 1489
			public IntPtr reserved;

			// Token: 0x040005D2 RID: 1490
			public int dwOffset;

			// Token: 0x040005D3 RID: 1491
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public IntPtr[] dwReserved;
		}

		// Token: 0x020000E8 RID: 232
		public struct MIDIPROPTEMPO
		{
			// Token: 0x040005D4 RID: 1492
			public int cbStruct;

			// Token: 0x040005D5 RID: 1493
			public int dwTempo;
		}
	}
}
