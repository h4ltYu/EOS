using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using NAudio.Utils;

namespace NAudio.Wave.Compression
{
	/// <summary>
	/// Represents an installed ACM Driver
	/// </summary>
	// Token: 0x02000162 RID: 354
	public class AcmDriver : IDisposable
	{
		/// <summary>
		/// Helper function to determine whether a particular codec is installed
		/// </summary>
		/// <param name="shortName">The short name of the function</param>
		/// <returns>Whether the codec is installed</returns>
		// Token: 0x06000785 RID: 1925 RVA: 0x000165A0 File Offset: 0x000147A0
		public static bool IsCodecInstalled(string shortName)
		{
			foreach (AcmDriver acmDriver in AcmDriver.EnumerateAcmDrivers())
			{
				if (acmDriver.ShortName == shortName)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Attempts to add a new ACM driver from a file
		/// </summary>
		/// <param name="driverFile">Full path of the .acm or dll file containing the driver</param>
		/// <returns>Handle to the driver</returns>
		// Token: 0x06000786 RID: 1926 RVA: 0x000165FC File Offset: 0x000147FC
		public static AcmDriver AddLocalDriver(string driverFile)
		{
			IntPtr intPtr = NativeMethods.LoadLibrary(driverFile);
			if (intPtr == IntPtr.Zero)
			{
				throw new ArgumentException("Failed to load driver file");
			}
			IntPtr procAddress = NativeMethods.GetProcAddress(intPtr, "DriverProc");
			if (procAddress == IntPtr.Zero)
			{
				NativeMethods.FreeLibrary(intPtr);
				throw new ArgumentException("Failed to discover DriverProc");
			}
			IntPtr hAcmDriver;
			MmResult mmResult = AcmInterop.acmDriverAdd(out hAcmDriver, intPtr, procAddress, 0, AcmDriverAddFlags.Function);
			if (mmResult != MmResult.NoError)
			{
				NativeMethods.FreeLibrary(intPtr);
				throw new MmException(mmResult, "acmDriverAdd");
			}
			AcmDriver acmDriver = new AcmDriver(hAcmDriver);
			if (string.IsNullOrEmpty(acmDriver.details.longName))
			{
				acmDriver.details.longName = "Local driver: " + Path.GetFileName(driverFile);
				acmDriver.localDllHandle = intPtr;
			}
			return acmDriver;
		}

		/// <summary>
		/// Removes a driver previously added using AddLocalDriver
		/// </summary>
		/// <param name="localDriver">Local driver to remove</param>
		// Token: 0x06000787 RID: 1927 RVA: 0x000166B8 File Offset: 0x000148B8
		public static void RemoveLocalDriver(AcmDriver localDriver)
		{
			if (localDriver.localDllHandle == IntPtr.Zero)
			{
				throw new ArgumentException("Please pass in the AcmDriver returned by the AddLocalDriver method");
			}
			MmResult result = AcmInterop.acmDriverRemove(localDriver.driverId, 0);
			NativeMethods.FreeLibrary(localDriver.localDllHandle);
			MmException.Try(result, "acmDriverRemove");
		}

		/// <summary>
		/// Show Format Choose Dialog
		/// </summary>
		/// <param name="ownerWindowHandle">Owner window handle, can be null</param>
		/// <param name="windowTitle">Window title</param>
		/// <param name="enumFlags">Enumeration flags. None to get everything</param>
		/// <param name="enumFormat">Enumeration format. Only needed with certain enumeration flags</param>
		/// <param name="selectedFormat">The selected format</param>
		/// <param name="selectedFormatDescription">Textual description of the selected format</param>
		/// <param name="selectedFormatTagDescription">Textual description of the selected format tag</param>
		/// <returns>True if a format was selected</returns>
		// Token: 0x06000788 RID: 1928 RVA: 0x00016708 File Offset: 0x00014908
		public static bool ShowFormatChooseDialog(IntPtr ownerWindowHandle, string windowTitle, AcmFormatEnumFlags enumFlags, WaveFormat enumFormat, out WaveFormat selectedFormat, out string selectedFormatDescription, out string selectedFormatTagDescription)
		{
			AcmFormatChoose acmFormatChoose = default(AcmFormatChoose);
			acmFormatChoose.structureSize = Marshal.SizeOf(acmFormatChoose);
			acmFormatChoose.styleFlags = AcmFormatChooseStyleFlags.None;
			acmFormatChoose.ownerWindowHandle = ownerWindowHandle;
			int num = 200;
			acmFormatChoose.selectedWaveFormatPointer = Marshal.AllocHGlobal(num);
			acmFormatChoose.selectedWaveFormatByteSize = num;
			acmFormatChoose.title = windowTitle;
			acmFormatChoose.name = null;
			acmFormatChoose.formatEnumFlags = enumFlags;
			acmFormatChoose.waveFormatEnumPointer = IntPtr.Zero;
			if (enumFormat != null)
			{
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(enumFormat));
				Marshal.StructureToPtr(enumFormat, intPtr, false);
				acmFormatChoose.waveFormatEnumPointer = intPtr;
			}
			acmFormatChoose.instanceHandle = IntPtr.Zero;
			acmFormatChoose.templateName = null;
			MmResult mmResult = AcmInterop.acmFormatChoose(ref acmFormatChoose);
			selectedFormat = null;
			selectedFormatDescription = null;
			selectedFormatTagDescription = null;
			if (mmResult == MmResult.NoError)
			{
				selectedFormat = WaveFormat.MarshalFromPtr(acmFormatChoose.selectedWaveFormatPointer);
				selectedFormatDescription = acmFormatChoose.formatDescription;
				selectedFormatTagDescription = acmFormatChoose.formatTagDescription;
			}
			Marshal.FreeHGlobal(acmFormatChoose.waveFormatEnumPointer);
			Marshal.FreeHGlobal(acmFormatChoose.selectedWaveFormatPointer);
			if (mmResult != MmResult.AcmCancelled && mmResult != MmResult.NoError)
			{
				throw new MmException(mmResult, "acmFormatChoose");
			}
			return mmResult == MmResult.NoError;
		}

		/// <summary>
		/// Gets the maximum size needed to store a WaveFormat for ACM interop functions
		/// </summary>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00016820 File Offset: 0x00014A20
		public int MaxFormatSize
		{
			get
			{
				int result;
				MmException.Try(AcmInterop.acmMetrics(this.driverHandle, AcmMetrics.MaxSizeFormat, out result), "acmMetrics");
				return result;
			}
		}

		/// <summary>
		/// Finds a Driver by its short name
		/// </summary>
		/// <param name="shortName">Short Name</param>
		/// <returns>The driver, or null if not found</returns>
		// Token: 0x0600078A RID: 1930 RVA: 0x00016848 File Offset: 0x00014A48
		public static AcmDriver FindByShortName(string shortName)
		{
			foreach (AcmDriver acmDriver in AcmDriver.EnumerateAcmDrivers())
			{
				if (acmDriver.ShortName == shortName)
				{
					return acmDriver;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets a list of the ACM Drivers installed
		/// </summary>
		// Token: 0x0600078B RID: 1931 RVA: 0x000168A4 File Offset: 0x00014AA4
		public static IEnumerable<AcmDriver> EnumerateAcmDrivers()
		{
			AcmDriver.drivers = new List<AcmDriver>();
			MmException.Try(AcmInterop.acmDriverEnum(new AcmInterop.AcmDriverEnumCallback(AcmDriver.DriverEnumCallback), IntPtr.Zero, (AcmDriverEnumFlags)0), "acmDriverEnum");
			return AcmDriver.drivers;
		}

		/// <summary>
		/// The callback for acmDriverEnum
		/// </summary>
		// Token: 0x0600078C RID: 1932 RVA: 0x000168D6 File Offset: 0x00014AD6
		private static bool DriverEnumCallback(IntPtr hAcmDriver, IntPtr dwInstance, AcmDriverDetailsSupportFlags flags)
		{
			AcmDriver.drivers.Add(new AcmDriver(hAcmDriver));
			return true;
		}

		/// <summary>
		/// Creates a new ACM Driver object
		/// </summary>
		/// <param name="hAcmDriver">Driver handle</param>
		// Token: 0x0600078D RID: 1933 RVA: 0x000168EC File Offset: 0x00014AEC
		private AcmDriver(IntPtr hAcmDriver)
		{
			this.driverId = hAcmDriver;
			this.details = default(AcmDriverDetails);
			this.details.structureSize = Marshal.SizeOf(this.details);
			MmException.Try(AcmInterop.acmDriverDetails(hAcmDriver, ref this.details, 0), "acmDriverDetails");
		}

		/// <summary>
		/// The short name of this driver
		/// </summary>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00016944 File Offset: 0x00014B44
		public string ShortName
		{
			get
			{
				return this.details.shortName;
			}
		}

		/// <summary>
		/// The full name of this driver
		/// </summary>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00016951 File Offset: 0x00014B51
		public string LongName
		{
			get
			{
				return this.details.longName;
			}
		}

		/// <summary>
		/// The driver ID
		/// </summary>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0001695E File Offset: 0x00014B5E
		public IntPtr DriverId
		{
			get
			{
				return this.driverId;
			}
		}

		/// <summary>
		/// ToString
		/// </summary>
		// Token: 0x06000791 RID: 1937 RVA: 0x00016966 File Offset: 0x00014B66
		public override string ToString()
		{
			return this.LongName;
		}

		/// <summary>
		/// The list of FormatTags for this ACM Driver
		/// </summary>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00016970 File Offset: 0x00014B70
		public IEnumerable<AcmFormatTag> FormatTags
		{
			get
			{
				if (this.formatTags == null)
				{
					if (this.driverHandle == IntPtr.Zero)
					{
						throw new InvalidOperationException("Driver must be opened first");
					}
					this.formatTags = new List<AcmFormatTag>();
					AcmFormatTagDetails acmFormatTagDetails = default(AcmFormatTagDetails);
					acmFormatTagDetails.structureSize = Marshal.SizeOf(acmFormatTagDetails);
					MmException.Try(AcmInterop.acmFormatTagEnum(this.driverHandle, ref acmFormatTagDetails, new AcmInterop.AcmFormatTagEnumCallback(this.AcmFormatTagEnumCallback), IntPtr.Zero, 0), "acmFormatTagEnum");
				}
				return this.formatTags;
			}
		}

		/// <summary>
		/// Gets all the supported formats for a given format tag
		/// </summary>
		/// <param name="formatTag">Format tag</param>
		/// <returns>Supported formats</returns>
		// Token: 0x06000793 RID: 1939 RVA: 0x000169F8 File Offset: 0x00014BF8
		public IEnumerable<AcmFormat> GetFormats(AcmFormatTag formatTag)
		{
			if (this.driverHandle == IntPtr.Zero)
			{
				throw new InvalidOperationException("Driver must be opened first");
			}
			this.tempFormatsList = new List<AcmFormat>();
			AcmFormatDetails acmFormatDetails = default(AcmFormatDetails);
			acmFormatDetails.structSize = Marshal.SizeOf(acmFormatDetails);
			acmFormatDetails.waveFormatByteSize = 1024;
			acmFormatDetails.waveFormatPointer = Marshal.AllocHGlobal(acmFormatDetails.waveFormatByteSize);
			acmFormatDetails.formatTag = (int)formatTag.FormatTag;
			MmResult result = AcmInterop.acmFormatEnum(this.driverHandle, ref acmFormatDetails, new AcmInterop.AcmFormatEnumCallback(this.AcmFormatEnumCallback), IntPtr.Zero, AcmFormatEnumFlags.None);
			Marshal.FreeHGlobal(acmFormatDetails.waveFormatPointer);
			MmException.Try(result, "acmFormatEnum");
			return this.tempFormatsList;
		}

		/// <summary>
		/// Opens this driver
		/// </summary>
		// Token: 0x06000794 RID: 1940 RVA: 0x00016AB0 File Offset: 0x00014CB0
		public void Open()
		{
			if (this.driverHandle == IntPtr.Zero)
			{
				MmException.Try(AcmInterop.acmDriverOpen(out this.driverHandle, this.DriverId, 0), "acmDriverOpen");
			}
		}

		/// <summary>
		/// Closes this driver
		/// </summary>
		// Token: 0x06000795 RID: 1941 RVA: 0x00016AE0 File Offset: 0x00014CE0
		public void Close()
		{
			if (this.driverHandle != IntPtr.Zero)
			{
				MmException.Try(AcmInterop.acmDriverClose(this.driverHandle, 0), "acmDriverClose");
				this.driverHandle = IntPtr.Zero;
			}
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00016B15 File Offset: 0x00014D15
		private bool AcmFormatTagEnumCallback(IntPtr hAcmDriverId, ref AcmFormatTagDetails formatTagDetails, IntPtr dwInstance, AcmDriverDetailsSupportFlags flags)
		{
			this.formatTags.Add(new AcmFormatTag(formatTagDetails));
			return true;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00016B2E File Offset: 0x00014D2E
		private bool AcmFormatEnumCallback(IntPtr hAcmDriverId, ref AcmFormatDetails formatDetails, IntPtr dwInstance, AcmDriverDetailsSupportFlags flags)
		{
			this.tempFormatsList.Add(new AcmFormat(formatDetails));
			return true;
		}

		/// <summary>
		/// Dispose
		/// </summary>
		// Token: 0x06000798 RID: 1944 RVA: 0x00016B47 File Offset: 0x00014D47
		public void Dispose()
		{
			if (this.driverHandle != IntPtr.Zero)
			{
				this.Close();
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x040007B9 RID: 1977
		private static List<AcmDriver> drivers;

		// Token: 0x040007BA RID: 1978
		private AcmDriverDetails details;

		// Token: 0x040007BB RID: 1979
		private IntPtr driverId;

		// Token: 0x040007BC RID: 1980
		private IntPtr driverHandle;

		// Token: 0x040007BD RID: 1981
		private List<AcmFormatTag> formatTags;

		// Token: 0x040007BE RID: 1982
		private List<AcmFormat> tempFormatsList;

		// Token: 0x040007BF RID: 1983
		private IntPtr localDllHandle;
	}
}
