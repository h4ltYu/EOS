using System;

namespace NAudio.Dmo
{
    internal struct DmoPartialMediaType
    {
        public Guid Type
        {
            get
            {
                return this.type;
            }
            internal set
            {
                this.type = value;
            }
        }

        public Guid Subtype
        {
            get
            {
                return this.subtype;
            }
            internal set
            {
                this.subtype = value;
            }
        }

        private Guid type;

        private Guid subtype;
    }
}
