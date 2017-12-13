using System;

namespace NAudio.Wave.Asio
{
    internal class ASIOSampleConvertor
    {
        public static ASIOSampleConvertor.SampleConvertor SelectSampleConvertor(WaveFormat waveFormat, AsioSampleType asioType)
        {
            ASIOSampleConvertor.SampleConvertor result = null;
            bool flag = waveFormat.Channels == 2;
            switch (asioType)
            {
                case AsioSampleType.Int16LSB:
                    {
                        int bitsPerSample = waveFormat.BitsPerSample;
                        if (bitsPerSample != 16)
                        {
                            if (bitsPerSample == 32)
                            {
                                result = (flag ? new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorFloatToShort2Channels) : new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorFloatToShortGeneric));
                            }
                        }
                        else
                        {
                            result = (flag ? new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorShortToShort2Channels) : new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorShortToShortGeneric));
                        }
                        break;
                    }
                case AsioSampleType.Int24LSB:
                    {
                        int bitsPerSample2 = waveFormat.BitsPerSample;
                        if (bitsPerSample2 == 16)
                        {
                            throw new ArgumentException("Not a supported conversion");
                        }
                        if (bitsPerSample2 == 32)
                        {
                            result = new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConverterFloatTo24LSBGeneric);
                        }
                        break;
                    }
                case AsioSampleType.Int32LSB:
                    {
                        int bitsPerSample3 = waveFormat.BitsPerSample;
                        if (bitsPerSample3 != 16)
                        {
                            if (bitsPerSample3 == 32)
                            {
                                result = (flag ? new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorFloatToInt2Channels) : new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorFloatToIntGeneric));
                            }
                        }
                        else
                        {
                            result = (flag ? new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorShortToInt2Channels) : new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorShortToIntGeneric));
                        }
                        break;
                    }
                case AsioSampleType.Float32LSB:
                    {
                        int bitsPerSample4 = waveFormat.BitsPerSample;
                        if (bitsPerSample4 == 16)
                        {
                            throw new ArgumentException("Not a supported conversion");
                        }
                        if (bitsPerSample4 == 32)
                        {
                            result = new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConverterFloatToFloatGeneric);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException(string.Format("ASIO Buffer Type {0} is not yet supported.", Enum.GetName(typeof(AsioSampleType), asioType)));
            }
            return result;
        }

        public unsafe static void ConvertorShortToInt2Channels(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
        {
            short* ptr = (short*)((void*)inputInterleavedBuffer);
            short* ptr2 = (short*)((void*)asioOutputBuffers[0]);
            short* ptr3 = (short*)((void*)asioOutputBuffers[1]);
            ptr2++;
            ptr3++;
            for (int i = 0; i < nbSamples; i++)
            {
                *ptr2 = *ptr;
                *ptr3 = ptr[1];
                ptr += 2;
                ptr2 += 2;
                ptr3 += 2;
            }
        }

        public unsafe static void ConvertorShortToIntGeneric(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
        {
            short* ptr = (short*)((void*)inputInterleavedBuffer);
            short*[] array = new short*[nbChannels];
            for (int i = 0; i < nbChannels; i++)
            {
                array[i] = (short*)((void*)asioOutputBuffers[i]);
                short*[] array2;
                IntPtr intPtr;
                (array2 = array)[(int)(intPtr = (IntPtr)i)] = array2[(int)intPtr] + 1;
            }
            for (int j = 0; j < nbSamples; j++)
            {
                for (int k = 0; k < nbChannels; k++)
                {
                    *array[k] = *(ptr++);
                    short*[] array3;
                    IntPtr intPtr2;
                    (array3 = array)[(int)(intPtr2 = (IntPtr)k)] = array3[(int)intPtr2] + 2;
                }
            }
        }

        public unsafe static void ConvertorFloatToInt2Channels(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
        {
            float* ptr = (float*)((void*)inputInterleavedBuffer);
            int* ptr2 = (int*)((void*)asioOutputBuffers[0]);
            int* ptr3 = (int*)((void*)asioOutputBuffers[1]);
            for (int i = 0; i < nbSamples; i++)
            {
                *(ptr2++) = ASIOSampleConvertor.clampToInt((double)(*ptr));
                *(ptr3++) = ASIOSampleConvertor.clampToInt((double)ptr[1]);
                ptr += 2;
            }
        }

        public unsafe static void ConvertorFloatToIntGeneric(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
        {
            float* ptr = (float*)((void*)inputInterleavedBuffer);
            int*[] array = new int*[nbChannels];
            for (int i = 0; i < nbChannels; i++)
            {
                array[i] = (int*)((void*)asioOutputBuffers[i]);
            }
            for (int j = 0; j < nbSamples; j++)
            {
                for (int k = 0; k < nbChannels; k++)
                {
                    int*[] array2;
                    IntPtr intPtr;
                    int* ptr2;
                    (array2 = array)[(int)(intPtr = (IntPtr)k)] = (ptr2 = array2[(int)intPtr]) + 1;
                    *ptr2 = ASIOSampleConvertor.clampToInt((double)(*(ptr++)));
                }
            }
        }

        public unsafe static void ConvertorShortToShort2Channels(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
        {
            short* ptr = (short*)((void*)inputInterleavedBuffer);
            short* ptr2 = (short*)((void*)asioOutputBuffers[0]);
            short* ptr3 = (short*)((void*)asioOutputBuffers[1]);
            for (int i = 0; i < nbSamples; i++)
            {
                *(ptr2++) = *ptr;
                *(ptr3++) = ptr[1];
                ptr += 2;
            }
        }

        public unsafe static void ConvertorShortToShortGeneric(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
        {
            short* ptr = (short*)((void*)inputInterleavedBuffer);
            short*[] array = new short*[nbChannels];
            for (int i = 0; i < nbChannels; i++)
            {
                array[i] = (short*)((void*)asioOutputBuffers[i]);
            }
            for (int j = 0; j < nbSamples; j++)
            {
                for (int k = 0; k < nbChannels; k++)
                {
                    short*[] array2;
                    IntPtr intPtr;
                    short* ptr2;
                    (array2 = array)[(int)(intPtr = (IntPtr)k)] = (ptr2 = array2[(int)intPtr]) + 1;
                    *ptr2 = *(ptr++);
                }
            }
        }

        public unsafe static void ConvertorFloatToShort2Channels(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
        {
            float* ptr = (float*)((void*)inputInterleavedBuffer);
            short* ptr2 = (short*)((void*)asioOutputBuffers[0]);
            short* ptr3 = (short*)((void*)asioOutputBuffers[1]);
            for (int i = 0; i < nbSamples; i++)
            {
                *(ptr2++) = ASIOSampleConvertor.clampToShort((double)(*ptr));
                *(ptr3++) = ASIOSampleConvertor.clampToShort((double)ptr[1]);
                ptr += 2;
            }
        }

        public unsafe static void ConvertorFloatToShortGeneric(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
        {
            float* ptr = (float*)((void*)inputInterleavedBuffer);
            short*[] array = new short*[nbChannels];
            for (int i = 0; i < nbChannels; i++)
            {
                array[i] = (short*)((void*)asioOutputBuffers[i]);
            }
            for (int j = 0; j < nbSamples; j++)
            {
                for (int k = 0; k < nbChannels; k++)
                {
                    short*[] array2;
                    IntPtr intPtr;
                    short* ptr2;
                    (array2 = array)[(int)(intPtr = (IntPtr)k)] = (ptr2 = array2[(int)intPtr]) + 1;
                    *ptr2 = ASIOSampleConvertor.clampToShort((double)(*(ptr++)));
                }
            }
        }

        public unsafe static void ConverterFloatTo24LSBGeneric(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
        {
            float* ptr = (float*)((void*)inputInterleavedBuffer);
            byte*[] array = new byte*[nbChannels];
            for (int i = 0; i < nbChannels; i++)
            {
                array[i] = (byte*)((void*)asioOutputBuffers[i]);
            }
            for (int j = 0; j < nbSamples; j++)
            {
                for (int k = 0; k < nbChannels; k++)
                {
                    int num = ASIOSampleConvertor.clampTo24Bit((double)(*(ptr++)));
                    byte*[] array2;
                    IntPtr intPtr;
                    byte* ptr2;
                    (array2 = array)[(int)(intPtr = (IntPtr)k)] = (ptr2 = array2[(int)intPtr]) + 1;
                    *ptr2 = (byte)num;
                    byte*[] array3;
                    IntPtr intPtr2;
                    byte* ptr3;
                    (array3 = array)[(int)(intPtr2 = (IntPtr)k)] = (ptr3 = array3[(int)intPtr2]) + 1;
                    *ptr3 = (byte)(num >> 8);
                    byte*[] array4;
                    IntPtr intPtr3;
                    byte* ptr4;
                    (array4 = array)[(int)(intPtr3 = (IntPtr)k)] = (ptr4 = array4[(int)intPtr3]) + 1;
                    *ptr4 = (byte)(num >> 16);
                }
            }
        }

        public unsafe static void ConverterFloatToFloatGeneric(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples)
        {
            float* ptr = (float*)((void*)inputInterleavedBuffer);
            float*[] array = new float*[nbChannels];
            for (int i = 0; i < nbChannels; i++)
            {
                array[i] = (float*)((void*)asioOutputBuffers[i]);
            }
            for (int j = 0; j < nbSamples; j++)
            {
                for (int k = 0; k < nbChannels; k++)
                {
                    float*[] array2;
                    IntPtr intPtr;
                    float* ptr2;
                    (array2 = array)[(int)(intPtr = (IntPtr)k)] = (ptr2 = array2[(int)intPtr]) + 1;
                    *ptr2 = *(ptr++);
                }
            }
        }

        private static int clampTo24Bit(double sampleValue)
        {
            sampleValue = ((sampleValue < -1.0) ? -1.0 : ((sampleValue > 1.0) ? 1.0 : sampleValue));
            return (int)(sampleValue * 8388607.0);
        }

        private static int clampToInt(double sampleValue)
        {
            sampleValue = ((sampleValue < -1.0) ? -1.0 : ((sampleValue > 1.0) ? 1.0 : sampleValue));
            return (int)(sampleValue * 2147483647.0);
        }

        private static short clampToShort(double sampleValue)
        {
            sampleValue = ((sampleValue < -1.0) ? -1.0 : ((sampleValue > 1.0) ? 1.0 : sampleValue));
            return (short)(sampleValue * 32767.0);
        }

        public delegate void SampleConvertor(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples);
    }
}
