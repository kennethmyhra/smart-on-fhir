using Hl7.Fhir.Model;

namespace EHRApp
{
    internal interface IPatientData
    {
        Patient Patient { get; }
    }
}