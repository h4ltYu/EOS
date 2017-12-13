using System;

namespace IRemote
{
    public interface IRemoteMonitorServer
    {
        int SaveScreenImage(byte[] img, int index, string examCode, string login);
    }
}
