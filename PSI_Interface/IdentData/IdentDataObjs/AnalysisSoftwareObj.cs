using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML AnalysisSoftwareType : Containers AnalysisSoftwareListType
    /// </summary>
    /// <remarks>The software used for performing the analyses.</remarks>
    /// <remarks>AnalysisSoftwareListType: The software packages used to perform the analyses.</remarks>
    /// <remarks>AnalysisSoftwareListType: child element AnalysisSoftware of type AnalysisSoftwareType, min 1, max unbounded</remarks>
    public class AnalysisSoftwareObj : IdentDataInternalTypeAbstract, IIdentifiableType, IEquatable<AnalysisSoftwareObj>
    {
        private ContactRoleObj _contactRole;
        private ParamObj _softwareName;

        /// <summary>
        ///     Constructor
        /// </summary>
        public AnalysisSoftwareObj()
        {
            Id = null;
            Name = null;
            Customizations = null;
            Version = null;
            URI = null;

            _contactRole = null;
            _softwareName = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="asi"></param>
        /// <param name="idata"></param>
        public AnalysisSoftwareObj(AnalysisSoftwareType asi, IdentDataObj idata)
            : base(idata)
        {
            Id = asi.id;
            Name = asi.name;
            Customizations = asi.Customizations;
            Version = asi.version;
            URI = asi.uri;

            _contactRole = null;
            _softwareName = null;

            if (asi.ContactRole != null)
                _contactRole = new ContactRoleObj(asi.ContactRole, IdentData);
            if (asi.SoftwareName != null)
                _softwareName = new ParamObj(asi.SoftwareName, IdentData);
        }

        /// <remarks>The contact details of the organisation or person that produced the software</remarks>
        /// <remarks>min 0, max 1</remarks>
        public ContactRoleObj ContactRole
        {
            get { return _contactRole; }
            set
            {
                _contactRole = value;
                if (_contactRole != null)
                    _contactRole.IdentData = IdentData;
            }
        }

        /// <remarks>The name of the analysis software package, sourced from a CV if available.</remarks>
        /// <remarks>min 1, max 1</remarks>
        public ParamObj SoftwareName
        {
            get { return _softwareName; }
            set
            {
                _softwareName = value;
                if (_softwareName != null)
                    _softwareName.IdentData = IdentData;
            }
        }

        /// <remarks>
        ///     Any customizations to the software, such as alternative scoring mechanisms implemented, should be documented
        ///     here as free text.
        /// </remarks>
        /// <remarks>min 0, max 1</remarks>
        public string Customizations { get; set; }

        /// <remarks>The version of Software used.</remarks>
        /// Optional Attribute
        /// string
        public string Version { get; set; }

        /// <remarks>URI of the analysis software e.g. manufacturer's website</remarks>
        /// Optional Attribute
        /// anyURI
        public string URI { get; set; }

        /// <remarks>
        ///     An identifier is an unambiguous string that is unique within the scope
        ///     (i.e. a document, a set of related documents, or a repository) of its use.
        /// </remarks>
        /// Required Attribute
        /// string
        public string Id { get; set; }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name { get; set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as AnalysisSoftwareObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AnalysisSoftwareObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && (Customizations == other.Customizations) && (URI == other.URI) &&
                (Version == other.Version) && Equals(ContactRole, other.ContactRole) &&
                Equals(SoftwareName, other.SoftwareName))
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
                hashCode = (hashCode * 397) ^ (Customizations != null ? Customizations.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (URI != null ? URI.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Version != null ? Version.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ContactRole != null ? ContactRole.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SoftwareName != null ? SoftwareName.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}