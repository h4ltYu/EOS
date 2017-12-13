using System;
using System.IO;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI control change event
	/// </summary>
	// Token: 0x020000D5 RID: 213
	public class ControlChangeEvent : MidiEvent
	{
		/// <summary>
		/// Reads a control change event from a MIDI stream
		/// </summary>
		/// <param name="br">Binary reader on the MIDI stream</param>
		// Token: 0x060004DE RID: 1246 RVA: 0x00010460 File Offset: 0x0000E660
		public ControlChangeEvent(BinaryReader br)
		{
			byte b = br.ReadByte();
			this.controllerValue = br.ReadByte();
			if ((b & 128) != 0)
			{
				throw new InvalidDataException("Invalid controller");
			}
			this.controller = (MidiController)b;
			if ((this.controllerValue & 128) != 0)
			{
				throw new InvalidDataException(string.Format("Invalid controllerValue {0} for controller {1}, Pos 0x{2:X}", this.controllerValue, this.controller, br.BaseStream.Position));
			}
		}

		/// <summary>
		/// Creates a control change event
		/// </summary>
		/// <param name="absoluteTime">Time</param>
		/// <param name="channel">MIDI Channel Number</param>
		/// <param name="controller">The MIDI Controller</param>
		/// <param name="controllerValue">Controller value</param>
		// Token: 0x060004DF RID: 1247 RVA: 0x000104E5 File Offset: 0x0000E6E5
		public ControlChangeEvent(long absoluteTime, int channel, MidiController controller, int controllerValue) : base(absoluteTime, channel, MidiCommandCode.ControlChange)
		{
			this.Controller = controller;
			this.ControllerValue = controllerValue;
		}

		/// <summary>
		/// Describes this control change event
		/// </summary>
		/// <returns>A string describing this event</returns>
		// Token: 0x060004E0 RID: 1248 RVA: 0x00010503 File Offset: 0x0000E703
		public override string ToString()
		{
			return string.Format("{0} Controller {1} Value {2}", base.ToString(), this.controller, this.controllerValue);
		}

		/// <summary>
		/// <see cref="M:NAudio.Midi.MidiEvent.GetAsShortMessage" />
		/// </summary>
		// Token: 0x060004E1 RID: 1249 RVA: 0x0001052C File Offset: 0x0000E72C
		public override int GetAsShortMessage()
		{
			byte b = (byte)this.controller;
			return base.GetAsShortMessage() + ((int)b << 8) + ((int)this.controllerValue << 16);
		}

		/// <summary>
		/// Calls base class export first, then exports the data 
		/// specific to this event
		/// <seealso cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)">MidiEvent.Export</seealso>
		/// </summary>
		// Token: 0x060004E2 RID: 1250 RVA: 0x00010554 File Offset: 0x0000E754
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write((byte)this.controller);
			writer.Write(this.controllerValue);
		}

		/// <summary>
		/// The controller number
		/// </summary>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00010576 File Offset: 0x0000E776
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0001057E File Offset: 0x0000E77E
		public MidiController Controller
		{
			get
			{
				return this.controller;
			}
			set
			{
				if (value < MidiController.BankSelect || value > (MidiController)127)
				{
					throw new ArgumentOutOfRangeException("value", "Controller number must be in the range 0-127");
				}
				this.controller = value;
			}
		}

		/// <summary>
		/// The controller value
		/// </summary>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000105A0 File Offset: 0x0000E7A0
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x000105A8 File Offset: 0x0000E7A8
		public int ControllerValue
		{
			get
			{
				return (int)this.controllerValue;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value", "Controller Value must be in the range 0-127");
				}
				this.controllerValue = (byte)value;
			}
		}

		// Token: 0x0400056A RID: 1386
		private MidiController controller;

		// Token: 0x0400056B RID: 1387
		private byte controllerValue;
	}
}
