using System;
using System.IO;
using System.Text;
using NAudio.Utils;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI text event
	/// </summary>
	// Token: 0x020000F6 RID: 246
	public class TextEvent : MetaEvent
	{
		/// <summary>
		/// Reads a new text event from a MIDI stream
		/// </summary>
		/// <param name="br">The MIDI stream</param>
		/// <param name="length">The data length</param>
		// Token: 0x060005B8 RID: 1464 RVA: 0x00012988 File Offset: 0x00010B88
		public TextEvent(BinaryReader br, int length)
		{
			Encoding instance = ByteEncoding.Instance;
			this.text = instance.GetString(br.ReadBytes(length));
		}

		/// <summary>
		/// Creates a new TextEvent
		/// </summary>
		/// <param name="text">The text in this type</param>
		/// <param name="metaEventType">MetaEvent type (must be one that is
		/// associated with text data)</param>
		/// <param name="absoluteTime">Absolute time of this event</param>
		// Token: 0x060005B9 RID: 1465 RVA: 0x000129B4 File Offset: 0x00010BB4
		public TextEvent(string text, MetaEventType metaEventType, long absoluteTime) : base(metaEventType, text.Length, absoluteTime)
		{
			this.text = text;
		}

		/// <summary>
		/// The contents of this text event
		/// </summary>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x000129CB File Offset: 0x00010BCB
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x000129D3 File Offset: 0x00010BD3
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
				this.metaDataLength = this.text.Length;
			}
		}

		/// <summary>
		/// Describes this MIDI text event
		/// </summary>
		/// <returns>A string describing this event</returns>
		// Token: 0x060005BC RID: 1468 RVA: 0x000129ED File Offset: 0x00010BED
		public override string ToString()
		{
			return string.Format("{0} {1}", base.ToString(), this.text);
		}

		/// <summary>
		/// Calls base class export first, then exports the data 
		/// specific to this event
		/// <seealso cref="M:NAudio.Midi.MidiEvent.Export(System.Int64@,System.IO.BinaryWriter)">MidiEvent.Export</seealso>
		/// </summary>
		// Token: 0x060005BD RID: 1469 RVA: 0x00012A08 File Offset: 0x00010C08
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			Encoding instance = ByteEncoding.Instance;
			byte[] bytes = instance.GetBytes(this.text);
			writer.Write(bytes);
		}

		// Token: 0x04000600 RID: 1536
		private string text;
	}
}
