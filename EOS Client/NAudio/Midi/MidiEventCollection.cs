using System;
using System.Collections;
using System.Collections.Generic;
using NAudio.Utils;

namespace NAudio.Midi
{
    public class MidiEventCollection : IEnumerable<IList<MidiEvent>>, IEnumerable
    {
        public MidiEventCollection(int midiFileType, int deltaTicksPerQuarterNote)
        {
            this.midiFileType = midiFileType;
            this.deltaTicksPerQuarterNote = deltaTicksPerQuarterNote;
            this.startAbsoluteTime = 0L;
            this.trackEvents = new List<IList<MidiEvent>>();
        }

        public int Tracks
        {
            get
            {
                return this.trackEvents.Count;
            }
        }

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

        public int DeltaTicksPerQuarterNote
        {
            get
            {
                return this.deltaTicksPerQuarterNote;
            }
        }

        public IList<MidiEvent> GetTrackEvents(int trackNumber)
        {
            return this.trackEvents[trackNumber];
        }

        public IList<MidiEvent> this[int trackNumber]
        {
            get
            {
                return this.trackEvents[trackNumber];
            }
        }

        public IList<MidiEvent> AddTrack()
        {
            return this.AddTrack(null);
        }

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

        public void RemoveTrack(int track)
        {
            this.trackEvents.RemoveAt(track);
        }

        public void Clear()
        {
            this.trackEvents.Clear();
        }

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

        private void EnsureTracks(int count)
        {
            for (int i = this.trackEvents.Count; i < count; i++)
            {
                this.trackEvents.Add(new List<MidiEvent>());
            }
        }

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

        public IEnumerator<IList<MidiEvent>> GetEnumerator()
        {
            return this.trackEvents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.trackEvents.GetEnumerator();
        }

        private int midiFileType;

        private List<IList<MidiEvent>> trackEvents;

        private int deltaTicksPerQuarterNote;

        private long startAbsoluteTime;
    }
}
