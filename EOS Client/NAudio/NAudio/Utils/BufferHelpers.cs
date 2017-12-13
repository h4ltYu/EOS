using System;

namespace NAudio.Utils
{
	/// <summary>
	/// Helper methods for working with audio buffers
	/// </summary>
	// Token: 0x02000114 RID: 276
	public static class BufferHelpers
	{
		/// <summary>
		/// Ensures the buffer is big enough
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="bytesRequired"></param>
		/// <returns></returns>
		// Token: 0x0600061E RID: 1566 RVA: 0x00014002 File Offset: 0x00012202
		public static byte[] Ensure(byte[] buffer, int bytesRequired)
		{
			if (buffer == null || buffer.Length < bytesRequired)
			{
				buffer = new byte[bytesRequired];
			}
			return buffer;
		}

		/// <summary>
		/// Ensures the buffer is big enough
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="samplesRequired"></param>
		/// <returns></returns>
		// Token: 0x0600061F RID: 1567 RVA: 0x00014016 File Offset: 0x00012216
		public static float[] Ensure(float[] buffer, int samplesRequired)
		{
			if (buffer == null || buffer.Length < samplesRequired)
			{
				buffer = new float[samplesRequired];
			}
			return buffer;
		}
	}
}
