using System;

namespace NAudio.Wave.Asio
{
	/// <summary>
	/// This class stores convertors for different interleaved WaveFormat to ASIOSampleType separate channel
	/// format.
	/// </summary>
	// Token: 0x02000150 RID: 336
	internal class ASIOSampleConvertor
	{
		/// <summary>
		/// Selects the sample convertor based on the input WaveFormat and the output ASIOSampleTtype.
		/// </summary>
		/// <param name="waveFormat">The wave format.</param>
		/// <param name="asioType">The type.</param>
		/// <returns></returns>
		// Token: 0x0600075B RID: 1883 RVA: 0x00015E24 File Offset: 0x00014024
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

		/// <summary>
		/// Optimized convertor for 2 channels SHORT
		/// </summary>
		// Token: 0x0600075C RID: 1884 RVA: 0x00015FA0 File Offset: 0x000141A0
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

		/// <summary>
		/// Generic convertor for SHORT
		/// </summary>
		// Token: 0x0600075D RID: 1885 RVA: 0x00016008 File Offset: 0x00014208
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

		/// <summary>
		/// Optimized convertor for 2 channels FLOAT
		/// </summary>
		// Token: 0x0600075E RID: 1886 RVA: 0x00016090 File Offset: 0x00014290
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

		/// <summary>
		/// Generic convertor SHORT
		/// </summary>
		// Token: 0x0600075F RID: 1887 RVA: 0x000160FC File Offset: 0x000142FC
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

		/// <summary>
		/// Optimized convertor for 2 channels SHORT
		/// </summary>
		// Token: 0x06000760 RID: 1888 RVA: 0x00016178 File Offset: 0x00014378
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

		/// <summary>
		/// Generic convertor for SHORT
		/// </summary>
		// Token: 0x06000761 RID: 1889 RVA: 0x000161D8 File Offset: 0x000143D8
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

		/// <summary>
		/// Optimized convertor for 2 channels FLOAT
		/// </summary>
		// Token: 0x06000762 RID: 1890 RVA: 0x00016250 File Offset: 0x00014450
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

		/// <summary>
		/// Generic convertor SHORT
		/// </summary>
		// Token: 0x06000763 RID: 1891 RVA: 0x000162BC File Offset: 0x000144BC
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

		/// <summary>
		/// Generic converter 24 LSB
		/// </summary>
		// Token: 0x06000764 RID: 1892 RVA: 0x00016338 File Offset: 0x00014538
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

		/// <summary>
		/// Generic convertor for float
		/// </summary>
		// Token: 0x06000765 RID: 1893 RVA: 0x000163F4 File Offset: 0x000145F4
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

		// Token: 0x06000766 RID: 1894 RVA: 0x0001646A File Offset: 0x0001466A
		private static int clampTo24Bit(double sampleValue)
		{
			sampleValue = ((sampleValue < -1.0) ? -1.0 : ((sampleValue > 1.0) ? 1.0 : sampleValue));
			return (int)(sampleValue * 8388607.0);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000164A9 File Offset: 0x000146A9
		private static int clampToInt(double sampleValue)
		{
			sampleValue = ((sampleValue < -1.0) ? -1.0 : ((sampleValue > 1.0) ? 1.0 : sampleValue));
			return (int)(sampleValue * 2147483647.0);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000164E8 File Offset: 0x000146E8
		private static short clampToShort(double sampleValue)
		{
			sampleValue = ((sampleValue < -1.0) ? -1.0 : ((sampleValue > 1.0) ? 1.0 : sampleValue));
			return (short)(sampleValue * 32767.0);
		}

		// Token: 0x02000151 RID: 337
		// (Invoke) Token: 0x0600076B RID: 1899
		public delegate void SampleConvertor(IntPtr inputInterleavedBuffer, IntPtr[] asioOutputBuffers, int nbChannels, int nbSamples);
	}
}
