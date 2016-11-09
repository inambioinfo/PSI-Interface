﻿
namespace PSI_Interface.MSData.mzML
{
    /// <summary>
    /// Supported schema formats for MzML
    /// </summary>
    public enum MzMLSchemaType : byte
    {
        /// <summary>
        /// MzML schema
        /// </summary>
        MzML,
        /// <summary>
        /// Indexed MzML wrapper schema
        /// </summary>
        IndexedMzML,
    }
}
