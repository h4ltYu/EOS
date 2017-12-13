using System;
using System.Collections;
using System.Collections.Generic;
using NAudio.Utils;

namespace NAudio.Midi
{
	/// <summary>
	/// A helper class to manage collection of MIDI events
	/// It has the ability to organise them in tracks
	/// </summary>
	// Token: 0x020000DB RID: 219
	public class MidiEventCollection : IEnumerable<IList<MidiEvent>>, IEnumerable
	{
		/// <summary>
		/// Creates a new Midi Event collection
		/// </summary>
		/// <param name="midiFileType">Initial file type</param>
		/// <param name="deltaTicksPerQuarterNote">Delta Ticks Per Quarter Note</param>
		// Token: 0x060004F3 RID: 1267 RVA: 0x00010871 File Offset: 0x0000EA71
		public MidiEventCollection(int midiFileType, int deltaTicksPerQuarterNote)
		{
			this.midiFileType = midiFileType;
			this.deltaTicksPerQuarterNote = deltaTicksPerQuarterNote;
			this.startAbsoluteTime = 0L;
			this.trackEvents = new List<IList<MidiEvent>>();
		}

		/// <summary>
		/// The number of tracks
		/// </summary>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001089A File Offset: 0x0000EA9A
		public int Tracks
		{
			get
			{
				return this.trackEvents.Count;
			}
		}

		/// <summary>
		/// The absolute time that should be considered as time zero
		/// Not directly used here, but useful for timeshifting applications
		/// </summary>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000108A7 File Offset: 0x0000EAA7
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x000108AF File Offset: 0x0000EAAF
		public long StartAbsoluteTime
		{
			get
			{
				return this.startAbsoluteTime;
			}
			set
			{
				this.startAbsoluteTime = value;
			}
		}

		/// <summary>
		/// The number of ticks per quarter note
		/// </summary>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000108B8 File Offset: 0x0000EAB8
		public int DeltaTicksPerQuarterNote
		{
			get
			{
				return this.deltaTicksPerQuarterNote;
			}
		}

		/// <summary>
		/// Gets events on a specified track
		/// </summary>
		/// <param name="trackNumber">Track number</param>
		/// <returns>The list of events</returns>
		// Token: 0x060004F8 RID: 1272 RVA: 0x000108C0 File Offset: 0x0000EAC0
		public IList<MidiEvent> GetTrackEvents(int trackNumber)
		{
			return this.trackEvents[trackNumber];
		}

		/// <summary>
		/// Gets events on a specific track
		/// </summary>
		/// <param name="trackNumber">Track number</param>
		/// <returns>The list of events</returns>
		// Token: 0x170000FD RID: 253
		public IList<MidiEvent> this[int trackNumber]
		{
			get
			{
				return this.trackEvents[trackNumber];
			}
		}

		/// <summary>
		/// Adds a new track
		/// </summary>
		/// <returns>The new track event list</returns>
		// Token: 0x060004FA RID: 1274 RVA: 0x000108DC File Offset: 0x0000EADC
		public IList<MidiEvent> AddTrack()
		{
			return this.AddTrack(null);
		}

		/// <summary>
		/// Adds a new track
		/// </summary>
		/// <param name="initialEvents">Initial events to add to the new track</param>
		/// <returns>The new track event list</returns>
		// Token: 0x060004FB RID: 1275 RVA: 0x000108E8 File Offset: 0x0000EAE8
		public IList<MidiEvent> AddTrack(IList<MidiEvent> initialEvents)
		{
			List<MidiEvent> list = new List<MidiEvent>();
			if (initialEvents != null)
			{
				list.AddRange(initialEvents);
			}
			this.trackEvents.Add(list);
			return list;
		}

		/// <summary>
		/// Removes a track
		/// </summary>
		/// <param name="track">Track number to remove</param>
		// Token: 0x060004FC RID: 1276 RVA: 0x00010912 File Offset: 0x0000EB12
		public void RemoveTrack(int track)
		{
			this.trackEvents.RemoveAt(track);
		}

		/// <summary>
		/// Clears all events
		/// </summary>
		// Token: 0x060004FD RID: 1277 RVA: 0x00010920 File Offset: 0x0000EB20
		public void Clear()
		{
			this.trackEvents.Clear();
		}

		/// <summary>
		/// The MIDI file type
		/// </summary>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001092D File Offset: 0x0000EB2D
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00010935 File Offset: 0x0000EB35
		public int MidiFileType
		{
			get
			{
				return this.midiFileType;
			}
			set
			{
				if (this.midiFileType != value)
				{
					this.midiFileType = value;
					if (value == 0)
					{
						this.FlattenToOneTrack();
						return;
					}
					this.ExplodeToManyTracks();
				}
			}
		}

		/// <summary>
		/// Adds an event to the appropriate track depending on file type
		/// </summary>
		/// <param name="midiEvent">The event to be added</param>
		/// <param name="originalTrack">The original (or desired) track number</param>
		/// <remarks>When adding events in type 0 mode, the originalTrack parameter
		/// is ignored. If in type 1 mode, it will use the original track number to
		/// store the new events. If the original track was 0 and this is a channel based
		/// event, it will create new tracks if necessary and put it on the track corresponding
		/// to its channel number</remarks>
		// Token: 0x06000500 RID: 1280 RVA: 0x00010958 File Offset: 0x0000EB58
		public void AddEvent(MidiEvent midiEvent, int originalTrack)
		{
			if (this.midiFileType == 0)
			{
				this.EnsureTracks(1);
				this.trackEvents[0].Add(midiEvent);
				return;
			}
			if (originalTrack == 0)
			{
				MidiCommandCode commandCode = midiEvent.CommandCode;
				if (commandCode <= MidiCommandCode.KeyAfterTouch)
				{
					if (commandCode != MidiCommandCode.NoteOff && commandCode != MidiCommandCode.NoteOn && commandCode != MidiCommandCode.KeyAfterTouch)
					{
						goto IL_A1;
					}
				}
				else if (commandCode <= MidiCommandCode.PatchChange)
				{
					if (commandCode != MidiCommandCode.ControlChange && commandCode != MidiCommandCode.PatchChange)
					{
						goto IL_A1;
					}
				}
				else if (commandCode != MidiCommandCode.ChannelAfterTouch && commandCode != MidiCommandCode.PitchWheelChange)
				{
					goto IL_A1;
				}
				this.EnsureTracks(midiEvent.Channel + 1);
				this.trackEvents[midiEvent.Channel].Add(midiEvent);
				return;
				IL_A1:
				this.EnsureTracks(1);
				this.trackEvents[0].Add(midiEvent);
				return;
			}
			this.EnsureTracks(originalTrack + 1);
			this.trackEvents[originalTrack].Add(midiEvent);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00010A3C File Offset: 0x0000EC3C
		private void EnsureTracks(int count)
		{
			for (int i = this.trackEvents.Count; i < count; i++)
			{
				this.trackEvents.Add(new List<MidiEvent>());
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00010A70 File Offset: 0x0000EC70
		private void ExplodeToManyTracks()
		{
			IList<MidiEvent> list = this.trackEvents[0];
			this.Clear();
			foreach (MidiEvent midiEvent in list)
			{
				this.AddEvent(midiEvent, 0);
			}
			this.PrepareForExport();
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00010AD4 File Offset: 0x0000ECD4
		private void FlattenToOneTrack()
		{
			bool flag = false;
			for (int i = 1; i < this.trackEvents.Count; i++)
			{
				foreach (MidiEvent midiEvent in this.trackEvents[i])
				{
					if (!MidiEvent.IsEndTrack(midiEvent))
					{
						this.trackEvents[0].Add(midiEvent);
						flag = true;
					}
				}
			}
			for (int j = this.trackEvents.Count - 1; j > 0; j--)
			{
				this.RemoveTrack(j);
			}
			if (flag)
			{
				this.PrepareForExport();
			}
		}

		/// <summary>
		/// Sorts, removes empty tracks and adds end track markers
		/// </summary>
		// Token: 0x06000504 RID: 1284 RVA: 0x00010B84 File Offset: 0x0000ED84
		public void PrepareForExport()
		{
			MidiEventComparer comparer = new MidiEventComparer();
			foreach (IList<MidiEvent> list in this.trackEvents)
			{
				List<MidiEvent> list2 = (List<MidiEvent>)list;
				MergeSort.Sort<MidiEvent>(list2, comparer);
				int i = 0;
				while (i < list2.Count - 1)
				{
					if (MidiEvent.IsEndTrack(list2[i]))
					{
						list2.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
			int j = 0;
			while (j < this.trackEvents.Count)
			{
				IList<MidiEvent> list3 = this.trackEvents[j];
				if (list3.Count == 0)
				{
					this.RemoveTrack(j);
				}
				else if (list3.Count == 1 && MidiEvent.IsEndTrack(list3[0]))
				{
					this.RemoveTrack(j);
				}
				else
				{
					if (!MidiEvent.IsEndTrack(list3[list3.Count - 1]))
					{
						list3.Add(new MetaEvent(MetaEventType.EndTrack, 0, list3[list3.Count - 1].AbsoluteTime));
					}
					j++;
				}
			}
		}

		/// <summary>
		/// Gets an enumerator for the lists of track events
		/// </summary>
		// Token: 0x06000505 RID: 1285 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		public IEnumerator<IList<MidiEvent>> GetEnumerator()
		{
			return this.trackEvents.GetEnumerator();
		}

		/// <summary>
		/// Gets an enumerator for the lists of track events
		/// </summary>
		// Token: 0x06000506 RID: 1286 RVA: 0x00010CBA File Offset: 0x0000EEBA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.trackEvents.GetEnumerator();
		}

		// Token: 0x040005A4 RID: 1444
		private int midiFileType;

		// Token: 0x040005A5 RID: 1445
		private List<IList<MidiEvent>> trackEvents;

		// Token: 0x040005A6 RID: 1446
		private int deltaTicksPerQuarterNote;

		// Token: 0x040005A7 RID: 1447
		private long startAbsoluteTime;
	}
}
