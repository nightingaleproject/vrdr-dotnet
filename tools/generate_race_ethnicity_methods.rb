# This script generates code for all the race and ethnicity methods in DeathRecord.cs
#
# Usage: ruby tools/generate_race_ethnicity_methods.rb

require 'json'
require 'erb'
require 'byebug'
template = "
        /// <summary>#descrip#.</summary>
        /// <value>#descrip#. A Dictionary representing a code, containing the following key/value pairs:
        /// <para>\"code\" - the code</para>
        /// <para>\"system\" - the code system this code belongs to</para>
        /// <para>\"display\" - a human readable meaning of the code</para>
        /// </value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>Dictionary&lt;string, string&gt; racecode = new Dictionary&lt;string, string&gt;();</para>
        /// <para>racecode.Add(\"code\", \"300\");</para>
        /// <para>racecode.Add(\"system\", CodeSystems.RaceCode);</para>
        /// <para>racecode.Add(\"display\", \"African\");</para>
        /// <para>ExampleDeathRecord.*name* = racecode;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($\"#descrip#: {ExampleDeathRecord.*name*['display']}\");</para>
        /// </example>
        [Property(\"*name*\", Property.Types.Dictionary, \"Coded Content\", \"#descrip#.\", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam(\"code\", \"The code used to describe this concept.\")]
        [PropertyParam(\"system\", \"The relevant code system.\")]
        [PropertyParam(\"display\", \"The human readable version of this code.\")]
        [FHIRPath(\"Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')\", \"\")]
        public Dictionary<string, string> *name*
        {
            get
            {
                if (CodedRaceandEthnicityObs != null)
                {
                    Observation.ComponentComponent racecode = CodedRaceandEthnicityObs.Component.FirstOrDefault(c => c.Code.Coding[0].Code == \"@component@\");
                    if (racecode != null && racecode.Value != null && racecode.Value as CodeableConcept != null)
                    {
                        return CodeableConceptToDict((CodeableConcept)racecode.Value);
                    }
                }
                return EmptyCodeableDict();
            }
            set
            {
                if (CodedRaceandEthnicityObs == null)
                {
                    CreateCodedRaceandEthnicityObs();
                }
                CodedRaceandEthnicityObs.Component.RemoveAll(c => c.Code.Coding[0].Code == \"@component@\");
                Observation.ComponentComponent component = new Observation.ComponentComponent();
                component.Code = new CodeableConcept(CodeSystems.ComponentCode, \"@component@\", \"&descripnorace&\", null);
                component.Value = DictToCodeableConcept(value);
                CodedRaceandEthnicityObs.Component.Add(component);
            }
        }

        /// <summary>#descrip#  Helper</summary>
        /// <value>#descrip# Helper.</value>
        /// <example>
        /// <para>// Setter:</para>
        /// <para>ExampleDeathRecord.*name*Helper = VRDR.ValueSets.RaceCode.African ;</para>
        /// <para>// Getter:</para>
        /// <para>Console.WriteLine($\"#descrip#: {ExampleDeathRecord.*name*Helper}\");</para>
        /// </example>
        [Property(\"#descrip# Helper\", Property.Types.String, \"Coded Content\", \"#descrip#.\", true, IGURL.CodedRaceAndEthnicity, false, 34)]
        [PropertyParam(\"code\", \"The code used to describe this concept.\")]
        [FHIRPath(\"Bundle.entry.resource.where($this is Observation).where(code.coding.code='codedraceandethnicity')\", \"\")]
        public string *name*Helper
        {
            get
            {
                if (*name*.ContainsKey(\"code\"))
                {
                    return *name*[\"code\"];
                }
                return null;
            }
            set
            {
                SetCodeValue(\"*name*\", value, VRDR.ValueSets.RaceCode.Codes);
            }
        }

"

race_methods = ["First Edited", "Second Edited","Third Edited", "Fourth Edited","Fifth Edited", "Sixth Edited","Seventh Edited", "Eighth Edited",
                         "First American Indian", "Second American Indian",
                         "First Other Asian", "Second Other Asian",
                         "First Other Pacific Islander", "Second Other Pacific Islander",
                         "First Other", "Second Other"];

race_methods.each do |prefix|
  descrip = prefix + " Race Code"
  descripnorace = prefix + " Race"
  name = descrip.split(' ').join()
  namenorace = descripnorace.split(' ').join()
  stage1 = template.gsub("#descrip#", descrip)
  stage2 = stage1.gsub("*name*", name)
  stage3 = stage2.gsub("@component@", namenorace)
  stage4 = stage3.gsub("&descripnorace&", descripnorace)
  puts stage4
end
ethnicity_methods = ["Hispanic Code", "Hispanic Code For Literal"];
ethnicity_methods.each do |prefix|
  descrip = prefix
  name = descrip.split(' ').join()
  stage1 = template.gsub("#descrip#", descrip)
  stage2 = stage1.gsub("*name*", name)
  stage3 = stage2.gsub("@component@", name)
  stage4 = stage3.gsub("&descripnorace&", descrip)
  puts stage4
end
