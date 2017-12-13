using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAudio.Utils;

namespace NAudio.Midi
{
	/// <summary>
	/// Class able to read a MIDI file
	/// </summary>
	// Token: 0x020000DD RID: 221
	public class MidiFile
	{
		/// <summary>
		/// Opens a MIDI file for reading
		/// </summary>
		/// <param name="filename">Name of MIDI file</param>
		// Token: 0x06000509 RID: 1289 RVA: 0x00010D4F File Offset: 0x0000EF4F
		public MidiFile(string filename) : this(filename, true)
		{
		}

		/// <summary>
		/// MIDI File format
		/// </summary>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00010D59 File Offset: 0x0000EF59
		public int FileFormat
		{
			get
			{
				return (int)this.fileFormat;
			}
		}

		/// <summary>
		/// Opens a MIDI file for reading
		/// </summary>
		/// <param name="filename">Name of MIDI file</param>
		/// <param name="strictChecking">If true will error on non-paired note events</param>
		// Token: 0x0600050B RID: 1291 RVA: 0x00010D64 File Offset: 0x0000EF64
		public MidiFile(string filename, bool strictChecking)
		{
			this.strictChecking = strictChecking;
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(filename));
			using (binaryReader)
			{
				string @string = Encoding.UTF8.GetString(binaryReader.ReadBytes(4));
				if (@string != "MThd")
				{
					throw new FormatException("Not a MIDI file - header chunk missing");
				}
				uint num = MidiFile.SwapUInt32(binaryReader.ReadUInt32());
				if (num != 6u)
				{
					throw new FormatException("Unexpected header chunk length");
				}
				this.fileFormat = MidiFile.SwapUInt16(binaryReader.ReadUInt16());
				int num2 = (int)MidiFile.SwapUInt16(binaryReader.ReadUInt16());
				this.deltaTicksPerQuarterNote = MidiFile.SwapUInt16(binaryReader.ReadUInt16());
				this.events = new MidiEventCollection((this.fileFormat == 0) ? 0 : 1, (int)this.deltaTicksPerQuarterNote);
				for (int i = 0; i < num2; i++)
				{
					this.events.AddTrack();
				}
				long num3 = 0L;
				for (int j = 0; j < num2; j++)
				{
					if (this.fileFormat == 1)
					{
						num3 = 0L;
					}
					@string = Encoding.UTF8.GetString(binaryReader.ReadBytes(4));
					if (@string != "MTrk")
					{
						throw new FormatException("Invalid chunk header");
					}
					num = MidiFile.SwapUInt32(binaryReader.ReadUInt32());
					long position = binaryReader.BaseStream.Position;
					MidiEvent midiEvent = null;
					List<NoteOnEvent> list = new List<NoteOnEvent>();
					while (binaryReader.BaseStream.Position < position + (long)((ulong)num))
					{
						midiEvent = MidiEvent.ReadNextEvent(binaryReader, midiEvent);
						num3 += (long)midiEvent.DeltaTime;
						midiEvent.AbsoluteTime = num3;
						this.events[j].Add(midiEvent);
						if (midiEvent.CommandCode == MidiCommandCode.NoteOn)
						{
							NoteEvent noteEvent = (NoteEvent)midiEvent;
							if (noteEvent.Velocity > 0)
							{
								list.Add((NoteOnEvent)noteEvent);
							}
							else
							{
								this.FindNoteOn(noteEvent, list);
							}
						}
						else if (midiEvent.CommandCode == MidiCommandCode.NoteOff)
						{
							this.FindNoteOn((NoteEvent)midiEvent, list);
						}
						else if (midiEvent.CommandCode == MidiCommandCode.MetaEvent)
						{
							MetaEvent metaEvent = (MetaEvent)midiEvent;
							if (metaEvent.MetaEventType == MetaEventType.EndTrack && strictChecking && binaryReader.BaseStream.Position < position + (long)((ulong)num))
							{
								throw new FormatException(string.Format("End Track event was not the last MIDI event on track {0}", j));
							}
						}
					}
					if (list.Count > 0 && strictChecking)
					{
						throw new FormatException(string.Format("Note ons without note offs {0} (file format {1})", list.Count, this.fileFormat));
					}
					if (binaryReader.BaseStream.Position != position + (long)((ulong)num))
					{
						throw new FormatException(string.Format("Read too far {0}+{1}!={2}", num, position, binaryReader.BaseStream.Position));
					}
				}
			}
		}

		/// <summary>
		/// The collection of events in this MIDI file
		/// </summary>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00011044 File Offset: 0x0000F244
		public MidiEventCollection Events
		{
			get
			{
				return this.events;
			}
		}

		/// <summary>
		/// Number of tracks in this MIDI file
		/// </summary>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0001104C File Offset: 0x0000F24C
		public int Tracks
		{
			get
			{
				return this.events.Tracks;
			}
		}

		/// <summary>
		/// Delta Ticks Per Quarter Note
		/// </summary>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00011059 File Offset: 0x0000F259
		public int DeltaTicksPerQuarterNote
		{
			get
			{
				return (int)this.deltaTicksPerQuarterNote;
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00011064 File Offset: 0x0000F264
		private void FindNoteOn(NoteEvent offEvent, List<NoteOnEvent> outstandingNoteOns)
		{
			bool flag = false;
			foreach (NoteOnEvent noteOnEvent in outstandingNoteOns)
			{
				if (noteOnEvent.Channel == offEvent.Channel && noteOnEvent.NoteNumber == offEvent.NoteNumber)
				{
					noteOnEvent.OffEvent = offEvent;
					outstandingNoteOns.Remove(noteOnEvent);
					flag = true;
					break;
				}
			}
			if (!flag && this.strictChecking)
			{
				throw new FormatException(string.Format("Got an off without an on {0}", offEvent));
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000110F8 File Offset: 0x0000F2F8
		private static uint SwapUInt32(uint i)
		{
			return (i & 4278190080u) >> 24 | (i & 16711680u) >> 8 | (i & 65280u) << 8 | (i & 255u) << 24;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00011123 File Offset: 0x0000F323
		private static ushort SwapUInt16(ushort i)
		{
			return (ushort)((i & 65280) >> 8 | (int)(i & 255) << 8);
		}

		/// <summary>
		/// Describes the MIDI file
		/// </summary>
		/// <returns>A string describing the MIDI file and its events</returns>
		// Token: 0x06000512 RID: 1298 RVA: 0x0001113C File Offset: 0x0000F33C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Format {0}, Tracks {1}, Delta Ticks Per Quarter Note {2}\r\n", this.fileFormat, this.Tracks, this.deltaTicksPerQuarterNote);
			for (int i = 0; i < this.Tracks; i++)
			{
				foreach (MidiEvent arg in this.events[i])
				{
					stringBuilder.AppendFormat("{0}\r\n", arg);
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Exports a MIDI file
		/// </summary>
		/// <param name="filename">Filename to export to</param>
		/// <param name="events">Events to export</param>
		// Token: 0x06000513 RID: 1299 RVA: 0x000111E0 File Offset: 0x0000F3E0
		public static void Export(string filename, MidiEventCollection events)
		{
			if (events.MidiFileType == 0 && events.Tracks > 1)
			{
				throw new ArgumentException("Can't export more than one track to a type 0 file");
			}
			using (BinaryWriter binaryWriter = new BinaryWriter(File.Create(filename)))
			{
				binaryWriter.Write(Encoding.UTF8.GetBytes("MThd"));
				binaryWriter.Write(MidiFile.SwapUInt32(6u));
				binaryWriter.Write(MidiFile.SwapUInt16((ushort)events.MidiFileType));
				binaryWriter.Write(MidiFile.SwapUInt16((ushort)events.Tracks));
				binaryWriter.Write(MidiFile.SwapUInt16((ushort)events.DeltaTicksPerQuarterNote));
				for (int i = 0; i < events.Tracks; i++)
				{
					IList<MidiEvent> list = events[i];
					binaryWriter.Write(Encoding.UTF8.GetBytes("MTrk"));
					long position = binaryWriter.BaseStream.Position;
					binaryWriter.Write(MidiFile.SwapUInt32(0u));
					long startAbsoluteTime = events.StartAbsoluteTime;
					MergeSort.Sort<MidiEvent>(list, new MidiEventComparer());
					int count = list.Count;
					foreach (MidiEvent midiEvent in list)
					{
						midiEvent.Export(ref startAbsoluteTime, binaryWriter);
					}
					uint num = (uint)(binaryWriter.BaseStream.Position - position) - 4u;
					binaryWriter.BaseStream.Position = position;
					binaryWriter.Write(MidiFile.SwapUInt32(num));
					binaryWriter.BaseStream.Position += (long)((ulong)num);
				}
			}
		}

		// Token: 0x040005A8 RID: 1448
		private MidiEventCollection events;

		// Token: 0x040005A9 RID: 1449
		private ushort fileFormat;

		// Token: 0x040005AA RID: 1450
		private ushort deltaTicksPerQuarterNote;

		// Token: 0x040005AB RID: 1451
		private bool strictChecking;
	}
}
