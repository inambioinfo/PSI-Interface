using System;
using System.Linq;
using PSI_Interface.CV;
using PSI_Interface.IdentData.IdentDataObjs;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData
{
    /// <summary>
    ///     MzIdentML MzIdentMLType
    /// </summary>
    /// <remarks>
    ///     The upper-most hierarchy level of mzIdentML with sub-containers for example describing software,
    ///     protocols and search results (spectrum identifications or protein detection results).
    /// </remarks>
    public class IdentDataObj : IIdentifiableType, IEquatable<IdentDataObj>
    {
        #region Private/Internal Fields
        private AnalysisCollectionObj _analysisCollection;
        private AnalysisProtocolCollectionObj _analysisProtocolCollection;
        private IdentDataList<SampleObj> _analysisSampleCollection;
        private IdentDataList<AnalysisSoftwareObj> _analysisSoftwareList;
        private IdentDataList<AbstractContactObj> _auditCollection;
        private IdentDataList<BibliographicReferenceObj> _bibliographicReferences;
        private DateTime _creationDate;
        private IdentDataList<CVInfo> _cvList;
        private DataCollectionObj _dataCollection;
        private ProviderObj _provider;
        private SequenceCollectionObj _sequenceCollection;

        internal CVTranslator CvTranslator;
        #endregion

        #region Constructors
        /// <summary>
        ///     Create an IdentDataObj directly from an MzIdentMLType object - fully cascades for the entire contents of
        ///     MzIdentMLType
        /// </summary>
        /// <param name="mzid"></param>
        public IdentDataObj(MzIdentMLType mzid)
        {
            Id = mzid.id;
            Name = mzid.name;
            Version = mzid.version;
            _creationDate = mzid.creationDate;
            CreationDateSpecified = mzid.creationDateSpecified;

            CvTranslator = new CVTranslator();
            _cvList = null;
            _analysisSoftwareList = null;
            _provider = null;
            _auditCollection = null;
            _analysisSampleCollection = null;
            _sequenceCollection = null;
            _analysisCollection = null;
            _analysisProtocolCollection = null;
            _dataCollection = null;
            _bibliographicReferences = null;

            // Referenced by anything using CV/User params
            if (mzid.cvList != null && mzid.cvList.Count > 0)
            {
                CVList = new IdentDataList<CVInfo>();
                foreach (var cv in mzid.cvList)
                {
                    CVList.Add(new CVInfo(cv, this));
                }
                CvTranslator = new CVTranslator(_cvList);
            }
            // Referenced by nothing
            if (mzid.BibliographicReference != null && mzid.BibliographicReference.Count > 0)
            {
                BibliographicReferences = new IdentDataList<BibliographicReferenceObj>();
                foreach (var br in mzid.BibliographicReference)
                {
                    BibliographicReferences.Add(new BibliographicReferenceObj(br, this));
                }
            }
            // Referenced by anything using organization, person, contactRoleInfo - SampleInfo, ProviderInfo, AnalysisSoftwareInfo
            if (mzid.AuditCollection != null && mzid.AuditCollection.Count > 0)
            {
                AuditCollection = new IdentDataList<AbstractContactObj>();
                foreach (var ac in mzid.AuditCollection)
                {
                    if (ac is PersonType)
                    {
                        AuditCollection.Add(new PersonObj(ac as PersonType, this));
                    }
                    else if (ac is OrganizationType)
                    {
                        AuditCollection.Add(new OrganizationObj(ac as OrganizationType, this));
                    }
                }
            }
            // Referenced by anything using SampleInfo: SubSample, SpectrumIdentificationItem
            if (mzid.AnalysisSampleCollection != null && mzid.AnalysisSampleCollection.Count > 0)
            {
                AnalysisSampleCollection = new IdentDataList<SampleObj>();
                foreach (var asc in mzid.AnalysisSampleCollection)
                {
                    AnalysisSampleCollection.Add(new SampleObj(asc, this));
                }
            }
            // Referenced by ProviderInfo, ProteinDetectionProtocol, SpectrumIdentificationProtocol, references AbstractContactInfo through ContactRoleInfo
            if (mzid.AnalysisSoftwareList != null && mzid.AnalysisSoftwareList.Count > 0)
            {
                AnalysisSoftwareList = new IdentDataList<AnalysisSoftwareObj>();
                foreach (var asl in mzid.AnalysisSoftwareList)
                {
                    AnalysisSoftwareList.Add(new AnalysisSoftwareObj(asl, this));
                }
            }
            // Referenced by nothing, references AnalysisSoftwareInfo
            if (mzid.Provider != null)
            {
                _provider = new ProviderObj(mzid.Provider, this);
            }
            // Referenced by SpectrumIdentification, ProteinDetection, SpectrumIdentificationItem, PeptideEvidence, references AnalysisSoftwareInfo
            if (mzid.AnalysisProtocolCollection != null)
            {
                _analysisProtocolCollection = new AnalysisProtocolCollectionObj(mzid.AnalysisProtocolCollection, this);
            }
            // InputsInfo referenced by DBSequence, SpectrumIdentification.SearchDatabaseRefInfo, SpectrumIdentificationResult, SpectrumIdentification.InputSpectraRef
            if (mzid.DataCollection != null)
            {
                //this._dataCollection = new DataCollection(mzid.DataCollection, this);
                _dataCollection = new DataCollectionObj {
                    Inputs = new InputsObj(mzid.DataCollection.Inputs, this)
                };
            }
            // Referenced by SpectrumIdentificationItem, ProteinDetectionHypothesis, PeptideHypothesis
            // References InputsInfo.DBSequence, AnalysisProtocolCollection.SoftwareIdentificationProtocol.DatabaseTranslation.TranslationTable
            if (mzid.SequenceCollection != null)
            {
                _sequenceCollection = new SequenceCollectionObj(mzid.SequenceCollection, this);
            }
            // AnalysisData referenced by SpectrumIdentification, InputSpectrumIdentifications, ProteinDetection, references Peptides, PeptideEvidence, SampleInfo, MassTable,
            if (mzid.DataCollection != null)
            {
                _dataCollection.AnalysisData = new AnalysisDataObj(mzid.DataCollection.AnalysisData, this);
            }
            // References SpectrumIdentificationProtocol, SpectrumIdentificationList, SpectraData, SeqrchDatabaseInfo, ProteinDetectionList, ProteinDetectionProtocol
            if (mzid.AnalysisCollection != null)
            {
                _analysisCollection = new AnalysisCollectionObj(mzid.AnalysisCollection, this);
            }
        }

        /// <summary>
        ///     Constructor - basic initialization
        /// </summary>
        /// <param name="createTranslator">If the default CV list should be used for the file</param>
        public IdentDataObj(bool createTranslator = true)
        {
            Id = null;
            Name = null;
            Version = "1.1.0";
            CreationDateSpecified = false;
            CreationDate = DateTime.Now;

            //this.CvTranslator = new CVTranslator(); // Create a generic translator by default; must be re-mapped when reading a file
            CvTranslator = null;
            CVList = new IdentDataList<CVInfo>();
            if (createTranslator)
            {
                //this.CvTranslator = new CVTranslator(); // Create a generic translator by default; must be re-mapped when reading a file
                DefaultCV(); // Create a generic translator by default; must be re-mapped when reading a file
            }
            AnalysisSoftwareList = new IdentDataList<AnalysisSoftwareObj>();
            _provider = null;
            AuditCollection = new IdentDataList<AbstractContactObj>();
            AnalysisSampleCollection = new IdentDataList<SampleObj>();
            _sequenceCollection = null;
            _analysisCollection = null;
            _analysisProtocolCollection = null;
            _dataCollection = null;
            BibliographicReferences = new IdentDataList<BibliographicReferenceObj>();
        }
        #endregion

        #region Properties

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

        /// <remarks>min 1, max 1</remarks>
        public IdentDataList<CVInfo> CVList
        {
            get { return _cvList; }
            set
            {
                _cvList = value;
                if (_cvList != null)
                {
                    _cvList.IdentData = this;
                    CvTranslator = new CVTranslator(_cvList);
                }
                else
                {
                    CvTranslator = new CVTranslator();
                }
            }
        }

        /// <remarks>min 0, max 1</remarks>
        public IdentDataList<AnalysisSoftwareObj> AnalysisSoftwareList
        {
            get { return _analysisSoftwareList; }
            set
            {
                _analysisSoftwareList = value;
                if (_analysisSoftwareList != null)
                {
                    _analysisSoftwareList.IdentData = this;
                }
            }
        }

        /// <remarks>The Provider of the mzIdentML record in terms of the contact and software.</remarks>
        /// <remarks>min 0, max 1</remarks>
        public ProviderObj Provider
        {
            get { return _provider; }
            set
            {
                _provider = value;
                if (_provider != null)
                {
                    _provider.IdentData = this;
                }
            }
        }

        /// <remarks>min 0, max 1</remarks>
        public IdentDataList<AbstractContactObj> AuditCollection
        {
            get { return _auditCollection; }
            set
            {
                _auditCollection = value;
                if (_auditCollection != null)
                {
                    _auditCollection.IdentData = this;
                }
            }
        }

        /// <remarks>min 0, max 1</remarks>
        public IdentDataList<SampleObj> AnalysisSampleCollection
        {
            get { return _analysisSampleCollection; }
            set
            {
                _analysisSampleCollection = value;
                if (_analysisSampleCollection != null)
                {
                    _analysisSampleCollection.IdentData = this;
                }
            }
        }

        /// <remarks>min 0, max 1</remarks>
        public SequenceCollectionObj SequenceCollection
        {
            get { return _sequenceCollection; }
            set
            {
                _sequenceCollection = value;
                if (_sequenceCollection != null)
                {
                    _sequenceCollection.IdentData = this;
                }
            }
        }

        /// <remarks>min 1, max 1</remarks>
        public AnalysisCollectionObj AnalysisCollection
        {
            get { return _analysisCollection; }
            set
            {
                _analysisCollection = value;
                if (_analysisCollection != null)
                {
                    _analysisCollection.IdentData = this;
                }
            }
        }

        /// <remarks>min 1, max 1</remarks>
        public AnalysisProtocolCollectionObj AnalysisProtocolCollection
        {
            get { return _analysisProtocolCollection; }
            set
            {
                _analysisProtocolCollection = value;
                if (_analysisProtocolCollection != null)
                {
                    _analysisProtocolCollection.IdentData = this;
                }
            }
        }

        /// <remarks>min 1, max 1</remarks>
        public DataCollectionObj DataCollection
        {
            get { return _dataCollection; }
            set
            {
                _dataCollection = value;
                if (_dataCollection != null)
                {
                    _dataCollection.IdentData = this;
                }
            }
        }

        /// <remarks>Any bibliographic references associated with the file</remarks>
        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<BibliographicReferenceObj> BibliographicReferences
        {
            get { return _bibliographicReferences; }
            set
            {
                _bibliographicReferences = value;
                if (_bibliographicReferences != null)
                {
                    _bibliographicReferences.IdentData = this;
                }
            }
        }

        /// <remarks>The date on which the file was produced.</remarks>
        /// Optional Attribute
        /// dataTime
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set
            {
                _creationDate = value;
                CreationDateSpecified = true;
            }
        }

        /// <summary>
        /// Attribute Existence - true if CreationDate has bee set.
        /// </summary>
        protected internal bool CreationDateSpecified { get; private set; }

        /// <remarks>
        ///     The version of the schema this instance document refers to, in the format x.y.z.
        ///     Changes to z should not affect prevent instance documents from validating.
        /// </remarks>
        /// Required Attribute
        /// string, regex: "(1\.1\.\d+)"
        public string Version { get; set; }

#endregion

        #region Utility Functions
        /// <summary>
        ///     Set the default CV lists for CV term reference and mapping
        /// </summary>
        public void DefaultCV()
        {
            CVList = new IdentDataList<CVInfo>();
            foreach (var cv in CV.CV.CVInfoList)
            {
                if (cv.Id.ToLower() == "pato")
                {
                    continue;
                }
                var newcv = new CVInfo
                {
                    FullName = cv.Name,
                    Id = cv.Id,
                    URI = cv.URI,
                    Version = cv.Version
                };
                CVList.Add(newcv);
            }
            CvTranslator = new CVTranslator(CVList);
        }

        /// <summary>
        ///     Cascade the identData reference throughout the entire set of objects.
        /// </summary>
        internal void CascadeProperties()
        {
            if (Provider != null)
            {
                Provider.CascadeProperties();
            }
            if (SequenceCollection != null)
            {
                SequenceCollection.CascadeProperties();
            }
            if (AnalysisCollection != null)
            {
                AnalysisCollection.CascadeProperties();
            }
            if (AnalysisProtocolCollection != null)
            {
                AnalysisProtocolCollection.CascadeProperties();
            }
            if (DataCollection != null)
            {
                DataCollection.CascadeProperties();
            }
            if (CVList != null)
            {
                CVList.CascadeProperties();
            }
            if (AnalysisSoftwareList != null)
            {
                AnalysisSoftwareList.CascadeProperties();
            }
            if (AuditCollection != null)
            {
                AuditCollection.CascadeProperties();
            }
            if (AnalysisSampleCollection != null)
            {
                AnalysisSampleCollection.CascadeProperties();
            }
            if (BibliographicReferences != null)
            {
                BibliographicReferences.CascadeProperties();
            }
        }

        /// <summary>
        ///     Rebuild some of the internal lists using object references
        /// </summary>
        public void RebuildLists()
        {
            // TODO: Not Implementing for now
            //_auditCollection.Clear();
            //_analysisSoftwareList.Clear();
            //_analysisSampleCollection.Clear();
            AnalysisProtocolCollection.RebuildSIPList();
            SequenceCollection.RebuildLists();
            DataCollection.RebuildLists();
        }

        #endregion

        #region Functions for resolving references

        /// <summary>
        ///     Find the <see cref="SpectrumIdentificationItemObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal SpectrumIdentificationItemObj FindSpectrumIdentificationItem(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                foreach (var sil in DataCollection.AnalysisData.SpectrumIdentificationList)
                {
                    foreach (var sir in sil.SpectrumIdentificationResults)
                    {
                        var result = sir.SpectrumIdentificationItems.Where(item => item.Id == id).ToList();
                        if (result.Count > 0)
                        {
                            return result.First();
                        }
                    }
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="DbSequenceObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal DbSequenceObj FindDbSequence(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = SequenceCollection.DBSequences.AsParallel().Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="PeptideObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal PeptideObj FindPeptide(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = SequenceCollection.Peptides.AsParallel().Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="PeptideEvidenceObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal PeptideEvidenceObj FindPeptideEvidence(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = SequenceCollection.PeptideEvidences.AsParallel().Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="MeasureObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal MeasureObj FindMeasure(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                foreach (var sil in DataCollection.AnalysisData.SpectrumIdentificationList)
                {
                    var result = sil.FragmentationTables.Where(item => item.Id == id).ToList();
                    if (result.Count > 0)
                    {
                        return result.First();
                    }
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="MassTableObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal MassTableObj FindMassTable(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                foreach (var sip in AnalysisProtocolCollection.SpectrumIdentificationProtocols)
                {
                    var result = sip.MassTables.Where(item => item.Id == id).ToList();
                    if (result.Count > 0)
                    {
                        return result.First();
                    }
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="SampleObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal SampleObj FindSample(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = AnalysisSampleCollection.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="AnalysisSoftwareObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal AnalysisSoftwareObj FindAnalysisSoftware(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = AnalysisSoftwareList.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="OrganizationObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal OrganizationObj FindOrganization(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = AuditCollection.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        var o = item as OrganizationObj;
                        if (o != null)
                        {
                            return o;
                        }
                    }
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="AbstractContactObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal AbstractContactObj FindContact(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = AuditCollection.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="SearchDatabaseInfo" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal SearchDatabaseInfo FindSearchDatabase(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = DataCollection.Inputs.SearchDatabases.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="SpectraDataObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal SpectraDataObj FindSpectraData(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = DataCollection.Inputs.SpectraDataList.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="SpectrumIdentificationListObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal SpectrumIdentificationListObj FindSpectrumIdentificationList(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result =
                    DataCollection.AnalysisData.SpectrumIdentificationList.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="SpectrumIdentificationProtocolObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal SpectrumIdentificationProtocolObj FindSpectrumIdentificationProtocol(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result =
                    AnalysisProtocolCollection.SpectrumIdentificationProtocols.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="ProteinDetectionProtocolObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal ProteinDetectionProtocolObj FindProteinDetectionProtocol(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                if (AnalysisProtocolCollection.ProteinDetectionProtocol.Id == id)
                {
                    return AnalysisProtocolCollection.ProteinDetectionProtocol;
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="ProteinDetectionListObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal ProteinDetectionListObj FindProteinDetectionList(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                if (DataCollection.AnalysisData.ProteinDetectionList.Id == id)
                {
                    return DataCollection.AnalysisData.ProteinDetectionList;
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        /// <summary>
        ///     Find the <see cref="TranslationTableObj" /> that matches id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected internal TranslationTableObj FindTranslationTable(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                foreach (var sip in AnalysisProtocolCollection.SpectrumIdentificationProtocols)
                {
                    var result = sip.DatabaseTranslation.TranslationTables.Where(item => item.Id == id).ToList();
                    if (result.Count > 0)
                    {
                        return result.First();
                    }
                }
            }
            catch
            {
                // Ignore errors; must resolve reference later...
            }
            return null;
        }

        #endregion

        #region Object Equality Overrides

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IdentDataObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Name == other.Name && Version == other.Version && Equals(CVList, other.CVList) &&
                Equals(AnalysisSoftwareList, other.AnalysisSoftwareList) && Equals(Provider, other.Provider) &&
                Equals(AuditCollection, other.AuditCollection) &&
                Equals(AnalysisSampleCollection, other.AnalysisSampleCollection) &&
                Equals(SequenceCollection, other.SequenceCollection) &&
                Equals(AnalysisCollection, other.AnalysisCollection) &&
                Equals(AnalysisProtocolCollection, other.AnalysisProtocolCollection) &&
                Equals(DataCollection, other.DataCollection) &&
                Equals(BibliographicReferences, other.BibliographicReferences))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Object Equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as IdentDataObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        /// Object Hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CVList != null ? CVList.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (AnalysisSoftwareList != null ? AnalysisSoftwareList.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Provider != null ? Provider.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AuditCollection != null ? AuditCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^
                           (AnalysisSampleCollection != null ? AnalysisSampleCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SequenceCollection != null ? SequenceCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AnalysisCollection != null ? AnalysisCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^
                           (AnalysisProtocolCollection != null ? AnalysisProtocolCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DataCollection != null ? DataCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^
                           (BibliographicReferences != null ? BibliographicReferences.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ CreationDate.GetHashCode();
                hashCode = (hashCode * 397) ^ (Version != null ? Version.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion
    }
}