using System;

namespace NAudio.Wave.SampleProviders
{
    public class SinPanStrategy : IPanStrategy
    {
        public StereoSamplePair GetMultipliers(float pan)
        {
            float num = (-pan + 1f) / 2f;
            float left = (float)Math.Sin((double)(num * 1.57079637f));
            float right = (float)Math.Cos((double)(num * 1.57079637f));
            return new StereoSamplePair
            {
                Left = left,
                Right = right
            };
        }

        private const float HalfPi = 1.57079637f;
    }
}
