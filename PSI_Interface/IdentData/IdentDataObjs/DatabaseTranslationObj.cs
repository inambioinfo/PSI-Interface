using System;
using System.Collections.Generic;
using PSI_Interface.IdentData.mzIdentML;
using PSI_Interface.Utils;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML DatabaseTranslationType
    /// </summary>
    /// <remarks>A specification of how a nucleic acid sequence database was translated for searching.</remarks>
    public class DatabaseTranslationObj : IdentDataInternalTypeAbstract, IEquatable<DatabaseTranslationObj>
    {
        private IdentDataList<TranslationTableObj> _translationTables;

        /// <summary>
        ///     Constructor
        /// </summary>
        public DatabaseTranslationObj()
        {
            TranslationTables = new IdentDataList<TranslationTableObj>();
            Frames = new List<int>();
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="idata"></param>
        public DatabaseTranslationObj(DatabaseTranslationType dt, IdentDataObj idata)
            : base(idata)
        {
            _translationTables = null;
            Frames = null;

            if ((dt.TranslationTable != null) && (dt.TranslationTable.Count > 0))
            {
                TranslationTables = new IdentDataList<TranslationTableObj>();
                foreach (var t in dt.TranslationTable)
                    TranslationTables.Add(new TranslationTableObj(t, IdentData));
            }
            if (dt.frames != null)
                Frames = new List<int>(dt.frames);
        }

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<TranslationTableObj> TranslationTables
        {
            get { return _translationTables; }
            set
            {
                _translationTables = value;
                if (_translationTables != null)
                    _translationTables.IdentData = IdentData;
            }
        }

        /// <remarks>The frames in which the nucleic acid sequence has been translated as a space separated IdentDataList</remarks>
        /// Optional Attribute
        /// listOfAllowedFrames: space-separated string, valid values -3, -2, -1, 1, 2, 3
        public List<int> Frames { get; set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as DatabaseTranslationObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DatabaseTranslationObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (Equals(TranslationTables, other.TranslationTables) &&
                ListUtils.ListEqualsUnOrdered(Frames, other.Frames))
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
                var hashCode = TranslationTables != null ? TranslationTables.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Frames != null ? Frames.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}