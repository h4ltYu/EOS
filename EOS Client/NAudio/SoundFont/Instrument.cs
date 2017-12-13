using System;

namespace NAudio.SoundFont
{
    public class Instrument
    {
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public Zone[] Zones
        {
            get
            {
                return this.zones;
            }
            set
            {
                this.zones = value;
            }
        }

        public override string ToString()
        {
            return this.name;
        }

        private string name;

        internal ushort startInstrumentZoneIndex;

        internal ushort endInstrumentZoneIndex;

        private Zone[] zones;
    }
}
