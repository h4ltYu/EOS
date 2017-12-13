using System;

namespace NAudio.Wave.SampleProviders
{
	/// <summary>
	/// Sample Provider to allow fading in and out
	/// </summary>
	// Token: 0x02000071 RID: 113
	public class FadeInOutSampleProvider : ISampleProvider
	{
		/// <summary>
		/// Creates a new FadeInOutSampleProvider
		/// </summary>
		/// <param name="source">The source stream with the audio to be faded in or out</param>
		/// <param name="initiallySilent">If true, we start faded out</param>
		// Token: 0x0600025E RID: 606 RVA: 0x00007B7A File Offset: 0x00005D7A
		public FadeInOutSampleProvider(ISampleProvider source, bool initiallySilent = false)
		{
			this.source = source;
			this.fadeState = (initiallySilent ? FadeInOutSampleProvider.FadeState.Silence : FadeInOutSampleProvider.FadeState.FullVolume);
		}

		/// <summary>
		/// Requests that a fade-in begins (will start on the next call to Read)
		/// </summary>
		/// <param name="fadeDurationInMilliseconds">Duration of fade in milliseconds</param>
		// Token: 0x0600025F RID: 607 RVA: 0x00007BA4 File Offset: 0x00005DA4
		public void BeginFadeIn(double fadeDurationInMilliseconds)
		{
			lock (this.lockObject)
			{
				this.fadeSamplePosition = 0;
				this.fadeSampleCount = (int)(fadeDurationInMilliseconds * (double)this.source.WaveFormat.SampleRate / 1000.0);
				this.fadeState = FadeInOutSampleProvider.FadeState.FadingIn;
			}
		}

		/// <summary>
		/// Requests that a fade-out begins (will start on the next call to Read)
		/// </summary>
		/// <param name="fadeDurationInMilliseconds">Duration of fade in milliseconds</param>
		// Token: 0x06000260 RID: 608 RVA: 0x00007C0C File Offset: 0x00005E0C
		public void BeginFadeOut(double fadeDurationInMilliseconds)
		{
			lock (this.lockObject)
			{
				this.fadeSamplePosition = 0;
				this.fadeSampleCount = (int)(fadeDurationInMilliseconds * (double)this.source.WaveFormat.SampleRate / 1000.0);
				this.fadeState = FadeInOutSampleProvider.FadeState.FadingOut;
			}
		}

		/// <summary>
		/// Reads samples from this sample provider
		/// </summary>
		/// <param name="buffer">Buffer to read into</param>
		/// <param name="offset">Offset within buffer to write to</param>
		/// <param name="count">Number of samples desired</param>
		/// <returns>Number of samples read</returns>
		// Token: 0x06000261 RID: 609 RVA: 0x00007C74 File Offset: 0x00005E74
		public int Read(float[] buffer, int offset, int count)
		{
			int num = this.source.Read(buffer, offset, count);
			lock (this.lockObject)
			{
				if (this.fadeState == FadeInOutSampleProvider.FadeState.FadingIn)
				{
					this.FadeIn(buffer, offset, num);
				}
				else if (this.fadeState == FadeInOutSampleProvider.FadeState.FadingOut)
				{
					this.FadeOut(buffer, offset, num);
				}
				else if (this.fadeState == FadeInOutSampleProvider.FadeState.Silence)
				{
					FadeInOutSampleProvider.ClearBuffer(buffer, offset, count);
				}
			}
			return num;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00007CF0 File Offset: 0x00005EF0
		private static void ClearBuffer(float[] buffer, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				buffer[i + offset] = 0f;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00007D14 File Offset: 0x00005F14
		private void FadeOut(float[] buffer, int offset, int sourceSamplesRead)
		{
			int i = 0;
			while (i < sourceSamplesRead)
			{
				float num = 1f - (float)this.fadeSamplePosition / (float)this.fadeSampleCount;
				for (int j = 0; j < this.source.WaveFormat.Channels; j++)
				{
					buffer[offset + i++] *= num;
				}
				this.fadeSamplePosition++;
				if (this.fadeSamplePosition > this.fadeSampleCount)
				{
					this.fadeState = FadeInOutSampleProvider.FadeState.Silence;
					FadeInOutSampleProvider.ClearBuffer(buffer, i + offset, sourceSamplesRead - i);
					return;
				}
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007DA4 File Offset: 0x00005FA4
		private void FadeIn(float[] buffer, int offset, int sourceSamplesRead)
		{
			int i = 0;
			while (i < sourceSamplesRead)
			{
				float num = (float)this.fadeSamplePosition / (float)this.fadeSampleCount;
				for (int j = 0; j < this.source.WaveFormat.Channels; j++)
				{
					buffer[offset + i++] *= num;
				}
				this.fadeSamplePosition++;
				if (this.fadeSamplePosition > this.fadeSampleCount)
				{
					this.fadeState = FadeInOutSampleProvider.FadeState.FullVolume;
					return;
				}
			}
		}

		/// <summary>
		/// WaveFormat of this SampleProvider
		/// </summary>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00007E22 File Offset: 0x00006022
		public WaveFormat WaveFormat
		{
			get
			{
				return this.source.WaveFormat;
			}
		}

		// Token: 0x04000377 RID: 887
		private readonly object lockObject = new object();

		// Token: 0x04000378 RID: 888
		private readonly ISampleProvider source;

		// Token: 0x04000379 RID: 889
		private int fadeSamplePosition;

		// Token: 0x0400037A RID: 890
		private int fadeSampleCount;

		// Token: 0x0400037B RID: 891
		private FadeInOutSampleProvider.FadeState fadeState;

		// Token: 0x02000072 RID: 114
		private enum FadeState
		{
			// Token: 0x0400037D RID: 893
			Silence,
			// Token: 0x0400037E RID: 894
			FadingIn,
			// Token: 0x0400037F RID: 895
			FullVolume,
			// Token: 0x04000380 RID: 896
			FadingOut
		}
	}
}
