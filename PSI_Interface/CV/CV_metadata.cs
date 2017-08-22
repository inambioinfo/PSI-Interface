// DO NOT EDIT THIS FILE!
// This file is autogenerated from the internet-sourced OBO files.
// Any edits made will be lost when it is recreated.

// ReSharper disable InconsistentNaming

namespace PSI_Interface.CV
{
    public static partial class CV
    {
        /// <summary>Populate the list of included Controlled Vocabularies, with descriptive information</summary>
        public static void PopulateCVInfoList()
        {
            CVInfoList.Add(new CVInfo("MS", "Proteomics Standards Initiative Mass Spectrometry Ontology", "https://raw.githubusercontent.com/HUPO-PSI/psi-ms-CV/master/psi-ms.obo", "4.0.14"));
            CVInfoList.Add(new CVInfo("UNIMOD", "UNIMOD", "http://www.unimod.org/obo/unimod.obo", "2017:03:10 16:18"));
            CVInfoList.Add(new CVInfo("PATO", "Quality Ontology", "http://ontologies.berkeleybop.org/pato.obo", "releases/2017-07-10"));
            CVInfoList.Add(new CVInfo("UO", "Unit Ontology", "http://ontologies.berkeleybop.org/uo.obo", "releases/2017-06-27"));
        }

        /// <summary>Enum listing all relationships between CV terms used in the included CVs</summary>
        public enum RelationsOtherTypes : int
        {
            /// <summary>Unknown term relationship</summary>
            Unknown,

            /// <summary>Description not provided</summary>
            has_regexp,

            /// <summary>Description not provided</summary>
            has_units,

            /// <summary>Description not provided</summary>
            has_order,

            /// <summary>Description not provided</summary>
            has_domain,

            /// <summary>Description not provided</summary>
            correlates_with,

            /// <summary>"q1 decreased_in_magnitude_relative_to q2 if and only if magnitude(q1) &lt; magnitude(q2). Here, magnitude(q) is a function that maps a quality to a unit-invariant scale."</summary>
            /// <remarks>This relation is used to determine the 'directionality' of relative qualities such as 'decreased strength', relative to the parent type, 'strength'.</remarks>
            decreased_in_magnitude_relative_to,

            /// <summary>"q1 different_in_magnitude_relative_to q2 if and only if magnitude(q1) NOT =~ magnitude(q2). Here, magnitude(q) is a function that maps a quality to a unit-invariant scale."</summary>
            different_in_magnitude_relative_to,

            /// <summary>"q1 directly_associated_with q2 iff q1 is dependent on q2, and the magnitude of q1 increases if the magnitude of q2 is increased, or the magnitude of q1 decreases if the magnitude of q2 is decreased. The relationship is not necessarily linear."</summary>
            /// <remarks>Example: 'Lewy bodies increased number related to dystrophic neurite increased number' (from annotation of PMID:8740227 in http://ccdb.ucsd.edu/1.0/NDPO.owl#ndpo_404). Here the increase in the number of lewy bodies is directly_associated_with the increase in the number of dystrophic neurites.\nAssociation is weaker than correlation or proportionality. These relations may be later added to PATO.</remarks>
            directly_associated_with,

            /// <summary>"s3 has_cross_section s3 if and only if : there exists some 2d plane that intersects the bearer of s3, and the impression of s3 upon that plane has shape quality s2."</summary>
            /// <remarks>Example: a spherical object has the quality of being spherical, and the spherical quality has_cross_section round.</remarks>
            has_cross_section,

            /// <summary>Description not provided</summary>
            has_dividend_entity,

            /// <summary>Description not provided</summary>
            has_dividend_quality,

            /// <summary>Description not provided</summary>
            has_divisor_entity,

            /// <summary>Description not provided</summary>
            has_divisor_quality,

            /// <summary>"Q1 has_part Q2 if and only if: every instance of Q1 is a quality_of an entity that has_quality some Q2."</summary>
            /// <remarks>We use the has_part relation to relate complex qualities to more primitive ones. A complex quality is a collection of qualities. The complex quality cannot exist without the sub-qualities. For example, the quality 'swollen' necessarily comes with the qualities of 'protruding' and 'increased size'.</remarks>
            has_part,

            /// <summary>Description not provided</summary>
            has_ratio_quality,

            /// <summary>Description not provided</summary>
            has_relative_magnitude,

            /// <summary>"q1 increased_in_magnitude_relative_to q2 if and only if magnitude(q1) &gt; magnitude(q2). Here, magnitude(q) is a function that maps a quality to a unit-invariant scale."</summary>
            /// <remarks>This relation is used to determine the 'directionality' of relative qualities such as 'increased strength', relative to the parent type, 'strength'.</remarks>
            increased_in_magnitude_relative_to,

            /// <summary>"q1 inversely_associated_with q2 iff q1 is dependent on q2, and the magnitude of q1 decreases if the magnitude of q2 is increased, or the magnitude of q1 increases if the magnitude of q2 is decreased. The relationship is not necessarily linear."</summary>
            /// <remarks>Association is weaker than correlation or proportionality. These relations may be later added to PATO.</remarks>
            inversely_associated_with,

            /// <summary>Description not provided</summary>
            is_magnitude_of,

            /// <summary>Description not provided</summary>
            is_measurement_of,

            /// <summary>Description not provided</summary>
            is_opposite_of,

            /// <summary>Description not provided</summary>
            is_unit_of,

            /// <summary>Description not provided</summary>
            realized_by,

            /// <summary>"q1 reciprocal_of q2 if and only if : q1 and q2 are relational qualities and a phenotype e q1 e2 mutually implies a phenotype e2 q2 e."</summary>
            /// <remarks>There are frequently two ways to state the same thing: we can say 'spermatocyte lacks asters' or 'asters absent from spermatocyte'. In this case the quality is 'lacking all parts of type' - it is a (relational) quality of the spermatocyte, and it is with respect to instances of 'aster'. One of the popular requirements of PATO is that it continue to support 'absent', so we need to relate statements which use this quality to the 'lacking all parts of type' quality.</remarks>
            reciprocal_of,

            /// <summary>"q1 similar_in_magnitude_relative_to q2 if and only if magnitude(q1) =~ magnitude(q2). Here, magnitude(q) is a function that maps a quality to a unit-invariant scale."</summary>
            similar_in_magnitude_relative_to,

            /// <summary>Description not provided</summary>
            /// <remarks>PATO divides qualities between normal (monadic, singly-occurring) qualities and relational qualities. Relational qualities stand in the 'towards' relation with respect to some additional entity. For example, The sensitivity of an eye towards red light. In some cases we want to represent a quality such as 'protruding' in both monadic and relational branches. We use this relation to link them.</remarks>
            singly_occurring_form_of,

            /// <summary>Description not provided</summary>
            /// <remarks>Relation binding a relational quality or disposition to the relevant type of entity.</remarks>
            towards,

        }
    }
}
