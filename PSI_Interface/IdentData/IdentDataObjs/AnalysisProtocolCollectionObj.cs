using System;
using System.Linq;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML AnalysisProtocolCollectionType
    /// </summary>
    /// <remarks>The collection of protocols which include the parameters and settings of the performed analyses.</remarks>
    public class AnalysisProtocolCollectionObj : IdentDataInternalTypeAbstract,
        IEquatable<AnalysisProtocolCollectionObj>
    {
        private static long _idCounter;
        private ProteinDetectionProtocolObj _proteinDetectionProtocol;

        private IdentDataList<SpectrumIdentificationProtocolObj> _spectrumIdentificationProtocols;

        /// <summary>
        ///     Constructor
        /// </summary>
        public AnalysisProtocolCollectionObj()
        {
            SpectrumIdentificationProtocols = new IdentDataList<SpectrumIdentificationProtocolObj>();
            _proteinDetectionProtocol = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="apc"></param>
        /// <param name="idata"></param>
        public AnalysisProtocolCollectionObj(AnalysisProtocolCollectionType apc, IdentDataObj idata)
            : base(idata)
        {
            _spectrumIdentificationProtocols = null;
            _proteinDetectionProtocol = null;

            idata.AnalysisProtocolCollection = this;

            if (apc.SpectrumIdentificationProtocol != null && apc.SpectrumIdentificationProtocol.Count > 0)
            {
                SpectrumIdentificationProtocols = new IdentDataList<SpectrumIdentificationProtocolObj>();
                foreach (var sip in apc.SpectrumIdentificationProtocol)
                {
                    SpectrumIdentificationProtocols.Add(new SpectrumIdentificationProtocolObj(sip, IdentData));
                }
            }
            if (apc.ProteinDetectionProtocol != null)
            {
                _proteinDetectionProtocol = new ProteinDetectionProtocolObj(apc.ProteinDetectionProtocol, IdentData);
            }
        }

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SpectrumIdentificationProtocolObj> SpectrumIdentificationProtocols
        {
            get { return _spectrumIdentificationProtocols; }
            set
            {
                _spectrumIdentificationProtocols = value;
                if (_spectrumIdentificationProtocols != null)
                {
                    _spectrumIdentificationProtocols.IdentData = IdentData;
                }
            }
        }

        /// <remarks>min 0, max 1</remarks>
        public ProteinDetectionProtocolObj ProteinDetectionProtocol
        {
            get { return _proteinDetectionProtocol; }
            set
            {
                _proteinDetectionProtocol = value;
                if (_proteinDetectionProtocol != null)
                {
                    _proteinDetectionProtocol.IdentData = IdentData;
                }
            }
        }

        internal void RebuildSIPList()
        {
            _idCounter = 0;
            _spectrumIdentificationProtocols.Clear();

            foreach (var specId in IdentData.AnalysisCollection.SpectrumIdentifications)
            {
                if (_spectrumIdentificationProtocols.Any(item => item.Equals(specId.SpectrumIdentificationProtocol)))
                {
                    continue;
                }

                specId.SpectrumIdentificationProtocol.Id = "SpecIdentProtocol_" + _idCounter;
                _idCounter++;
                _spectrumIdentificationProtocols.Add(specId.SpectrumIdentificationProtocol);
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
            var o = other as AnalysisProtocolCollectionObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AnalysisProtocolCollectionObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(ProteinDetectionProtocol, other.ProteinDetectionProtocol) &&
                Equals(SpectrumIdentificationProtocols, other.SpectrumIdentificationProtocols))
            {
                return true;
            }
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
                var hashCode = ProteinDetectionProtocol != null ? ProteinDetectionProtocol.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^
                           (SpectrumIdentificationProtocols != null ? SpectrumIdentificationProtocols.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}