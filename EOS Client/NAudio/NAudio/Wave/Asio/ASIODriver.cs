using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace NAudio.Wave.Asio
{
	/// <summary>
	/// Main ASIODriver Class. To use this class, you need to query first the GetASIODriverNames() and
	/// then use the GetASIODriverByName to instantiate the correct ASIODriver.
	/// This is the first ASIODriver binding fully implemented in C#!
	///
	/// Contributor: Alexandre Mutel - email: alexandre_mutel at yahoo.fr
	/// </summary>
	// Token: 0x02000137 RID: 311
	internal class ASIODriver
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x00015210 File Offset: 0x00013410
		private ASIODriver()
		{
		}

		/// <summary>
		/// Gets the ASIO driver names installed.
		/// </summary>
		/// <returns>a list of driver names. Use this name to GetASIODriverByName</returns>
		// Token: 0x060006D4 RID: 1748 RVA: 0x00015218 File Offset: 0x00013418
		public static string[] GetASIODriverNames()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ASIO");
			string[] result = new string[0];
			if (registryKey != null)
			{
				result = registryKey.GetSubKeyNames();
				registryKey.Close();
			}
			return result;
		}

		/// <summary>
		/// Instantiate a ASIODriver given its name.
		/// </summary>
		/// <param name="name">The name of the driver</param>
		/// <returns>an ASIODriver instance</returns>
		// Token: 0x060006D5 RID: 1749 RVA: 0x00015250 File Offset: 0x00013450
		public static ASIODriver GetASIODriverByName(string name)
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ASIO\\" + name);
			if (registryKey == null)
			{
				throw new ArgumentException(string.Format("Driver Name {0} doesn't exist", name));
			}
			string g = registryKey.GetValue("CLSID").ToString();
			return ASIODriver.GetASIODriverByGuid(new Guid(g));
		}

		/// <summary>
		/// Instantiate the ASIO driver by GUID.
		/// </summary>
		/// <param name="guid">The GUID.</param>
		/// <returns>an ASIODriver instance</returns>
		// Token: 0x060006D6 RID: 1750 RVA: 0x000152A4 File Offset: 0x000134A4
		public static ASIODriver GetASIODriverByGuid(Guid guid)
		{
			ASIODriver asiodriver = new ASIODriver();
			asiodriver.initFromGuid(guid);
			return asiodriver;
		}

		/// <summary>
		/// Inits the ASIODriver..
		/// </summary>
		/// <param name="sysHandle">The sys handle.</param>
		/// <returns></returns>
		// Token: 0x060006D7 RID: 1751 RVA: 0x000152C0 File Offset: 0x000134C0
		public bool init(IntPtr sysHandle)
		{
			int num = this.asioDriverVTable.init(this.pASIOComObject, sysHandle);
			return num == 1;
		}

		/// <summary>
		/// Gets the name of the driver.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060006D8 RID: 1752 RVA: 0x000152EC File Offset: 0x000134EC
		public string getDriverName()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			this.asioDriverVTable.getDriverName(this.pASIOComObject, stringBuilder);
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Gets the driver version.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060006D9 RID: 1753 RVA: 0x00015321 File Offset: 0x00013521
		public int getDriverVersion()
		{
			return this.asioDriverVTable.getDriverVersion(this.pASIOComObject);
		}

		/// <summary>
		/// Gets the error message.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060006DA RID: 1754 RVA: 0x0001533C File Offset: 0x0001353C
		public string getErrorMessage()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			this.asioDriverVTable.getErrorMessage(this.pASIOComObject, stringBuilder);
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Starts this instance.
		/// </summary>
		// Token: 0x060006DB RID: 1755 RVA: 0x00015371 File Offset: 0x00013571
		public void start()
		{
			this.handleException(this.asioDriverVTable.start(this.pASIOComObject), "start");
		}

		/// <summary>
		/// Stops this instance.
		/// </summary>
		// Token: 0x060006DC RID: 1756 RVA: 0x00015394 File Offset: 0x00013594
		public ASIOError stop()
		{
			return this.asioDriverVTable.stop(this.pASIOComObject);
		}

		/// <summary>
		/// Gets the channels.
		/// </summary>
		/// <param name="numInputChannels">The num input channels.</param>
		/// <param name="numOutputChannels">The num output channels.</param>
		// Token: 0x060006DD RID: 1757 RVA: 0x000153AC File Offset: 0x000135AC
		public void getChannels(out int numInputChannels, out int numOutputChannels)
		{
			this.handleException(this.asioDriverVTable.getChannels(this.pASIOComObject, out numInputChannels, out numOutputChannels), "getChannels");
		}

		/// <summary>
		/// Gets the latencies (n.b. does not throw an exception)
		/// </summary>
		/// <param name="inputLatency">The input latency.</param>
		/// <param name="outputLatency">The output latency.</param>
		// Token: 0x060006DE RID: 1758 RVA: 0x000153D1 File Offset: 0x000135D1
		public ASIOError GetLatencies(out int inputLatency, out int outputLatency)
		{
			return this.asioDriverVTable.getLatencies(this.pASIOComObject, out inputLatency, out outputLatency);
		}

		/// <summary>
		/// Gets the size of the buffer.
		/// </summary>
		/// <param name="minSize">Size of the min.</param>
		/// <param name="maxSize">Size of the max.</param>
		/// <param name="preferredSize">Size of the preferred.</param>
		/// <param name="granularity">The granularity.</param>
		// Token: 0x060006DF RID: 1759 RVA: 0x000153EB File Offset: 0x000135EB
		public void getBufferSize(out int minSize, out int maxSize, out int preferredSize, out int granularity)
		{
			this.handleException(this.asioDriverVTable.getBufferSize(this.pASIOComObject, out minSize, out maxSize, out preferredSize, out granularity), "getBufferSize");
		}

		/// <summary>
		/// Determines whether this instance can use the specified sample rate.
		/// </summary>
		/// <param name="sampleRate">The sample rate.</param>
		/// <returns>
		/// 	<c>true</c> if this instance [can sample rate] the specified sample rate; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x060006E0 RID: 1760 RVA: 0x00015414 File Offset: 0x00013614
		public bool canSampleRate(double sampleRate)
		{
			ASIOError asioerror = this.asioDriverVTable.canSampleRate(this.pASIOComObject, sampleRate);
			if (asioerror == ASIOError.ASE_NoClock)
			{
				return false;
			}
			if (asioerror == ASIOError.ASE_OK)
			{
				return true;
			}
			this.handleException(asioerror, "canSampleRate");
			return false;
		}

		/// <summary>
		/// Gets the sample rate.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060006E1 RID: 1761 RVA: 0x00015458 File Offset: 0x00013658
		public double getSampleRate()
		{
			double result;
			this.handleException(this.asioDriverVTable.getSampleRate(this.pASIOComObject, out result), "getSampleRate");
			return result;
		}

		/// <summary>
		/// Sets the sample rate.
		/// </summary>
		/// <param name="sampleRate">The sample rate.</param>
		// Token: 0x060006E2 RID: 1762 RVA: 0x00015489 File Offset: 0x00013689
		public void setSampleRate(double sampleRate)
		{
			this.handleException(this.asioDriverVTable.setSampleRate(this.pASIOComObject, sampleRate), "setSampleRate");
		}

		/// <summary>
		/// Gets the clock sources.
		/// </summary>
		/// <param name="clocks">The clocks.</param>
		/// <param name="numSources">The num sources.</param>
		// Token: 0x060006E3 RID: 1763 RVA: 0x000154AD File Offset: 0x000136AD
		public void getClockSources(out long clocks, int numSources)
		{
			this.handleException(this.asioDriverVTable.getClockSources(this.pASIOComObject, out clocks, numSources), "getClockSources");
		}

		/// <summary>
		/// Sets the clock source.
		/// </summary>
		/// <param name="reference">The reference.</param>
		// Token: 0x060006E4 RID: 1764 RVA: 0x000154D2 File Offset: 0x000136D2
		public void setClockSource(int reference)
		{
			this.handleException(this.asioDriverVTable.setClockSource(this.pASIOComObject, reference), "setClockSources");
		}

		/// <summary>
		/// Gets the sample position.
		/// </summary>
		/// <param name="samplePos">The sample pos.</param>
		/// <param name="timeStamp">The time stamp.</param>
		// Token: 0x060006E5 RID: 1765 RVA: 0x000154F6 File Offset: 0x000136F6
		public void getSamplePosition(out long samplePos, ref ASIO64Bit timeStamp)
		{
			this.handleException(this.asioDriverVTable.getSamplePosition(this.pASIOComObject, out samplePos, ref timeStamp), "getSamplePosition");
		}

		/// <summary>
		/// Gets the channel info.
		/// </summary>
		/// <param name="channelNumber">The channel number.</param>
		/// <param name="trueForInputInfo">if set to <c>true</c> [true for input info].</param>
		/// <returns></returns>
		// Token: 0x060006E6 RID: 1766 RVA: 0x0001551C File Offset: 0x0001371C
		public ASIOChannelInfo getChannelInfo(int channelNumber, bool trueForInputInfo)
		{
			ASIOChannelInfo result = new ASIOChannelInfo
			{
				channel = channelNumber,
				isInput = trueForInputInfo
			};
			this.handleException(this.asioDriverVTable.getChannelInfo(this.pASIOComObject, ref result), "getChannelInfo");
			return result;
		}

		/// <summary>
		/// Creates the buffers.
		/// </summary>
		/// <param name="bufferInfos">The buffer infos.</param>
		/// <param name="numChannels">The num channels.</param>
		/// <param name="bufferSize">Size of the buffer.</param>
		/// <param name="callbacks">The callbacks.</param>
		// Token: 0x060006E7 RID: 1767 RVA: 0x00015568 File Offset: 0x00013768
		public void createBuffers(IntPtr bufferInfos, int numChannels, int bufferSize, ref ASIOCallbacks callbacks)
		{
			this.pinnedcallbacks = Marshal.AllocHGlobal(Marshal.SizeOf(callbacks));
			Marshal.StructureToPtr(callbacks, this.pinnedcallbacks, false);
			this.handleException(this.asioDriverVTable.createBuffers(this.pASIOComObject, bufferInfos, numChannels, bufferSize, this.pinnedcallbacks), "createBuffers");
		}

		/// <summary>
		/// Disposes the buffers.
		/// </summary>
		// Token: 0x060006E8 RID: 1768 RVA: 0x000155D4 File Offset: 0x000137D4
		public ASIOError disposeBuffers()
		{
			ASIOError result = this.asioDriverVTable.disposeBuffers(this.pASIOComObject);
			Marshal.FreeHGlobal(this.pinnedcallbacks);
			return result;
		}

		/// <summary>
		/// Controls the panel.
		/// </summary>
		// Token: 0x060006E9 RID: 1769 RVA: 0x00015604 File Offset: 0x00013804
		public void controlPanel()
		{
			this.handleException(this.asioDriverVTable.controlPanel(this.pASIOComObject), "controlPanel");
		}

		/// <summary>
		/// Futures the specified selector.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <param name="opt">The opt.</param>
		// Token: 0x060006EA RID: 1770 RVA: 0x00015627 File Offset: 0x00013827
		public void future(int selector, IntPtr opt)
		{
			this.handleException(this.asioDriverVTable.future(this.pASIOComObject, selector, opt), "future");
		}

		/// <summary>
		/// Notifies OutputReady to the ASIODriver.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060006EB RID: 1771 RVA: 0x0001564C File Offset: 0x0001384C
		public ASIOError outputReady()
		{
			return this.asioDriverVTable.outputReady(this.pASIOComObject);
		}

		/// <summary>
		/// Releases this instance.
		/// </summary>
		// Token: 0x060006EC RID: 1772 RVA: 0x00015664 File Offset: 0x00013864
		public void ReleaseComASIODriver()
		{
			Marshal.Release(this.pASIOComObject);
		}

		/// <summary>
		/// Handles the exception. Throws an exception based on the error.
		/// </summary>
		/// <param name="error">The error to check.</param>
		/// <param name="methodName">Method name</param>
		// Token: 0x060006ED RID: 1773 RVA: 0x00015674 File Offset: 0x00013874
		private void handleException(ASIOError error, string methodName)
		{
			if (error != ASIOError.ASE_OK && error != ASIOError.ASE_SUCCESS)
			{
				throw new ASIOException(string.Format("Error code [{0}] while calling ASIO method <{1}>, {2}", ASIOException.getErrorName(error), methodName, this.getErrorMessage()))
				{
					Error = error
				};
			}
		}

		/// <summary>
		/// Inits the vTable method from GUID. This is a tricky part of this class.
		/// </summary>
		/// <param name="ASIOGuid">The ASIO GUID.</param>
		// Token: 0x060006EE RID: 1774 RVA: 0x000156B4 File Offset: 0x000138B4
		private void initFromGuid(Guid ASIOGuid)
		{
			int num = ASIODriver.CoCreateInstance(ref ASIOGuid, IntPtr.Zero, 1u, ref ASIOGuid, out this.pASIOComObject);
			if (num != 0)
			{
				throw new COMException("Unable to instantiate ASIO. Check if STAThread is set", num);
			}
			IntPtr ptr = Marshal.ReadIntPtr(this.pASIOComObject);
			this.asioDriverVTable = new ASIODriver.ASIODriverVTable();
			FieldInfo[] fields = typeof(ASIODriver.ASIODriverVTable).GetFields();
			for (int i = 0; i < fields.Length; i++)
			{
				FieldInfo fieldInfo = fields[i];
				IntPtr ptr2 = Marshal.ReadIntPtr(ptr, (i + 3) * IntPtr.Size);
				object delegateForFunctionPointer = Marshal.GetDelegateForFunctionPointer(ptr2, fieldInfo.FieldType);
				fieldInfo.SetValue(this.asioDriverVTable, delegateForFunctionPointer);
			}
		}

		// Token: 0x060006EF RID: 1775
		[DllImport("ole32.Dll")]
		private static extern int CoCreateInstance(ref Guid clsid, IntPtr inner, uint context, ref Guid uuid, out IntPtr rReturnedComObject);

		// Token: 0x0400074B RID: 1867
		private IntPtr pASIOComObject;

		// Token: 0x0400074C RID: 1868
		private IntPtr pinnedcallbacks;

		// Token: 0x0400074D RID: 1869
		private ASIODriver.ASIODriverVTable asioDriverVTable;

		/// <summary>
		/// Internal VTable structure to store all the delegates to the C++ COM method.
		/// </summary>
		// Token: 0x02000138 RID: 312
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		private class ASIODriverVTable
		{
			// Token: 0x0400074E RID: 1870
			public ASIODriver.ASIODriverVTable.ASIOInit init;

			// Token: 0x0400074F RID: 1871
			public ASIODriver.ASIODriverVTable.ASIOgetDriverName getDriverName;

			// Token: 0x04000750 RID: 1872
			public ASIODriver.ASIODriverVTable.ASIOgetDriverVersion getDriverVersion;

			// Token: 0x04000751 RID: 1873
			public ASIODriver.ASIODriverVTable.ASIOgetErrorMessage getErrorMessage;

			// Token: 0x04000752 RID: 1874
			public ASIODriver.ASIODriverVTable.ASIOstart start;

			// Token: 0x04000753 RID: 1875
			public ASIODriver.ASIODriverVTable.ASIOstop stop;

			// Token: 0x04000754 RID: 1876
			public ASIODriver.ASIODriverVTable.ASIOgetChannels getChannels;

			// Token: 0x04000755 RID: 1877
			public ASIODriver.ASIODriverVTable.ASIOgetLatencies getLatencies;

			// Token: 0x04000756 RID: 1878
			public ASIODriver.ASIODriverVTable.ASIOgetBufferSize getBufferSize;

			// Token: 0x04000757 RID: 1879
			public ASIODriver.ASIODriverVTable.ASIOcanSampleRate canSampleRate;

			// Token: 0x04000758 RID: 1880
			public ASIODriver.ASIODriverVTable.ASIOgetSampleRate getSampleRate;

			// Token: 0x04000759 RID: 1881
			public ASIODriver.ASIODriverVTable.ASIOsetSampleRate setSampleRate;

			// Token: 0x0400075A RID: 1882
			public ASIODriver.ASIODriverVTable.ASIOgetClockSources getClockSources;

			// Token: 0x0400075B RID: 1883
			public ASIODriver.ASIODriverVTable.ASIOsetClockSource setClockSource;

			// Token: 0x0400075C RID: 1884
			public ASIODriver.ASIODriverVTable.ASIOgetSamplePosition getSamplePosition;

			// Token: 0x0400075D RID: 1885
			public ASIODriver.ASIODriverVTable.ASIOgetChannelInfo getChannelInfo;

			// Token: 0x0400075E RID: 1886
			public ASIODriver.ASIODriverVTable.ASIOcreateBuffers createBuffers;

			// Token: 0x0400075F RID: 1887
			public ASIODriver.ASIODriverVTable.ASIOdisposeBuffers disposeBuffers;

			// Token: 0x04000760 RID: 1888
			public ASIODriver.ASIODriverVTable.ASIOcontrolPanel controlPanel;

			// Token: 0x04000761 RID: 1889
			public ASIODriver.ASIODriverVTable.ASIOfuture future;

			// Token: 0x04000762 RID: 1890
			public ASIODriver.ASIODriverVTable.ASIOoutputReady outputReady;

			// Token: 0x02000139 RID: 313
			// (Invoke) Token: 0x060006F2 RID: 1778
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate int ASIOInit(IntPtr _pUnknown, IntPtr sysHandle);

			// Token: 0x0200013A RID: 314
			// (Invoke) Token: 0x060006F6 RID: 1782
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate void ASIOgetDriverName(IntPtr _pUnknown, StringBuilder name);

			// Token: 0x0200013B RID: 315
			// (Invoke) Token: 0x060006FA RID: 1786
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate int ASIOgetDriverVersion(IntPtr _pUnknown);

			// Token: 0x0200013C RID: 316
			// (Invoke) Token: 0x060006FE RID: 1790
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate void ASIOgetErrorMessage(IntPtr _pUnknown, StringBuilder errorMessage);

			// Token: 0x0200013D RID: 317
			// (Invoke) Token: 0x06000702 RID: 1794
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOstart(IntPtr _pUnknown);

			// Token: 0x0200013E RID: 318
			// (Invoke) Token: 0x06000706 RID: 1798
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOstop(IntPtr _pUnknown);

			// Token: 0x0200013F RID: 319
			// (Invoke) Token: 0x0600070A RID: 1802
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetChannels(IntPtr _pUnknown, out int numInputChannels, out int numOutputChannels);

			// Token: 0x02000140 RID: 320
			// (Invoke) Token: 0x0600070E RID: 1806
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetLatencies(IntPtr _pUnknown, out int inputLatency, out int outputLatency);

			// Token: 0x02000141 RID: 321
			// (Invoke) Token: 0x06000712 RID: 1810
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetBufferSize(IntPtr _pUnknown, out int minSize, out int maxSize, out int preferredSize, out int granularity);

			// Token: 0x02000142 RID: 322
			// (Invoke) Token: 0x06000716 RID: 1814
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOcanSampleRate(IntPtr _pUnknown, double sampleRate);

			// Token: 0x02000143 RID: 323
			// (Invoke) Token: 0x0600071A RID: 1818
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetSampleRate(IntPtr _pUnknown, out double sampleRate);

			// Token: 0x02000144 RID: 324
			// (Invoke) Token: 0x0600071E RID: 1822
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOsetSampleRate(IntPtr _pUnknown, double sampleRate);

			// Token: 0x02000145 RID: 325
			// (Invoke) Token: 0x06000722 RID: 1826
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetClockSources(IntPtr _pUnknown, out long clocks, int numSources);

			// Token: 0x02000146 RID: 326
			// (Invoke) Token: 0x06000726 RID: 1830
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOsetClockSource(IntPtr _pUnknown, int reference);

			// Token: 0x02000147 RID: 327
			// (Invoke) Token: 0x0600072A RID: 1834
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetSamplePosition(IntPtr _pUnknown, out long samplePos, ref ASIO64Bit timeStamp);

			// Token: 0x02000148 RID: 328
			// (Invoke) Token: 0x0600072E RID: 1838
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetChannelInfo(IntPtr _pUnknown, ref ASIOChannelInfo info);

			// Token: 0x02000149 RID: 329
			// (Invoke) Token: 0x06000732 RID: 1842
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOcreateBuffers(IntPtr _pUnknown, IntPtr bufferInfos, int numChannels, int bufferSize, IntPtr callbacks);

			// Token: 0x0200014A RID: 330
			// (Invoke) Token: 0x06000736 RID: 1846
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOdisposeBuffers(IntPtr _pUnknown);

			// Token: 0x0200014B RID: 331
			// (Invoke) Token: 0x0600073A RID: 1850
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOcontrolPanel(IntPtr _pUnknown);

			// Token: 0x0200014C RID: 332
			// (Invoke) Token: 0x0600073E RID: 1854
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOfuture(IntPtr _pUnknown, int selector, IntPtr opt);

			// Token: 0x0200014D RID: 333
			// (Invoke) Token: 0x06000742 RID: 1858
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOoutputReady(IntPtr _pUnknown);
		}
	}
}
