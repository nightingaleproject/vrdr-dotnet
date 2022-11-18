using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using Newtonsoft.Json;

// DeathRecord_responseOnlyProperties.cs
//    These fields are used ONLY in coded messages sent from NCHS to EDRS corresponding to TRX and MRE content.

namespace VRDR
{
    /// <summary>Class <c>DeathRecord</c> models a FHIR Vital Records Death Reporting (VRDR) Death
    /// Record. This class was designed to help consume and produce death records that follow the
    /// HL7 FHIR Vital Records Death Reporting Implementation Guide, as described at:
    /// http://hl7.org/fhir/us/vrdr and https://github.com/hl7/vrdr.
    /// </summary>
    public partial class DeathRecord
    {

        /// <summary>Activity at Time of Death.</summary>
        /// <value>the decedent's activity at time of death. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; activity = new Dictionary&lt;string, string&gt;();</para>
        /// <para>elevel.Add("code", "0");</para>
        /// <para>elevel.Add("system", CodeSystems.ActivityAtTimeOfDeath);</para>
        /// <para>elevel.Add("display", "While engaged in sports activity");</para>
        /// <para>ExampleDeathRecord.ActivityAtDeath = activity;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Education Level: {ExampleDeathRecord.EducationLevel['display']}");</para>
        /// </example>
        [Property("Activity at Time of Death", Property.Types.Dictionary, "Coded Content", "Decedent's Activity at Time of Death.", true, IGURL.ActivityAtTimeOfDeath, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80626-5')", "")]
        public Dictionary<string, string> ActivityAtDeath
        {
            get
            {
                if (ActivityAtTimeOfDeathObs != null && ActivityAtTimeOfDeathObs.Value != null && ActivityAtTimeOfDeathObs.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)ActivityAtTimeOfDeathObs.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (ActivityAtTimeOfDeathObs == null)
                {
                    CreateActivityAtTimeOfDeathObs();
                }
                ActivityAtTimeOfDeathObs.Value = DictToCodeableConcept(value);
            }
        }

        /// <summary>Decedent's Activity At Time of Death Helper</summary>
        /// <value>Decedent's Activity at Time of Death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ActivityAtDeath = 0;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Activity at Time of Death: {ExampleDeathRecord.ActivityAtDeath}");</para>
        /// </example>
        [Property("Activity at Time of Death Helper", Property.Types.String, "Coded Content", "Decedent's Activity at Time of Death.", false, IGURL.ActivityAtTimeOfDeath, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80626-5')", "")]
        public string ActivityAtDeathHelper
        {
            get
            {
                if (ActivityAtDeath.ContainsKey("code"))
                {
                    return ActivityAtDeath["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("ActivityAtDeath", value, VRDR.ValueSets.ActivityAtTimeOfDeath.Codes);
            }
        }

        /// <summary>Decedent's Automated Underlying Cause of Death</summary>
        /// <value>Decedent's Automated Underlying Cause of Death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AutomatedUnderlyingCOD = "I13.1";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Automated Underlying Cause of Death: {ExampleDeathRecord.AutomatedUnderlyingCOD}");</para>
        /// </example>
        [Property("Automated Underlying Cause of Death", Property.Types.String, "Coded Content", "Automated Underlying Cause of Death.", true, IGURL.AutomatedUnderlyingCauseOfDeath, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80358-5')", "")]
        public string AutomatedUnderlyingCOD
        {
            get
            {
                if (AutomatedUnderlyingCauseOfDeathObs != null && AutomatedUnderlyingCauseOfDeathObs.Value != null && AutomatedUnderlyingCauseOfDeathObs.Value as CodeableConcept != null)
                {

                    return CodeableConceptToDict((CodeableConcept)AutomatedUnderlyingCauseOfDeathObs.Value)["code"];
                }
                return "";
            }
            set
            {
                if (AutomatedUnderlyingCauseOfDeathObs == null)
                {
                    CreateAutomatedUnderlyingCauseOfDeathObs();
                }
                AutomatedUnderlyingCauseOfDeathObs.Value = new CodeableConcept(CodeSystems.ICD10, value, null, null);
            }
        }

        /// <summary>Decedent's Manual Underlying Cause of Death</summary>
        /// <value>Decedent's Manual Underlying Cause of Death.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ManUnderlyingCOD = "I13.1";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Decedent's Manual Underlying Cause of Death: {ExampleDeathRecord.ManUnderlyingCOD}");</para>
        /// </example>
        [Property("Manual Underlying Cause of Death", Property.Types.String, "Coded Content", "Manual Underlying Cause of Death.", true, IGURL.ManualUnderlyingCauseOfDeath, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='80358-580359-3')", "")]
        public string ManUnderlyingCOD
        {
            get
            {
                if (ManualUnderlyingCauseOfDeathObs != null && ManualUnderlyingCauseOfDeathObs.Value != null && ManualUnderlyingCauseOfDeathObs.Value as CodeableConcept != null)
                {

                    return CodeableConceptToDict((CodeableConcept)ManualUnderlyingCauseOfDeathObs.Value)["code"];
                }
                return "";
            }
            set
            {
                if (ManualUnderlyingCauseOfDeathObs == null)
                {
                    CreateManualUnderlyingCauseOfDeathObs();
                }
                ManualUnderlyingCauseOfDeathObs.Value = new CodeableConcept(CodeSystems.ICD10, value, null, null);
            }
        }

        /// <summary>Place of Injury.</summary>
        /// <value>Place of Injury. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; elevel = new Dictionary&lt;string, string&gt;();</para>
        /// <para>elevel.Add("code", "LA14084-0");</para>
        /// <para>elevel.Add("system", CodeSystems.LOINC);</para>
        /// <para>elevel.Add("display", "Home");</para>
        /// <para>ExampleDeathRecord.PlaceOfInjury = elevel;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"PlaceOfInjury: {ExampleDeathRecord.PlaceOfInjury['display']}");</para>
        /// </example>
        [Property("Place of Injury", Property.Types.Dictionary, "Coded Content", "Place of Injury.", true, IGURL.PlaceOfInjury, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11376-1')", "")]
        public Dictionary<string, string> PlaceOfInjury
        {
            get
            {
                if (PlaceOfInjuryObs != null && PlaceOfInjuryObs.Value != null && PlaceOfInjuryObs.Value as CodeableConcept != null)
                {
                    return CodeableConceptToDict((CodeableConcept)PlaceOfInjuryObs.Value);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (PlaceOfInjuryObs == null)
                {
                    CreatePlaceOfInjuryObs();
                }
                PlaceOfInjuryObs.Value = DictToCodeableConcept(value);
            }
        }

        /// <summary>Decedent's Place of Injury Helper</summary>
        /// <value>Place of Injury.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.PlaceOfInjuryHelper = ValueSets.PlaceOfInjury.Home;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Place of Injury: {ExampleDeathRecord.PlaceOfInjuryHelper}");</para>
        /// </example>
        [Property("Place of Injury Helper", Property.Types.String, "Coded Content", "Place of Injury.", false, IGURL.PlaceOfInjury, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='11376-1')", "")]
        public string PlaceOfInjuryHelper
        {
            get
            {
                if (PlaceOfInjury.ContainsKey("code"))
                {
                    return PlaceOfInjury["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("PlaceOfInjury", value, VRDR.ValueSets.PlaceOfInjury.Codes);
            }
        }

        /// <summary>First Edited Race Code.</summary>
        /// <value>First Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FirstEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Edited Race Code: {ExampleDeathRecord.FirstEditedRaceCode['display']}");</para>
        /// </example>
        [Property("FirstEditedRaceCode", Property.Types.Dictionary, "Coded Content", "First Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FirstEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FirstEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FirstEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FirstEditedCode", "First Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>First Edited Race Code  Helper</summary>
        /// <value>First Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FirstEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Edited Race Code: {ExampleDeathRecord.FirstEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("First Edited Race Code Helper", Property.Types.String, "Coded Content", "First Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FirstEditedRaceCodeHelper
        {
            get
            {
                if (FirstEditedRaceCode.ContainsKey("code"))
                {
                    return FirstEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FirstEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Second Edited Race Code.</summary>
        /// <value>Second Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SecondEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Edited Race Code: {ExampleDeathRecord.SecondEditedRaceCode['display']}");</para>
        /// </example>
        [Property("SecondEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Second Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SecondEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SecondEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SecondEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SecondEditedCode", "Second Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Second Edited Race Code  Helper</summary>
        /// <value>Second Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SecondEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Edited Race Code: {ExampleDeathRecord.SecondEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Second Edited Race Code Helper", Property.Types.String, "Coded Content", "Second Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SecondEditedRaceCodeHelper
        {
            get
            {
                if (SecondEditedRaceCode.ContainsKey("code"))
                {
                    return SecondEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SecondEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Third Edited Race Code.</summary>
        /// <value>Third Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.ThirdEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Third Edited Race Code: {ExampleDeathRecord.ThirdEditedRaceCode['display']}");</para>
        /// </example>
        [Property("ThirdEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Third Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> ThirdEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "ThirdEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "ThirdEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "ThirdEditedCode", "Third Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Third Edited Race Code  Helper</summary>
        /// <value>Third Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ThirdEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Third Edited Race Code: {ExampleDeathRecord.ThirdEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Third Edited Race Code Helper", Property.Types.String, "Coded Content", "Third Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string ThirdEditedRaceCodeHelper
        {
            get
            {
                if (ThirdEditedRaceCode.ContainsKey("code"))
                {
                    return ThirdEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("ThirdEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Fourth Edited Race Code.</summary>
        /// <value>Fourth Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FourthEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Fourth Edited Race Code: {ExampleDeathRecord.FourthEditedRaceCode['display']}");</para>
        /// </example>
        [Property("FourthEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Fourth Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FourthEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FourthEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FourthEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FourthEditedCode", "Fourth Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Fourth Edited Race Code  Helper</summary>
        /// <value>Fourth Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FourthEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Fourth Edited Race Code: {ExampleDeathRecord.FourthEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Fourth Edited Race Code Helper", Property.Types.String, "Coded Content", "Fourth Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FourthEditedRaceCodeHelper
        {
            get
            {
                if (FourthEditedRaceCode.ContainsKey("code"))
                {
                    return FourthEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FourthEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Fifth Edited Race Code.</summary>
        /// <value>Fifth Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FifthEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Fifth Edited Race Code: {ExampleDeathRecord.FifthEditedRaceCode['display']}");</para>
        /// </example>
        [Property("FifthEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Fifth Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FifthEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FifthEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FifthEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FifthEditedCode", "Fifth Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Fifth Edited Race Code  Helper</summary>
        /// <value>Fifth Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FifthEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Fifth Edited Race Code: {ExampleDeathRecord.FifthEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Fifth Edited Race Code Helper", Property.Types.String, "Coded Content", "Fifth Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FifthEditedRaceCodeHelper
        {
            get
            {
                if (FifthEditedRaceCode.ContainsKey("code"))
                {
                    return FifthEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FifthEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Sixth Edited Race Code.</summary>
        /// <value>Sixth Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SixthEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Sixth Edited Race Code: {ExampleDeathRecord.SixthEditedRaceCode['display']}");</para>
        /// </example>
        [Property("SixthEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Sixth Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SixthEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SixthEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SixthEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SixthEditedCode", "Sixth Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Sixth Edited Race Code  Helper</summary>
        /// <value>Sixth Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SixthEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Sixth Edited Race Code: {ExampleDeathRecord.SixthEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Sixth Edited Race Code Helper", Property.Types.String, "Coded Content", "Sixth Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SixthEditedRaceCodeHelper
        {
            get
            {
                if (SixthEditedRaceCode.ContainsKey("code"))
                {
                    return SixthEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SixthEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Seventh Edited Race Code.</summary>
        /// <value>Seventh Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SeventhEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Seventh Edited Race Code: {ExampleDeathRecord.SeventhEditedRaceCode['display']}");</para>
        /// </example>
        [Property("SeventhEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Seventh Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SeventhEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SeventhEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SeventhEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SeventhEditedCode", "Seventh Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Seventh Edited Race Code  Helper</summary>
        /// <value>Seventh Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SeventhEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Seventh Edited Race Code: {ExampleDeathRecord.SeventhEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Seventh Edited Race Code Helper", Property.Types.String, "Coded Content", "Seventh Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SeventhEditedRaceCodeHelper
        {
            get
            {
                if (SeventhEditedRaceCode.ContainsKey("code"))
                {
                    return SeventhEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SeventhEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Eighth Edited Race Code.</summary>
        /// <value>Eighth Edited Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.EighthEditedRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Eighth Edited Race Code: {ExampleDeathRecord.EighthEditedRaceCode['display']}");</para>
        /// </example>
        [Property("EighthEditedRaceCode", Property.Types.Dictionary, "Coded Content", "Eighth Edited Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> EighthEditedRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "EighthEditedCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "EighthEditedCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "EighthEditedCode", "Eighth Edited Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Eighth Edited Race Code  Helper</summary>
        /// <value>Eighth Edited Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.EighthEditedRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Eighth Edited Race Code: {ExampleDeathRecord.EighthEditedRaceCodeHelper}");</para>
        /// </example>
        [Property("Eighth Edited Race Code Helper", Property.Types.String, "Coded Content", "Eighth Edited Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string EighthEditedRaceCodeHelper
        {
            get
            {
                if (EighthEditedRaceCode.ContainsKey("code"))
                {
                    return EighthEditedRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("EighthEditedRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>First American Indian Race Code.</summary>
        /// <value>First American Indian Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FirstAmericanIndianRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First American Indian Race Code: {ExampleDeathRecord.FirstAmericanIndianRaceCode['display']}");</para>
        /// </example>
        [Property("FirstAmericanIndianRaceCode", Property.Types.Dictionary, "Coded Content", "First American Indian Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FirstAmericanIndianRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FirstAmericanIndianRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FirstAmericanIndianRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FirstAmericanIndianRace", "First American Indian Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>First American Indian Race Code  Helper</summary>
        /// <value>First American Indian Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FirstAmericanIndianRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First American Indian Race Code: {ExampleDeathRecord.FirstAmericanIndianRaceCodeHelper}");</para>
        /// </example>
        [Property("First American Indian Race Code Helper", Property.Types.String, "Coded Content", "First American Indian Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FirstAmericanIndianRaceCodeHelper
        {
            get
            {
                if (FirstAmericanIndianRaceCode.ContainsKey("code"))
                {
                    return FirstAmericanIndianRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FirstAmericanIndianRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Second American Indian Race Code.</summary>
        /// <value>Second American Indian Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SecondAmericanIndianRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second American Indian Race Code: {ExampleDeathRecord.SecondAmericanIndianRaceCode['display']}");</para>
        /// </example>
        [Property("SecondAmericanIndianRaceCode", Property.Types.Dictionary, "Coded Content", "Second American Indian Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SecondAmericanIndianRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SecondAmericanIndianRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SecondAmericanIndianRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SecondAmericanIndianRace", "Second American Indian Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Second American Indian Race Code  Helper</summary>
        /// <value>Second American Indian Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SecondAmericanIndianRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second American Indian Race Code: {ExampleDeathRecord.SecondAmericanIndianRaceCodeHelper}");</para>
        /// </example>
        [Property("Second American Indian Race Code Helper", Property.Types.String, "Coded Content", "Second American Indian Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SecondAmericanIndianRaceCodeHelper
        {
            get
            {
                if (SecondAmericanIndianRaceCode.ContainsKey("code"))
                {
                    return SecondAmericanIndianRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SecondAmericanIndianRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>First Other Asian Race Code.</summary>
        /// <value>First Other Asian Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FirstOtherAsianRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Other Asian Race Code: {ExampleDeathRecord.FirstOtherAsianRaceCode['display']}");</para>
        /// </example>
        [Property("FirstOtherAsianRaceCode", Property.Types.Dictionary, "Coded Content", "First Other Asian Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FirstOtherAsianRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FirstOtherAsianRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FirstOtherAsianRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FirstOtherAsianRace", "First Other Asian Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>First Other Asian Race Code  Helper</summary>
        /// <value>First Other Asian Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FirstOtherAsianRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Other Asian Race Code: {ExampleDeathRecord.FirstOtherAsianRaceCodeHelper}");</para>
        /// </example>
        [Property("First Other Asian Race Code Helper", Property.Types.String, "Coded Content", "First Other Asian Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FirstOtherAsianRaceCodeHelper
        {
            get
            {
                if (FirstOtherAsianRaceCode.ContainsKey("code"))
                {
                    return FirstOtherAsianRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FirstOtherAsianRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Second Other Asian Race Code.</summary>
        /// <value>Second Other Asian Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SecondOtherAsianRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Other Asian Race Code: {ExampleDeathRecord.SecondOtherAsianRaceCode['display']}");</para>
        /// </example>
        [Property("SecondOtherAsianRaceCode", Property.Types.Dictionary, "Coded Content", "Second Other Asian Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SecondOtherAsianRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SecondOtherAsianRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SecondOtherAsianRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SecondOtherAsianRace", "Second Other Asian Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Second Other Asian Race Code  Helper</summary>
        /// <value>Second Other Asian Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SecondOtherAsianRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Other Asian Race Code: {ExampleDeathRecord.SecondOtherAsianRaceCodeHelper}");</para>
        /// </example>
        [Property("Second Other Asian Race Code Helper", Property.Types.String, "Coded Content", "Second Other Asian Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SecondOtherAsianRaceCodeHelper
        {
            get
            {
                if (SecondOtherAsianRaceCode.ContainsKey("code"))
                {
                    return SecondOtherAsianRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SecondOtherAsianRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>First Other Pacific Islander Race Code.</summary>
        /// <value>First Other Pacific Islander Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FirstOtherPacificIslanderRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Other Pacific Islander Race Code: {ExampleDeathRecord.FirstOtherPacificIslanderRaceCode['display']}");</para>
        /// </example>
        [Property("FirstOtherPacificIslanderRaceCode", Property.Types.Dictionary, "Coded Content", "First Other Pacific Islander Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FirstOtherPacificIslanderRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FirstOtherPacificIslanderRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FirstOtherPacificIslanderRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FirstOtherPacificIslanderRace", "First Other Pacific Islander Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>First Other Pacific Islander Race Code  Helper</summary>
        /// <value>First Other Pacific Islander Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FirstOtherPacificIslanderRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Other Pacific Islander Race Code: {ExampleDeathRecord.FirstOtherPacificIslanderRaceCodeHelper}");</para>
        /// </example>
        [Property("First Other Pacific Islander Race Code Helper", Property.Types.String, "Coded Content", "First Other Pacific Islander Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FirstOtherPacificIslanderRaceCodeHelper
        {
            get
            {
                if (FirstOtherPacificIslanderRaceCode.ContainsKey("code"))
                {
                    return FirstOtherPacificIslanderRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FirstOtherPacificIslanderRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Second Other Pacific Islander Race Code.</summary>
        /// <value>Second Other Pacific Islander Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SecondOtherPacificIslanderRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Other Pacific Islander Race Code: {ExampleDeathRecord.SecondOtherPacificIslanderRaceCode['display']}");</para>
        /// </example>
        [Property("SecondOtherPacificIslanderRaceCode", Property.Types.Dictionary, "Coded Content", "Second Other Pacific Islander Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SecondOtherPacificIslanderRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SecondOtherPacificIslanderRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SecondOtherPacificIslanderRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SecondOtherPacificIslanderRace", "Second Other Pacific Islander Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Second Other Pacific Islander Race Code  Helper</summary>
        /// <value>Second Other Pacific Islander Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SecondOtherPacificIslanderRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Other Pacific Islander Race Code: {ExampleDeathRecord.SecondOtherPacificIslanderRaceCodeHelper}");</para>
        /// </example>
        [Property("Second Other Pacific Islander Race Code Helper", Property.Types.String, "Coded Content", "Second Other Pacific Islander Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SecondOtherPacificIslanderRaceCodeHelper
        {
            get
            {
                if (SecondOtherPacificIslanderRaceCode.ContainsKey("code"))
                {
                    return SecondOtherPacificIslanderRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SecondOtherPacificIslanderRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>First Other Race Code.</summary>
        /// <value>First Other Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.FirstOtherRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Other Race Code: {ExampleDeathRecord.FirstOtherRaceCode['display']}");</para>
        /// </example>
        [Property("FirstOtherRaceCode", Property.Types.Dictionary, "Coded Content", "First Other Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> FirstOtherRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "FirstOtherRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "FirstOtherRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "FirstOtherRace", "First Other Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>First Other Race Code  Helper</summary>
        /// <value>First Other Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.FirstOtherRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Other Race Code: {ExampleDeathRecord.FirstOtherRaceCodeHelper}");</para>
        /// </example>
        [Property("First Other Race Code Helper", Property.Types.String, "Coded Content", "First Other Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string FirstOtherRaceCodeHelper
        {
            get
            {
                if (FirstOtherRaceCode.ContainsKey("code"))
                {
                    return FirstOtherRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("FirstOtherRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Second Other Race Code.</summary>
        /// <value>Second Other Race Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.SecondOtherRaceCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Other Race Code: {ExampleDeathRecord.SecondOtherRaceCode['display']}");</para>
        /// </example>
        [Property("SecondOtherRaceCode", Property.Types.Dictionary, "Coded Content", "Second Other Race Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> SecondOtherRaceCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "SecondOtherRace");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "SecondOtherRace");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "SecondOtherRace", "Second Other Race", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Second Other Race Code  Helper</summary>
        /// <value>Second Other Race Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.SecondOtherRaceCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Second Other Race Code: {ExampleDeathRecord.SecondOtherRaceCodeHelper}");</para>
        /// </example>
        [Property("Second Other Race Code Helper", Property.Types.String, "Coded Content", "Second Other Race Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string SecondOtherRaceCodeHelper
        {
            get
            {
                if (SecondOtherRaceCode.ContainsKey("code"))
                {
                    return SecondOtherRaceCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("SecondOtherRaceCode", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

        /// <summary>Hispanic Code.</summary>
        /// <value>Hispanic Code. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.HispanicCode = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Hispanic Code: {ExampleDeathRecord.HispanicCode['display']}");</para>
        /// </example>
        [Property("HispanicCode", Property.Types.Dictionary, "Coded Content", "Hispanic Code.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> HispanicCode
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "HispanicCode");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "HispanicCode");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "HispanicCode", "Hispanic Code", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Hispanic Code  Helper</summary>
        /// <value>Hispanic Code Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.HispanicCodeHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Hispanic Code: {ExampleDeathRecord.HispanicCodeHelper}");</para>
        /// </example>
        [Property("Hispanic Code Helper", Property.Types.String, "Coded Content", "Hispanic Code.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string HispanicCodeHelper
        {
            get
            {
                if (HispanicCode.ContainsKey("code"))
                {
                    return HispanicCode["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("HispanicCode", value, VRDR.ValueSets.HispanicOrigin.Codes);
            }
        }

        /// <summary>Hispanic Code For Literal.</summary>
        /// <value>Hispanic Code For Literal. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "300");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add("display", "African");</para>
        /// <para>ExampleDeathRecord.HispanicCodeForLiteral = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Hispanic Code For Literal: {ExampleDeathRecord.HispanicCodeForLiteral['display']}");</para>
        /// </example>
        [Property("HispanicCodeForLiteral", Property.Types.Dictionary, "Coded Content", "Hispanic Code For Literal.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> HispanicCodeForLiteral
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "HispanicCodeForLiteral");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "HispanicCodeForLiteral");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "HispanicCodeForLiteral", "Hispanic Code For Literal", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Hispanic Code For Literal  Helper</summary>
        /// <value>Hispanic Code For Literal Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.HispanicCodeForLiteralHelper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Hispanic Code For Literal: {ExampleDeathRecord.HispanicCodeForLiteralHelper}");</para>
        /// </example>
        [Property("Hispanic Code For Literal Helper", Property.Types.String, "Coded Content", "Hispanic Code For Literal.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string HispanicCodeForLiteralHelper
        {
            get
            {
                if (HispanicCodeForLiteral.ContainsKey("code"))
                {
                    return HispanicCodeForLiteral["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("HispanicCodeForLiteral", value, VRDR.ValueSets.HispanicOrigin.Codes);
            }
        }

        /// <summary>Race Recode 40.</summary>
        /// <value>Race Recode 40. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>"code" - the code</para>
        /// <para>"system" - the code system this code belongs to</para>
        /// <para>"display" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add("code", "09");</para>
        /// <para>racecode.Add("system", CodeSystems.RaceRecode40CS);</para>
        /// <para>racecode.Add("display", "Vietnamiese");</para>
        /// <para>ExampleDeathRecord.RaceRecode40 = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"RaceRecode40: {ExampleDeathRecord.RaceRecode40['display']}");</para>
        /// </example>
        [Property("RaceRecode40", Property.Types.Dictionary, "Coded Content", "RaceRecode40.", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public Dictionary<string, string> RaceRecode40
        {
            get
            {
                if (CodedRaceAndEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceAndEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == "RaceRecode40");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceAndEthnicityObs == null)
                {
                    CreateCodedRaceAndEthnicityObs();
                }
                CodedRaceAndEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == "RaceRecode40");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, "RaceRecode40", null, null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceAndEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>Race Recode 40  Helper</summary>
        /// <value>Race Recode 40 Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.RaceRecode40Helper = VRDR.ValueSets.RaceRecode40.AIAN ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Race Recode 40: {ExampleDeathRecord.RaceRecode40Helper}");</para>
        /// </example>
        [Property("Race Recode 40 Helper", Property.Types.String, "Coded Content", "Race Recode 40.", false, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')", "")]
        public string RaceRecode40Helper
        {
            get
            {
                if (RaceRecode40.ContainsKey("code"))
                {
                    return RaceRecode40["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("RaceRecode40", value, VRDR.ValueSets.RaceRecode40.Codes);
            }
        }

        /// <summary>Entity Axis Cause Of Death
        /// <para>Note that record axis codes have an unusual and obscure handling of a Pregnancy flag, for more information see
        /// http://build.fhir.org/ig/HL7/vrdr/branches/master/StructureDefinition-vrdr-record-axis-cause-of-death.html#usage></para>
        /// </summary>
        /// <value>Entity-axis codes</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para> ExampleDeathRecord.EntityAxisCauseOfDeath = new [] {(LineNumber: 2, Position: 1, Code: "T27.3", ECode: true)};</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Entity Axis Code: {ExampleDeathRecord.EntityAxisCauseOfDeath.ElementAt(0).Code}");</para>
        /// </example>
        [Property("Entity Axis Cause of Death", Property.Types.Tuple4Arr, "Coded Content", "", true, IGURL.EntityAxisCauseOfDeath, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=80356-9)", "")]
        public IEnumerable<(int LineNumber, int Position, string Code, bool ECode)> EntityAxisCauseOfDeath
        {
            get
            {
                List<(int LineNumber, int Position, string Code, bool ECode)> eac = new List<(int LineNumber, int Position, string Code, bool ECode)>();
                if (EntityAxisCauseOfDeathObsList != null)
                {
                    foreach (Observation ob in EntityAxisCauseOfDeathObsList)
                    {
                        int? lineNumber = null;
                        int? position = null;
                        string icd10code = null;
                        bool ecode = false;
                        Observation.ComponentComponent positionComp = ob.Component.Where(c => c.Code.Coding[0].Code == "position").FirstOrDefault();
                        if (positionComp != null && positionComp.Value != null)
                        {
                            position = ((Integer)positionComp.Value).Value;
                        }
                        Observation.ComponentComponent lineNumComp = ob.Component.Where(c => c.Code.Coding[0].Code == "lineNumber").FirstOrDefault();
                        if (lineNumComp != null && lineNumComp.Value != null)
                        {
                            lineNumber = ((Integer)lineNumComp.Value).Value;
                        }
                        CodeableConcept valueCC = (CodeableConcept)ob.Value;
                        if (valueCC != null && valueCC.Coding != null && valueCC.Coding.Count() > 0)
                        {
                            icd10code = valueCC.Coding[0].Code.Trim();
                        }
                        Observation.ComponentComponent ecodeComp = ob.Component.Where(c => c.Code.Coding[0].Code == "eCodeIndicator").FirstOrDefault();
                        if (ecodeComp != null && ecodeComp.Value != null)
                        {
                            ecode = (bool)((FhirBoolean)ecodeComp.Value).Value;
                        }
                        if (lineNumber != null && position != null && icd10code != null)
                        {
                            eac.Add((LineNumber: (int)lineNumber, Position: (int)position, Code: icd10code, ECode: ecode));
                        }
                    }
                }
                return eac.OrderBy(element => element.LineNumber).ThenBy(element => element.Position);
            }
            set
            {
                // clear all existing eac
                Bundle.Entry.RemoveAll(entry => entry.Resource is Observation && (((Observation)entry.Resource).Code.Coding.First().Code == "80356-9"));
                if (EntityAxisCauseOfDeathObsList != null)
                {
                    EntityAxisCauseOfDeathObsList.Clear();
                }
                else
                {
                    EntityAxisCauseOfDeathObsList = new List<Observation>();
                }
                // Rebuild the list of observations
                foreach ((int LineNumber, int Position, string Code, bool ECode) eac in value)
                {
                    Observation ob = new Observation();
                    ob.Id = Guid.NewGuid().ToString();
                    ob.Meta = new Meta();
                    string[] entityAxis_profile = { ProfileURL.EntityAxisCauseOfDeath };
                    ob.Meta.Profile = entityAxis_profile;
                    ob.Status = ObservationStatus.Final;
                    ob.Code = new CodeableConcept(CodeSystems.LOINC, "80356-9", "Cause of death entity axis code [Automated]", null);
                    ob.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    AddReferenceToComposition(ob.Id, "CodedContent");

                    ob.Effective = new FhirDateTime();
                    ob.Value = new CodeableConcept(CodeSystems.ICD10, eac.Code, null, null);

                    Observation.ComponentComponent lineNumComp = new Observation.ComponentComponent();
                    lineNumComp.Value = new Integer(eac.LineNumber);
                    lineNumComp.Code = new CodeableConcept(CodeSystems.Component, "lineNumber", "lineNumber", null);
                    ob.Component.Add(lineNumComp);

                    Observation.ComponentComponent positionComp = new Observation.ComponentComponent();
                    positionComp.Value = new Integer(eac.Position);
                    positionComp.Code = new CodeableConcept(CodeSystems.Component, "position", "Position", null);
                    ob.Component.Add(positionComp);

                    Observation.ComponentComponent eCodeComp = new Observation.ComponentComponent();
                    eCodeComp.Value = new FhirBoolean(eac.ECode);
                    eCodeComp.Code = new CodeableConcept(CodeSystems.Component, "eCodeIndicator", "eCodeIndicator", null);
                    ob.Component.Add(eCodeComp);

                    Bundle.AddResourceEntry(ob, "urn:uuid:" + ob.Id);
                    EntityAxisCauseOfDeathObsList.Add(ob);
                }
            }
        }

        /// <summary>Record Axis Cause Of Death</summary>
        /// <value>record-axis codes</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Tuple&lt;string, string, string&gt;[] eac = new Tuple&lt;string, string, string&gt;{Tuple.Create("position", "code", "pregnancy")}</para>
        /// <para>ExampleDeathRecord.RecordAxisCauseOfDeath = new [] { (Position: 1, Code: "T27.3", Pregnancy: true) };</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"First Record Axis Code: {ExampleDeathRecord.RecordAxisCauseOfDeath.ElememtAt(0).Code}");</para>
        /// </example>
        [Property("Record Axis Cause Of Death", Property.Types.Tuple4Arr, "Coded Content", "", true, IGURL.RecordAxisCauseOfDeath, false, 50)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=80357-7)", "")]
        public IEnumerable<(int Position, string Code, bool Pregnancy)> RecordAxisCauseOfDeath
        {
            get
            {
                List<(int Position, string Code, bool Pregnancy)> rac = new List<(int Position, string Code, bool Pregnancy)>();
                if (RecordAxisCauseOfDeathObsList != null)
                {
                    foreach (Observation ob in RecordAxisCauseOfDeathObsList)
                    {
                        int? position = null;
                        string icd10code = null;
                        bool pregnancy = false;
                        Observation.ComponentComponent positionComp = ob.Component.Where(c => c.Code.Coding[0].Code == "position").FirstOrDefault();
                        if (positionComp != null && positionComp.Value != null)
                        {
                            position = ((Integer)positionComp.Value).Value;
                        }
                        CodeableConcept valueCC = (CodeableConcept)ob.Value;
                        if (valueCC != null && valueCC.Coding != null && valueCC.Coding.Count() > 0)
                        {
                            icd10code = valueCC.Coding[0].Code;
                        }
                        Observation.ComponentComponent pregComp = ob.Component.Where(c => c.Code.Coding[0].Code == "wouldBeUnderlyingCauseOfDeathWithoutPregnancy").FirstOrDefault();
                        if (pregComp != null && pregComp.Value != null)
                        {
                            pregnancy = (bool)((FhirBoolean)pregComp.Value).Value;
                        }
                        if (position != null && icd10code != null)
                        {
                            rac.Add((Position: (int)position, Code: icd10code, Pregnancy: pregnancy));
                        }
                    }
                }
                return rac.OrderBy(entry => entry.Position);
            }
            set
            {
                // clear all existing eac
                Bundle.Entry.RemoveAll(entry => entry.Resource is Observation && (((Observation)entry.Resource).Code.Coding.First().Code == "80357-7"));
                if (RecordAxisCauseOfDeathObsList != null)
                {
                    RecordAxisCauseOfDeathObsList.Clear();
                }
                else
                {
                    RecordAxisCauseOfDeathObsList = new List<Observation>();
                }
                // Rebuild the list of observations
                foreach ((int Position, string Code, bool Pregnancy) rac in value)
                {
                    Observation ob = new Observation();
                    ob.Id = Guid.NewGuid().ToString();
                    ob.Meta = new Meta();
                    string[] recordAxis_profile = { ProfileURL.RecordAxisCauseOfDeath };
                    ob.Meta.Profile = recordAxis_profile;
                    ob.Status = ObservationStatus.Final;
                    ob.Code = new CodeableConcept(CodeSystems.LOINC, "80357-7", "Cause of death record axis code [Automated]", null);
                    ob.Subject = new ResourceReference("urn:uuid:" + Decedent.Id);
                    AddReferenceToComposition(ob.Id, "CodedContent");

                    ob.Effective = new FhirDateTime();
                    ob.Value = new CodeableConcept(CodeSystems.ICD10, rac.Code, null, null);

                    Observation.ComponentComponent positionComp = new Observation.ComponentComponent();
                    positionComp.Value = new Integer(rac.Position);
                    positionComp.Code = new CodeableConcept(CodeSystems.Component, "position", "Position", null);
                    ob.Component.Add(positionComp);

                    // Record axis codes have an unusual and obscure handling of a Pregnancy flag, for more information see
                    // http://build.fhir.org/ig/HL7/vrdr/branches/master/StructureDefinition-vrdr-record-axis-cause-of-death.html#usage
                    if (rac.Pregnancy)
                    {
                        Observation.ComponentComponent pregComp = new Observation.ComponentComponent();
                        pregComp.Value = new FhirBoolean(true);
                        pregComp.Code = new CodeableConcept(CodeSystems.Component, "wouldBeUnderlyingCauseOfDeathWithoutPregnancy", "Would be underlying cause of death without pregnancy, if true");
                        ob.Component.Add(pregComp);
                    }

                    Bundle.AddResourceEntry(ob, "urn:uuid:" + ob.Id);
                    RecordAxisCauseOfDeathObsList.Add(ob);
                }
            }
        }


        /// <summary>The year NCHS received the death record.</summary>
        /// <value>year, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ReceiptYear = 2022 </para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Receipt Year: {ExampleDeathRecord.ReceiptYear}");</para>
        /// </example>
        [Property("ReceiptYear", Property.Types.Int32, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, true)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public int? ReceiptYear
        {
            get
            {
                Date date = CodingStatusValues?.GetSingleValue<Date>("receiptDate");
                return GetDateFragmentOrPartialDate(date, ExtensionURL.DateYear);
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                Date date = CodingStatusValues?.GetSingleValue<Date>("receiptDate");
                SetPartialDate(date.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate), ExtensionURL.DateYear, value);
            }
        }

        /// <summary>
        /// The month NCHS received the death record.
        /// </summary>
        /// <summary>The month NCHS received the death record.</summary>
        /// <value>month, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ReceiptMonth = 11 </para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Receipt Month: {ExampleDeathRecord.ReceiptMonth}");</para>
        /// </example>
        [Property("ReceiptMonth", Property.Types.Int32, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, true)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public int? ReceiptMonth
        {
            get
            {
                Date date = CodingStatusValues?.GetSingleValue<Date>("receiptDate");
                return GetDateFragmentOrPartialDate(date, ExtensionURL.DateMonth);
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                Date date = CodingStatusValues?.GetSingleValue<Date>("receiptDate");
                SetPartialDate(date.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate), ExtensionURL.DateMonth, value);
            }
        }

        /// <summary>The day NCHS received the death record.</summary>
        /// <value>day, or -1 if explicitly unknown, or null if never specified</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ReceiptDay = 13 </para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Receipt Day: {ExampleDeathRecord.ReceiptDay}");</para>
        /// </example>
        [Property("ReceiptDay", Property.Types.Int32, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, true)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public int? ReceiptDay
        {
            get
            {
                Date date = CodingStatusValues?.GetSingleValue<Date>("receiptDate");
                return GetDateFragmentOrPartialDate(date, ExtensionURL.DateDay);
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                Date date = CodingStatusValues?.GetSingleValue<Date>("receiptDate");
                SetPartialDate(date.Extension.Find(ext => ext.Url == ExtensionURL.PartialDate), ExtensionURL.DateDay, value);
            }
        }

        /// <summary>Receipt Date.</summary>
        /// <value>receipt date</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ReceiptDate = "2018-02-19";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Receipt Date: {ExampleDeathRecord.ReceiptDate}");</para>
        /// </example>
        [Property("Receipt Date", Property.Types.StringDateTime, "Coded Content", "Receipt Date.", true, IGURL.CodingStatusValues, true, 25)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public string ReceiptDate
        {
            get
            {
                // We support this legacy-style API entrypoint via the new partial date and time entrypoints
                if (ReceiptYear != null && ReceiptYear != -1 && ReceiptMonth != null && ReceiptMonth != -1 && ReceiptDay != null && ReceiptDay != -1)
                {
                    Date result = new Date((int)ReceiptYear, (int)ReceiptMonth, (int)ReceiptDay);
                    return result.ToString();
                }
                return null;
            }
            set
            {
                // We support this legacy-style API entrypoint via the new partial date and time entrypoints
                DateTimeOffset parsedDate;
                if (DateTimeOffset.TryParse(value, out parsedDate))
                {
                    ReceiptYear = parsedDate.Year;
                    ReceiptMonth = parsedDate.Month;
                    ReceiptDay = parsedDate.Day;
                }
            }
        }

        /// <summary>
        /// Coder Status; TRX field with no IJE mapping
        /// </summary>
        /// <summary>Coder Status; TRX field with no IJE mapping</summary>
        /// <value>integer code</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.CoderStatus = 3;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Coder STatus {ExampleDeathRecord.CoderStatus}");</para>
        /// </example>
        [Property("CoderStatus", Property.Types.Int32, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, false)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public int? CoderStatus
        {
            get
            {
                return this.CodingStatusValues?.GetSingleValue<Integer>("coderStatus")?.Value;
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                CodingStatusValues.Remove("coderStatus");
                if (value != null)
                {
                    CodingStatusValues.Add("coderStatus", new Integer(value));
                }
            }
        }

        /// <summary>
        /// Shipment Number; TRX field with no IJE mapping
        /// </summary>
        /// <summary>Coder Status; TRX field with no IJE mapping</summary>
        /// <value>string</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.ShipmentNumber = "abc123"";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Shipment Number{ExampleDeathRecord.ShipmentNumber}");</para>
        /// </example>
        [Property("ShipmentNumber", Property.Types.String, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, false)]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public string ShipmentNumber
        {
            get
            {
                return this.CodingStatusValues?.GetSingleValue<FhirString>("shipmentNumber")?.Value;
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                CodingStatusValues.Remove("shipmentNumber");
                if (value != null)
                {
                    CodingStatusValues.Add("shipmentNumber", new FhirString(value));
                }
            }
        }
        /// <summary>
        /// Intentional Reject
        /// </summary>
        /// <summary>Intentional Reject</summary>
        /// <value>string</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; reject = new Dictionary&lt;string, string&gt;();</para>
        /// <para>format.Add("code", ValueSets.FilingFormat.electronic);</para>
        /// <para>format.Add("system", CodeSystems.IntentionalReject);</para>
        /// <para>format.Add("display", "Reject1");</para>
        /// <para>ExampleDeathRecord.IntentionalReject = "reject";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Intentional Reject {ExampleDeathRecord.IntentionalReject}");</para>
        /// </example>
        [Property("IntentionalReject", Property.Types.Dictionary, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, true)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public Dictionary<string, string> IntentionalReject
        {
            get
            {
                CodeableConcept intentionalReject = this.CodingStatusValues?.GetSingleValue<CodeableConcept>("intentionalReject");
                if (intentionalReject != null)
                {
                    return CodeableConceptToDict(intentionalReject);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                CodingStatusValues.Remove("intentionalReject");
                if (value != null)
                {
                    CodingStatusValues.Add("intentionalReject", DictToCodeableConcept(value));
                }
            }
        }

        /// <summary>Intentional Reject Helper.</summary>
        /// <value>Intentional Reject
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.IntentionalRejectHelper = ValueSets.IntentionalReject.Not_Rejected;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Intentional Reject Code: {ExampleDeathRecord.IntentionalRejectHelper}");</para>
        /// </example>
        [Property("IntentionalRejectHelper", Property.Types.String, "Intentional Reject Codes", "IntentionalRejectCodes.", false, IGURL.CodingStatusValues, true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public string IntentionalRejectHelper
        {
            get
            {
                if (IntentionalReject.ContainsKey("code"))
                {
                    return IntentionalReject["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("IntentionalReject", value, VRDR.ValueSets.IntentionalReject.Codes);
            }
        }

        /// <summary>Acme System Reject.</summary>
        /// <value>
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; reject = new Dictionary&lt;string, string&gt;();</para>
        /// <para>format.Add("code", ValueSets.FilingFormat.electronic);</para>
        /// <para>format.Add("system", CodeSystems.SystemReject);</para>
        /// <para>format.Add("display", "3");</para>
        /// <para>ExampleDeathRecord.AcmeSystemReject = reject;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Acme System Reject Code: {ExampleDeathRecord.AcmeSystemReject}");</para>
        /// </example>

        [Property("AcmeSystemReject", Property.Types.Dictionary, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, true)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public Dictionary<string, string> AcmeSystemReject
        {
            get
            {
                CodeableConcept acmeSystemReject = this.CodingStatusValues?.GetSingleValue<CodeableConcept>("acmeSystemReject");
                if (acmeSystemReject != null)
                {
                    return CodeableConceptToDict(acmeSystemReject);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                CodingStatusValues.Remove("acmeSystemReject");
                if (value != null)
                {
                    CodingStatusValues.Add("acmeSystemReject", DictToCodeableConcept(value));
                }
            }
        }

        /// <summary>Acme System Reject.</summary>
        /// <value>
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.AcmeSystemRejectHelper = "3";</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Acme System Reject Code: {ExampleDeathRecord.AcmeSystemReject}");</para>
        /// </example>
        [Property("AcmeSystemRejectHelper", Property.Types.String, "Acme System Reject Codes", "AcmeSystemRejectCodes.", false, IGURL.CodingStatusValues, true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public string AcmeSystemRejectHelper
        {
            get
            {
                if (AcmeSystemReject.ContainsKey("code"))
                {
                    return AcmeSystemReject["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("AcmeSystemReject", value, VRDR.ValueSets.AcmeSystemReject.Codes);
            }
        }


        /// <summary>Transax Conversion Flag</summary>
        /// <value>
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; tcf = new Dictionary&lt;string, string&gt;();</para>
        /// <para>tcf.Add("code", "3");</para>
        /// <para>tcf.Add("system", CodeSystems.TransaxConversion);</para>
        /// <para>tcf.Add("display", "Conversion using non-ambivalent table entries");</para>
        /// <para>ExampleDeathRecord.TransaxConversion = tcf;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Transax Conversion Code: {ExampleDeathRecord.TransaxConversion}");</para>
        /// </example>
        [Property("TransaxConversion", Property.Types.Dictionary, "Coded Content", "Coding Status", true, IGURL.CodingStatusValues, true)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [PropertyParam("system", "The relevant code system.")]
        [PropertyParam("display", "The human readable version of this code.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public Dictionary<string, string> TransaxConversion
        {
            get
            {
                CodeableConcept transaxConversion = this.CodingStatusValues?.GetSingleValue<CodeableConcept>("transaxConversion");
                if (transaxConversion != null)
                {
                    return CodeableConceptToDict(transaxConversion);
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodingStatusValues == null)
                {
                    CreateCodingStatusValues();
                }
                CodingStatusValues.Remove("transaxConversion");
                if (value != null)
                {
                    CodingStatusValues.Add("transaxConversion", DictToCodeableConcept(value));
                }
            }
        }

        /// <summary>TransaxConversion Helper.</summary>
        /// <value>transax conversion code
        /// <para>"code" - the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.TransaxConversionHelper = ValueSets.TransaxConversion.Conversion_Using_Non_Ambivalent_Table_Entries;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($"Filing Format: {ExampleDeathRecord.TransaxConversionHelper}");</para>
        /// </example>
        [Property("TransaxConversionFlag Helper", Property.Types.String, "Transax Conversion", "TransaxConversion Flag.", false, IGURL.CodingStatusValues, true, 4)]
        [PropertyParam("code", "The code used to describe this concept.")]
        [FHIRPath("Bundle.entry.resource.where($this is Observation).where(code.coding.code=codingstatus)", "")]
        public string TransaxConversionHelper
        {
            get
            {
                if (TransaxConversion.ContainsKey("code"))
                {
                    return TransaxConversion["code"];
                }
                return null;
            }
            set
            {
                SetCodeValue("TransaxConversion", value, VRDR.ValueSets.TransaxConversion.Codes);
            }
        }

    }
}