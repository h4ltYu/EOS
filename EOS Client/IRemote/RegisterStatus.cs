using System;

namespace IRemote
{
    public enum RegisterStatus
    {
        NEW,
        RE_ASSIGN,
        FINISHED,
        REGISTERED,
        REGISTER_ERROR,
        EXAM_CODE_NOT_EXISTS,
        NOT_ALLOW_MACHINE,
        NOT_ALLOW_STUDENT,
        LOGIN_FAILED
    }
}
