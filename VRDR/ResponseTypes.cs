namespace VRDR
{
    /// <summary> enum for describing response types </summary>
    public enum ResponseTypes
    {
        /// <summary> Response type is ERROR </summary>
        Error,
        /// <summary> Response type is IJE </summary>
        Ije,
        /// <summary> Response type is FHIR XML </summary>
        FhirXml,
        /// <summary> Response type is FHIR JSON </summary>
        FhirJson,
        /// <summary> Response type is NIGHTINGALE </summary>
        Nightingale
    }
}