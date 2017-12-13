using System;

namespace NAudio.Wave.Asio
{
    public enum AsioSampleType
    {
        Int16MSB,
        Int24MSB,
        Int32MSB,
        Float32MSB,
        Float64MSB,
        Int32MSB16 = 8,
        Int32MSB18,
        Int32MSB20,
        Int32MSB24,
        Int16LSB = 16,
        Int24LSB,
        Int32LSB,
        Float32LSB,
        Float64LSB,
        Int32LSB16 = 24,
        Int32LSB18,
        Int32LSB20,
        Int32LSB24,
        DSDInt8LSB1 = 32,
        DSDInt8MSB1,
        DSDInt8NER8 = 40
    }
}
