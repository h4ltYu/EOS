using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAudio.Utils;

namespace NAudio.Wave
{
	/// <summary>
	/// Holds a list of cues
	/// </summary>
	/// <remarks>
	/// The specs for reading and writing cues from the cue and list RIFF chunks 
	/// are from http://www.sonicspot.com/guide/wavefiles.html and http://www.wotsit.org/
	/// ------------------------------
	/// The cues are stored like this:
	/// ------------------------------
	/// struct CuePoint
	/// {
	///  Int32 dwIdentifier;
	///  Int32 dwPosition;
	///  Int32 fccChunk;
	///  Int32 dwChunkStart;
	///  Int32 dwBlockStart;
	///  Int32 dwSampleOffset;
	/// } 
	///
	/// struct CueChunk
	/// {
	///  Int32 chunkID;
	///  Int32 chunkSize;
	///  Int32 dwCuePoints;
	///  CuePoint[] points;
	/// }
	/// ------------------------------
	/// Labels look like this:
	/// ------------------------------
	/// struct ListHeader 
	/// {
	///   Int32 listID;      /* 'list' */
	///   Int32 chunkSize;   /* includes the Type ID below */
	///   Int32 typeID;      /* 'adtl' */
	/// } 
	///
	/// struct LabelChunk 
	/// {
	///   Int32 chunkID;
	///   Int32 chunkSize;
	///   Int32 dwIdentifier;
	///   Char[] dwText;  /* Encoded with extended ASCII */
	/// } LabelChunk;
	/// </remarks>
	// Token: 0x020001EB RID: 491
	public class CueList
	{
		/// <summary>
		/// Creates an empty cue list
		/// </summary>
		// Token: 0x06000AE3 RID: 2787 RVA: 0x00020095 File Offset: 0x0001E295
		public CueList()
		{
		}

		/// <summary>
		/// Adds an item to the list
		/// </summary>
		/// <param name="cue">Cue</param>
		// Token: 0x06000AE4 RID: 2788 RVA: 0x000200A8 File Offset: 0x0001E2A8
		public void Add(Cue cue)
		{
			this.cues.Add(cue);
		}

		/// <summary>
		/// Gets sample positions for the embedded cues
		/// </summary>
		/// <returns>Array containing the cue positions</returns>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x000200B8 File Offset: 0x0001E2B8
		public int[] CuePositions
		{
			get
			{
				int[] array = new int[this.cues.Count];
				for (int i = 0; i < this.cues.Count; i++)
				{
					array[i] = this.cues[i].Position;
				}
				return array;
			}
		}

		/// <summary>
		/// Gets labels for the embedded cues
		/// </summary>
		/// <returns>Array containing the labels</returns>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00020104 File Offset: 0x0001E304
		public string[] CueLabels
		{
			get
			{
				string[] array = new string[this.cues.Count];
				for (int i = 0; i < this.cues.Count; i++)
				{
					array[i] = this.cues[i].Label;
				}
				return array;
			}
		}

		/// <summary>
		/// Creates a cue list from the cue RIFF chunk and the list RIFF chunk
		/// </summary>
		/// <param name="cueChunkData">The data contained in the cue chunk</param>
		/// <param name="listChunkData">The data contained in the list chunk</param>
		// Token: 0x06000AE7 RID: 2791 RVA: 0x00020150 File Offset: 0x0001E350
		internal CueList(byte[] cueChunkData, byte[] listChunkData)
		{
			int num = BitConverter.ToInt32(cueChunkData, 0);
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			int[] array = new int[num];
			int num2 = 0;
			int num3 = 4;
			while (cueChunkData.Length - num3 >= 24)
			{
				dictionary[BitConverter.ToInt32(cueChunkData, num3)] = num2;
				array[num2] = BitConverter.ToInt32(cueChunkData, num3 + 20);
				num3 += 24;
				num2++;
			}
			string[] array2 = new string[num];
			int num4 = 0;
			int num5 = ChunkIdentifier.ChunkIdentifierToInt32("labl");
			int num6 = 4;
			while (listChunkData.Length - num6 >= 16)
			{
				if (BitConverter.ToInt32(listChunkData, num6) == num5)
				{
					num4 = BitConverter.ToInt32(listChunkData, num6 + 4) - 4;
					int key = BitConverter.ToInt32(listChunkData, num6 + 8);
					num2 = dictionary[key];
					array2[num2] = Encoding.Default.GetString(listChunkData, num6 + 12, num4 - 1);
				}
				num6 += num4 + num4 % 2 + 12;
			}
			for (int i = 0; i < num; i++)
			{
				this.cues.Add(new Cue(array[i], array2[i]));
			}
		}

		/// <summary>
		/// Gets the cues as the concatenated cue and list RIFF chunks.
		/// </summary>
		/// <returns>RIFF chunks containing the cue data</returns>
		// Token: 0x06000AE8 RID: 2792 RVA: 0x00020264 File Offset: 0x0001E464
		internal byte[] GetRIFFChunks()
		{
			if (this.Count == 0)
			{
				return null;
			}
			int num = 12 + 24 * this.Count;
			int num2 = 12;
			for (int i = 0; i < this.Count; i++)
			{
				int num3 = this[i].Label.Length + 1;
				num2 += num3 + num3 % 2 + 12;
			}
			byte[] array = new byte[num + num2];
			int value = ChunkIdentifier.ChunkIdentifierToInt32("cue ");
			int value2 = ChunkIdentifier.ChunkIdentifierToInt32("data");
			int value3 = ChunkIdentifier.ChunkIdentifierToInt32("LIST");
			int value4 = ChunkIdentifier.ChunkIdentifierToInt32("adtl");
			int value5 = ChunkIdentifier.ChunkIdentifierToInt32("labl");
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(value);
					binaryWriter.Write(num - 8);
					binaryWriter.Write(this.Count);
					for (int j = 0; j < this.Count; j++)
					{
						int position = this[j].Position;
						binaryWriter.Write(j);
						binaryWriter.Write(position);
						binaryWriter.Write(value2);
						binaryWriter.Seek(8, SeekOrigin.Current);
						binaryWriter.Write(position);
					}
					binaryWriter.Write(value3);
					binaryWriter.Write(num2 - 8);
					binaryWriter.Write(value4);
					for (int k = 0; k < this.Count; k++)
					{
						binaryWriter.Write(value5);
						binaryWriter.Write(this[k].Label.Length + 1 + 4);
						binaryWriter.Write(k);
						binaryWriter.Write(Encoding.Default.GetBytes(this[k].Label.ToCharArray()));
						if (this[k].Label.Length % 2 == 0)
						{
							binaryWriter.Seek(2, SeekOrigin.Current);
						}
						else
						{
							binaryWriter.Seek(1, SeekOrigin.Current);
						}
					}
				}
			}
			return array;
		}

		/// <summary>
		/// Number of cues
		/// </summary>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00020490 File Offset: 0x0001E690
		public int Count
		{
			get
			{
				return this.cues.Count;
			}
		}

		/// <summary>
		/// Accesses the cue at the specified index
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		// Token: 0x1700024A RID: 586
		public Cue this[int index]
		{
			get
			{
				return this.cues[index];
			}
		}

		/// <summary>
		/// Checks if the cue and list chunks exist and if so, creates a cue list
		/// </summary>
		// Token: 0x06000AEB RID: 2795 RVA: 0x000204AC File Offset: 0x0001E6AC
		internal static CueList FromChunks(WaveFileReader reader)
		{
			CueList result = null;
			byte[] array = null;
			byte[] array2 = null;
			foreach (RiffChunk riffChunk in reader.ExtraChunks)
			{
				if (riffChunk.IdentifierAsString.ToLower() == "cue ")
				{
					array = reader.GetChunkData(riffChunk);
				}
				else if (riffChunk.IdentifierAsString.ToLower() == "list")
				{
					array2 = reader.GetChunkData(riffChunk);
				}
			}
			if (array != null && array2 != null)
			{
				result = new CueList(array, array2);
			}
			return result;
		}

		// Token: 0x04000B99 RID: 2969
		private readonly List<Cue> cues = new List<Cue>();
	}
}
