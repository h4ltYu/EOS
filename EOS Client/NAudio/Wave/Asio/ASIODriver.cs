using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace NAudio.Wave.Asio
{
    internal class ASIODriver
    {
        private ASIODriver()
        {
        }

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

        public static ASIODriver GetASIODriverByGuid(Guid guid)
        {
            ASIODriver asiodriver = new ASIODriver();
            asiodriver.initFromGuid(guid);
            return asiodriver;
        }

        public bool init(IntPtr sysHandle)
        {
            int num = this.asioDriverVTable.init(this.pASIOComObject, sysHandle);
            return num == 1;
        }

        public string getDriverName()
        {
            StringBuilder stringBuilder = new StringBuilder(256);
            this.asioDriverVTable.getDriverName(this.pASIOComObject, stringBuilder);
            return stringBuilder.ToString();
        }

        public int getDriverVersion()
        {
            return this.asioDriverVTable.getDriverVersion(this.pASIOComObject);
        }

        public string getErrorMessage()
        {
            StringBuilder stringBuilder = new StringBuilder(256);
            this.asioDriverVTable.getErrorMessage(this.pASIOComObject, stringBuilder);
            return stringBuilder.ToString();
        }

        public void start()
        {
            this.handleException(this.asioDriverVTable.start(this.pASIOComObject), "start");
        }

        public ASIOError stop()
        {
            return this.asioDriverVTable.stop(this.pASIOComObject);
        }

        public void getChannels(out int numInputChannels, out int numOutputChannels)
        {
            this.handleException(this.asioDriverVTable.getChannels(this.pASIOComObject, out numInputChannels, out numOutputChannels), "getChannels");
        }

        public ASIOError GetLatencies(out int inputLatency, out int outputLatency)
        {
            return this.asioDriverVTable.getLatencies(this.pASIOComObject, out inputLatency, out outputLatency);
        }

        public void getBufferSize(out int minSize, out int maxSize, out int preferredSize, out int granularity)
        {
            this.handleException(this.asioDriverVTable.getBufferSize(this.pASIOComObject, out minSize, out maxSize, out preferredSize, out granularity), "getBufferSize");
        }

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

        public double getSampleRate()
        {
            double result;
            this.handleException(this.asioDriverVTable.getSampleRate(this.pASIOComObject, out result), "getSampleRate");
            return result;
        }

        public void setSampleRate(double sampleRate)
        {
            this.handleException(this.asioDriverVTable.setSampleRate(this.pASIOComObject, sampleRate), "setSampleRate");
        }

        public void getClockSources(out long clocks, int numSources)
        {
            this.handleException(this.asioDriverVTable.getClockSources(this.pASIOComObject, out clocks, numSources), "getClockSources");
        }

        public void setClockSource(int reference)
        {
            this.handleException(this.asioDriverVTable.setClockSource(this.pASIOComObject, reference), "setClockSources");
        }

        public void getSamplePosition(out long samplePos, ref ASIO64Bit timeStamp)
        {
            this.handleException(this.asioDriverVTable.getSamplePosition(this.pASIOComObject, out samplePos, ref timeStamp), "getSamplePosition");
        }

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

        public void createBuffers(IntPtr bufferInfos, int numChannels, int bufferSize, ref ASIOCallbacks callbacks)
        {
            this.pinnedcallbacks = Marshal.AllocHGlobal(Marshal.SizeOf(callbacks));
            Marshal.StructureToPtr(callbacks, this.pinnedcallbacks, false);
            this.handleException(this.asioDriverVTable.createBuffers(this.pASIOComObject, bufferInfos, numChannels, bufferSize, this.pinnedcallbacks), "createBuffers");
        }

        public ASIOError disposeBuffers()
        {
            ASIOError result = this.asioDriverVTable.disposeBuffers(this.pASIOComObject);
            Marshal.FreeHGlobal(this.pinnedcallbacks);
            return result;
        }

        public void controlPanel()
        {
            this.handleException(this.asioDriverVTable.controlPanel(this.pASIOComObject), "controlPanel");
        }

        public void future(int selector, IntPtr opt)
        {
            this.handleException(this.asioDriverVTable.future(this.pASIOComObject, selector, opt), "future");
        }

        public ASIOError outputReady()
        {
            return this.asioDriverVTable.outputReady(this.pASIOComObject);
        }

        public void ReleaseComASIODriver()
        {
            Marshal.Release(this.pASIOComObject);
        }

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

        [DllImport("ole32.Dll")]
        private static extern int CoCreateInstance(ref Guid clsid, IntPtr inner, uint context, ref Guid uuid, out IntPtr rReturnedComObject);

        private IntPtr pASIOComObject;

        private IntPtr pinnedcallbacks;

        private ASIODriver.ASIODriverVTable asioDriverVTable;

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        private class ASIODriverVTable
        {
            public ASIODriver.ASIODriverVTable.ASIOInit init;

            public ASIODriver.ASIODriverVTable.ASIOgetDriverName getDriverName;

            public ASIODriver.ASIODriverVTable.ASIOgetDriverVersion getDriverVersion;

            public ASIODriver.ASIODriverVTable.ASIOgetErrorMessage getErrorMessage;

            public ASIODriver.ASIODriverVTable.ASIOstart start;

            public ASIODriver.ASIODriverVTable.ASIOstop stop;

            public ASIODriver.ASIODriverVTable.ASIOgetChannels getChannels;

            public ASIODriver.ASIODriverVTable.ASIOgetLatencies getLatencies;

            public ASIODriver.ASIODriverVTable.ASIOgetBufferSize getBufferSize;

            public ASIODriver.ASIODriverVTable.ASIOcanSampleRate canSampleRate;

            public ASIODriver.ASIODriverVTable.ASIOgetSampleRate getSampleRate;

            public ASIODriver.ASIODriverVTable.ASIOsetSampleRate setSampleRate;

            public ASIODriver.ASIODriverVTable.ASIOgetClockSources getClockSources;

            public ASIODriver.ASIODriverVTable.ASIOsetClockSource setClockSource;

            public ASIODriver.ASIODriverVTable.ASIOgetSamplePosition getSamplePosition;

            public ASIODriver.ASIODriverVTable.ASIOgetChannelInfo getChannelInfo;

            public ASIODriver.ASIODriverVTable.ASIOcreateBuffers createBuffers;

            public ASIODriver.ASIODriverVTable.ASIOdisposeBuffers disposeBuffers;

            public ASIODriver.ASIODriverVTable.ASIOcontrolPanel controlPanel;

            public ASIODriver.ASIODriverVTable.ASIOfuture future;

            public ASIODriver.ASIODriverVTable.ASIOoutputReady outputReady;

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate int ASIOInit(IntPtr _pUnknown, IntPtr sysHandle);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate void ASIOgetDriverName(IntPtr _pUnknown, StringBuilder name);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate int ASIOgetDriverVersion(IntPtr _pUnknown);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate void ASIOgetErrorMessage(IntPtr _pUnknown, StringBuilder errorMessage);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOstart(IntPtr _pUnknown);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOstop(IntPtr _pUnknown);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOgetChannels(IntPtr _pUnknown, out int numInputChannels, out int numOutputChannels);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOgetLatencies(IntPtr _pUnknown, out int inputLatency, out int outputLatency);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOgetBufferSize(IntPtr _pUnknown, out int minSize, out int maxSize, out int preferredSize, out int granularity);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOcanSampleRate(IntPtr _pUnknown, double sampleRate);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOgetSampleRate(IntPtr _pUnknown, out double sampleRate);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOsetSampleRate(IntPtr _pUnknown, double sampleRate);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOgetClockSources(IntPtr _pUnknown, out long clocks, int numSources);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOsetClockSource(IntPtr _pUnknown, int reference);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOgetSamplePosition(IntPtr _pUnknown, out long samplePos, ref ASIO64Bit timeStamp);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOgetChannelInfo(IntPtr _pUnknown, ref ASIOChannelInfo info);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOcreateBuffers(IntPtr _pUnknown, IntPtr bufferInfos, int numChannels, int bufferSize, IntPtr callbacks);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOdisposeBuffers(IntPtr _pUnknown);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOcontrolPanel(IntPtr _pUnknown);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOfuture(IntPtr _pUnknown, int selector, IntPtr opt);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate ASIOError ASIOoutputReady(IntPtr _pUnknown);
        }
    }
}
