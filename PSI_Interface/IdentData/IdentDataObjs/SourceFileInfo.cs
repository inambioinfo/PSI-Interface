using System;
using System.IO;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML SourceFileType
    /// </summary>
    /// <remarks>A file from which this mzIdentML instance was created.</remarks>
    /// <remarks>CVParams/UserParams: Any additional parameters description the source file.</remarks>
    public class SourceFileInfo : ParamGroupObj, IExternalDataType, IEquatable<SourceFileInfo>
    {
        private FileFormatInfo _fileFormat;

        /// <summary>
        ///     Constructor
        /// </summary>
        public SourceFileInfo()
        {
            Id = null;
            Name = null;
            ExternalFormatDocumentation = null;
            Location = null;

            _fileFormat = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="sf"></param>
        /// <param name="idata"></param>
        public SourceFileInfo(SourceFileType sf, IdentDataObj idata)
            : base(sf, idata)
        {
            Id = sf.id;
            Name = sf.name;
            ExternalFormatDocumentation = sf.ExternalFormatDocumentation;
            Location = sf.location;

            _fileFormat = null;

            if (sf.FileFormat != null)
                _fileFormat = new FileFormatInfo(sf.FileFormat, IdentData);
        }

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

        /// <remarks>
        ///     A URI to access documentation and tools to interpret the external format of the ExternalData instance.
        ///     For example, XML Schema or static libraries (APIs) to access binary formats.
        /// </remarks>
        /// <remarks>min 0, max 1</remarks>
        public string ExternalFormatDocumentation { get; set; }

        /// <remarks>min 0, max 1</remarks>
        public FileFormatInfo FileFormat
        {
            get { return _fileFormat; }
            set
            {
                _fileFormat = value;
                if (_fileFormat != null)
                    _fileFormat.IdentData = IdentData;
            }
        }

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        public string Location { get; set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as SourceFileInfo;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SourceFileInfo other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && (ExternalFormatDocumentation == other.ExternalFormatDocumentation) &&
                (Path.GetFileName(Location) == Path.GetFileName(other.Location)) && Equals(FileFormat, other.FileFormat) &&
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
                hashCode = (hashCode * 397) ^ (ExternalFormatDocumentation != null ? ExternalFormatDocumentation.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Location != null ? Location.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FileFormat != null ? FileFormat.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}