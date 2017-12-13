using System;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	// Token: 0x02000104 RID: 260
	internal class MixerInterop
	{
		// Token: 0x06000601 RID: 1537
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern int mixerGetNumDevs();

		// Token: 0x06000602 RID: 1538
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern MmResult mixerOpen(out IntPtr hMixer, int uMxId, IntPtr dwCallback, IntPtr dwInstance, MixerFlags dwOpenFlags);

		// Token: 0x06000603 RID: 1539
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern MmResult mixerClose(IntPtr hMixer);

		// Token: 0x06000604 RID: 1540
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern MmResult mixerGetControlDetails(IntPtr hMixer, ref MixerInterop.MIXERCONTROLDETAILS mixerControlDetails, MixerFlags dwDetailsFlags);

		// Token: 0x06000605 RID: 1541
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern MmResult mixerGetDevCaps(IntPtr nMixerID, ref MixerInterop.MIXERCAPS mixerCaps, int mixerCapsSize);

		// Token: 0x06000606 RID: 1542
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern MmResult mixerGetID(IntPtr hMixer, out int mixerID, MixerFlags dwMixerIDFlags);

		// Token: 0x06000607 RID: 1543
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern MmResult mixerGetLineControls(IntPtr hMixer, ref MixerInterop.MIXERLINECONTROLS mixerLineControls, MixerFlags dwControlFlags);

		// Token: 0x06000608 RID: 1544
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern MmResult mixerGetLineInfo(IntPtr hMixer, ref MixerInterop.MIXERLINE mixerLine, MixerFlags dwInfoFlags);

		// Token: 0x06000609 RID: 1545
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern MmResult mixerMessage(IntPtr hMixer, uint nMessage, IntPtr dwParam1, IntPtr dwParam2);

		// Token: 0x0600060A RID: 1546
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern MmResult mixerSetControlDetails(IntPtr hMixer, ref MixerInterop.MIXERCONTROLDETAILS mixerControlDetails, MixerFlags dwDetailsFlags);

		// Token: 0x04000665 RID: 1637
		public const uint MIXERCONTROL_CONTROLF_UNIFORM = 1u;

		// Token: 0x04000666 RID: 1638
		public const uint MIXERCONTROL_CONTROLF_MULTIPLE = 2u;

		// Token: 0x04000667 RID: 1639
		public const uint MIXERCONTROL_CONTROLF_DISABLED = 2147483648u;

		// Token: 0x04000668 RID: 1640
		public const int MAXPNAMELEN = 32;

		// Token: 0x04000669 RID: 1641
		public const int MIXER_SHORT_NAME_CHARS = 16;

		// Token: 0x0400066A RID: 1642
		public const int MIXER_LONG_NAME_CHARS = 64;

		// Token: 0x02000105 RID: 261
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
		public struct MIXERCONTROLDETAILS
		{
			// Token: 0x0400066B RID: 1643
			public int cbStruct;

			// Token: 0x0400066C RID: 1644
			public int dwControlID;

			// Token: 0x0400066D RID: 1645
			public int cChannels;

			// Token: 0x0400066E RID: 1646
			public IntPtr hwndOwner;

			// Token: 0x0400066F RID: 1647
			public int cbDetails;

			// Token: 0x04000670 RID: 1648
			public IntPtr paDetails;
		}

		// Token: 0x02000106 RID: 262
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct MIXERCAPS
		{
			// Token: 0x04000671 RID: 1649
			public ushort wMid;

			// Token: 0x04000672 RID: 1650
			public ushort wPid;

			// Token: 0x04000673 RID: 1651
			public uint vDriverVersion;

			// Token: 0x04000674 RID: 1652
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string szPname;

			// Token: 0x04000675 RID: 1653
			public uint fdwSupport;

			// Token: 0x04000676 RID: 1654
			public uint cDestinations;
		}

		// Token: 0x02000107 RID: 263
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct MIXERLINECONTROLS
		{
			// Token: 0x04000677 RID: 1655
			public int cbStruct;

			// Token: 0x04000678 RID: 1656
			public int dwLineID;

			// Token: 0x04000679 RID: 1657
			public int dwControlID;

			// Token: 0x0400067A RID: 1658
			public int cControls;

			// Token: 0x0400067B RID: 1659
			public int cbmxctrl;

			// Token: 0x0400067C RID: 1660
			public IntPtr pamxctrl;
		}

		/// <summary>
		/// Mixer Line Flags
		/// </summary>
		// Token: 0x02000108 RID: 264
		[Flags]
		public enum MIXERLINE_LINEF
		{
			/// <summary>
			/// Audio line is active. An active line indicates that a signal is probably passing 
			/// through the line.
			/// </summary>
			// Token: 0x0400067E RID: 1662
			MIXERLINE_LINEF_ACTIVE = 1,
			/// <summary>
			/// Audio line is disconnected. A disconnected line's associated controls can still be 
			/// modified, but the changes have no effect until the line is connected.
			/// </summary>
			// Token: 0x0400067F RID: 1663
			MIXERLINE_LINEF_DISCONNECTED = 32768,
			/// <summary>
			/// Audio line is an audio source line associated with a single audio destination line. 
			/// If this flag is not set, this line is an audio destination line associated with zero 
			/// or more audio source lines.
			/// </summary>
			// Token: 0x04000680 RID: 1664
			MIXERLINE_LINEF_SOURCE = -2147483648
		}

		// Token: 0x02000109 RID: 265
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct MIXERLINE
		{
			// Token: 0x04000681 RID: 1665
			public int cbStruct;

			// Token: 0x04000682 RID: 1666
			public int dwDestination;

			// Token: 0x04000683 RID: 1667
			public int dwSource;

			// Token: 0x04000684 RID: 1668
			public int dwLineID;

			// Token: 0x04000685 RID: 1669
			public MixerInterop.MIXERLINE_LINEF fdwLine;

			// Token: 0x04000686 RID: 1670
			public IntPtr dwUser;

			// Token: 0x04000687 RID: 1671
			public MixerLineComponentType dwComponentType;

			// Token: 0x04000688 RID: 1672
			public int cChannels;

			// Token: 0x04000689 RID: 1673
			public int cConnections;

			// Token: 0x0400068A RID: 1674
			public int cControls;

			// Token: 0x0400068B RID: 1675
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string szShortName;

			// Token: 0x0400068C RID: 1676
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szName;

			// Token: 0x0400068D RID: 1677
			public uint dwType;

			// Token: 0x0400068E RID: 1678
			public uint dwDeviceID;

			// Token: 0x0400068F RID: 1679
			public ushort wMid;

			// Token: 0x04000690 RID: 1680
			public ushort wPid;

			// Token: 0x04000691 RID: 1681
			public uint vDriverVersion;

			// Token: 0x04000692 RID: 1682
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string szPname;
		}

		/// <summary>
		/// BOUNDS structure
		/// </summary>
		// Token: 0x0200010A RID: 266
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Bounds
		{
			/// <summary>
			/// dwMinimum / lMinimum / reserved 0
			/// </summary>
			// Token: 0x04000693 RID: 1683
			public int minimum;

			/// <summary>
			/// dwMaximum / lMaximum / reserved 1
			/// </summary>
			// Token: 0x04000694 RID: 1684
			public int maximum;

			/// <summary>
			/// reserved 2
			/// </summary>
			// Token: 0x04000695 RID: 1685
			public int reserved2;

			/// <summary>
			/// reserved 3
			/// </summary>
			// Token: 0x04000696 RID: 1686
			public int reserved3;

			/// <summary>
			/// reserved 4
			/// </summary>
			// Token: 0x04000697 RID: 1687
			public int reserved4;

			/// <summary>
			/// reserved 5
			/// </summary>
			// Token: 0x04000698 RID: 1688
			public int reserved5;
		}

		/// <summary>
		/// METRICS structure
		/// </summary>
		// Token: 0x0200010B RID: 267
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Metrics
		{
			/// <summary>
			/// cSteps / reserved[0]
			/// </summary>
			// Token: 0x04000699 RID: 1689
			public int step;

			/// <summary>
			/// cbCustomData / reserved[1], number of bytes for control details
			/// </summary>
			// Token: 0x0400069A RID: 1690
			public int customData;

			/// <summary>
			/// reserved 2
			/// </summary>
			// Token: 0x0400069B RID: 1691
			public int reserved2;

			/// <summary>
			/// reserved 3
			/// </summary>
			// Token: 0x0400069C RID: 1692
			public int reserved3;

			/// <summary>
			/// reserved 4
			/// </summary>
			// Token: 0x0400069D RID: 1693
			public int reserved4;

			/// <summary>
			/// reserved 5
			/// </summary>
			// Token: 0x0400069E RID: 1694
			public int reserved5;
		}

		/// <summary>
		/// MIXERCONTROL struct
		/// http://msdn.microsoft.com/en-us/library/dd757293%28VS.85%29.aspx
		/// </summary>
		// Token: 0x0200010C RID: 268
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct MIXERCONTROL
		{
			// Token: 0x0400069F RID: 1695
			public uint cbStruct;

			// Token: 0x040006A0 RID: 1696
			public int dwControlID;

			// Token: 0x040006A1 RID: 1697
			public MixerControlType dwControlType;

			// Token: 0x040006A2 RID: 1698
			public uint fdwControl;

			// Token: 0x040006A3 RID: 1699
			public uint cMultipleItems;

			// Token: 0x040006A4 RID: 1700
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string szShortName;

			// Token: 0x040006A5 RID: 1701
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szName;

			// Token: 0x040006A6 RID: 1702
			public MixerInterop.Bounds Bounds;

			// Token: 0x040006A7 RID: 1703
			public MixerInterop.Metrics Metrics;
		}

		// Token: 0x0200010D RID: 269
		public struct MIXERCONTROLDETAILS_BOOLEAN
		{
			// Token: 0x040006A8 RID: 1704
			public int fValue;
		}

		// Token: 0x0200010E RID: 270
		public struct MIXERCONTROLDETAILS_SIGNED
		{
			// Token: 0x040006A9 RID: 1705
			public int lValue;
		}

		// Token: 0x0200010F RID: 271
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct MIXERCONTROLDETAILS_LISTTEXT
		{
			// Token: 0x040006AA RID: 1706
			public uint dwParam1;

			// Token: 0x040006AB RID: 1707
			public uint dwParam2;

			// Token: 0x040006AC RID: 1708
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szName;
		}

		// Token: 0x02000110 RID: 272
		public struct MIXERCONTROLDETAILS_UNSIGNED
		{
			// Token: 0x040006AD RID: 1709
			public uint dwValue;
		}
	}
}
