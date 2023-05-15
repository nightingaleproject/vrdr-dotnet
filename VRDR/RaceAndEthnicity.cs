using System.Collections.Generic;
using Hl7.Fhir.ElementModel.Types;

namespace VRDR
{
    /// <summary> String representations of IJE Race fields </summary>
    public static class NvssRace
    {
        /// <summary> White </summary>
        public const string White = "White";
        /// <summary> BlackOrAfricanAmerican </summary>
        public const string BlackOrAfricanAmerican = "BlackOrAfricanAmerican";
        /// <summary> AmericanIndianOrAlaskanNative </summary>
        public const string AmericanIndianOrAlaskanNative = "AmericanIndianOrAlaskanNative";
        /// <summary> AsianIndian </summary>
        public const string AsianIndian = "AsianIndian";
        /// <summary> Chinese </summary>
        public const string Chinese = "Chinese";
        /// <summary> Filipino </summary>
        public const string Filipino = "Filipino";
        /// <summary> Japanese </summary>
        public const string Japanese = "Japanese";
        /// <summary> Korean </summary>
        public const string Korean = "Korean";
        /// <summary> Vietnamese </summary>
        public const string Vietnamese = "Vietnamese";
        /// <summary> OtherAsian </summary>
        public const string OtherAsian = "OtherAsian";
        /// <summary> NativeHawaiian </summary>
        public const string NativeHawaiian = "NativeHawaiian";
        /// <summary> GuamanianOrChamorro </summary>
        public const string GuamanianOrChamorro = "GuamanianOrChamorro";
        /// <summary> Samoan </summary>
        public const string Samoan = "Samoan";
        /// <summary> OtherPacificIslander </summary>
        public const string OtherPacificIslander = "OtherPacificIslander";
        /// <summary> OtherRace </summary>
        public const string OtherRace = "OtherRace";
        /// <summary> FirstAmericanIndianOrAlaskanNativeLiteral </summary>
        public const string FirstAmericanIndianOrAlaskanNativeLiteral = "FirstAmericanIndianOrAlaskanNativeLiteral";
        /// <summary> SecondAmericanIndianOrAlaskanNativeLiteral </summary>
        public const string SecondAmericanIndianOrAlaskanNativeLiteral = "SecondAmericanIndianOrAlaskanNativeLiteral";
        /// <summary> FirstOtherAsianLiteralFirst </summary>
        public const string FirstOtherAsianLiteral = "FirstOtherAsianLiteral";
        /// <summary> SecondOtherPacificIslanderLiteral </summary>
        public const string SecondOtherAsianLiteral = "SecondOtherAsianLiteral";
        /// <summary> FirstOtherPacificIslanderLiteral </summary>
        public const string FirstOtherPacificIslanderLiteral = "FirstOtherPacificIslanderLiteral";
        /// <summary> SecondOtherPacificIslanderLiteral </summary>
        public const string SecondOtherPacificIslanderLiteral = "SecondOtherPacificIslanderLiteral";
        /// <summary> FirstOtherRaceLiteral </summary>
        public const string FirstOtherRaceLiteral = "FirstOtherRaceLiteral";
        /// <summary> SecondOtherRaceLiteral </summary>
        public const string SecondOtherRaceLiteral = "SecondOtherRaceLiteral";
        /// <summary> MissingValueReason </summary>
        public const string MissingValueReason = "MissingValueReason";
        /// <summary> GetBooleanRaceCodes Returns a list of the Boolean Race Codes, Y or N values </summary>
        public static List<string> GetBooleanRaceCodes()
        {
            List<string> booleanRaceCodes = new List<string>();
            booleanRaceCodes.Add(NvssRace.White);
            booleanRaceCodes.Add(NvssRace.BlackOrAfricanAmerican);
            booleanRaceCodes.Add(NvssRace.AmericanIndianOrAlaskanNative);
            booleanRaceCodes.Add(NvssRace.AsianIndian);
            booleanRaceCodes.Add(NvssRace.Chinese);
            booleanRaceCodes.Add(NvssRace.Filipino);
            booleanRaceCodes.Add(NvssRace.Japanese);
            booleanRaceCodes.Add(NvssRace.Korean);
            booleanRaceCodes.Add(NvssRace.Vietnamese);
            booleanRaceCodes.Add(NvssRace.OtherAsian);
            booleanRaceCodes.Add(NvssRace.NativeHawaiian);
            booleanRaceCodes.Add(NvssRace.GuamanianOrChamorro);
            booleanRaceCodes.Add(NvssRace.Samoan);
            booleanRaceCodes.Add(NvssRace.OtherPacificIslander);
            booleanRaceCodes.Add(NvssRace.OtherRace);
            return booleanRaceCodes;
        }
        /// <summary> GetDisplayValueForCode returns the display value for a race code, or the code itself if none exists</summary>
        public static string GetDisplayValueForCode(string code)
        {
            switch (code)
            {
                case BlackOrAfricanAmerican:
                    return "Black Or African American";
                case AmericanIndianOrAlaskanNative:
                    return "American Indian Or Alaskan Native";
                case AsianIndian:
                    return "Asian Indian";
                case OtherAsian:
                    return "Other Asian";
                case NativeHawaiian:
                    return "Native Hawaiian";
                case GuamanianOrChamorro:
                    return "Guamanian Or Chamorro";
                case OtherPacificIslander:
                    return "Other Pacific Islander";
                case OtherRace:
                    return "Other Race";
                default:
                    return code;
            }
        }
        /// <summary> GetLiteralRaceCodes Returns a list of the literal Race Codes</summary>
        public static List<string> GetLiteralRaceCodes()
        {
            List<string> literalRaceCodes = new List<string>();
            literalRaceCodes.Add(NvssRace.FirstAmericanIndianOrAlaskanNativeLiteral);
            literalRaceCodes.Add(NvssRace.SecondAmericanIndianOrAlaskanNativeLiteral);
            literalRaceCodes.Add(NvssRace.FirstOtherAsianLiteral);
            literalRaceCodes.Add(NvssRace.SecondOtherAsianLiteral);
            literalRaceCodes.Add(NvssRace.FirstOtherPacificIslanderLiteral);
            literalRaceCodes.Add(NvssRace.SecondOtherPacificIslanderLiteral);
            literalRaceCodes.Add(NvssRace.FirstOtherRaceLiteral);
            literalRaceCodes.Add(NvssRace.SecondOtherRaceLiteral);
            return literalRaceCodes;
        }
    };
    /// <summary> String representations of IJE Ethnicity fields </summary>
    public static class NvssEthnicity
    {
        /// <summary> Mexican </summary>
        public const string Mexican = "HispanicMexican";
        /// <summary> Hispanic Mexican </summary>
        public const string MexicanDisplay = "Hispanic Mexican";
        /// <summary> Puerto Rican </summary>
        public const string PuertoRican = "HispanicPuertoRican";
        /// <summary> Hispanic Puerto Rican </summary>
        public const string PuertoRicanDisplay = "Hispanic Puerto Rican";
        /// <summary> Cuban </summary>
        public const string Cuban = "HispanicCuban";
        /// <summary> Hispanic Cuban </summary>
        public const string CubanDisplay = "Hispanic Cuban";
        /// <summary> Other </summary>
        public const string Other = "HispanicOther";
        /// <summary> Hispanic Other </summary>
        public const string OtherDisplay = "Hispanic Other";
        /// <summary> Literal </summary>
        public const string Literal = "HispanicLiteral";
        /// <summary> Hispanic Literal </summary>
        public const string LiteralDisplay = "Hispanic Literal";
    }


}