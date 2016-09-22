﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSI_Interface.IdentData.IdentDataObjs;

namespace PSI_Interface.IdentData
{
    /// <summary>
    /// A simplified class for creating mzIdentML output
    /// </summary>
    public class IdentDataCreator
    {
        /// <summary>
        /// Get the IdentData object, after tying it all together
        /// </summary>
        /// <returns></returns>
        public IdentDataObj GetIdentData()
        {
            FinalizeFile();
            return identData;
        }

        /// <summary>
        /// The IdentData object that will be populated and prepared for output.
        /// </summary>
        private readonly IdentDataObj identData;

        private int dbCounter = 1;
        private int specDataCounter = 1;
        private int searchProtocolCounter = 1;
        private int specListCounter = 1;

        private Dictionary<string, SpectrumIdentificationResultObj> identificationResults = new Dictionary<string, SpectrumIdentificationResultObj>();

        /// <summary>
        /// Prepare to populate and then generate an MZID file
        /// </summary>
        /// <param name="identifier">Identifier for the file data, often the name of the dataset or the analysis software. Unique in the scope.</param>
        /// <param name="name">Name of the file. Can be the name of the dataset or analysis software</param>
        public IdentDataCreator(string identifier, string name)
        {
            identData = new IdentDataObj()
            {
                Id = identifier,
                Name = name,
                AnalysisProtocolCollection = new AnalysisProtocolCollectionObj(),
                DataCollection = new DataCollectionObj()
                {
                    AnalysisData = new AnalysisDataObj(),
                },
                AnalysisCollection = new AnalysisCollectionObj(),
                SequenceCollection = new SequenceCollectionObj(),
            };
        }

        private void FinalizeFile()
        {
            identData.DataCollection.AnalysisData.SpectrumIdentificationList = new IdentDataList<SpectrumIdentificationListObj>();
            var specList = new SpectrumIdentificationListObj()
            {
                Id = "SI_LIST_" + specListCounter++,

            };
            identData.DataCollection.AnalysisData.SpectrumIdentificationList.Add(specList);

            var analysisCollection = new SpectrumIdentificationObj()
            {
                SpectrumIdentificationProtocol = identData.AnalysisProtocolCollection.SpectrumIdentificationProtocols.First(),
                SpectrumIdentificationList = identData.DataCollection.AnalysisData.SpectrumIdentificationList.First(),
                Id = "SpecIdent_1",
                InputSpectra = new IdentDataList<InputSpectraRefObj>(),
                SearchDatabases = new IdentDataList<SearchDatabaseRefObj>(),
            };

            foreach (var ispec in identData.DataCollection.Inputs.SpectraDataList)
            {
                analysisCollection.InputSpectra.Add(new InputSpectraRefObj()
                {
                    SpectraData = ispec
                });
            }

            foreach (var searchDb in identData.DataCollection.Inputs.SearchDatabases)
            {
                analysisCollection.SearchDatabases.Add(new SearchDatabaseRefObj()
                {
                    SearchDatabase = searchDb
                });
            }
            identData.AnalysisCollection.SpectrumIdentifications.Add(analysisCollection);

            specList.SpectrumIdentificationResults = new IdentDataList<SpectrumIdentificationResultObj>();
            var dbseqList = new List<DbSequenceObj>();
            var peptideList = new List<PeptideObj>();
            var pepEvList = new List<PeptideEvidenceObj>();
            foreach (var result in identificationResults.Values)
            {
                specList.SpectrumIdentificationResults.Add(result);
                foreach (var sii in result.SpectrumIdentificationItems)
                {
                    if (sii.Peptide != null)
                    {
                        peptideList.Add(sii.Peptide);
                    }
                    foreach (var pepEv in sii.PeptideEvidences)
                    {
                        dbseqList.Add(pepEv.PeptideEvidence.DBSequence);
                        pepEvList.Add(pepEv.PeptideEvidence);
                    }
                }
            }



            // TODO: Deduplicate the peptide and dbsequence lists

            var dbSeqCounter = 1L;
            var peptideCounter = 1L;

            var dbSeqDeDup = new Dictionary<string, DbSequenceObj>();

            foreach (var dbseq in dbseqList)
            {
                DbSequenceObj matched;
                if (dbSeqDeDup.TryGetValue(dbseq.Accession, out matched))
                {
                    //System.Console.WriteLine("Dropped duplicate DBSequence!");
                    dbseq.Id = matched.Id;
                }
                else
                {
                    dbseq.Id = "DBSeq" + dbSeqCounter++;
                    dbSeqDeDup.Add(dbseq.Accession, dbseq);
                }
            }
            //identData.SequenceCollection.DBSequences.AddRange(dbseqList);
            identData.SequenceCollection.DBSequences.AddRange(dbSeqDeDup.Values);

            var pepDeDup = new Dictionary<string, Dictionary<string, PeptideObj>>();
            var peptideListFinal = new List<PeptideObj>();
            foreach (var pep in peptideList)
            {
                var modKey = PepModConcat(pep);
                Dictionary<string, PeptideObj> similar;
                if (pepDeDup.TryGetValue(pep.PeptideSequence, out similar))
                {
                    PeptideObj other;
                    if (similar.TryGetValue(modKey, out other))
                    {
                        //System.Console.WriteLine("Dropped duplicate Peptide!");
                        pep.Id = other.Id;
                        continue;
                    }
                }
                else
                {
                    similar = new Dictionary<string, PeptideObj>();
                    pepDeDup.Add(pep.PeptideSequence, similar);
                }
                // TODO: Make id more meaningful. Doing this earlier might make deduplication easier too.
                pep.Id = "Pep_" + peptideCounter++;
                peptideListFinal.Add(pep);
                similar.Add(modKey, pep);
            }
            //identData.SequenceCollection.Peptides.AddRange(peptideList);
            identData.SequenceCollection.Peptides.AddRange(peptideListFinal);

            var pepEvDeDup = new Dictionary<string, PeptideEvidenceObj>();
            foreach (var pepEv in pepEvList)
            {
                pepEv.Id = "PepEv_" + pepEv.DBSequence.Id.Remove(0, 5) + "_" + pepEv.Peptide.Id.Remove(0, 4) + "_" +
                           pepEv.Start;
                if (!pepEvDeDup.ContainsKey(pepEv.Id))
                {
                    pepEvDeDup.Add(pepEv.Id, pepEv);
                }
            }
            //identData.SequenceCollection.PeptideEvidences.AddRange(pepEvList);
            identData.SequenceCollection.PeptideEvidences.AddRange(pepEvDeDup.Values);

            specList.NumSequencesSearched = identData.SequenceCollection.DBSequences.Count;

            identData.CascadeProperties();
        }

        private string PepModConcat(PeptideObj pep)
        {
            var result = "";
            foreach (var mod in pep.Modifications)
            {
                var modCv = mod.CVParams.First();
                var modName = "";
                if (modCv.Cvid == CV.CV.CVID.MS_unknown_modification)
                {
                    modName = modCv.Value;
                }
                else
                {
                    modName = modCv.Name;
                }
                result += string.Format("{0} {1},", modName, mod.Location);
            }
            return result;
        }

        /// <summary>
        /// Adds information about the analysis software to the file
        /// </summary>
        /// <param name="id">Unique identifier for the entry</param>
        /// <param name="name">Name of the analysis software</param>
        /// <param name="version">Version of the analysis software</param>
        /// <param name="cvid">CVID (if it exists) for the analysis software</param>
        /// <param name="userParamSoftwareName">Long name of the Analysis software. Not used unless cvid is "CVID_Unknown"</param>
        /// <returns>The analysis software object to further modify, if needed.</returns>
        public AnalysisSoftwareObj AddAnalysisSoftware(string id, string name, string version, CV.CV.CVID cvid,
            string userParamSoftwareName = "")
        {
            var software = new AnalysisSoftwareObj()
            {
                Name = name,
                Id = id,
                Version = version,
            };
            software.SoftwareName = new ParamObj();
            if (cvid != CV.CV.CVID.CVID_Unknown)
            {
                software.SoftwareName.Item = new CVParamObj(cvid);
            }
            else
            {
                software.SoftwareName.Item = new UserParamObj()
                {
                    Name = userParamSoftwareName,
                };
            }

            identData.AnalysisSoftwareList.Add(software);

            return software;
        }

        /// <summary>
        /// Adds information about a search database to the IdentData
        /// </summary>
        /// <param name="location">Path to the database (absolute path)</param>
        /// <param name="numberOfSequences">Number of sequences in the database</param>
        /// <param name="databaseName">Name of the database (can be the filename)</param>
        /// <param name="publicDatabaseName">Name of the database, if it maps exactly to a CV term; otherwise use CVID_Unknown</param>
        /// <param name="fileFormatCvid">The format of the database, if it exists in the CV</param>
        /// <returns>An initialized and added database, can add CVParams that add details about the database. See remarks.</returns>
        /// <remarks>
        /// Valid CVParams to add:
        ///    CVID.MS_MD5
        ///    CVID.MS_SHA_1
        ///
        ///    CVID.MS_database_source
        ///    CVID.MS_database_name
        ///    CVID.MS_database_local_file_path_OBSOLETE
        ///    CVID.MS_database_original_uri
        ///    CVID.MS_database_version_OBSOLETE
        ///    CVID.MS_database_release_date_OBSOLETE
        ///    CVID.MS_database_type
        ///    CVID.MS_database_filtering
        ///    CVID.MS_translation_frame
        ///    CVID.MS_translation_table
        ///    CVID.MS_number_of_peptide_seqs_compared_to_each_spectrum
        ///    CVID.MS_database_sequence_details
        ///    CVID.MS_database_file_formats
        ///    CVID.MS_translation_start_codon,
        ///    CVID.MS_translation_table_description
        ///    CVID.MS_decoy_DB_details
        ///    CVID.MS_number_of_decoy_sequences
        /// 
        ///    CVID.MS_decoy_DB_type_reverse
        ///    CVID.MS_decoy_DB_type_randomized
        ///    CVID.MS_DB_composition_target_decoy
        ///    CVID.MS_decoy_DB_accession_regexp
        ///    CVID.MS_decoy_DB_derived_from_OBSOLETE
        /// </remarks>
        public SearchDatabaseInfo AddSearchDatabase(string location, long numberOfSequences, string databaseName, CV.CV.CVID publicDatabaseName = CV.CV.CVID.CVID_Unknown,
            CV.CV.CVID fileFormatCvid = CV.CV.CVID.CVID_Unknown)
        {
            var db = new SearchDatabaseInfo()
            {
                Id = "SearchDB_" + dbCounter++,
                Location = location,
                Name = databaseName,
                NumDatabaseSequences = numberOfSequences,
            };
            db.DatabaseName = new ParamObj();
            if (publicDatabaseName != CV.CV.CVID.CVID_Unknown)
            {
                db.DatabaseName.Item = new CVParamObj(publicDatabaseName);
            }
            else
            {
                db.DatabaseName.Item = new UserParamObj()
                {
                    Name = databaseName,
                };
            }
            if (fileFormatCvid != CV.CV.CVID.CVID_Unknown)
            {
                db.FileFormat = new FileFormatInfo();
                db.FileFormat.CVParam = new CVParamObj(fileFormatCvid);
            }

            if (identData.DataCollection.Inputs == null)
            {
                identData.DataCollection.Inputs = new InputsObj();
            }
            if (identData.DataCollection.Inputs.SearchDatabases == null)
            {
                identData.DataCollection.Inputs.SearchDatabases = new IdentDataList<SearchDatabaseInfo>();
            }
            identData.DataCollection.Inputs.SearchDatabases.Add(db);

            return db;
        }

        /// <summary>
        /// Add information about the spectra input to the search
        /// </summary>
        /// <param name="location"></param>
        /// <param name="name"></param>
        /// <param name="spectrumIdFormat"></param>
        /// <param name="fileFormatCvid"></param>
        /// <returns></returns>
        public SpectraDataObj AddSpectraData(string location, string name, CV.CV.CVID spectrumIdFormat,
            CV.CV.CVID fileFormatCvid = CV.CV.CVID.CVID_Unknown)
        {
            var dataFile = new SpectraDataObj()
            {
                Id = "SD_" + specDataCounter++,
                Location = location,
                Name = name,
            };
            dataFile.SpectrumIDFormat = new SpectrumIDFormatObj()
            {
                CVParam = new CVParamObj(spectrumIdFormat),
            };
            if (fileFormatCvid != CV.CV.CVID.CVID_Unknown)
            {
                dataFile.FileFormat = new FileFormatInfo()
                {
                    CVParam = new CVParamObj(fileFormatCvid),
                };
            }
            if (identData.DataCollection.Inputs == null)
            {
                identData.DataCollection.Inputs = new InputsObj();
            }
            if (identData.DataCollection.Inputs.SpectraDataList == null)
            {
                identData.DataCollection.Inputs.SpectraDataList = new IdentDataList<SpectraDataObj>();
            }
            identData.DataCollection.Inputs.SpectraDataList.Add(dataFile);

            return dataFile;
        }

        /// <summary>
        /// Add the settings used with the software "analysisSoftwareInfo"
        /// </summary>
        /// <param name="analysisSoftwareInfo">The object returned by <see cref="AddAnalysisSoftware"/></param>
        /// <param name="name">The name of the set of settings</param>
        /// <param name="searchType">The type of search performed: 
        ///     CVID.MS_de_novo_search
        ///     CVID.MS_spectral_library_search
        ///     CVID.MS_spectral_library_search
        ///     CVID.MS_pmf_search
        ///     CVID.MS_tag_search
        ///     CVID.MS_ms_ms_search
        ///     CVID.MS_combined_pmf___ms_ms_search
        ///     CVID.MS_special_processing
        ///     CVID.MS_peptide_level_scoring
        ///     CVID.MS_modification_localization_scoring
        ///     CVID.MS_consensus_scoring
        ///     CVID.MS_sample_pre_fractionation
        ///     CVID.MS_cross_linking_search
        ///     CVID.MS_no_special_processing
        /// </param>
        /// <returns>An object that needs multiple items set - add CV/User params to AdditionalSearchParams, ModificationParams, Enzymes, FragmentTolerances, ParentTolerances, and Threshold</returns>
        public SpectrumIdentificationProtocolObj AddAnalysisSettings(AnalysisSoftwareObj analysisSoftwareInfo, string name, CV.CV.CVID searchType)
        {
            var settings = new SpectrumIdentificationProtocolObj()
            {
                Id = "SearchProtocol_" + searchProtocolCounter++,
                AnalysisSoftware = analysisSoftwareInfo,
                Name = name,
            };

            settings.SearchType = new ParamObj();
            if (searchType != CV.CV.CVID.CVID_Unknown)
            {
                settings.SearchType.Item = new CVParamObj(searchType);
            }
            settings.AdditionalSearchParams = new ParamListObj()
            {
                Items = new IdentDataList<ParamBaseObj>()
            };
            settings.ModificationParams = new IdentDataList<SearchModificationObj>();
            settings.Enzymes = new EnzymeListObj()
            {
                Enzymes = new IdentDataList<EnzymeObj>(),
            };

            settings.FragmentTolerances = new IdentDataList<CVParamObj>();
            settings.ParentTolerances = new IdentDataList<CVParamObj>();
            settings.Threshold = new ParamListObj()
            {
                Items = new IdentDataList<ParamBaseObj>(),
            };

            if (identData.AnalysisProtocolCollection.SpectrumIdentificationProtocols == null)
            {
                identData.AnalysisProtocolCollection.SpectrumIdentificationProtocols = new IdentDataList<SpectrumIdentificationProtocolObj>();
            }
            identData.AnalysisProtocolCollection.SpectrumIdentificationProtocols.Add(settings);

            return settings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spectraSource">source object for the spectrum, returned from <see cref="AddSpectraData"/></param>
        /// <param name="nativeId">native id of the spectrum</param>
        /// <param name="retentionTimeMinutes">retention time in minutes of the spectrum</param>
        /// <param name="expMz">experimental m/z of the peptide</param>
        /// <param name="calcMz">Calculated m/z of the peptide</param>
        /// <param name="charge">Charge of the peptide</param>
        /// <param name="rank">rank of result for a spectrum; may be the same as another result for the same spectrum if they score equally</param>
        /// <returns>Object: Must populate PeptideEvidences and Peptide, as well as add score information to CVParams and UserParams</returns>
        public SpectrumIdentificationItemObj AddSpectrumIdentification(SpectraDataObj spectraSource, string nativeId, double retentionTimeMinutes, double expMz, double calcMz, int charge, int rank = 1)
        {
            SpectrumIdentificationResultObj specResult;
            if (!identificationResults.TryGetValue(nativeId, out specResult))
            {
                specResult = new SpectrumIdentificationResultObj()
                {
                    SpectrumID = nativeId,
                    SpectraData = spectraSource,
                    SpectrumIdentificationItems = new IdentDataList<SpectrumIdentificationItemObj>(),
                    CVParams = new IdentDataList<CVParamObj>(),
                    UserParams = new IdentDataList<UserParamObj>(),
                };
                identificationResults.Add(nativeId, specResult);

                var number = nativeId.Split('=').Last();
                specResult.Id = "SIR_" + number;
                var rt = new CVParamObj(CV.CV.CVID.MS_scan_start_time, retentionTimeMinutes.ToString(CultureInfo.InvariantCulture))
                {
                    UnitCvid = CV.CV.CVID.UO_minute,
                };
                specResult.CVParams.Add(rt);
            }

            var specIdent = new SpectrumIdentificationItemObj()
            {
                Id = specResult.Id + "_" + (specResult.SpectrumIdentificationItems.Count + 1),
                PeptideEvidences = new IdentDataList<PeptideEvidenceRefObj>(),
                ChargeState = charge,
                ExperimentalMassToCharge = expMz,
                CalculatedMassToCharge = calcMz,
                Rank = rank,
                PassThreshold = true,
                CVParams = new IdentDataList<CVParamObj>(),
                UserParams = new IdentDataList<UserParamObj>(),
            };

            specResult.SpectrumIdentificationItems.Add(specIdent);

            return specIdent;
        }
    }
}
