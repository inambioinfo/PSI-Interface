﻿using System.Collections.Generic;
using PSI_Interface.MSData;

namespace PSI_Interface.CV
{
    public class CVTranslator
    {
        private readonly Dictionary<string, string> _fileToObo = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _oboToFile = new Dictionary<string, string>();

        public CVTranslator()
        {
            foreach (var cv in CV.CVInfoList)
            {
                _oboToFile.Add(cv.Id, cv.Id);
                _fileToObo.Add(cv.Id, cv.Id);
            }
        }

        public CVTranslator(IEnumerable<CV.CVInfo> fileCvInfo)
        {
            foreach (var cv in CV.CVInfoList)
            {
                foreach (var fcv in fileCvInfo)
                {
                    var cvFilename = cv.URI.Substring(cv.URI.LastIndexOf("/") + 1);
                    var fcvFilename = fcv.URI.Substring(fcv.URI.LastIndexOf("/") + 1);
                    if (cvFilename.ToLower().Equals(fcvFilename.ToLower()))
                    {
                        _oboToFile.Add(cv.Id, fcv.Id);
                    }
                }
                if (!_oboToFile.ContainsKey(cv.Id))
                {
                    _oboToFile.Add(cv.Id, cv.Id);
                }
            }

            foreach (var mapping in _oboToFile)
            {
                _fileToObo.Add(mapping.Value, mapping.Key);
            }
        }

        public CVTranslator(IEnumerable<PSI_Interface.IdentData.CVInfo> fileCvInfo)
        {
            foreach (var cv in CV.CVInfoList)
            {
                foreach (var fcv in fileCvInfo)
                {
                    var cvFilename = cv.URI.Substring(cv.URI.LastIndexOf("/") + 1);
                    var fcvFilename = fcv.URI.Substring(fcv.URI.LastIndexOf("/") + 1);
                    if (cvFilename.ToLower().Equals(fcvFilename.ToLower()))
                    {
                        _oboToFile.Add(cv.Id, fcv.Id);
                    }
                }
                if (!_oboToFile.ContainsKey(cv.Id))
                {
                    _oboToFile.Add(cv.Id, cv.Id);
                }
            }

            foreach (var mapping in _oboToFile)
            {
                _fileToObo.Add(mapping.Value, mapping.Key);
            }
        }

        public CVTranslator(IEnumerable<CVInfo> fileCvInfo)
        {
            foreach (var cv in CV.CVInfoList)
            {
                foreach (var fcv in fileCvInfo)
                {
                    var cvFilename = cv.URI.Substring(cv.URI.LastIndexOf("/") + 1);
                    var fcvFilename = fcv.URI.Substring(fcv.URI.LastIndexOf("/") + 1);
                    if (cvFilename.ToLower().Equals(fcvFilename.ToLower()))
                    {
                        _oboToFile.Add(cv.Id, fcv.Id);
                    }
                }
                if (!_oboToFile.ContainsKey(cv.Id))
                {
                    _oboToFile.Add(cv.Id, cv.Id);
                }
            }

            foreach (var mapping in _oboToFile)
            {
                _fileToObo.Add(mapping.Value, mapping.Key);
            }
        }

        public string ConvertFileCVRef(string cvRef)
        {
            return ConvertCVRef(cvRef, _fileToObo);
        }

        public string ConvertOboCVRef(string cvRef)
        {
            return ConvertCVRef(cvRef, _oboToFile);
        }

        private string ConvertCVRef(string cvRef, Dictionary<string, string> map)
        {
            if (map.ContainsKey(cvRef))
            {
                return map[cvRef];
            }
            return null;
        }
}
}