﻿using System;
using System.Collections.Generic;
using PSI_Interface.IdentData.mzIdentML;
using PSI_Interface.Utils;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML ModificationType
    /// </summary>
    /// <remarks>
    ///     A molecule modification specification. If n modifications have been found on a peptide, there should
    ///     be n instances of Modification. If multiple modifications are provided as cvParams, it is assumed that the
    ///     modification is ambiguous i.e. one modification or another. A cvParam must be provided with the identification
    ///     of the modification sourced from a suitable CV e.g. UNIMOD. If the modification is not present in the CV (and
    ///     this will be checked by the semantic validator within a given tolerance window), there is a â€œunknown
    ///     modificationâ€ CV term that must be used instead. A neutral loss should be defined as an additional CVParam
    ///     within Modification. If more complex information should be given about neutral losses (such as presence/absence
    ///     on particular product ions), this can additionally be encoded within the FragmentationArray.
    /// </remarks>
    /// <remarks>CV terms capturing the modification, sourced from an appropriate controlled vocabulary. min 1, max unbounded</remarks>
    public class ModificationObj : CVParamGroupObj, IEquatable<ModificationObj>
    {
        private double _avgMassDelta;

        //private IdentDataList<CVParamType> _cvParams;
        private int _location;
        private double _monoisotopicMassDelta;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ModificationObj()
        {
            _location = -1;
            LocationSpecified = false;
            _avgMassDelta = 0;
            AvgMassDeltaSpecified = false;
            _monoisotopicMassDelta = 0;
            MonoisotopicMassDeltaSpecified = false;

            Residues = new List<string>();
        }

        /// <summary>
        ///     Create a modification with the specified values
        /// </summary>
        /// <param name="unimodCv">CV term for the modification, if available; otherwise, use CVID.MS_unknown_modification</param>
        /// <param name="modificationName">
        ///     Name of the modification, if a CV term for the modification is not available or unknown.
        ///     If this matches an Unimod modification name, the Unimod CV term will be used.
        /// </param>
        /// <param name="location">
        ///     location of the modification, using '0' for N-term and length+1 for C-term, and otherwise
        ///     1-based indexing
        /// </param>
        /// <param name="monoMassDelta">monoisotopic mass delta</param>
        /// <returns></returns>
        public ModificationObj(CV.CV.CVID unimodCv, string modificationName = "", int location = int.MinValue, double monoMassDelta = double.NaN) : this()
        {
            if (!monoMassDelta.Equals(double.NaN))
            {
                MonoisotopicMassDelta = monoMassDelta;
            }
            if (location != int.MinValue)
            {
                Location = location;
            }

            if ((unimodCv != CV.CV.CVID.CVID_Unknown) && (unimodCv != CV.CV.CVID.MS_unknown_modification))
            {
                CVParams.Add(new CVParamObj(unimodCv));
            }
            else if (!string.IsNullOrWhiteSpace(modificationName))
            {
                var result = SearchForUnimodMod(modificationName);
                if (result != CV.CV.CVID.MS_unknown_modification)
                    CVParams.Add(new CVParamObj(result));
                else
                    CVParams.Add(new CVParamObj(CV.CV.CVID.MS_unknown_modification, modificationName));
            }
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="m"></param>
        /// <param name="idata"></param>
        public ModificationObj(ModificationType m, IdentDataObj idata)
            : base(m, idata)
        {
            _location = m.location;
            LocationSpecified = m.locationSpecified;
            _avgMassDelta = m.avgMassDelta;
            AvgMassDeltaSpecified = m.avgMassDeltaSpecified;
            _monoisotopicMassDelta = m.monoisotopicMassDelta;
            MonoisotopicMassDeltaSpecified = m.monoisotopicMassDeltaSpecified;

            Residues = null;

            if (m.residues != null)
                Residues = new List<string>(m.residues);
        }

        /// <remarks>
        ///     Location of the modification within the peptide - position in peptide sequence, counted from
        ///     the N-terminus residue, starting at position 1. Specific modifications to the N-terminus should be
        ///     given the location 0. Modification to the C-terminus should be given as peptide length + 1. If the
        ///     modification location is unknown e.g. for PMF data, this attribute should be omitted.
        /// </remarks>
        /// Optional Attribute
        /// integer
        public int Location
        {
            get { return _location; }
            set
            {
                _location = value;
                LocationSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool LocationSpecified { get; private set; }

        /// <remarks>
        ///     Specification of the residue (amino acid) on which the modification occurs. If multiple values
        ///     are given, it is assumed that the exact residue modified is unknown i.e. the modification is to ONE of
        ///     the residues listed. Multiple residues would usually only be specified for PMF data.
        /// </remarks>
        /// Optional Attribute
        /// listOfChars, string, space-separated regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
        public List<string> Residues { get; set; }

        /// <remarks>Atomic mass delta considering the natural distribution of isotopes in Daltons.</remarks>
        /// Optional Attribute
        /// double
        public double AvgMassDelta
        {
            get { return _avgMassDelta; }
            set
            {
                _avgMassDelta = value;
                AvgMassDeltaSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool AvgMassDeltaSpecified { get; private set; }

        /// <remarks>Atomic mass delta when assuming only the most common isotope of elements in Daltons.</remarks>
        /// Optional Attribute
        /// double
        public double MonoisotopicMassDelta
        {
            get { return _monoisotopicMassDelta; }
            set
            {
                _monoisotopicMassDelta = value;
                MonoisotopicMassDeltaSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool MonoisotopicMassDeltaSpecified { get; private set; }

        private CV.CV.CVID SearchForUnimodMod(string modName)
        {
            if (modName.ToLower().StartsWith("unimod"))
                modName = modName.Remove(0, 6);
            var nameSearch = modName.ToLower().TrimStart('_');
            CV.CV.CVID result;
            // First search for the name in the lookup
            if (!CV.CV.TermNameLookup["UNIMOD"].TryGetValue(nameSearch, out result))
            {
                // If searching for the name fails, try a last ditch attempt to parse it as an enum, before giving up
                modName = modName.Trim('_').Trim().Replace(' ', '_');
                var testEnum = "UNIMOD_" + modName;
                if (!Enum.TryParse(testEnum, true, out result))
                    result = CV.CV.CVID.MS_unknown_modification;
            }

            return result;
        }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as ModificationObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ModificationObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (AvgMassDelta.Equals(other.AvgMassDelta) &&
                MonoisotopicMassDelta.Equals(other.MonoisotopicMassDelta) && (Location == other.Location) &&
                ListUtils.ListEqualsUnOrdered(Residues, other.Residues) && Equals(CVParams, other.CVParams))
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
                var hashCode = AvgMassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ MonoisotopicMassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ Location;
                hashCode = (hashCode * 397) ^ (Residues != null ? Residues.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}