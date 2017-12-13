using System;
using NAudio.Utils;

namespace NAudio.CoreAudioApi.Interfaces
{
    internal static class ErrorCodes
    {
        private const int SEVERITY_ERROR = 1;

        private const int FACILITY_AUDCLNT = 2185;

        private static readonly int AUDCLNT_E_NOT_INITIALIZED = HResult.MAKE_HRESULT(1, 2185, 1);

        private static readonly int AUDCLNT_E_ALREADY_INITIALIZED = HResult.MAKE_HRESULT(1, 2185, 2);

        private static readonly int AUDCLNT_E_WRONG_ENDPOINT_TYPE = HResult.MAKE_HRESULT(1, 2185, 3);

        private static readonly int AUDCLNT_E_DEVICE_INVALIDATED = HResult.MAKE_HRESULT(1, 2185, 4);

        private static readonly int AUDCLNT_E_NOT_STOPPED = HResult.MAKE_HRESULT(1, 2185, 5);

        private static readonly int AUDCLNT_E_BUFFER_TOO_LARGE = HResult.MAKE_HRESULT(1, 2185, 6);

        private static readonly int AUDCLNT_E_OUT_OF_ORDER = HResult.MAKE_HRESULT(1, 2185, 7);

        private static readonly int AUDCLNT_E_UNSUPPORTED_FORMAT = HResult.MAKE_HRESULT(1, 2185, 8);

        private static readonly int AUDCLNT_E_INVALID_SIZE = HResult.MAKE_HRESULT(1, 2185, 9);

        private static readonly int AUDCLNT_E_DEVICE_IN_USE = HResult.MAKE_HRESULT(1, 2185, 10);

        private static readonly int AUDCLNT_E_BUFFER_OPERATION_PENDING = HResult.MAKE_HRESULT(1, 2185, 11);

        private static readonly int AUDCLNT_E_THREAD_NOT_REGISTERED = HResult.MAKE_HRESULT(1, 2185, 12);

        private static readonly int AUDCLNT_E_EXCLUSIVE_MODE_NOT_ALLOWED = HResult.MAKE_HRESULT(1, 2185, 14);

        private static readonly int AUDCLNT_E_ENDPOINT_CREATE_FAILED = HResult.MAKE_HRESULT(1, 2185, 15);

        private static readonly int AUDCLNT_E_SERVICE_NOT_RUNNING = HResult.MAKE_HRESULT(1, 2185, 16);

        private static readonly int AUDCLNT_E_EVENTHANDLE_NOT_EXPECTED = HResult.MAKE_HRESULT(1, 2185, 17);

        private static readonly int AUDCLNT_E_EXCLUSIVE_MODE_ONLY = HResult.MAKE_HRESULT(1, 2185, 18);

        private static readonly int AUDCLNT_E_BUFDURATION_PERIOD_NOT_EQUAL = HResult.MAKE_HRESULT(1, 2185, 19);

        private static readonly int AUDCLNT_E_EVENTHANDLE_NOT_SET = HResult.MAKE_HRESULT(1, 2185, 20);

        private static readonly int AUDCLNT_E_INCORRECT_BUFFER_SIZE = HResult.MAKE_HRESULT(1, 2185, 21);

        private static readonly int AUDCLNT_E_BUFFER_SIZE_ERROR = HResult.MAKE_HRESULT(1, 2185, 22);

        private static readonly int AUDCLNT_E_CPUUSAGE_EXCEEDED = HResult.MAKE_HRESULT(1, 2185, 23);
    }
}
