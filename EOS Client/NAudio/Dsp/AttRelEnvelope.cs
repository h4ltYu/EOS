using System;

namespace NAudio.Dsp
{
    internal class AttRelEnvelope
    {
        public AttRelEnvelope(double attackMilliseconds, double releaseMilliseconds, double sampleRate)
        {
            this.attack = new EnvelopeDetector(attackMilliseconds, sampleRate);
            this.release = new EnvelopeDetector(releaseMilliseconds, sampleRate);
        }

        public double Attack
        {
            get
            {
                return this.attack.TimeConstant;
            }
            set
            {
                this.attack.TimeConstant = value;
            }
        }

        public double Release
        {
            get
            {
                return this.release.TimeConstant;
            }
            set
            {
                this.release.TimeConstant = value;
            }
        }

        public double SampleRate
        {
            get
            {
                return this.attack.SampleRate;
            }
            set
            {
                EnvelopeDetector envelopeDetector = this.attack;
                this.release.SampleRate = value;
                envelopeDetector.SampleRate = value;
            }
        }

        public void Run(double inValue, ref double state)
        {
            if (inValue > state)
            {
                this.attack.Run(inValue, ref state);
                return;
            }
            this.release.Run(inValue, ref state);
        }

        protected const double DC_OFFSET = 1E-25;

        private readonly EnvelopeDetector attack;

        private readonly EnvelopeDetector release;
    }
}
