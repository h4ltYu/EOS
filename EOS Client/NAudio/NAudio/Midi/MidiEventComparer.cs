using System;
using System.Collections.Generic;

namespace NAudio.Midi
{
	/// <summary>
	/// Utility class for comparing MidiEvent objects
	/// </summary>
	// Token: 0x020000DC RID: 220
	public class MidiEventComparer : IComparer<MidiEvent>
	{
		/// <summary>
		/// Compares two MidiEvents
		/// Sorts by time, with EndTrack always sorted to the end
		/// </summary>
		// Token: 0x06000507 RID: 1287 RVA: 0x00010CCC File Offset: 0x0000EECC
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
