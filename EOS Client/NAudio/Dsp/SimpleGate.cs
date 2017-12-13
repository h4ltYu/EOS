using System;
using NAudio.Utils;

namespace NAudio.Dsp
{
    internal class SimpleGate : AttRelEnvelope
    {
        public SimpleGate() : base(10.0, 10.0, 44100.0)
        {
            this.threshdB = 0.0;
            this.thresh = 1.0;
            this.env = 1E-25;
        }

        public void Process(ref double in1, ref double in2)
        {
            double val = Math.Abs(in1);
            double val2 = Math.Abs(in2);
            double num = Math.Max(val, val2);
            double num2 = (num > this.thresh) ? 1.0 : 0.0;
            num2 += 1E-25;
            base.Run(num2, ref this.env);
            num2 = this.env - 1E-25;
            in1 *= num2;
            in2 *= num2;
        }

        public double Threshold
        {
            get
            {
                return this.threshdB;
            }
            set
            {
                this.threshdB = value;
                this.thresh = Decibels.DecibelsToLinear(value);
            }
        }

        private double threshdB;

        private double thresh;

        private double env;
    }
}
