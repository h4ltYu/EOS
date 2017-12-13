using System;
using QuestionLib;

namespace IRemote
{
    public interface IRemoteServer
    {
        EOSData ConductExam(RegisterData rd);

        SubmitStatus Submit(SubmitPaper submitPaper, ref string msg);
    }
}
