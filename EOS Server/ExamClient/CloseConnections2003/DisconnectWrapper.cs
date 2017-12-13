using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CloseConnections2003
{
    public class DisconnectWrapper
    {
        [DllImport("iphlpapi.dll")]
        private static extern int GetTcpTable(IntPtr pTcpTable, ref int pdwSize, bool bOrder);

        [DllImport("iphlpapi.dll")]
        private static extern int SetTcpEntry(IntPtr pTcprow);

        [DllImport("wsock32.dll")]
        private static extern int ntohs(int netshort);

        [DllImport("wsock32.dll")]
        private static extern int htons(int netshort);

        public static void CloseRemoteIP(string IP)
        {
            DisconnectWrapper.ConnectionInfo[] tcpTable = DisconnectWrapper.getTcpTable();
            for (int i = 0; i < tcpTable.Length; i++)
            {
                if (tcpTable[i].dwRemoteAddr == DisconnectWrapper.IPStringToInt(IP))
                {
                    tcpTable[i].dwState = 12;
                    IntPtr ptrToNewObject = DisconnectWrapper.GetPtrToNewObject(tcpTable[i]);
                    int num = DisconnectWrapper.SetTcpEntry(ptrToNewObject);
                }
            }
        }

        public static void CloseLocalIP(string IP)
        {
            DisconnectWrapper.ConnectionInfo[] tcpTable = DisconnectWrapper.getTcpTable();
            for (int i = 0; i < tcpTable.Length; i++)
            {
                if (tcpTable[i].dwLocalAddr == DisconnectWrapper.IPStringToInt(IP))
                {
                    tcpTable[i].dwState = 12;
                    IntPtr ptrToNewObject = DisconnectWrapper.GetPtrToNewObject(tcpTable[i]);
                    int num = DisconnectWrapper.SetTcpEntry(ptrToNewObject);
                }
            }
        }

        public static void CloseRemotePort(int port)
        {
            DisconnectWrapper.ConnectionInfo[] tcpTable = DisconnectWrapper.getTcpTable();
            for (int i = 0; i < tcpTable.Length; i++)
            {
                if (port == DisconnectWrapper.ntohs(tcpTable[i].dwRemotePort))
                {
                    tcpTable[i].dwState = 12;
                    IntPtr ptrToNewObject = DisconnectWrapper.GetPtrToNewObject(tcpTable[i]);
                    int num = DisconnectWrapper.SetTcpEntry(ptrToNewObject);
                }
            }
        }

        public static void CloseLocalPort(int port)
        {
            DisconnectWrapper.ConnectionInfo[] tcpTable = DisconnectWrapper.getTcpTable();
            for (int i = 0; i < tcpTable.Length; i++)
            {
                if (port == DisconnectWrapper.ntohs(tcpTable[i].dwLocalPort))
                {
                    tcpTable[i].dwState = 12;
                    IntPtr ptrToNewObject = DisconnectWrapper.GetPtrToNewObject(tcpTable[i]);
                    int num = DisconnectWrapper.SetTcpEntry(ptrToNewObject);
                }
            }
        }

        public static void CloseConnection(string connectionstring)
        {
            try
            {
                string[] array = connectionstring.Split(new char[]
                {
                    '-'
                });
                if (array.Length != 4)
                {
                    throw new Exception("Invalid connectionstring - use the one provided by Connections.");
                }
                string[] array2 = array[0].Split(new char[]
                {
                    ':'
                });
                string[] array3 = array[1].Split(new char[]
                {
                    ':'
                });
                string[] array4 = array2[0].Split(new char[]
                {
                    '.'
                });
                string[] array5 = array3[0].Split(new char[]
                {
                    '.'
                });
                DisconnectWrapper.ConnectionInfo connectionInfo = default(DisconnectWrapper.ConnectionInfo);
                connectionInfo.dwState = 12;
                byte[] value = new byte[]
                {
                    byte.Parse(array4[0]),
                    byte.Parse(array4[1]),
                    byte.Parse(array4[2]),
                    byte.Parse(array4[3])
                };
                byte[] value2 = new byte[]
                {
                    byte.Parse(array5[0]),
                    byte.Parse(array5[1]),
                    byte.Parse(array5[2]),
                    byte.Parse(array5[3])
                };
                connectionInfo.dwLocalAddr = BitConverter.ToInt32(value, 0);
                connectionInfo.dwRemoteAddr = BitConverter.ToInt32(value2, 0);
                connectionInfo.dwLocalPort = DisconnectWrapper.htons(int.Parse(array2[1]));
                connectionInfo.dwRemotePort = DisconnectWrapper.htons(int.Parse(array3[1]));
                IntPtr ptrToNewObject = DisconnectWrapper.GetPtrToNewObject(connectionInfo);
                int num = DisconnectWrapper.SetTcpEntry(ptrToNewObject);
                if (num == -1)
                {
                    throw new Exception("Unsuccessful");
                }
                if (num == 65)
                {
                    throw new Exception("User has no sufficient privilege to execute this API successfully");
                }
                if (num == 87)
                {
                    throw new Exception("Specified port is not in state to be closed down");
                }
                if (num != 0)
                {
                    throw new Exception("Unknown error (" + num + ")");
                }
            }
            catch
            {
            }
        }

        public static string[] Connections()
        {
            return DisconnectWrapper.Connections(DisconnectWrapper.ConnectionState.All);
        }

        public static string[] Connections(DisconnectWrapper.ConnectionState state)
        {
            DisconnectWrapper.ConnectionInfo[] tcpTable = DisconnectWrapper.getTcpTable();
            ArrayList arrayList = new ArrayList();
            foreach (DisconnectWrapper.ConnectionInfo connectionInfo in tcpTable)
            {
                if (state == DisconnectWrapper.ConnectionState.All || state == (DisconnectWrapper.ConnectionState)connectionInfo.dwState)
                {
                    string text = DisconnectWrapper.IPIntToString(connectionInfo.dwLocalAddr) + ":" + DisconnectWrapper.ntohs(connectionInfo.dwLocalPort);
                    string text2 = DisconnectWrapper.IPIntToString(connectionInfo.dwRemoteAddr) + ":" + DisconnectWrapper.ntohs(connectionInfo.dwRemotePort);
                    arrayList.Add(string.Concat(new object[]
                    {
                        text,
                        "-",
                        text2,
                        "-",
                        ((DisconnectWrapper.ConnectionState)connectionInfo.dwState).ToString(),
                        "-",
                        connectionInfo.dwState
                    }));
                }
            }
            return (string[])arrayList.ToArray(typeof(string));
        }

        private static DisconnectWrapper.ConnectionInfo[] getTcpTable()
        {
            IntPtr intPtr = IntPtr.Zero;
            bool flag = false;
            DisconnectWrapper.ConnectionInfo[] result;
            try
            {
                int cb = 0;
                DisconnectWrapper.GetTcpTable(IntPtr.Zero, ref cb, false);
                intPtr = Marshal.AllocCoTaskMem(cb);
                flag = true;
                DisconnectWrapper.GetTcpTable(intPtr, ref cb, false);
                int num = Marshal.ReadInt32(intPtr);
                IntPtr intPtr2 = (IntPtr)((int)intPtr + 4);
                DisconnectWrapper.ConnectionInfo[] array = new DisconnectWrapper.ConnectionInfo[num];
                int num2 = Marshal.SizeOf(default(DisconnectWrapper.ConnectionInfo));
                for (int i = 0; i < num; i++)
                {
                    array[i] = (DisconnectWrapper.ConnectionInfo)Marshal.PtrToStructure(intPtr2, typeof(DisconnectWrapper.ConnectionInfo));
                    intPtr2 = (IntPtr)((int)intPtr2 + num2);
                }
                result = array;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat(new string[]
                {
                    "getTcpTable failed! [",
                    ex.GetType().ToString(),
                    ",",
                    ex.Message,
                    "]"
                }));
            }
            finally
            {
                if (flag)
                {
                    Marshal.FreeCoTaskMem(intPtr);
                }
            }
            return result;
        }

        private static IntPtr GetPtrToNewObject(object obj)
        {
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(obj));
            Marshal.StructureToPtr(obj, intPtr, false);
            return intPtr;
        }

        private static int IPStringToInt(string IP)
        {
            if (IP.IndexOf(".") < 0)
            {
                throw new Exception("Invalid IP address");
            }
            string[] array = IP.Split(new char[]
            {
                '.'
            });
            if (array.Length != 4)
            {
                throw new Exception("Invalid IP address");
            }
            byte[] value = new byte[]
            {
                byte.Parse(array[0]),
                byte.Parse(array[1]),
                byte.Parse(array[2]),
                byte.Parse(array[3])
            };
            return BitConverter.ToInt32(value, 0);
        }

        private static string IPIntToString(int IP)
        {
            byte[] bytes = BitConverter.GetBytes(IP);
            return string.Concat(new object[]
            {
                bytes[0],
                ".",
                bytes[1],
                ".",
                bytes[2],
                ".",
                bytes[3]
            });
        }

        public enum ConnectionState
        {
            All,
            Closed,
            Listen,
            Syn_Sent,
            Syn_Rcvd,
            Established,
            Fin_Wait1,
            Fin_Wait2,
            Close_Wait,
            Closing,
            Last_Ack,
            Time_Wait,
            Delete_TCB
        }

        private struct ConnectionInfo
        {
            public int dwState;

            public int dwLocalAddr;

            public int dwLocalPort;

            public int dwRemoteAddr;

            public int dwRemotePort;
        }
    }
}
