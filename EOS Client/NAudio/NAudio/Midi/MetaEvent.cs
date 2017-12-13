using System;
using System.IO;
using System.Text;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI meta event
	/// </summary>
	// Token: 0x020000D6 RID: 214
	public class MetaEvent : MidiEvent
	{
		/// <summary>
		/// Gets the type of this meta event
		/// </summary>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x000105CB File Offset: 0x0000E7CB
		public MetaEventType MetaEventType
		{
			get
			{
				return this.metaEvent;
			}
		}

		/// <summary>
		/// Empty constructor
		/// </summary>
		// Token: 0x060004E8 RID: 1256 RVA: 0x000105D3 File Offset: 0x0000E7D3
		protected MetaEvent()
		{
		}

		/// <summary>
		/// Custom constructor for use by derived types, who will manage the data themselves
		/// </summary>
		/// <param name="metaEventType">Meta event type</param>
		/// <param name="metaDataLength">Meta data length</param>
		/// <param name="absoluteTime">Absolute time</param>
		// Token: 0x060004E9 RID: 1257 RVA: 0x000105DB File Offset: 0x0000E7DB
		public MetaEvent(MetaEventType metaEventType, int metaDataLength, long absoluteTime) : base(absoluteTime, 1, MidiCommandCode.MetaEvent)
		{
			this.metaEvent = metaEventType;
			this.metaDataLength = metaDataLength;
		}

		/// <summary>
		/// Reads a meta-event from a stream
		/// </summary>
		/// <param name="br">A binary reader based on the stream of MIDI data</param>
		/// <returns>A new MetaEvent object</returns>
		// Token: 0x060004EA RID: 1258 RVA: 0x000105F8 File Offset: 0x0000E7F8
		public static MetaEvent ReadMetaEvent(BinaryReader br)
		{
			MetaEventType metaEventType = (MetaEventType)br.ReadByte();
			int num = MidiEvent.ReadVarInt(br);
			MetaEvent metaEvent = new MetaEvent();
			MetaEventType metaEventType2 = metaEventType;
			if (metaEventType2 <= MetaEventType.SetTempo)
			{
				switch (metaEventType2)
				{
				case MetaEventType.TrackSequenceNumber:
					metaEvent = new TrackSequenceNumberEvent(br, num);
					goto IL_E9;
				case MetaEventType.TextEvent:
				case MetaEventType.Copyright:
				case MetaEventType.SequenceTrackName:
				case MetaEventType.TrackInstrumentName:
				case MetaEventType.Lyric:
				case MetaEventType.Marker:
				case MetaEventType.CuePoint:
				case MetaEventType.ProgramName:
				case MetaEventType.DeviceName:
					metaEvent = new TextEvent(br, num);
					goto IL_E9;
				default:
					if (metaEventType2 != MetaEventType.EndTrack)
					{
						if (metaEventType2 == MetaEventType.SetTempo)
						{
							metaEvent = new TempoEvent(br, num);
							goto IL_E9;
						}
					}
					else
					{
						if (num != 0)
						{
							throw new FormatException("End track length");
						}
						goto IL_E9;
					}
					break;
				}
			}
			else
			{
				if (metaEventType2 == MetaEventType.SmpteOffset)
				{
					metaEvent = new SmpteOffsetEvent(br, num);
					goto IL_E9;
				}
				switch (metaEventType2)
				{
				case MetaEventType.TimeSignature:
					metaEvent = new TimeSignatureEvent(br, num);
					goto IL_E9;
				case MetaEventType.KeySignature:
					metaEvent = new KeySignatureEvent(br, num);
					goto IL_E9;
				default:
					if (metaEventType2 == MetaEventType.SequencerSpecific)
					{
						metaEvent = new SequencerSpecificEvent(br, num);
						goto IL_E9;
					}
					break;
				}
			}
			metaEvent.data = br.ReadBytes(num);
			if (metaEvent.data.Length != num)
			{
				throw new FormatException("Failed to read metaevent's data fully");
			}
			IL_E9:
			metaEvent.metaEvent = metaEventType;
			metaEvent.metaDataLength = num;
			return metaEvent;
		}

		/// <summary>
		/// Describes this Meta event
		/// </summary>
		/// <returns>String describing the metaevent</returns>
		// Token: 0x060004EB RID: 1259 RVA: 0x00010700 File Offset: 0x0000E900
		public override string ToString()
		{
			if (this.data == null)
			{
				return string.Format("{0} {1}", base.AbsoluteTime, this.metaEvent);
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in this.data)
			{
				stringBuilder.AppendFormat("{0:X2} ", b);
			}
			return string.Format("{0} {1}\r\n{2}", base.AbsoluteTime, this.metaEvent, stringBuilder.ToString());
		}

		/// <summary>
		/// <see cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)" />
		/// </summary>
		// Token: 0x060004EC RID: 1260 RVA: 0x0001078D File Offset: 0x0000E98D
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write((byte)this.metaEvent);
			MidiEvent.WriteVarInt(writer, this.metaDataLength);
			if (this.data != null)
			{
				writer.Write(this.data, 0, this.data.Length);
			}
		}

		// Token: 0x0400056C RID: 1388
		private MetaEventType metaEvent;

		// Token: 0x0400056D RID: 1389
		internal int metaDataLength;

		// Token: 0x0400056E RID: 1390
		private byte[] data;
	}
}
