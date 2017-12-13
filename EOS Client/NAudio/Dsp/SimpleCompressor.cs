using System;
using NAudio.Utils;

namespace NAudio.Dsp
{
    internal class SimpleCompressor : AttRelEnvelope
    {
        public SimpleCompressor(double attackTime, double releaseTime, double sampleRate) : base(attackTime, releaseTime, sampleRate)
        {
            this.Threshold = 0.0;
            this.Ratio = 1.0;
            this.MakeUpGain = 0.0;
            this.envdB = 1E-25;
        }

        public SimpleCompressor() : base(10.0, 10.0, 44100.0)
        {
            this.Threshold = 0.0;
            this.Ratio = 1.0;
            this.MakeUpGain = 0.0;
            this.envdB = 1E-25;
        }

        public double MakeUpGain { get; set; }

        public double Threshold { get; set; }

        public double Ratio { get; set; }

        public void InitRuntime()
        {
            this.envdB = 1E-25;
        }

        public void Process(ref double in1, ref double in2)
        {
            double val = Math.Abs(in1);
            double val2 = Math.Abs(in2);
            double num = Math.Max(val, val2);
            num += 1E-25;
            double num2 = Decibels.LinearToDecibels(num);
            double num3 = num2 - this.Threshold;
            if (num3 < 0.0)
            {
                num3 = 0.0;
            }
            num3 += 1E-25;
            base.Run(num3, ref this.envdB);
            num3 = this.envdB - 1E-25;
            double num4 = num3 * (this.Ratio - 1.0);
            num4 = Decibels.DecibelsToLinear(num4) * Decibels.DecibelsToLinear(this.MakeUpGain);
            in1 *= num4;
            in2 *= num4;
        }

        private double envdB;
    }
}
