﻿// 
// Data translation code from MSData to mzML.
// 

using System;
using PSI_Interface.MSData;
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
        /// Translating Constructor from a MSData object
        /// </summary>
        /// <param name="msData"></param>
        public mzMLType(MSData msData)
        {
            this.idField = msData.Id;
            this.versionField = msData.Version;
            this.accessionField = msData.Accession; // Not tied to CV

            // Default value
            this.cvListField = null;
            this.fileDescriptionField = null;
            this.referenceableParamGroupListField = null;
            this.sampleListField = null;
            this.softwareListField = null;
            this.scanSettingsListField = null;
            this.instrumentConfigurationListField = null;
            this.dataProcessingListField = null;
            this.runField = null;

            if (msData.CVList != null)
            {
                this.cvListField = new CVListType(msData.CVList);
            }
            if (msData.FileDescription != null)
            {
                this.fileDescriptionField = new FileDescriptionType(msData.FileDescription);
            }
            if (msData.ReferenceableParamGroupList != null)
            {
                this.referenceableParamGroupListField = new ReferenceableParamGroupListType(msData.ReferenceableParamGroupList);
            }
            if (msData.SampleList != null)
            {
                this.sampleListField = new SampleListType(msData.SampleList);
            }
            if (msData.SoftwareList != null)
            {
                this.softwareListField = new SoftwareListType(msData.SoftwareList);
            }
            if (msData.ScanSettingsList != null)
            {
                this.scanSettingsListField = new ScanSettingsListType(msData.ScanSettingsList);
            }
            if (msData.InstrumentConfigurationList != null)
            {
                this.instrumentConfigurationListField = new InstrumentConfigurationListType(msData.InstrumentConfigurationList);
            }
            if (msData.DataProcessingList != null)
            {
                this.dataProcessingListField = new DataProcessingListType(msData.DataProcessingList);
            }
            if (msData.Run != null)
            {
                this.runField = new RunType(msData.Run);
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="ocvList"></param>
        public CVListType(List<CVInfo> ocvList)
        {
            // Default value
            this.cvField = null;

            if (ocvList != null && ocvList.Count > 0)
            {
                this.cvField = new List<CVType>();
                foreach (var ocv in ocvList)
                {
                    this.cvField.Add(new CVType(ocv));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="cv"></param>
        public CVType(CVInfo cv)
        {
            this.idField = cv.Id;
            this.fullNameField = cv.FullName;
            this.versionField = cv.Version;
            this.uRIField = cv.URI;
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="dpList"></param>
        public DataProcessingListType(List<DataProcessingInfo> dpList)
        {
            // Default value
            this.dataProcessingField = null;

            if (dpList != null && dpList.Count > 0)
            {
                this.dataProcessingField = new List<DataProcessingType>();
                foreach (var dp in dpList)
                {
                    this.dataProcessingField.Add(new DataProcessingType(dp));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="dp"></param>
        public DataProcessingType(DataProcessingInfo dp)
        {
            this.idField = dp.Id;

            // Default value
            this.processingMethodField = null;

            if (dp.ProcessingMethods != null && dp.ProcessingMethods.Count > 0)
            {
                this.processingMethodField = new List<ProcessingMethodType>();
                foreach (var pm in dp.ProcessingMethods)
                {
                    this.processingMethodField.Add(new ProcessingMethodType(pm));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="process"></param>
        public ProcessingMethodType(ProcessingMethodInfo process) : base(process)
        {
            this.orderField = process.Order.ToString();
            this.softwareRefField = process.SoftwareRef;
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="pg"></param>
        public ParamGroupType(ParamGroup pg)
        {
            // Default value
            this.referenceableParamGroupRefField = null;
            this.cvParamField = null;
            this.userParamField = null;

            if (pg.ReferenceableParamGroupRefs != null && pg.ReferenceableParamGroupRefs.Count > 0)
            {
                this.referenceableParamGroupRefField = new List<ReferenceableParamGroupRefType>();
                foreach (var rpgr in pg.ReferenceableParamGroupRefs)
                {
                    this.referenceableParamGroupRefField.Add(new ReferenceableParamGroupRefType(rpgr));
                }
            }
            if (pg.CVParams != null && pg.CVParams.Count > 0)
            {
                this.cvParamField = new List<CVParamType>();
                foreach (var cvp in pg.CVParams)
                {
                    this.cvParamField.Add(new CVParamType(cvp));
                }
            }
            if (pg.UserParams != null && pg.UserParams.Count > 0)
            {
                this.userParamField = new List<UserParamType>();
                foreach (var up in pg.UserParams)
                {
                    this.userParamField.Add(new UserParamType(up));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="rpgList"></param>
        public ReferenceableParamGroupListType(List<ReferenceableParamGroup> rpgList)
        {
            // Default value
            this.referenceableParamGroupField = null;

            if (rpgList != null && rpgList.Count > 0)
            {
                this.referenceableParamGroupField = new List<ReferenceableParamGroupType>();
                foreach (var rpg in rpgList)
                {
                    this.referenceableParamGroupField.Add(new ReferenceableParamGroupType(rpg));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="rpg"></param>
        public ReferenceableParamGroupType(ReferenceableParamGroup rpg)
        {
            this.idField = rpg.Id;

            // Default value
            this.cvParamField = null;
            this.userParamField = null;

            if (rpg.CVParams != null && rpg.CVParams.Count > 0)
            {
                this.cvParamField = new List<CVParamType>();
                foreach (var cvp in rpg.CVParams)
                {
                    this.cvParamField.Add(new CVParamType(cvp));
                }
            }
            if (rpg.UserParams != null && rpg.UserParams.Count > 0)
            {
                this.userParamField = new List<UserParamType>();
                foreach (var up in rpg.UserParams)
                {
                    this.userParamField.Add(new UserParamType(up));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="rpgr"></param>
        public ReferenceableParamGroupRefType(ReferenceableParamGroupRef rpgr)
        {
            this.@refField = rpgr.Ref;
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="cvp"></param>
        public CVParamType(CVParam cvp)
        {
            this.cvRefField = cvp.CVRef;
            this.accessionField = cvp.Accession;
            this.nameField = cvp.Name;
            this.valueField = cvp.Value;
            this.unitCvRefField = cvp.UnitCVRef;
            this.unitAccessionField = cvp.UnitAccession;
            this.unitNameField = cvp.UnitName;
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="up"></param>
        public UserParamType(UserParam up)
        {
            this.nameField = up.Name;
            this.typeField = up.Type;
            this.valueField = up.Value;
            this.unitAccessionField = up.UnitAccession;
            this.unitNameField = up.UnitName;
            this.unitCvRefField = up.UnitCVRef;
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="precursors"></param>
        public PrecursorListType(List<Precursor> precursors)
        {
            // Default value
            this.precursorField = null;

            if (precursors != null && precursors.Count > 0)
            {
                this.precursorField = new List<PrecursorType>();
                foreach (var p in precursors)
                {
                    this.precursorField.Add(new PrecursorType(p));
                }
            }
        }

        /*/// min 1, max unbounded
        //public Precursor[] precursor

        /// <remarks>The number of precursor isolations in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        //public string count*/
    }

    /// <summary>
    /// mzML Precursor
    /// </summary>
    /// <remarks>The method of precursor ion selection and activation</remarks>
    public partial class PrecursorType
    {
        /// <summary>
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="precursor"></param>
        public PrecursorType(Precursor precursor)
        {
            this.spectrumRefField = precursor.SpectrumRef;
            this.sourceFileRefField = precursor.SourceFileRef;
            this.externalSpectrumIDField = precursor.ExternalSpectrumID;

            // Default values
            this.isolationWindowField = null;
            this.selectedIonListField = null;
            this.activationField = null;

            if (precursor.IsolationWindow != null)
            {
                this.isolationWindowField = new ParamGroupType(precursor.IsolationWindow);
            }
            if (precursor.SelectedIonList != null)
            {
                this.selectedIonListField = new SelectedIonListType(precursor.SelectedIonList);
            }
            if (precursor.Activation != null)
            {
                this.activationField = new ParamGroupType(precursor.Activation);
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="siList"></param>
        public SelectedIonListType(List<ParamGroup> siList)
        {
            // Default value
            this.selectedIonField = null;

            if (siList != null && siList.Count > 0)
            {
                this.selectedIonField = new List<ParamGroupType>();
                foreach (var si in siList)
                {
                    this.selectedIonField.Add(new ParamGroupType(si));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="bdal"></param>
        public BinaryDataArrayListType(List<BinaryDataArray> bdal)
        {
            // Default value
            this.binaryDataArrayField = null;

            if (bdal != null && bdal.Count > 0)
            {
                this.binaryDataArrayField = new List<BinaryDataArrayType>();
                foreach (var bda in bdal)
                {
                    this.binaryDataArrayField.Add(new BinaryDataArrayType(bda));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="bda"></param>
        public BinaryDataArrayType(BinaryDataArray bda) : base(bda)
        {
            this.binaryField = bda.Binary;
            this.arrayLengthField = null;
            // Array length: (should) never specified for m/z or intensity arrays, otherwise only if different from the default.
            if (bda.DataType != BinaryDataArray.ArrayType.m_z &&
                bda.DataType != BinaryDataArray.ArrayType.intensity && bda.ArrayLength != bda.BdaDefaultArrayLength)
            {
                this.arrayLengthField = bda.ArrayLength.ToString();
            }
            this.dataProcessingRefField = bda.DataProcessingRef;
            // Not the length of the binary array, but the length of the base64 string...
            //this.encodedLengthField = binary.Length.ToString();
            this.encodedLengthField = Convert.ToBase64String(binary).Length.ToString();
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="scans"></param>
        public ScanListType(ScanList scans) : base(scans)
        {
            // Default value
            this.scanField = null;

            if (scans.Scan != null && scans.Scan.Count > 0)
            {
                this.scanField = new List<ScanType>();
                foreach (var s in scans.Scan)
                {
                    this.scanField.Add(new ScanType(s));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="scan"></param>
        public ScanType(Scan scan) : base(scan)
        {
            this.spectrumRefField = scan.SpectrumRef;
            this.sourceFileRefField = scan.SourceFileRef;
            this.externalSpectrumIDField = scan.ExternalSpectrumID;
            this.instrumentConfigurationRefField = scan.InstrumentConfigurationRef;

            // Default value
            this.scanWindowListField = null;

            if (scan.ScanWindowList != null)
            {
                this.scanWindowListField = new ScanWindowListType(scan.ScanWindowList);
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="swList"></param>
        public ScanWindowListType(List<ParamGroup> swList)
        {
            // Default value
            this.scanWindowField = null;

            if (swList != null && swList.Count > 0)
            {
                this.scanWindowField = new List<ParamGroupType>();
                foreach (var sw in swList)
                {
                    this.scanWindowField.Add(new ParamGroupType(sw));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="specList"></param>
        public SpectrumListType(SpectrumList specList)
        {
            this.defaultDataProcessingRefField = specList.DefaultDataProcessingRef;

            // Default value
            this.spectrumField = null;

            if (specList.Spectra != null && specList.Spectra.Count > 0)
            {
                this.spectrumField = new List<SpectrumType>();
                foreach (var s in specList.Spectra)
                {
                    this.spectrumField.Add(new SpectrumType(s));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="spectrum"></param>
        public SpectrumType(Spectrum spectrum) : base(spectrum)
        {
            this.idField = spectrum.Id;
            this.spotIDField = spectrum.SpotID;
            this.indexField = spectrum.Index;
            this.defaultArrayLengthField = spectrum.DefaultArrayLength;
            this.dataProcessingRefField = spectrum.DataProcessingRef;
            this.sourceFileRefField = spectrum.SourceFileRef;

            // Default values
            this.scanListField = null;
            this.precursorListField = null;
            this.productListField = null;
            this.binaryDataArrayListField = null;

            if (spectrum.ScanList != null)
            {
                this.scanListField = new ScanListType(spectrum.ScanList);
            }
            if (spectrum.PrecursorList != null)
            {
                this.precursorListField = new PrecursorListType(spectrum.PrecursorList);
            }
            if (spectrum.ProductList != null)
            {
                this.productListField = new ProductListType(spectrum.ProductList);
            }
            if (spectrum.BinaryDataArrayList != null)
            {
                this.binaryDataArrayListField = new BinaryDataArrayListType(spectrum.BinaryDataArrayList);
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="products"></param>
        public ProductListType(List<Product> products)
        {
            // Default value
            this.productField = null;

            if (products != null && products.Count > 0)
            {
                this.productField = new List<ProductType>();
                foreach (var p in products)
                {
                    this.productField.Add(new ProductType(p));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="product"></param>
        public ProductType(Product product)
        {
            // Default value
            this.isolationWindowField = null;

            if (product.IsolationWindow != null)
            {
                this.isolationWindowField = new ParamGroupType(product.IsolationWindow);
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="run"></param>
        public RunType(Run run) : base(run)
        {
            this.idField = run.Id;
            this.defaultInstrumentConfigurationRefField = run.DefaultInstrumentConfigurationRef;
            this.defaultSourceFileRefField = run.DefaultSourceFileRef;
            this.sampleRefField = run.SampleRef;
            this.startTimeStampField = run.StartTimeStamp;
            this.startTimeStampFieldSpecified = run.StartTimeStampSpecified;

            // Default values
            this.spectrumListField = null;
            this.chromatogramListField = null;

            if (run.SpectrumList != null)
            {
                this.spectrumListField = new SpectrumListType(run.SpectrumList);
            }
            if (run.ChromatogramList != null)
            {
                this.chromatogramListField = new ChromatogramListType(run.ChromatogramList);
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="chromList"></param>
        public ChromatogramListType(ChromatogramList chromList)
        {
            this.defaultDataProcessingRefField = chromList.DefaultDataProcessingRef;

            // Default value
            this.chromatogramField = null;

            if (chromList.Chromatograms != null && chromList.Chromatograms.Count > 0)
            {
                this.chromatogramField = new List<ChromatogramType>();
                foreach (var c in chromList.Chromatograms)
                {
                    this.chromatogramField.Add(new ChromatogramType(c));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="chromatogram"></param>
        public ChromatogramType(Chromatogram chromatogram) : base(chromatogram)
        {
            this.indexField = chromatogram.Index;
            this.idField = chromatogram.Id;
            this.defaultArrayLengthField = chromatogram.DefaultArrayLength;
            this.dataProcessingRefField = chromatogram.DataProcessingRef;

            // Default value
            this.precursorField = null;
            this.productField = null;
            this.binaryDataArrayListField = null;

            if (chromatogram.Precursor != null)
            {
                this.precursorField = new PrecursorType(chromatogram.Precursor);
            }
            if (chromatogram.Product != null)
            {
                this.productField = new ProductType(chromatogram.Product);
            }
            if (chromatogram.BinaryDataArrayList != null)
            {
                this.binaryDataArrayListField = new BinaryDataArrayListType(chromatogram.BinaryDataArrayList);
            }
        }

        /*/// min 0, max 1
        //public Precursor precursor

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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="ssList"></param>
        public ScanSettingsListType(List<ScanSettingsInfo> ssList)
        {
            // Default value
            this.scanSettingsField = null;

            if (ssList != null && ssList.Count > 0)
            {
                this.scanSettingsField = new List<ScanSettingsType>();
                foreach (var ss in ssList)
                {
                    this.scanSettingsField.Add(new ScanSettingsType(ss));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="settings"></param>
        public ScanSettingsType(ScanSettingsInfo settings) : base(settings)
        {
            this.idField = settings.Id;

            // Default values
            this.sourceFileRefListField = null;
            this.targetListField = null;

            if (settings.SourceFileRefList != null)
            {
                this.sourceFileRefListField = new SourceFileRefListType(settings.SourceFileRefList);
            }
            if (settings.TargetList != null)
            {
                this.targetListField = new TargetListType(settings.TargetList);
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="sfrList"></param>
        public SourceFileRefListType(List<SourceFileRef> sfrList)
        {
            // Default value
            this.sourceFileRefField = null;

            if (sfrList != null && sfrList.Count > 0)
            {
                this.sourceFileRefField = new List<SourceFileRefType>();
                foreach (var sfr in sfrList)
                {
                    this.sourceFileRefField.Add(new SourceFileRefType(sfr));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="sfr"></param>
        public SourceFileRefType(SourceFileRef sfr)
        {
            this.@refField = sfr.Ref;
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="oTargetList"></param>
        public TargetListType(List<ParamGroup> oTargetList)
        {
            // Default value
            this.targetField = null;

            if (oTargetList != null && oTargetList.Count > 0)
            {
                this.targetField = new List<ParamGroupType>();
                foreach (var t in oTargetList)
                {
                    this.targetField.Add(new ParamGroupType(t));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="scanList"></param>
        public SoftwareListType(List<SoftwareInfo> scanList)
        {
            // Default value
            this.softwareField = null;

            if (scanList != null && scanList.Count > 0)
            {
                this.softwareField = new List<SoftwareType>();
                foreach (var s in scanList)
                {
                    this.softwareField.Add(new SoftwareType(s));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="software"></param>
        public SoftwareType(SoftwareInfo software) : base(software)
        {
            this.idField = software.Id;
            this.versionField = software.Version;
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="icList"></param>
        public InstrumentConfigurationListType(List<InstrumentConfigurationInfo> icList)
        {
            // Default value
            this.instrumentConfigurationField = null;

            if (icList != null && icList.Count > 0)
            {
                this.instrumentConfigurationField = new List<InstrumentConfigurationType>();
                foreach (var ic in icList)
                {
                    this.instrumentConfigurationField.Add(new InstrumentConfigurationType(ic));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="ic"></param>
        public InstrumentConfigurationType(InstrumentConfigurationInfo ic) : base(ic)
        {
            this.idField = ic.Id;
            this.scanSettingsRefField = ic.ScanSettingsRef;

            // Default value
            this.componentListField = null;
            this.softwareRefField = null;

            if (ic.ComponentList != null)
            {
                this.componentListField = new ComponentListType(ic.ComponentList);
            }
            if (ic.SoftwareRef != null)
            {
                this.softwareRefField = new SoftwareRefType(ic.SoftwareRef);
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="comp"></param>
        public ComponentListType(ComponentList comp)
        {
            // Default values
            this.sourceField = null;
            this.analyzerField = null;
            this.detectorField = null;

            if (comp.Sources != null && comp.Sources.Count > 0)
            {
                this.sourceField = new List<SourceComponentType>();
                foreach (var s in comp.Sources)
                {
                    this.sourceField.Add(new SourceComponentType(s));
                }
            }
            if (comp.Analyzers != null && comp.Analyzers.Count > 0)
            {
                this.analyzerField = new List<AnalyzerComponentType>();
                foreach (var a in comp.Analyzers)
                {
                    this.analyzerField.Add(new AnalyzerComponentType(a));
                }
            }
            if (comp.Detectors != null && comp.Detectors.Count > 0)
            {
                this.detectorField = new List<DetectorComponentType>();
                foreach (var d in comp.Detectors)
                {
                    this.detectorField.Add(new DetectorComponentType(d));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="comp"></param>
        public ComponentType(Component comp) : base(comp)
        {
            this.orderField = comp.Order;
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="sc"></param>
        public SourceComponentType(SourceComponent sc) : base(sc)
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="ac"></param>
        public AnalyzerComponentType(AnalyzerComponent ac) : base(ac)
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="dc"></param>
        public DetectorComponentType(DetectorComponent dc) : base(dc)
        { }
    }

    /// <summary>
    /// mzML SoftwareRefType
    /// </summary>
    /// <remarks>Reference to a previously defined software element</remarks>
    public partial class SoftwareRefType
    {
        /// <summary>
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="sr"></param>
        public SoftwareRefType(SoftwareRef sr)
        {
            this.@refField = sr.Ref;
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="oSampleList"></param>
        public SampleListType(List<SampleInfo> oSampleList)
        {
            // Default value
            this.sampleField = null;

            if (oSampleList != null && oSampleList.Count > 0)
            {
                this.sampleField = new List<SampleType>();
                foreach (var s in oSampleList)
                {
                    this.sampleField.Add(new SampleType(s));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="sample"></param>
        public SampleType(SampleInfo sample) : base(sample)
        {
            this.idField = sample.Id;
            this.nameField = sample.Name;
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="sfList"></param>
        public SourceFileListType(List<SourceFileInfo> sfList)
        {
            // Default value
            this.sourceFileField = null;

            if (sfList != null && sfList.Count > 0)
            {
                this.sourceFileField = new List<SourceFileType>();
                foreach (var sf in sfList)
                {
                    this.sourceFileField.Add(new SourceFileType(sf));
                }
            }
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="sf"></param>
        public SourceFileType(SourceFileInfo sf) : base(sf)
        {
            this.idField = sf.Id;
            this.nameField = sf.Id;
            this.locationField = sf.Location;
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
        /// Translating Constructor from a MSData child object
        /// </summary>
        /// <param name="fd"></param>
        public FileDescriptionType(FileDescription fd)
        {
            // Default values
            this.fileContentField = null;
            this.sourceFileListField = null;
            this.contactField = null;

            if (fd.FileContentInfo != null)
            {
                this.fileContentField = new ParamGroupType(fd.FileContentInfo);
            }
            if (fd.SourceFileList != null)
            {
                this.sourceFileListField = new SourceFileListType(fd.SourceFileList);
            }
            if (fd.ContactInfo != null && fd.ContactInfo.Count > 0)
            {
                this.contactField = new List<ParamGroupType>();
                foreach (var c in fd.ContactInfo)
                {
                    this.contactField.Add(new ParamGroupType(c));
                }
            }
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
