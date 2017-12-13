using System;
using System.Globalization;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Manages the AudioStreamVolume for the <see cref="T:NAudio.CoreAudioApi.AudioClient" />.
	/// </summary>
	// Token: 0x02000023 RID: 35
	public class AudioStreamVolume : IDisposable
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00004ACA File Offset: 0x00002CCA
		internal AudioStreamVolume(IAudioStreamVolume audioStreamVolumeInterface)
		{
			this.audioStreamVolumeInterface = audioStreamVolumeInterface;
		}

		/// <summary>
		/// Verify that the channel index is valid.
		/// </summary>
		/// <param name="channelIndex"></param>
		/// <param name="parameter"></param>
		// Token: 0x06000097 RID: 151 RVA: 0x00004ADC File Offset: 0x00002CDC
		private void CheckChannelIndex(int channelIndex, string parameter)
		{
			int channelCount = this.ChannelCount;
			if (channelIndex >= channelCount)
			{
				throw new ArgumentOutOfRangeException(parameter, "You must supply a valid channel index < current count of channels: " + channelCount.ToString());
			}
		}

		/// <summary>
		/// Return the current stream volumes for all channels
		/// </summary>
		/// <returns>An array of volume levels between 0.0 and 1.0 for each channel in the audio stream.</returns>
		// Token: 0x06000098 RID: 152 RVA: 0x00004B0C File Offset: 0x00002D0C
		public float[] GetAllVolumes()
		{
			uint num;
			Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.GetChannelCount(out num));
			float[] array = new float[num];
			Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.GetAllVolumes(num, array));
			return array;
		}

		/// <summary>
		/// Returns the current number of channels in this audio stream.
		/// </summary>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004B48 File Offset: 0x00002D48
		public int ChannelCount
		{
			get
			{
				uint result;
				Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.GetChannelCount(out result));
				return (int)result;
			}
		}

		/// <summary>
		/// Return the current volume for the requested channel.
		/// </summary>
		/// <param name="channelIndex">The 0 based index into the channels.</param>
		/// <returns>The volume level for the channel between 0.0 and 1.0.</returns>
		// Token: 0x0600009A RID: 154 RVA: 0x00004B68 File Offset: 0x00002D68
		public float GetChannelVolume(int channelIndex)
		{
			this.CheckChannelIndex(channelIndex, "channelIndex");
			float result;
			Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.GetChannelVolume((uint)channelIndex, out result));
			return result;
		}

		/// <summary>
		/// Set the volume level for each channel of the audio stream.
		/// </summary>
		/// <param name="levels">An array of volume levels (between 0.0 and 1.0) one for each channel.</param>
		/// <remarks>
		/// A volume level MUST be supplied for reach channel in the audio stream.
		/// </remarks>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// Thrown when <paramref name="levels" /> does not contain <see cref="P:NAudio.CoreAudioApi.AudioStreamVolume.ChannelCount" /> elements.
		/// </exception>
		// Token: 0x0600009B RID: 155 RVA: 0x00004B98 File Offset: 0x00002D98
		public void SetAllVolumes(float[] levels)
		{
			int channelCount = this.ChannelCount;
			if (levels == null)
			{
				throw new ArgumentNullException("levels");
			}
			if (levels.Length != channelCount)
			{
				throw new ArgumentOutOfRangeException("levels", string.Format(CultureInfo.InvariantCulture, "SetAllVolumes MUST be supplied with a volume level for ALL channels. The AudioStream has {0} channels and you supplied {1} channels.", new object[]
				{
					channelCount,
					levels.Length
				}));
			}
			for (int i = 0; i < levels.Length; i++)
			{
				float num = levels[i];
				if (num < 0f)
				{
					throw new ArgumentOutOfRangeException("levels", "All volumes must be between 0.0 and 1.0. Invalid volume at index: " + i.ToString());
				}
				if (num > 1f)
				{
					throw new ArgumentOutOfRangeException("levels", "All volumes must be between 0.0 and 1.0. Invalid volume at index: " + i.ToString());
				}
			}
			Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.SetAllVoumes((uint)channelCount, levels));
		}

		/// <summary>
		/// Sets the volume level for one channel in the audio stream.
		/// </summary>
		/// <param name="index">The 0-based index into the channels to adjust the volume of.</param>
		/// <param name="level">The volume level between 0.0 and 1.0 for this channel of the audio stream.</param>
		// Token: 0x0600009C RID: 156 RVA: 0x00004C64 File Offset: 0x00002E64
		public void SetChannelVolume(int index, float level)
		{
			this.CheckChannelIndex(index, "index");
			if (level < 0f)
			{
				throw new ArgumentOutOfRangeException("level", "Volume must be between 0.0 and 1.0");
			}
			if (level > 1f)
			{
				throw new ArgumentOutOfRangeException("level", "Volume must be between 0.0 and 1.0");
			}
			Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.SetChannelVolume((uint)index, level));
		}

		/// <summary>
		/// Dispose
		/// </summary>
		// Token: 0x0600009D RID: 157 RVA: 0x00004CBF File Offset: 0x00002EBF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Release/cleanup objects during Dispose/finalization.
		/// </summary>
		/// <param name="disposing">True if disposing and false if being finalized.</param>
		// Token: 0x0600009E RID: 158 RVA: 0x00004CCE File Offset: 0x00002ECE
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.audioStreamVolumeInterface != null)
			{
				Marshal.ReleaseComObject(this.audioStreamVolumeInterface);
				this.audioStreamVolumeInterface = null;
			}
		}

		// Token: 0x04000074 RID: 116
		private IAudioStreamVolume audioStreamVolumeInterface;
	}
}
