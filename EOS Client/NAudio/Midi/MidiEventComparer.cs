using System;
using System.Collections.Generic;

namespace NAudio.Midi
{
    public class MidiEventComparer : IComparer<MidiEvent>
    {
        public int Compare(MidiEvent x, MidiEvent y)
        {
            long num = x.AbsoluteTime;
            long num2 = y.AbsoluteTime;
            if (num == num2)
            {
                MetaEvent metaEvent = x as MetaEvent;
                MetaEvent metaEvent2 = y as MetaEvent;
                if (metaEvent != null)
                {
                    if (metaEvent.MetaEventType == MetaEventType.EndTrack)
                    {
                        num = long.MaxValue;
                    }
                    else
                    {
                        num = long.MinValue;
                    }
                }
                if (metaEvent2 != null)
                {
                    if (metaEvent2.MetaEventType == MetaEventType.EndTrack)
                    {
                        num2 = long.MaxValue;
                    }
                    else
                    {
                        num2 = long.MinValue;
                    }
                }
            }
            return num.CompareTo(num2);
        }
    }
}
