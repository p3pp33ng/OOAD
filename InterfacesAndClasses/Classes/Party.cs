using System;

namespace AccountabilityLib
{
    public class Party : IEquatable<Party>
    {
        public int PartyId { get; set; }
        public string LegalId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsManager { get; set; }

        public bool Equals(Party other)
        {
            return this.PartyId.Equals(other.PartyId);
        }
    }
}
