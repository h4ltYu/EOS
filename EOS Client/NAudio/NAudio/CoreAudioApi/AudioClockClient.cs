using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Clock Client
	/// </summary>
	// Token: 0x02000011 RID: 17
	public class AudioClockClient : IDisposable
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00004145 File Offset: 0x00002345
		internal AudioClockClient(IAudioClock audioClockClientInterface)
		{
			this.audioClockClientInterface = audioClockClientInterface;
		}

		/// <summary>
		/// Characteristics
		/// </summary>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00004154 File Offset: 0x00002354
		public int Characteristics
		{
			get
			{
				uint result;
				Marshal.ThrowExceptionForHR(this.audioClockClientInterface.GetCharacteristics(out result));
				return (int)result;
			}
		}

		/// <summary>
		/// Frequency
		/// </summary>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00004174 File Offset: 0x00002374
		public ulong Frequency
		{
			get
			{
				ulong result;
				Marshal.ThrowExceptionForHR(this.audioClockClientInterface.GetFrequency(out result));
				return result;
			}
		}

		/// <summary>
		/// Get Position
		/// </summary>
		// Token: 0x06000042 RID: 66 RVA: 0x00004194 File Offset: 0x00002394
		public bool GetPosition(out ulong position, out ulong qpcPosition)
		{
			int position2 = this.audioClockClientInterface.GetPosition(out position, out qpcPosition);
			if (position2 == -1)
			{
				return false;
			}
			Marshal.ThrowExceptionForHR(position2);
			return true;
		}

		/// <summary>
		/// Adjusted Position
		/// </summary>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000041BC File Offset: 0x000023BC
		public ulong AdjustedPosition
		{
			get
			{
				ulong num = 10000000UL / this.Frequency;
				int num2 = 0;
				ulong num3;
				ulong num4;
				while (!this.GetPosition(out num3, out num4) && ++num2 != 5)
				{
				}
				if (Stopwatch.IsHighResolution)
				{
					ulong num5 = (ulong)(Stopwatch.GetTimestamp() * 10000000m / Stopwatch.Frequency);
					ulong num6 = (num5 - num4) / 100UL;
					ulong num7 = num6 / num;
					num3 += num7;
				}
				return num3;
			}
		}

		/// <summary>
		/// Can Adjust Position
		/// </summary>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000423B File Offset: 0x0000243B
		public bool CanAdjustPosition
		{
			get
			{
				return Stopwatch.IsHighResolution;
			}
		}

		/// <summary>
		/// Dispose
		/// </summary>
		// Token: 0x06000045 RID: 69 RVA: 0x00004242 File Offset: 0x00002442
		public void Dispose()
		{
			if (this.audioClockClientInterface != null)
			{
				Marshal.ReleaseComObject(this.audioClockClientInterface);
				this.audioClockClientInterface = null;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000050 RID: 80
		private IAudioClock audioClockClientInterface;
	}
}
