using System;

namespace NAudio.SoundFont
{
    public class Zone
    {
        public override string ToString()
        {
            return string.Format("Zone {0} Gens:{1} {2} Mods:{3}", new object[]
            {
                this.generatorCount,
                this.generatorIndex,
                this.modulatorCount,
                this.modulatorIndex
            });
        }

        public Modulator[] Modulators
        {
            get
            {
                return this.modulators;
            }
            set
            {
                this.modulators = value;
            }
        }

        public Generator[] Generators
        {
            get
            {
                return this.generators;
            }
            set
            {
                this.generators = value;
            }
        }

        internal ushort generatorIndex;

        internal ushort modulatorIndex;

        internal ushort generatorCount;

        internal ushort modulatorCount;

        private Modulator[] modulators;

        private Generator[] generators;
    }
}
