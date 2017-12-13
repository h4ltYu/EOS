using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	/// <summary>Represents a Windows mixer device</summary>
	// Token: 0x020000FD RID: 253
	public class Mixer
	{
		/// <summary>The number of mixer devices available</summary>	
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x000134C6 File Offset: 0x000116C6
		public static int NumberOfDevices
		{
			get
			{
				return MixerInterop.mixerGetNumDevs();
			}
		}

		/// <summary>Connects to the specified mixer</summary>
		/// <param name="mixerIndex">The index of the mixer to use. 
		/// This should be between zero and NumberOfDevices - 1</param>
		// Token: 0x060005E6 RID: 1510 RVA: 0x000134D0 File Offset: 0x000116D0
		public Mixer(int mixerIndex)
		{
			if (mixerIndex < 0 || mixerIndex >= Mixer.NumberOfDevices)
			{
				throw new ArgumentOutOfRangeException("mixerID");
			}
			this.caps = default(MixerInterop.MIXERCAPS);
			MmException.Try(MixerInterop.mixerGetDevCaps((IntPtr)mixerIndex, ref this.caps, Marshal.SizeOf(this.caps)), "mixerGetDevCaps");
			this.mixerHandle = (IntPtr)mixerIndex;
			this.mixerHandleType = MixerFlags.Mixer;
		}

		/// <summary>The number of destinations this mixer supports</summary>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00013544 File Offset: 0x00011744
		public int DestinationCount
		{
			get
			{
				return (int)this.caps.cDestinations;
			}
		}

		/// <summary>The name of this mixer device</summary>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00013551 File Offset: 0x00011751
		public string Name
		{
			get
			{
				return this.caps.szPname;
			}
		}

		/// <summary>The manufacturer code for this mixer device</summary>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0001355E File Offset: 0x0001175E
		public Manufacturers Manufacturer
		{
			get
			{
				return (Manufacturers)this.caps.wMid;
			}
		}

		/// <summary>The product identifier code for this mixer device</summary>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001356B File Offset: 0x0001176B
		public int ProductID
		{
			get
			{
				return (int)this.caps.wPid;
			}
		}

		/// <summary>Retrieve the specified MixerDestination object</summary>
		/// <param name="destinationIndex">The ID of the destination to use.
		/// Should be between 0 and DestinationCount - 1</param>
		// Token: 0x060005EB RID: 1515 RVA: 0x00013578 File Offset: 0x00011778
		public MixerLine GetDestination(int destinationIndex)
		{
			if (destinationIndex < 0 || destinationIndex >= this.DestinationCount)
			{
				throw new ArgumentOutOfRangeException("destinationIndex");
			}
			return new MixerLine(this.mixerHandle, destinationIndex, this.mixerHandleType);
		}

		/// <summary>
		/// A way to enumerate the destinations
		/// </summary>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x000136AC File Offset: 0x000118AC
		public IEnumerable<MixerLine> Destinations
		{
			get
			{
				for (int destination = 0; destination < this.DestinationCount; destination++)
				{
					yield return this.GetDestination(destination);
				}
				yield break;
			}
		}

		/// <summary>
		/// A way to enumerate all available devices
		/// </summary>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x000137BC File Offset: 0x000119BC
		public static IEnumerable<Mixer> Mixers
		{
			get
			{
				for (int device = 0; device < Mixer.NumberOfDevices; device++)
				{
					yield return new Mixer(device);
				}
				yield break;
			}
		}

		// Token: 0x0400060C RID: 1548
		private MixerInterop.MIXERCAPS caps;

		// Token: 0x0400060D RID: 1549
		private IntPtr mixerHandle;

		// Token: 0x0400060E RID: 1550
		private MixerFlags mixerHandleType;
	}
}
