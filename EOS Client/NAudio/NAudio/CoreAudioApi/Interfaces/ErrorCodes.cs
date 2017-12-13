using System;
using NAudio.Utils;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000027 RID: 39
	internal static class ErrorCodes
	{
		// Token: 0x04000083 RID: 131
		private const int SEVERITY_ERROR = 1;

		// Token: 0x04000084 RID: 132
		private const int FACILITY_AUDCLNT = 2185;

		// Token: 0x04000085 RID: 133
		private static readonly int AUDCLNT_E_NOT_INITIALIZED = HResult.MAKE_HRESULT(1, 2185, 1);

		// Token: 0x04000086 RID: 134
		private static readonly int AUDCLNT_E_ALREADY_INITIALIZED = HResult.MAKE_HRESULT(1, 2185, 2);

		// Token: 0x04000087 RID: 135
		private static readonly int AUDCLNT_E_WRONG_ENDPOINT_TYPE = HResult.MAKE_HRESULT(1, 2185, 3);

		// Token: 0x04000088 RID: 136
		private static readonly int AUDCLNT_E_DEVICE_INVALIDATED = HResult.MAKE_HRESULT(1, 2185, 4);

		// Token: 0x04000089 RID: 137
		private static readonly int AUDCLNT_E_NOT_STOPPED = HResult.MAKE_HRESULT(1, 2185, 5);

		// Token: 0x0400008A RID: 138
		private static readonly int AUDCLNT_E_BUFFER_TOO_LARGE = HResult.MAKE_HRESULT(1, 2185, 6);

		// Token: 0x0400008B RID: 139
		private static readonly int AUDCLNT_E_OUT_OF_ORDER = HResult.MAKE_HRESULT(1, 2185, 7);

		// Token: 0x0400008C RID: 140
		private static readonly int AUDCLNT_E_UNSUPPORTED_FORMAT = HResult.MAKE_HRESULT(1, 2185, 8);

		// Token: 0x0400008D RID: 141
		private static readonly int AUDCLNT_E_INVALID_SIZE = HResult.MAKE_HRESULT(1, 2185, 9);

		// Token: 0x0400008E RID: 142
		private static readonly int AUDCLNT_E_DEVICE_IN_USE = HResult.MAKE_HRESULT(1, 2185, 10);

		// Token: 0x0400008F RID: 143
		private static readonly int AUDCLNT_E_BUFFER_OPERATION_PENDING = HResult.MAKE_HRESULT(1, 2185, 11);

		// Token: 0x04000090 RID: 144
		private static readonly int AUDCLNT_E_THREAD_NOT_REGISTERED = HResult.MAKE_HRESULT(1, 2185, 12);

		// Token: 0x04000091 RID: 145
		private static readonly int AUDCLNT_E_EXCLUSIVE_MODE_NOT_ALLOWED = HResult.MAKE_HRESULT(1, 2185, 14);

		// Token: 0x04000092 RID: 146
		private static readonly int AUDCLNT_E_ENDPOINT_CREATE_FAILED = HResult.MAKE_HRESULT(1, 2185, 15);

		// Token: 0x04000093 RID: 147
		private static readonly int AUDCLNT_E_SERVICE_NOT_RUNNING = HResult.MAKE_HRESULT(1, 2185, 16);

		// Token: 0x04000094 RID: 148
		private static readonly int AUDCLNT_E_EVENTHANDLE_NOT_EXPECTED = HResult.MAKE_HRESULT(1, 2185, 17);

		// Token: 0x04000095 RID: 149
		private static readonly int AUDCLNT_E_EXCLUSIVE_MODE_ONLY = HResult.MAKE_HRESULT(1, 2185, 18);

		// Token: 0x04000096 RID: 150
		private static readonly int AUDCLNT_E_BUFDURATION_PERIOD_NOT_EQUAL = HResult.MAKE_HRESULT(1, 2185, 19);

		// Token: 0x04000097 RID: 151
		private static readonly int AUDCLNT_E_EVENTHANDLE_NOT_SET = HResult.MAKE_HRESULT(1, 2185, 20);

		// Token: 0x04000098 RID: 152
		private static readonly int AUDCLNT_E_INCORRECT_BUFFER_SIZE = HResult.MAKE_HRESULT(1, 2185, 21);

		// Token: 0x04000099 RID: 153
		private static readonly int AUDCLNT_E_BUFFER_SIZE_ERROR = HResult.MAKE_HRESULT(1, 2185, 22);

		// Token: 0x0400009A RID: 154
		private static readonly int AUDCLNT_E_CPUUSAGE_EXCEEDED = HResult.MAKE_HRESULT(1, 2185, 23);
	}
}
