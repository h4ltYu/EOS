using System;
using System.Text;

namespace NAudio.Wave
{
	/// <summary>
	/// Holds information about a RIFF file chunk
	/// </summary>
	// Token: 0x020001F9 RID: 505
	public class RiffChunk
	{
		/// <summary>
		/// Creates a RiffChunk object
		/// </summary>
		// Token: 0x06000B4F RID: 2895 RVA: 0x00021BBA File Offset: 0x0001FDBA
		public RiffChunk(int identifier, int length, long streamPosition)
		{
			this.Identifier = identifier;
			this.Length = length;
			this.StreamPosition = streamPosition;
		}

		/// <summary>
		/// The chunk identifier
		/// </summary>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00021BD7 File Offset: 0x0001FDD7
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00021BDF File Offset: 0x0001FDDF
		public int Identifier { get; private set; }

		/// <summary>
		/// The chunk identifier converted to a string
		/// </summary>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00021BE8 File Offset: 0x0001FDE8
		public string IdentifierAsString
		{
			get
			{
				return Encoding.UTF8.GetString(BitConverter.GetBytes(this.Identifier));
			}
		}

		/// <summary>
		/// The chunk length
		/// </summary>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00021BFF File Offset: 0x0001FDFF
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x00021C07 File Offset: 0x0001FE07
		public int Length { get; private set; }

		/// <summary>
		/// The stream position this chunk is located at
		/// </summary>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00021C10 File Offset: 0x0001FE10
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x00021C18 File Offset: 0x0001FE18
		public long StreamPosition { get; private set; }
	}
}
