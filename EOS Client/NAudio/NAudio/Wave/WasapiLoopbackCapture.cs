using System;
using NAudio.CoreAudioApi;

namespace NAudio.Wave
{
	/// <summary>
	/// WASAPI Loopback Capture
	/// based on a contribution from "Pygmy" - http://naudio.codeplex.com/discussions/203605
	/// </summary>
	// Token: 0x02000182 RID: 386
	public class WasapiLoopbackCapture : WasapiCapture
	{
		/// <summary>
		/// Initialises a new instance of the WASAPI capture class
		/// </summary>
		// Token: 0x060007EA RID: 2026 RVA: 0x00017272 File Offset: 0x00015472
		public WasapiLoopbackCapture() : this(WasapiLoopbackCapture.GetDefaultLoopbackCaptureDevice())
		{
		}

		/// <summary>
		/// Initialises a new instance of the WASAPI capture class
		/// </summary>
		/// <param name="captureDevice">Capture device to use</param>
		// Token: 0x060007EB RID: 2027 RVA: 0x0001727F File Offset: 0x0001547F
		public WasapiLoopbackCapture(MMDevice captureDevice) : base(captureDevice)
		{
		}

		/// <summary>
		/// Gets the default audio loopback capture device
		/// </summary>
		/// <returns>The default audio loopback capture device</returns>
		// Token: 0x060007EC RID: 2028 RVA: 0x00017288 File Offset: 0x00015488
		public static MMDevice GetDefaultLoopbackCaptureDevice()
		{
			MMDeviceEnumerator mmdeviceEnumerator = new MMDeviceEnumerator();
			return mmdeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
		}

		/// <summary>
		/// Recording wave format
		/// </summary>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x000172A3 File Offset: 0x000154A3
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x000172AB File Offset: 0x000154AB
		public override WaveFormat WaveFormat
		{
			get
			{
				return base.WaveFormat;
			}
			set
			{
				throw new InvalidOperationException("WaveFormat cannot be set for WASAPI Loopback Capture");
			}
		}

		/// <summary>
		/// Specify loopback
		/// </summary>
		// Token: 0x060007EF RID: 2031 RVA: 0x000172B7 File Offset: 0x000154B7
		protected override AudioClientStreamFlags GetAudioClientStreamFlags()
		{
			return AudioClientStreamFlags.Loopback;
		}
	}
}
