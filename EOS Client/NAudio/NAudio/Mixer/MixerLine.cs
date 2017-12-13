using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	/// <summary>
	/// Represents a mixer line (source or destination)
	/// </summary>
	// Token: 0x02000102 RID: 258
	public class MixerLine
	{
		/// <summary>
		/// Creates a new mixer destination
		/// </summary>
		/// <param name="mixerHandle">Mixer Handle</param>
		/// <param name="destinationIndex">Destination Index</param>
		/// <param name="mixerHandleType">Mixer Handle Type</param>
		// Token: 0x060005EE RID: 1518 RVA: 0x000137D4 File Offset: 0x000119D4
		public MixerLine(IntPtr mixerHandle, int destinationIndex, MixerFlags mixerHandleType)
		{
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.mixerLine = default(MixerInterop.MIXERLINE);
			this.mixerLine.cbStruct = Marshal.SizeOf(this.mixerLine);
			this.mixerLine.dwDestination = destinationIndex;
			MmException.Try(MixerInterop.mixerGetLineInfo(mixerHandle, ref this.mixerLine, mixerHandleType), "mixerGetLineInfo");
		}

		/// <summary>
		/// Creates a new Mixer Source For a Specified Source
		/// </summary>
		/// <param name="mixerHandle">Mixer Handle</param>
		/// <param name="destinationIndex">Destination Index</param>
		/// <param name="sourceIndex">Source Index</param>
		/// <param name="mixerHandleType">Flag indicating the meaning of mixerHandle</param>
		// Token: 0x060005EF RID: 1519 RVA: 0x00013840 File Offset: 0x00011A40
		public MixerLine(IntPtr mixerHandle, int destinationIndex, int sourceIndex, MixerFlags mixerHandleType)
		{
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.mixerLine = default(MixerInterop.MIXERLINE);
			this.mixerLine.cbStruct = Marshal.SizeOf(this.mixerLine);
			this.mixerLine.dwDestination = destinationIndex;
			this.mixerLine.dwSource = sourceIndex;
			MmException.Try(MixerInterop.mixerGetLineInfo(mixerHandle, ref this.mixerLine, mixerHandleType | MixerFlags.ListText), "mixerGetLineInfo");
		}

		/// <summary>
		/// Creates a new Mixer Source
		/// </summary>
		/// <param name="waveInDevice">Wave In Device</param>
		// Token: 0x060005F0 RID: 1520 RVA: 0x000138BC File Offset: 0x00011ABC
		public static int GetMixerIdForWaveIn(int waveInDevice)
		{
			int result = -1;
			MmException.Try(MixerInterop.mixerGetID((IntPtr)waveInDevice, out result, MixerFlags.WaveIn), "mixerGetID");
			return result;
		}

		/// <summary>
		/// Mixer Line Name
		/// </summary>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000138E8 File Offset: 0x00011AE8
		public string Name
		{
			get
			{
				return this.mixerLine.szName;
			}
		}

		/// <summary>
		/// Mixer Line short name
		/// </summary>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000138F5 File Offset: 0x00011AF5
		public string ShortName
		{
			get
			{
				return this.mixerLine.szShortName;
			}
		}

		/// <summary>
		/// The line ID
		/// </summary>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00013902 File Offset: 0x00011B02
		public int LineId
		{
			get
			{
				return this.mixerLine.dwLineID;
			}
		}

		/// <summary>
		/// Component Type
		/// </summary>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001390F File Offset: 0x00011B0F
		public MixerLineComponentType ComponentType
		{
			get
			{
				return this.mixerLine.dwComponentType;
			}
		}

		/// <summary>
		/// Mixer destination type description
		/// </summary>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001391C File Offset: 0x00011B1C
		public string TypeDescription
		{
			get
			{
				MixerLineComponentType dwComponentType = this.mixerLine.dwComponentType;
				switch (dwComponentType)
				{
				case MixerLineComponentType.DestinationUndefined:
					return "Undefined Destination";
				case MixerLineComponentType.DestinationDigital:
					return "Digital Destination";
				case MixerLineComponentType.DestinationLine:
					return "Line Level Destination";
				case MixerLineComponentType.DestinationMonitor:
					return "Monitor Destination";
				case MixerLineComponentType.DestinationSpeakers:
					return "Speakers Destination";
				case MixerLineComponentType.DestinationHeadphones:
					return "Headphones Destination";
				case MixerLineComponentType.DestinationTelephone:
					return "Telephone Destination";
				case MixerLineComponentType.DestinationWaveIn:
					return "Wave Input Destination";
				case MixerLineComponentType.DestinationVoiceIn:
					return "Voice Recognition Destination";
				default:
					switch (dwComponentType)
					{
					case MixerLineComponentType.SourceUndefined:
						return "Undefined Source";
					case MixerLineComponentType.SourceDigital:
						return "Digital Source";
					case MixerLineComponentType.SourceLine:
						return "Line Level Source";
					case MixerLineComponentType.SourceMicrophone:
						return "Microphone Source";
					case MixerLineComponentType.SourceSynthesizer:
						return "Synthesizer Source";
					case MixerLineComponentType.SourceCompactDisc:
						return "Compact Disk Source";
					case MixerLineComponentType.SourceTelephone:
						return "Telephone Source";
					case MixerLineComponentType.SourcePcSpeaker:
						return "PC Speaker Source";
					case MixerLineComponentType.SourceWaveOut:
						return "Wave Out Source";
					case MixerLineComponentType.SourceAuxiliary:
						return "Auxiliary Source";
					case MixerLineComponentType.SourceAnalog:
						return "Analog Source";
					default:
						return "Invalid Component Type";
					}
					break;
				}
			}
		}

		/// <summary>
		/// Number of channels
		/// </summary>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00013A16 File Offset: 0x00011C16
		public int Channels
		{
			get
			{
				return this.mixerLine.cChannels;
			}
		}

		/// <summary>
		/// Number of sources
		/// </summary>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00013A23 File Offset: 0x00011C23
		public int SourceCount
		{
			get
			{
				return this.mixerLine.cConnections;
			}
		}

		/// <summary>
		/// Number of controls
		/// </summary>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00013A30 File Offset: 0x00011C30
		public int ControlsCount
		{
			get
			{
				return this.mixerLine.cControls;
			}
		}

		/// <summary>
		/// Is this destination active
		/// </summary>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00013A3D File Offset: 0x00011C3D
		public bool IsActive
		{
			get
			{
				return (this.mixerLine.fdwLine & MixerInterop.MIXERLINE_LINEF.MIXERLINE_LINEF_ACTIVE) != (MixerInterop.MIXERLINE_LINEF)0;
			}
		}

		/// <summary>
		/// Is this destination disconnected
		/// </summary>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00013A52 File Offset: 0x00011C52
		public bool IsDisconnected
		{
			get
			{
				return (this.mixerLine.fdwLine & MixerInterop.MIXERLINE_LINEF.MIXERLINE_LINEF_DISCONNECTED) != (MixerInterop.MIXERLINE_LINEF)0;
			}
		}

		/// <summary>
		/// Is this destination a source
		/// </summary>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00013A6B File Offset: 0x00011C6B
		public bool IsSource
		{
			get
			{
				return (this.mixerLine.fdwLine & MixerInterop.MIXERLINE_LINEF.MIXERLINE_LINEF_SOURCE) != (MixerInterop.MIXERLINE_LINEF)0;
			}
		}

		/// <summary>
		/// Gets the specified source
		/// </summary>
		// Token: 0x060005FC RID: 1532 RVA: 0x00013A84 File Offset: 0x00011C84
		public MixerLine GetSource(int sourceIndex)
		{
			if (sourceIndex < 0 || sourceIndex >= this.SourceCount)
			{
				throw new ArgumentOutOfRangeException("sourceIndex");
			}
			return new MixerLine(this.mixerHandle, this.mixerLine.dwDestination, sourceIndex, this.mixerHandleType);
		}

		/// <summary>
		/// Enumerator for the controls on this Mixer Limne
		/// </summary>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00013ABB File Offset: 0x00011CBB
		public IEnumerable<MixerControl> Controls
		{
			get
			{
				return MixerControl.GetMixerControls(this.mixerHandle, this, this.mixerHandleType);
			}
		}

		/// <summary>
		/// Enumerator for the sources on this Mixer Line
		/// </summary>
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00013BD8 File Offset: 0x00011DD8
		public IEnumerable<MixerLine> Sources
		{
			get
			{
				for (int source = 0; source < this.SourceCount; source++)
				{
					yield return this.GetSource(source);
				}
				yield break;
			}
		}

		/// <summary>
		/// The name of the target output device
		/// </summary>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00013BF5 File Offset: 0x00011DF5
		public string TargetName
		{
			get
			{
				return this.mixerLine.szPname;
			}
		}

		/// <summary>
		/// Describes this Mixer Line (for diagnostic purposes)
		/// </summary>
		// Token: 0x06000600 RID: 1536 RVA: 0x00013C04 File Offset: 0x00011E04
		public override string ToString()
		{
			return string.Format("{0} {1} ({2} controls, ID={3})", new object[]
			{
				this.Name,
				this.TypeDescription,
				this.ControlsCount,
				this.mixerLine.dwLineID
			});
		}

		// Token: 0x04000649 RID: 1609
		private MixerInterop.MIXERLINE mixerLine;

		// Token: 0x0400064A RID: 1610
		private IntPtr mixerHandle;

		// Token: 0x0400064B RID: 1611
		private MixerFlags mixerHandleType;
	}
}
