using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML PersonType
    /// </summary>
    /// <remarks>
    ///     A person's name and contact details. Any additional information such as the address,
    ///     contact email etc. should be supplied using CV parameters or user parameters.
    /// </remarks>
    public class PersonObj : AbstractContactObj, IEquatable<PersonObj>
    {
        private IdentDataList<AffiliationObj> _affiliations;

        /// <summary>
        ///     Constructor
        /// </summary>
        public PersonObj()
        {
            LastName = null;
            FirstName = null;
            MidInitials = null;

            Affiliations = new IdentDataList<AffiliationObj>();
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="p"></param>
        /// <param name="idata"></param>
        public PersonObj(PersonType p, IdentDataObj idata)
            : base(p, idata)
        {
            LastName = p.lastName;
            FirstName = p.firstName;
            MidInitials = p.midInitials;

            _affiliations = null;

            if ((p.Affiliation != null) && (p.Affiliation.Count > 0))
            {
                Affiliations = new IdentDataList<AffiliationObj>();
                foreach (var a in p.Affiliation)
                    Affiliations.Add(new AffiliationObj(a, IdentData));
            }
        }

        /// <remarks>The organization a person belongs to.</remarks>
        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<AffiliationObj> Affiliations
        {
            get { return _affiliations; }
            set
            {
                _affiliations = value;
                if (_affiliations != null)
                    _affiliations.IdentData = IdentData;
            }
        }

        /// <remarks>The Person's last/family name.</remarks>
        /// Optional Attribute
        public string LastName { get; set; }

        /// <remarks>The Person's first name.</remarks>
        /// Optional Attribute
        public string FirstName { get; set; }

        /// <remarks>The Person's middle initial.</remarks>
        /// Optional Attribute
        public string MidInitials { get; set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as PersonObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PersonObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && (LastName == other.LastName) && (FirstName == other.FirstName) &&
                (MidInitials == other.MidInitials) && Equals(Affiliations, other.Affiliations) &&
                Equals(CVParams, other.CVParams) && Equals(UserParams, other.UserParams))
                return true;
            return false;
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MidInitials != null ? MidInitials.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Affiliations != null ? Affiliations.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}