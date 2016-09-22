using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML FilterType : Containers DatabaseFiltersType
    /// </summary>
    /// <remarks>
    ///     Filters applied to the search database. The filter must include at least one of Include and Exclude.
    ///     If both are used, it is assumed that inclusion is performed first.
    /// </remarks>
    /// <remarks>DatabaseFiltersType: The specification of filters applied to the database searched.</remarks>
    /// <remarks>DatabaseFiltersType: child element Filter, of type FilterType, min 1, max unbounded</remarks>
    public class FilterInfo : IdentDataInternalTypeAbstract, IEquatable<FilterInfo>
    {
        private ParamListObj _exclude;

        private ParamObj _filterType;
        private ParamListObj _include;

        /// <summary>
        ///     Constructor
        /// </summary>
        public FilterInfo()
        {
            _filterType = null;
            _include = null;
            _exclude = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="f"></param>
        /// <param name="idata"></param>
        public FilterInfo(FilterType f, IdentDataObj idata)
            : base(idata)
        {
            _filterType = null;
            _include = null;
            _exclude = null;

            if (f.FilterType1 != null)
                _filterType = new ParamObj(f.FilterType1, IdentData);
            if (f.Include != null)
                _include = new ParamListObj(f.Include, IdentData);
            if (f.Exclude != null)
                _exclude = new ParamListObj(f.Exclude, IdentData);
        }

        /// <remarks>The type of filter e.g. database taxonomy filter, pi filter, mw filter</remarks>
        /// <remarks>min 1, max 1</remarks>
        public ParamObj FilterType
        {
            get { return _filterType; }
            set
            {
                _filterType = value;
                if (_filterType != null)
                    _filterType.IdentData = IdentData;
            }
        }

        /// <remarks>All sequences fulfilling the specifed criteria are included.</remarks>
        /// <remarks>min 0, max 1</remarks>
        public ParamListObj Include
        {
            get { return _include; }
            set
            {
                _include = value;
                if (_include != null)
                    _include.IdentData = IdentData;
            }
        }

        /// <remarks>All sequences fulfilling the specifed criteria are excluded.</remarks>
        /// <remarks>min 0, max 1</remarks>
        public ParamListObj Exclude
        {
            get { return _exclude; }
            set
            {
                _exclude = value;
                if (_exclude != null)
                    _exclude.IdentData = IdentData;
            }
        }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as FilterInfo;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(FilterInfo other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (Equals(FilterType, other.FilterType) && Equals(Include, other.Include) &&
                Equals(Exclude, other.Exclude))
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
                var hashCode = FilterType != null ? FilterType.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Include != null ? Include.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Exclude != null ? Exclude.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}