using System;
using System.Text;

namespace NAudio.Utils
{
	/// <summary>
	/// Chunk Identifier helpers
	/// </summary>
	// Token: 0x02000044 RID: 68
	public class ChunkIdentifier
	{
		/// <summary>
		/// Chunk identifier to Int32 (replaces mmioStringToFOURCC)
		/// </summary>
		/// <param name="s">four character chunk identifier</param>
		/// <returns>Chunk identifier as int 32</returns>
		// Token: 0x06000124 RID: 292 RVA: 0x00006D60 File Offset: 0x00004F60
		public static int ChunkIdentifierToInt32(string s)
		{
			if (s.Length != 4)
			{
				throw new ArgumentException("Must be a four character string");
			}
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			if (bytes.Length != 4)
			{
				throw new ArgumentException("Must encode to exactly four bytes");
			}
			return BitConverter.ToInt32(bytes, 0);
		}
	}
}
