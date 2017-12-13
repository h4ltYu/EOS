using System;

namespace NAudio.Wave
{
	/// <summary>
	/// Wave Callback Info
	/// </summary>
	// Token: 0x020001CB RID: 459
	public class WaveCallbackInfo
	{
		/// <summary>
		/// Callback Strategy
		/// </summary>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0001C755 File Offset: 0x0001A955
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x0001C75D File Offset: 0x0001A95D
		public WaveCallbackStrategy Strategy { get; private set; }

		/// <summary>
		/// Window Handle (if applicable)
		/// </summary>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0001C766 File Offset: 0x0001A966
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0001C76E File Offset: 0x0001A96E
		public IntPtr Handle { get; private set; }

		/// <summary>
		/// Sets up a new WaveCallbackInfo for function callbacks
		/// </summary>
		// Token: 0x060009E1 RID: 2529 RVA: 0x0001C777 File Offset: 0x0001A977
		public static WaveCallbackInfo FunctionCallback()
		{
			return new WaveCallbackInfo(WaveCallbackStrategy.FunctionCallback, IntPtr.Zero);
		}

		/// <summary>
		/// Sets up a new WaveCallbackInfo to use a New Window
		/// IMPORTANT: only use this on the GUI thread
		/// </summary>
		// Token: 0x060009E2 RID: 2530 RVA: 0x0001C784 File Offset: 0x0001A984
		public static WaveCallbackInfo NewWindow()
		{
			return new WaveCallbackInfo(WaveCallbackStrategy.NewWindow, IntPtr.Zero);
		}

		/// <summary>
		/// Sets up a new WaveCallbackInfo to use an existing window
		/// IMPORTANT: only use this on the GUI thread
		/// </summary>
		// Token: 0x060009E3 RID: 2531 RVA: 0x0001C791 File Offset: 0x0001A991
		public static WaveCallbackInfo ExistingWindow(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException("Handle cannot be zero");
			}
			return new WaveCallbackInfo(WaveCallbackStrategy.ExistingWindow, handle);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0001C7B2 File Offset: 0x0001A9B2
		private WaveCallbackInfo(WaveCallbackStrategy strategy, IntPtr handle)
		{
			this.Strategy = strategy;
			this.Handle = handle;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001C7C8 File Offset: 0x0001A9C8
		internal void Connect(WaveInterop.WaveCallback callback)
		{
			if (this.Strategy == WaveCallbackStrategy.NewWindow)
			{
				this.waveOutWindow = new WaveWindow(callback);
				this.waveOutWindow.CreateControl();
				this.Handle = this.waveOutWindow.Handle;
				return;
			}
			if (this.Strategy == WaveCallbackStrategy.ExistingWindow)
			{
				this.waveOutWindowNative = new WaveWindowNative(callback);
				this.waveOutWindowNative.AssignHandle(this.Handle);
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0001C830 File Offset: 0x0001AA30
		internal MmResult WaveOutOpen(out IntPtr waveOutHandle, int deviceNumber, WaveFormat waveFormat, WaveInterop.WaveCallback callback)
		{
			MmResult result;
			if (this.Strategy == WaveCallbackStrategy.FunctionCallback)
			{
				result = WaveInterop.waveOutOpen(out waveOutHandle, (IntPtr)deviceNumber, waveFormat, callback, IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackFunction);
			}
			else
			{
				result = WaveInterop.waveOutOpenWindow(out waveOutHandle, (IntPtr)deviceNumber, waveFormat, this.Handle, IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackWindow);
			}
			return result;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0001C880 File Offset: 0x0001AA80
		internal MmResult WaveInOpen(out IntPtr waveInHandle, int deviceNumber, WaveFormat waveFormat, WaveInterop.WaveCallback callback)
		{
			MmResult result;
			if (this.Strategy == WaveCallbackStrategy.FunctionCallback)
			{
				result = WaveInterop.waveInOpen(out waveInHandle, (IntPtr)deviceNumber, waveFormat, callback, IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackFunction);
			}
			else
			{
				result = WaveInterop.waveInOpenWindow(out waveInHandle, (IntPtr)deviceNumber, waveFormat, this.Handle, IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackWindow);
			}
			return result;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0001C8D0 File Offset: 0x0001AAD0
		internal void Disconnect()
		{
			if (this.waveOutWindow != null)
			{
				this.waveOutWindow.Close();
				this.waveOutWindow = null;
			}
			if (this.waveOutWindowNative != null)
			{
				this.waveOutWindowNative.ReleaseHandle();
				this.waveOutWindowNative = null;
			}
		}

		// Token: 0x04000B14 RID: 2836
		private WaveWindow waveOutWindow;

		// Token: 0x04000B15 RID: 2837
		private WaveWindowNative waveOutWindowNative;
	}
}
