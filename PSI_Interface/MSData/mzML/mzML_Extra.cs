﻿// 
// Data translation code from MSData to mzML.
// 

using System.Collections.Generic;

namespace PSI_Interface.MSData.mzML
{
    /// <summary>
    /// mzML mzMLType
    /// </summary>
    /// <remarks>This is the root element for the Proteomics Standards Initiative (PSI) mzML schema, which 
    /// is intended to capture the use of a mass spectrometer, the data generated, and 
    /// the initial processing of that data (to the level of the peak list)</remarks>
    public partial class mzMLType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public mzMLType()
        {
            this.cvList = null;
            this.fileDescription = null;
            this.referenceableParamGroupList = null;
            this.sampleList = null;
            this.softwareList = null;
            this.scanSettingsList = null;
            this.instrumentConfigurationList = null;
            this.dataProcessingList = null;
            this.run = null;
            this.accession = null;
            this.id = null;
            this.version = null;
        }
        /*/// min 1, max 1
        //public CVListType cvList

        /// min 1, max 1
        //public FileDescriptionType fileDescription

        /// min 0, max 1
        //public ReferenceableParamGroupListType referenceableParamGroupList

        /// min 0, max 1
        //public SampleListType sampleList

        /// min 1, max 1
        //public SoftwareListType softwareList

        /// min 0, max 1
        //public ScanSettingsListType scanSettingsList

        /// min 1, max 1
        //public InstrumentConfigurationListType instrumentConfigurationList

        /// min 1, max 1
        //public DataProcessingListType dataProcessingList

        /// min 1, max 1
        //public RunType run

        /// <remarks>An optional accession number for the mzML document used for storage, e.g. in PRIDE.</remarks>
        /// Optional Attribute
        /// string
        //public string accession

        /// <remarks>An optional id for the mzML document used for referencing from external files. It is recommended to use LSIDs when possible.</remarks>
        /// Optional Attribute
        /// string
        //public string id

        /// <remarks>The version of this mzML document.</remarks>
        /// Required Attribute
        /// string
        //public string version*/
    }

    /// <summary>
    /// mzML CVListType
    /// </summary>
    /// <remarks>Container for one or more controlled vocabulary definitions.</remarks>
    public partial class CVListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CVListType()
        {
            this.cv = new List<CVType>();
        }

        /*/// min 1, max unbounded
        //public CVType[] cv

        /// <remarks>The number of CV definitions in this mzML file.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML CVType
    /// </summary>
    /// <remarks>Information about an ontology or CV source and a short 'lookup' tag to refer to.</remarks>
    public partial class CVType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CVType()
        {
            this.id = null;
            this.fullName = null;
            this.version = null;
            this.URI = null;
        }

        /*/// <remarks>The short label to be used as a reference tag with which to refer to this particular Controlled Vocabulary source description (e.g., from the cvLabel attribute, in CVParamType elements).</remarks>
        /// Required Attribute
        /// ID
        //public string id

        /// <remarks>The usual name for the resource (e.g. The PSI-MS Controlled Vocabulary).</remarks>
        /// Required Attribute
        /// string
        //public string fullName

        /// <remarks>The version of the CV from which the referred-to terms are drawn.</remarks>
        /// Optional Attribute
        /// string
        //public string version

        /// <remarks>The URI for the resource.</remarks>
        /// Required Attribute
        /// anyURI
        //public string URI*/
    }

    /// <summary>
    /// mzML DataProcessingListType
    /// </summary>
    /// <remarks>List and descriptions of data processing applied to this data.</remarks>
    public partial class DataProcessingListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataProcessingListType()
        {
            this.dataProcessingField = new List<DataProcessingType>();
        }

        /*/// min 1, max unbounded
        //public DataProcessingType[] dataProcessing

        /// <remarks>The number of DataProcessingTypes in this mzML file.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML DataProcessingType
    /// </summary>
    /// <remarks>Description of the way in which a particular software was used.</remarks>
    public partial class DataProcessingType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataProcessingType()
        {
            this.processingMethodField = new List<ProcessingMethodType>();
            this.id = null;
        }

        /*/// <remarks>Description of the default peak processing method. 
        /// This element describes the base method used in the generation of a particular mzML file. 
        /// Variable methods should be described in the appropriate acquisition section - if 
        /// no acquisition-specific details are found, then this information serves as the default.</remarks>
        /// min 1, max unbounded
        //public ProcessingMethodType[] processingMethod

        /// <remarks>A unique identifier for this data processing that is unique across all DataProcessingTypes.</remarks>
        /// Required Attribute
        /// ID
        //public string id*/
    }

    /// <summary>
    /// mzML ProcessingMethodType
    /// </summary>
    public partial class ProcessingMethodType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProcessingMethodType()
        {
            this.order = null;
            this.softwareRef = null;
        }

        /*/// <remarks>This attributes allows a series of consecutive steps to be placed in the correct order.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string order

        /// <remarks>This attribute must reference the 'id' of the appropriate SoftwareType.</remarks>
        /// Required Attribute
        /// IDREF
        //public string softwareRef*/
    }

    /// <summary>
    /// mzML ParamGroupType
    /// </summary>
    /// <remarks>Structure allowing the use of a controlled (cvParam) or uncontrolled vocabulary (userParam), or a reference to a predefined set of these in this mzML file (paramGroupRef).</remarks>
    public partial class ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParamGroupType()
        {
            this.referenceableParamGroupRefField = new List<ReferenceableParamGroupRefType>();
            this.cvParamField = new List<CVParamType>();
            this.userParamField = new List<UserParamType>();
        }

        /*/// min 0, max unbounded
        //public ReferenceableParamGroupRefType[] referenceableParamGroupRef

        /// min 0, max unbounded
        //public CVParamType[] cvParam

        /// min 0, max unbounded
        //public UserParamType[] userParam*/
    }

    /// <summary>
    /// mzML ReferenceableParamGroupListType
    /// </summary>
    /// <remarks>Container for a list of referenceableParamGroups</remarks>
    public partial class ReferenceableParamGroupListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReferenceableParamGroupListType()
        {
            this.referenceableParamGroupField = new List<ReferenceableParamGroupType>();
        }

        /*/// min 1, max unbounded
        //public ReferenceableParamGroupType[] referenceableParamGroup

        /// <remarks>The number of ParamGroups defined in this mzML file.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML ReferenceableParamGroupType
    /// </summary>
    /// <remarks>A collection of CVParam and UserParam elements that can be referenced from elsewhere in this mzML document by using the 'paramGroupRef' element in that location to reference the 'id' attribute value of this element.</remarks>
    public partial class ReferenceableParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReferenceableParamGroupType()
        {
            this.cvParamField = new List<CVParamType>();
            this.userParamField = new List<UserParamType>();
            this.id = null;
        }

        /*/// min 0, max unbounded
        //public CVParamType[] cvParam

        /// min 0, max unbounded
        //public UserParamType[] userParam

        /// <remarks>The identifier with which to reference this ReferenceableParamGroup.</remarks>
        /// Required Attribute
        /// ID
        //public string id*/
    }

    /// <summary>
    /// mzML ReferenceableParamGroupRefType
    /// </summary>
    /// <remarks>A reference to a previously defined ParamGroup, which is a reusable container of one or more cvParams.</remarks>
    public partial class ReferenceableParamGroupRefType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReferenceableParamGroupRefType()
        {
            this.@ref = null;
        }

        /*/// <remarks>Reference to the id attribute in a referenceableParamGroup.</remarks>
        /// Required Attribute
        /// IDREF
        //public string @ref*/
    }

    /// <summary>
    /// mzML CVParamType
    /// </summary>
    /// <remarks>This element holds additional data or annotation. Only controlled values are allowed here.</remarks>
    public partial class CVParamType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CVParamType()
        {
            this.cvRef = null;
            this.accession = null;
            this.name = null;
            this.value = null;
            this.unitCvRef = null;
            this.unitAccession = null;
            this.unitName = null;
        }

        /*/// <remarks>A reference to the CV 'id' attribute as defined in the cvList in this mzML file.</remarks>
        /// Required Attribute
        /// IDREF
        //public string cvRef

        /// <remarks>The accession number of the referred-to term in the named resource (e.g.: MS:000012).</remarks>
        /// Required Attribute
        /// string
        //public string accession

        /// <remarks>The actual name for the parameter, from the referred-to controlled vocabulary. This should be the preferred name associated with the specified accession number.</remarks>
        /// Required Attribute
        /// string
        //public string name

        /// <remarks>The value for the parameter; may be absent if not appropriate, or a numeric or symbolic value, or may itself be CV (legal values for a parameter should be enumerated and defined in the ontology).</remarks>
        /// Optional Attribute
        /// string
        //public string value

        /// <remarks>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this mzML file.</remarks>
        /// Optional Attribute
        /// IDREF
        //public string unitCvRef

        /// <remarks>An optional CV accession number for the unit term associated with the value, if any (e.g., 'UO:0000266' for 'electron volt').</remarks>
        /// Optional Attribute
        /// string
        //public string unitAccession

        /// <remarks>An optional CV name for the unit accession number, if any (e.g., 'electron volt' for 'UO:0000266' ).</remarks>
        /// Optional Attribute
        /// string
        //public string unitName*/
    }

    /// <summary>
    /// mzML UserParamType
    /// </summary>
    /// <remarks>Uncontrolled user parameters (essentially allowing free text). 
    /// Before using these, one should verify whether there is an appropriate 
    /// CV term available, and if so, use the CV term instead</remarks>
    public partial class UserParamType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserParamType()
        {
            this.name = null;
            this.type = null;
            this.value = null;
            this.unitAccession = null;
            this.unitName = null;
            this.unitCvRef = null;
        }

        /*/// <remarks>The name for the parameter.</remarks>
        /// Required Attribute
        /// string
        //public string name

        /// <remarks>The datatype of the parameter, where appropriate (e.g.: xsd:float).</remarks>
        /// Optional Attribute
        /// string
        //public string type

        /// <remarks>The value for the parameter, where appropriate.</remarks>
        /// Optional Attribute
        /// string
        //public string value

        /// <remarks>An optional CV accession number for the unit term associated with the value, if any (e.g., 'UO:0000266' for 'electron volt').</remarks>
        /// Optional Attribute
        /// string
        //public string unitAccession

        /// <remarks>An optional CV name for the unit accession number, if any (e.g., 'electron volt' for 'UO:0000266' ).</remarks>
        /// Optional Attribute
        /// string
        //public string unitName

        /// <remarks>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this mzML file.</remarks>
        /// Optional Attribute
        /// IDREF
        //public string unitCvRef*/
    }

    /// <summary>
    /// mzML PrecursorListType
    /// </summary>
    /// <remarks>List and descriptions of precursor isolations to the spectrum currently being described, ordered.</remarks>
    public partial class PrecursorListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrecursorListType()
        {
            this.precursorField = new List<PrecursorType>();
        }

        /*/// min 1, max unbounded
        //public PrecursorType[] precursor

        /// <remarks>The number of precursor isolations in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML PrecursorType
    /// </summary>
    /// <remarks>The method of precursor ion selection and activation</remarks>
    public partial class PrecursorType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrecursorType()
        {
            this.isolationWindow = null;
            this.selectedIonList = null;
            this.activation = null;
            this.spectrumRef = null;
            this.sourceFileRef = null;
            this.externalSpectrumID = null;
        }

        /*/// <remarks>This element captures the isolation (or 'selection') window configured to isolate one or more ions.</remarks>
        /// min 0, max 1
        //public ParamGroupType isolationWindow

        /// <remarks>A list of ions that were selected.</remarks>
        /// min 0, max 1
        //public SelectedIonListType selectedIonList

        /// <remarks>The type and energy level used for activation.</remarks>
        /// min 1, max 1
        //public ParamGroupType activation

        /// <remarks>For precursor spectra that are local to this document, this attribute must be used to reference the 'id' attribute of the spectrum corresponding to the precursor spectrum.</remarks>
        /// Optional Attribute
        /// string
        //public string spectrumRef

        /// <remarks>For precursor spectra that are external to this document, this attribute must reference the 'id' attribute of a sourceFile representing that external document.</remarks>
        /// Optional Attribute
        /// IDREF
        //public string sourceFileRef

        /// <remarks>For precursor spectra that are external to this document, this string must correspond to the 'id' attribute of a spectrum in the external document indicated by 'sourceFileRef'.</remarks>
        /// Optional Attribute
        /// string
        //public string externalSpectrumID*/
    }

    /// <summary>
    /// mzML SelectedIonListType
    /// </summary>
    /// <remarks>The list of selected precursor ions.</remarks>
    public partial class SelectedIonListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SelectedIonListType()
        {
            this.selectedIonField = new List<ParamGroupType>();
        }

        /*/// min 1, max unbounded
        //public ParamGroupType[] selectedIon

        /// <remarks>The number of selected precursor ions defined in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML BinaryDataArrayListType
    /// </summary>
    /// <remarks>List of binary data arrays.</remarks>
    public partial class BinaryDataArrayListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BinaryDataArrayListType()
        {
            this.binaryDataArrayField = new List<BinaryDataArrayType>();
        }

        /*/// <remarks>Data point arrays for default data arrays (m/z, intensity, time) and meta data arrays. 
        /// Default data arrays must not have the attributes 'arrayLength' and 'dataProcessingRef'.</remarks>
        /// min 2, max unbounded
        //public BinaryDataArrayType[] binaryDataArray

        /// <remarks>The number of binary data arrays defined in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML BinaryDataArrayType
    /// </summary>
    /// <remarks>The structure into which encoded binary data goes. Byte ordering is always little endian (Intel style). 
    /// Computers using a different endian style must convert to/from little endian when writing/reading mzML</remarks>
    public partial class BinaryDataArrayType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BinaryDataArrayType()
        {
            this.binary = null;
            this.arrayLength = null;
            this.dataProcessingRef = null;
            this.encodedLength = null;
        }

        /*/// <remarks>The actual base64 encoded binary data. The byte order is always 'little endian'.</remarks>
        /// base64Binary
        //public byte[] binary

        /// <remarks>This optional attribute may override the 'defaultArrayLength' defined in SpectrumType. 
        /// The two default arrays (m/z and intensity) should NEVER use this override option, and should 
        /// therefore adhere to the 'defaultArrayLength' defined in SpectrumType. Parsing software can thus 
        /// safely choose to ignore arrays of lengths different from the one defined in the 'defaultArrayLength' SpectrumType element.</remarks>
        /// Optional Attribute
        /// non-negative integer
        //public string arrayLength

        /// <remarks>This optional attribute may reference the 'id' attribute of the appropriate dataProcessing.</remarks>
        /// Optional Attribute
        /// IDREF
        //public string dataProcessingRef

        /// <remarks>The encoded length of the binary data array.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string encodedLength*/
    }

    /// <summary>
    /// mzML ScanListType
    /// </summary>
    /// <remarks>List and descriptions of scans.</remarks>
    public partial class ScanListType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScanListType()
        {
            this.scanField = new List<ScanType>();
        }

        /*/// min 1, max unbounded
        //public ScanType[] scan

        /// <remarks>the number of scans defined in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML ScanType
    /// </summary>
    /// <remarks>Scan or acquisition from original raw file used to create this peak list, as specified in sourceF</remarks>
    public partial class ScanType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScanType()
        {
            this.scanWindowList = null;
            this.spectrumRef = null;
            this.sourceFileRef = null;
            this.externalSpectrumID = null;
            this.instrumentConfigurationRef = null;
        }

        /*/// <remarks>Container for a list of scan windows.</remarks>
        /// min 0, max 1
        //public ScanWindowListType scanWindowList

        /// <remarks>For scans that are local to this document, this attribute can be used to reference the 'id' attribute of the spectrum corresponding to the scan.</remarks>
        /// Optional Attribute
        /// string
        //public string spectrumRef

        /// <remarks>If this attribute is set, it must reference the 'id' attribute of a sourceFile representing the external document containing the spectrum referred to by 'externalSpectrumID'.</remarks>
        /// Optional Attribute
        /// IDREF
        //public string sourceFileRef

        /// <remarks>For scans that are external to this document, this string must correspond to the 'id' attribute of a spectrum in the external document indicated by 'sourceFileRef'.</remarks>
        /// Optional Attribute
        /// string
        //public string externalSpectrumID

        /// <remarks>This attribute can optionally reference the 'id' attribute of the appropriate instrument configuration.</remarks>
        /// Optional Attribute
        /// IDREF
        //public string instrumentConfigurationRef*/
    }

    /// <summary>
    /// mzML ScanWindowListType
    /// </summary>
    /// <remarks></remarks>
    public partial class ScanWindowListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScanWindowListType()
        {
            this.scanWindowField = new List<ParamGroupType>();
        }

        /*/// <remarks>A range of m/z values over which the instrument scans and acquires a spectrum.</remarks>
        /// min 1, max unbounded
        //public ParamGroupType[] scanWindow

        /// <remarks>The number of scan windows defined in this list.</remarks>
        /// Required Attribute
        /// int
        //public int count*/
    }

    /// <summary>
    /// mzML SpectrumListType
    /// </summary>
    /// <remarks>List and descriptions of spectra.</remarks>
    public partial class SpectrumListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SpectrumListType()
        {
            this.spectrumField = new List<SpectrumType>();
            this.defaultDataProcessingRef = null;
        }

        /*/// min 0, max unbounded
        //public SpectrumType[] spectrum

        /// <remarks>The number of spectra defined in this mzML file.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count

        /// <remarks>This attribute MUST reference the 'id' of the default data processing for the spectrum list. 
        /// If an acquisition does not reference any data processing, it implicitly refers to this data processing. 
        /// This attribute is required because the minimum amount of data processing that any format will undergo is "conversion to mzML".</remarks>
        /// Required Attribute
        /// IDREF
        //public string defaultDataProcessingRef*/
    }

    /// <summary>
    /// mzML SpectrumType
    /// </summary>
    /// <remarks>The structure that captures the generation of a peak list (including the underlying acquisitions). 
    /// Also describes some of the parameters for the mass spectrometer for a given acquisition (or list of acquisitions).</remarks>
    public partial class SpectrumType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SpectrumType()
        {
            this.scanList = null;
            this.precursorList = null;
            this.productList = null;
            this.binaryDataArrayList = null;
            this.id = null;
            this.spotID = null;
            this.index = null;
            this.defaultArrayLength = 0;
            this.dataProcessingRef = null;
            this.sourceFileRef = null;
        }

        /*/// min 0, max 1
        //public ScanListType scanList

        /// min 0, max 1
        //public PrecursorListType PrecursorList

        /// min 0, max 1
        //public ProductListType ProductList

        /// min 0, max 1
        //public BinaryDataArrayListType BinaryDataArrayList

        /// <remarks>The zero-based, consecutive index of  the spectrum in the SpectrumList.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string index

        /// <remarks>The native identifier for a spectrum. For unmerged native spectra or spectra from older open file formats, 
        /// the format of the identifier is defined in the PSI-MS CV and referred to in the mzML header. 
        /// External documents may use this identifier together with the mzML filename or accession to reference a particular spectrum.</remarks>
        /// Required Attribute
        /// Regex: "\S+=\S+( \S+=\S+)*"
        //public string id

        /// <remarks>The identifier for the spot from which this spectrum was derived, if a MALDI or similar run.</remarks>
        /// Optional Attribute
        /// string
        //public string spotID

        /// <remarks>Default length of binary data arrays contained in this element.</remarks>
        /// Required Attribute
        /// integer
        //public int defaultArrayLength

        /// <remarks>This attribute can optionally reference the 'id' of the appropriate dataProcessing.</remarks>
        /// Optional Attribute
        /// IDREF
        //public string dataProcessingRef

        /// <remarks>This attribute can optionally reference the 'id' of the appropriate sourceFile.</remarks>
        /// Optional Attribute
        /// IDREF
        //public string sourceFileRef*/
    }

    /// <summary>
    /// mzML ProductListType
    /// </summary>
    /// <remarks>List and descriptions of product isolations to the spectrum currently being described, ordered.</remarks>
    public partial class ProductListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductListType()
        {
            this.productField = new List<ProductType>();
        }

        /*/// min 1, max unbounded
        //public ProductType[] product

        /// <remarks>The number of product isolations in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML ProductType
    /// </summary>
    /// <remarks>The method of product ion selection and activation in a precursor ion scan</remarks>
    public partial class ProductType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductType()
        {
            this.isolationWindow = null;
        }

        /*/// <remarks>This element captures the isolation (or 'selection') window configured to isolate one or more ions.</remarks>
        /// min 0, max 1
        //public ParamGroupType isolationWindow*/
    }

    /// <summary>
    /// mzML RunType
    /// </summary>
    /// <remarks>A run in mzML should correspond to a single, consecutive and coherent set of scans on an instrument.</remarks>
    public partial class RunType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RunType()
        {
            this.spectrumList = null;
            this.chromatogramList = null;
            this.id = null;
            this.defaultInstrumentConfigurationRef = null;
            this.defaultSourceFileRef = null;
            this.sampleRef = null;
            this.startTimeStamp = System.DateTime.Now;
            this.startTimeStampSpecified = startTimeStamp != null;
        }

        /*/// <remarks>All mass spectra and the acquisitions underlying them are described and attached here. 
        /// Subsidiary data arrays are also both described and attached here.</remarks>
        /// min 0, max 1
        //public SpectrumListType spectrumList

        /// <remarks>All chromatograms for this run.</remarks>
        /// min 0, max 1
        //public ChromatogramListType chromatogramList

        /// <remarks>A unique identifier for this run.</remarks>
        /// Required Attribute
        /// ID
        //public string id

        /// <remarks>This attribute must reference the 'id' of the default instrument configuration. 
        /// If a scan does not reference an instrument configuration, it implicitly refers to this configuration.</remarks>
        /// Required Attribute
        /// IDREF
        //public string defaultInstrumentConfigurationRef

        /// <remarks>This attribute can optionally reference the 'id' of the default source file. 
        /// If a spectrum or scan does not reference a source file and this attribute is set, then it implicitly refers to this source file.</remarks>
        /// Optional Attribute
        /// IDREF
        //public string defaultSourceFileRef

        /// <remarks>This attribute must reference the 'id' of the appropriate sample.</remarks>
        /// Optional Attribute
        /// IDREF
        //public string sampleRef

        /// <remarks>The optional start timestamp of the run, in UT.</remarks>
        /// Optional Attribute
        /// DateTime
        //public System.DateTime startTimeStamp

        /// "Ignored" Attribute - only used to signify existence of valid value in startTimeStamp
        //public bool startTimeStampSpecified*/
    }

    /// <summary>
    /// mzML ChromatogramListType
    /// </summary>
    /// <remarks>List of chromatograms.</remarks>
    public partial class ChromatogramListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ChromatogramListType()
        {
            this.chromatogramField = new List<ChromatogramType>();
            this.defaultDataProcessingRef = null;
        }

        /*/// <remarks></remarks>
        /// min 1, max unbounded
        //public ChromatogramType[] chromatogram

        /// <remarks>The number of chromatograms defined in this mzML file.</remarks>
        /// Required Attribute
        /// Non-negative integer
        //public string count

        /// <remarks>This attribute MUST reference the 'id' of the default data processing for the chromatogram list. 
        /// If an acquisition does not reference any data processing, it implicitly refers to this data processing. 
        /// This attribute is required because the minimum amount of data processing that any format will undergo is "conversion to mzML".</remarks>
        /// Required Attribute
        /// IDREF
        //public string defaultDataProcessingRef*/
    }

    /// <summary>
    /// mzML ChromatogramType
    /// </summary>
    /// <remarks>A single Chromatogram</remarks>
    public partial class ChromatogramType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ChromatogramType()
        {
            this.precursor = null;
            this.product = null;
            this.binaryDataArrayList = null;
            this.index = null;
            this.id = null;
            this.defaultArrayLength = 0;
            this.dataProcessingRef = null;
        }

        /*/// min 0, max 1
        //public PrecursorType precursor

        /// min 0, max 1
        //public ProductType product

        /// min 1, max 1
        //public BinaryDataArrayListType BinaryDataArrayList

        /// <remarks>The zero-based index for this chromatogram in the chromatogram List</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string index

        /// <remarks>A unique identifier for this chromatogram</remarks>
        /// Required Attribute
        /// string
        //public string id

        /// <remarks>Default length of binary data arrays contained in this element.</remarks>
        /// Required Attribute
        /// integer
        //public int defaultArrayLength

        /// <remarks>This attribute can optionally reference the 'id' of the appropriate dataProcessing.</remarks>
        /// Optional Attribute
        /// IDREF
        //public string dataProcessingRef*/
    }

    /// <summary>
    /// mzML ScanSettingListType
    /// </summary>
    /// <remarks>List with the descriptions of the acquisition settings applied prior to the start of data acquisition.</remarks>
    public partial class ScanSettingsListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScanSettingsListType()
        {
            this.scanSettingsField = new List<ScanSettingsType>();
        }

        /*/// min 1, max unbounded
        //public ScanSettingsType[] scanSettings

        /// <remarks>The number of AcquisitionType elements in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML ScanSettingsType
    /// </summary>
    /// <remarks>Description of the acquisition settings of the instrument prior to the start of the run.</remarks>
    public partial class ScanSettingsType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScanSettingsType()
        {
            this.sourceFileRefList = null;
            this.targetList = null;
            this.id = null;
        }

        /*/// <remarks>List with the source files containing the acquisition settings.</remarks>
        /// min 0, max 1
        //public SourceFileRefListType sourceFileRefList

        /// <remarks>Target list (or 'inclusion list') configured prior to the run.</remarks>
        /// min 0, max 1
        //public TargetListType targetList

        /// <remarks>A unique identifier for this acquisition setting.</remarks>
        /// Required Attribute
        /// ID
        //public string id*/
    }

    /// <summary>
    /// mzML SourceFileRefListType
    /// </summary>
    public partial class SourceFileRefListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SourceFileRefListType()
        {
            this.sourceFileRefField = new List<SourceFileRefType>();
        }

        /*/// <remarks>Reference to a previously defined sourceFile.</remarks>
        /// min 0, max unbounded
        //public SourceFileRefType[] sourceFileRef

        /// <remarks>This number of source files referenced in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML SourceFileRefType
    /// </summary>
    /// <remarks></remarks>
    public partial class SourceFileRefType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SourceFileRefType()
        {
            this.@ref = null;
        }

        /*/// <remarks>This attribute must reference the 'id' of the appropriate sourceFile.</remarks>
        /// Required Attribute
        /// IDREF
        //public string @ref*/
    }

    /// <summary>
    /// mzML TargetListType
    /// </summary>
    /// <remarks>Target list (or 'inclusion list') configured prior to the run.</remarks>
    public partial class TargetListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TargetListType()
        {
            this.targetField = new List<ParamGroupType>();
        }

        /*/// min 1, max unbounded
        //public ParamGroupType[] target

        /// <remarks>The number of TargetType elements in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML SoftwareListType
    /// </summary>
    /// <remarks>List and descriptions of software used to acquire and/or process the data in this mzML file.</remarks>
    public partial class SoftwareListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SoftwareListType()
        {
            this.softwareField = new List<SoftwareType>();
        }

        /*/// <remarks>A piece of software.</remarks>
        /// min 1, max unbounded
        //public SoftwareType[] software

        /// <remarks>The number of softwares defined in this mzML file.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML SoftwareType
    /// </summary>
    /// <remarks>Software information</remarks>
    public partial class SoftwareType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SoftwareType()
        {
            this.id = null;
            this.version = null;
        }

        /*/// <remarks>An identifier for this software that is unique across all SoftwareTypes.</remarks>
        /// Required Attribute
        /// ID
        //public string id

        /// <remarks>The software version.</remarks>
        /// Required Attribute
        /// string
        //public string version*/
    }

    /// <summary>
    /// mzML InstrumentConfigurationListType
    /// </summary>
    /// <remarks>List and descriptions of instrument configurations. 
    /// At least one instrument configuration must be specified, even if it is 
    /// only to specify that the instrument is unknown. In that case, 
    /// the "instrument model" term is used to indicate the unknown 
    /// instrument in the instrumentConfiguration.</remarks>
    public partial class InstrumentConfigurationListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public InstrumentConfigurationListType()
        {
            this.instrumentConfigurationField = new List<InstrumentConfigurationType>();
        }

        /*/// min 1, max unbounded
        //public InstrumentConfigurationType[] instrumentConfiguration

        /// <remarks>The number of instrument configurations present in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML InstrumentConfigurationType
    /// </summary>
    /// <remarks>Description of a particular hardware configuration of a mass spectrometer. 
    /// Each configuration must have one (and only one) of the three different components used for an analysis. 
    /// For hybrid instruments, such as an LTQ-FT, there must be one configuration for each permutation of 
    /// the components that is used in the document. For software configuration, use a ReferenceableParamGroup element</remarks>
    public partial class InstrumentConfigurationType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public InstrumentConfigurationType()
        {
            this.componentList = null;
            this.softwareRef = null;
            this.id = null;
            this.scanSettingsRef = null;
        }

        /*/// min 0, max 1
        //public ComponentListType componentList

        /// min 0, max 1
        //public SoftwareRefType softwareRef

        /// <remarks>An identifier for this instrument configuration.</remarks>
        /// Required Attribute
        /// ID
        //public string id

        /// Optional Attribute
        /// IDREF
        //public string scanSettingsRef*/
    }

    /// <summary>
    /// mzML ComponentListType
    /// </summary>
    /// <remarks>List with the different components used in the mass spectrometer. At least one source, one mass analyzer and one detector need to be specified.</remarks>
    public partial class ComponentListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ComponentListType()
        {
            this.sourceField = new List<SourceComponentType>();
            this.analyzerField = new List<AnalyzerComponentType>();
            this.detectorField = new List<DetectorComponentType>();
        }

        /*/// <remarks>A source component.</remarks>
        /// min 1, max unbounded
        //public SourceComponentType[] source

        /// <remarks>A mass analyzer (or mass filter) component.</remarks>
        /// min 1, max unbounded
        //public AnalyzerComponentType[] analyzer

        /// <remarks>A detector component.</remarks>
        /// min 1, max unbounded
        //public DetectorComponentType[] detector

        /// <remarks>The number of components in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML ComponentType
    /// </summary>
    public partial class ComponentType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ComponentType()
        {
            this.order = 0;
        }

        /*/// <remarks>This attribute must be used to indicate the order in which the components 
        /// are encountered from source to detector (e.g., in a Q-TOF, the quadrupole would 
        /// have the lower order number, and the TOF the higher number of the two).</remarks>
        /// Required Attribute
        /// integer
        //public int order*/
    }

    /// <summary>
    /// mzML SourceComponentType
    /// </summary>
    /// <remarks>This element must be used to describe a Source Component Type. 
    /// This is a PRIDE3-specific modification of the core MzML schema that does not 
    /// have any impact on the base schema validation.</remarks>
    public partial class SourceComponentType : ComponentType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SourceComponentType()
        { }
    }

    /// <summary>
    /// mzML AnalyzerComponentType
    /// </summary>
    /// <remarks>This element must be used to describe an Analyzer Component Type. 
    /// This is a PRIDE3-specific modification of the core MzML schema that does not 
    /// have any impact on the base schema validation.</remarks>
    public partial class AnalyzerComponentType : ComponentType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AnalyzerComponentType()
        { }
    }

    /// <summary>
    /// mzML DetectorComponentType
    /// </summary>
    /// <remarks>This element must be used to describe a Detector Component Type. 
    /// This is a PRIDE3-specific modification of the core MzML schema that does not 
    /// have any impact on the base schema validation.</remarks>
    public partial class DetectorComponentType : ComponentType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DetectorComponentType()
        { }
    }

    /// <summary>
    /// mzML SoftwareRefType
    /// </summary>
    /// <remarks>Reference to a previously defined software element</remarks>
    public partial class SoftwareRefType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SoftwareRefType()
        {
            this.@ref = null;
        }

        /*/// <remarks>This attribute must be used to reference the 'id' attribute of a software element.</remarks>
        /// Required Attribute
        /// IDREF
        //public string @ref*/
    }

    /// <summary>
    /// mzML SampleListType
    /// </summary>
    /// <remarks>List and descriptions of samples.</remarks>
    public partial class SampleListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SampleListType()
        {
            this.sampleField = new List<SampleType>();
        }

        /*/// min 1, max unbounded
        //public SampleType[] sample

        /// <remarks>The number of Samples defined in this mzML file.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML SampleType
    /// </summary>
    /// <remarks>Expansible description of the sample used to generate the dataset, named in sampleName.</remarks>
    public partial class SampleType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SampleType()
        {
            this.id = null;
            this.name = null;
        }

        /*/// <remarks>A unique identifier across the samples with which to reference this sample description.</remarks>
        /// Required Attribute
        /// ID
        //public string id

        /// <remarks>An optional name for the sample description, mostly intended as a quick mnemonic.</remarks>
        /// Optional Attribute
        /// string
        //public string name*/
    }

    /// <summary>
    /// mzML SourceFileListType
    /// </summary>
    /// <remarks>List and descriptions of the source files this mzML document was generated or derived from</remarks>
    public partial class SourceFileListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SourceFileListType()
        {
            this.sourceFileField = new List<SourceFileType>();
        }

        /*/// min 1, max unbounded
        //public SourceFileType[] sourceFile

        /// <remarks>Number of source files used in generating the instance document.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML SourceFileType
    /// </summary>
    /// <remarks>Description of the source file, including location and type.</remarks>
    public partial class SourceFileType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SourceFileType()
        {
            this.id = null;
            this.name = null;
            this.location = null;
        }

        /*/// <remarks>An identifier for this file.</remarks>
        /// Required Attribute
        /// ID
        //public string id

        /// <remarks>Name of the source file, without reference to location (either URI or local path).</remarks>
        /// Required Attribute
        /// string
        //public string name

        /// <remarks>URI-formatted location where the file was retrieved.</remarks>
        /// Required Attribute
        /// anyURI
        //public string location*/
    }

    /// <summary>
    /// mzML FileDescriptionType
    /// </summary>
    /// <remarks>Information pertaining to the entire mzML file (i.e. not specific to any part of the data set) is stored here.</remarks>
    public partial class FileDescriptionType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FileDescriptionType()
        {
            this.fileContent = null;
            this.sourceFileList = null;
            this.contactField = new List<ParamGroupType>();
        }

        /*/// <remarks>This summarizes the different types of spectra that can be expected in the file. 
        /// This is expected to aid processing software in skipping files that do not contain appropriate 
        /// spectrum types for it. It should also describe the nativeID format used in the file by referring to an appropriate CV term.</remarks>
        /// min 1, max 1
        //public ParamGroupType fileContent

        /// min 0, max 1
        //public SourceFileListType sourceFileList

        /// min 0, max unbounded
        //public ParamGroupType[] contact*/
    }
}
