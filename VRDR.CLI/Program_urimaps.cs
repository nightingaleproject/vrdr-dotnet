using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;
using System.Net.Http;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VRDR;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace VRDR.CLI
{
    partial class Program
    {
    
    // UrisSTU3toSTU2: URIs that change between VRDR STU2.2 and STU3 and can be simply string substituted
    private static readonly Dictionary<string, string> UrisSTU3toSTU2 = new Dictionary<string, string>
        {
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/BypassEditFlag", "http://hl7.org/fhir/us/vrdr/StructureDefinition/BypassEditFlag" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/Extension-jurisdiction-id-vr", "http://hl7.org/fhir/us/vrdr/StructureDefinition/Location-Jurisdiction-Id" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/AuxiliaryStateIdentifier1", "http://hl7.org/fhir/us/vrdr/StructureDefinition/AuxiliaryStateIdentifier1" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/AuxiliaryStateIdentifier2", "http://hl7.org/fhir/us/vrdr/StructureDefinition/AuxiliaryStateIdentifier2" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/CertificateNumber", "http://hl7.org/fhir/us/vrdr/StructureDefinition/CertificateNumber" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/input-race-and-ethnicity-vr", "http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-input-race-and-ethnicity" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/coded-race-and-ethnicity-vr", "http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-coded-race-and-ethnicity" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/Observation-emerging-issues-vr", "http://hl7.org/fhir/us/vrdr/StructureDefinition/vrdr-emerging-issues" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/CityCode", "http://hl7.org/fhir/us/vrdr/StructureDefinition/CityCode" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/DistrictCode", "http://hl7.org/fhir/us/vrdr/StructureDefinition/DistrictCode" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/StreetDesignator", "http://hl7.org/fhir/us/vrdr/StructureDefinition/StreetDesignator" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/StreetName", "http://hl7.org/fhir/us/vrdr/StructureDefinition/StreetName" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/StreetNumber", "http://hl7.org/fhir/us/vrdr/StructureDefinition/StreetNumber" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/PreDirectional", "http://hl7.org/fhir/us/vrdr/StructureDefinition/PreDirectional" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/PostDirectional", "http://hl7.org/fhir/us/vrdr/StructureDefinition/PostDirectional" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/UnitOrAptNumber", "http://hl7.org/fhir/us/vrdr/StructureDefinition/UnitOrAptNumber" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/Extension-partial-date-time-vr", "http://hl7.org/fhir/us/vrdr/StructureDefinition/PartialDateTime" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/Extension-partial-date-vr", "http://hl7.org/fhir/us/vrdr/StructureDefinition/PartialDate" },
            { "http://hl7.org/fhir/us/vr-common-library/StructureDefinition/Extension-within-city-limits-indicator-vr", "http://hl7.org/fhir/us/vrdr/StructureDefinition/WithinCityLimitsIndicator"},
            { "http://hl7.org/fhir/us/vr-common-library/CodeSystem/CodeSystem-vr-edit-flags", "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-bypass-edit-flag-cs" },
            { "http://hl7.org/fhir/us/vr-common-library/CodeSystem/CodeSystem-local-observation-codes-vr", "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-observations-cs" },
            { "http://hl7.org/fhir/us/vr-common-library/CodeSystem/codesystem-vr-component", "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-component-cs" },
            { "http://hl7.org/fhir/us/vr-common-library/CodeSystem/CodeSystem-race-code-vr", "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-race-code-cs" },
            { "http://hl7.org/fhir/us/vr-common-library/CodeSystem/CodeSystem-race-recode-40-vr", "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-race-recode-40-cs" },
            { "http://hl7.org/fhir/us/vr-common-library/CodeSystem/CodeSystem-hispanic-origin-vr", "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-hispanic-origin-cs" },
            { "http://hl7.org/fhir/us/vr-common-library/CodeSystem/CodeSystem-country-code-vr", "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-country-code-cs" },
            { "http://hl7.org/fhir/us/vr-common-library/CodeSystem/CodeSystem-jurisdictions-vr", "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-jurisdictions-cs" },
            { "http://hl7.org/fhir/us/vrdr/CodeSystem/CodeSystem-death-pregnancy-status", "http://hl7.org/fhir/us/vrdr/CodeSystem/vrdr-pregnancy-status-cs" }
        };
    // dateTimeComponentsSTU3toSTU2: URIs that change between VRDR STU2.2 and STU3 and can't be simply string substituted
        private static readonly Dictionary<string, string> dateTimeComponentsSTU3toSTU2 = new Dictionary<string, string>
        {
            { "day", "http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Day" },
            { "month", "http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Month" },
            { "year", "http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Year" },
            { "time", "http://hl7.org/fhir/us/vrdr/StructureDefinition/Date-Time" }
        };
 
    };
}
