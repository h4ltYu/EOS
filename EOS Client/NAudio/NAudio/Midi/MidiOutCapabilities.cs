using System;
using System.Runtime.InteropServices;

namespace NAudio.Midi
{
	/// <summary>
	/// class representing the capabilities of a MIDI out device
	/// MIDIOUTCAPS: http://msdn.microsoft.com/en-us/library/dd798467%28VS.85%29.aspx
	/// </summary>
	// Token: 0x020000EB RID: 235
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct MidiOutCapabilities
	{
		/// <summary>
		/// Gets the manufacturer of this device
		/// </summary>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00011928 File Offset: 0x0000FB28
		public Manufacturers Manufacturer
		{
			get
			{
				return (Manufacturers)this.manufacturerId;
			}
		}

		/// <summary>
		/// Gets the product identifier (manufacturer specific)
		/// </summary>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00011930 File Offset: 0x0000FB30
		public short ProductId
		{
			get
			{
				return this.productId;
			}
		}

		/// <summary>
		/// Gets the product name
		/// </summary>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00011938 File Offset: 0x0000FB38
		public string ProductName
		{
			get
			{
				return this.productName;
			}
		}

		/// <summary>
		/// Returns the number of supported voices
		/// </summary>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00011940 File Offset: 0x0000FB40
		public int Voices
		{
			get
			{
				return (int)this.wVoices;
			}
		}

		/// <summary>
		/// Gets the polyphony of the device
		/// </summary>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00011948 File Offset: 0x0000FB48
		public int Notes
		{
			get
			{
				return (int)this.wNotes;
			}
		}

		/// <summary>
		/// Returns true if the device supports all channels
		/// </summary>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00011950 File Offset: 0x0000FB50
		public bool SupportsAllChannels
		{
			get
			{
				return this.wChannelMask == ushort.MaxValue;
			}
		}

		/// <summary>
		/// Queries whether a particular channel is supported
		/// </summary>
		/// <param name="channel">Channel number to test</param>
		/// <returns>True if the channel is supported</returns>
		// Token: 0x06000572 RID: 1394 RVA: 0x0001195F File Offset: 0x0000FB5F
		public bool SupportsChannel(int channel)
		{
			return ((int)this.wChannelMask & 1 << channel - 1) > 0;
		}

		/// <summary>
		/// Returns true if the device supports patch caching
		/// </summary>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00011973 File Offset: 0x0000FB73
		public bool SupportsPatchCaching
		{
			get
			{
				return (this.dwSupport & MidiOutCapabilities.MidiOutCapabilityFlags.PatchCaching) != (MidiOutCapabilities.MidiOutCapabilityFlags)0;
			}
		}

		/// <summary>
		/// Returns true if the device supports separate left and right volume
		/// </summary>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00011983 File Offset: 0x0000FB83
		public bool SupportsSeparateLeftAndRightVolume
		{
			get
			{
				return (this.dwSupport & MidiOutCapabilities.MidiOutCapabilityFlags.LeftRightVolume) != (MidiOutCapabilities.MidiOutCapabilityFlags)0;
			}
		}

		/// <summary>
		/// Returns true if the device supports MIDI stream out
		/// </summary>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00011993 File Offset: 0x0000FB93
		public bool SupportsMidiStreamOut
		{
			get
			{
				return (this.dwSupport & MidiOutCapabilities.MidiOutCapabilityFlags.Stream) != (MidiOutCapabilities.MidiOutCapabilityFlags)0;
			}
		}

		/// <summary>
		/// Returns true if the device supports volume control
		/// </summary>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000119A3 File Offset: 0x0000FBA3
		public bool SupportsVolumeControl
		{
			get
			{
				return (this.dwSupport & MidiOutCapabilities.MidiOutCapabilityFlags.Volume) != (MidiOutCapabilities.MidiOutCapabilityFlags)0;
			}
		}

		/// <summary>
		/// Returns the type of technology used by this MIDI out device
		/// </summary>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x000119B3 File Offset: 0x0000FBB3
		public MidiOutTechnology Technology
		{
			get
			{
				return (MidiOutTechnology)this.wTechnology;
			}
		}

		// Token: 0x040005DA RID: 1498
		private const int MaxProductNameLength = 32;

		// Token: 0x040005DB RID: 1499
		private short manufacturerId;

		// Token: 0x040005DC RID: 1500
		private short productId;

		// Token: 0x040005DD RID: 1501
		private int driverVersion;

		// Token: 0x040005DE RID: 1502
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		private string productName;

		// Token: 0x040005DF RID: 1503
		private short wTechnology;

		// Token: 0x040005E0 RID: 1504
		private short wVoices;

		// Token: 0x040005E1 RID: 1505
		private short wNotes;

		// Token: 0x040005E2 RID: 1506
		private ushort wChannelMask;

		// Token: 0x040005E3 RID: 1507
		private MidiOutCapabilities.MidiOutCapabilityFlags dwSupport;

		// Token: 0x020000EC RID: 236
		[Flags]
		private enum MidiOutCapabilityFlags
		{
			/// <summary>
			/// MIDICAPS_VOLUME
			/// </summary>
			// Token: 0x040005E5 RID: 1509
			Volume = 1,
			/// <summary>
			/// separate left-right volume control
			/// MIDICAPS_LRVOLUME
			/// </summary>
			// Token: 0x040005E6 RID: 1510
			LeftRightVolume = 2,
			/// <summary>
			/// MIDICAPS_CACHE
			/// </summary>
			// Token: 0x040005E7 RID: 1511
			PatchCaching = 4,
			/// <summary>
			/// MIDICAPS_STREAM
			/// driver supports midiStreamOut directly
			/// </summary>
			// Token: 0x040005E8 RID: 1512
			Stream = 8
		}
	}
}
