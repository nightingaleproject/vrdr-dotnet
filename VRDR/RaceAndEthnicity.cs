using System.Collections.Generic;
namespace VRDR
{
    /// <summary> String representations of IJE Race fields </summary>
    public static class NvssRace {
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
        /// <summary> AmericanIndianOrAlaskanNativeLiteralFirst </summary>
        public const string AmericanIndianOrAlaskanNativeLiteral1 = "AmericanIndianOrAlaskanNativeLiteral1";
        /// <summary> AmericanIndianOrAlaskanNativeLiteralSecond </summary>
        public const string AmericanIndianOrAlaskanNativeLiteral2 = "AmericanIndianOrAlaskanNativeLiteral2";
        /// <summary> OtherAsianLiteralFirst </summary>
        public const string OtherAsianLiteral1 = "OtherAsianLiteral1";
        /// <summary> OtherAsianLiteralFirstSecond </summary>
        public const string OtherAsianLiteral2 = "OtherAsianLiteral2";
        /// <summary> OtherPacificIslandLiteralFirst </summary>
        public const string OtherPacificIslandLiteral1 = "OtherPacificIslandLiteral1";
        /// <summary> OtherPacificIslandLiteralSecond </summary>
        public const string OtherPacificIslandLiteral2 = "OtherPacificIslandLiteral2";
        /// <summary> OtherRaceLiteralFirst </summary>
        public const string OtherRaceLiteral1 = "OtherRaceLiteral1";
        /// <summary> OtherRaceLiteral2 </summary>
        public const string OtherRaceLiteral2 = "OtherRaceLiteral2";
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
        /// <summary> GetLiteralRaceCodes Returns a list of the literal Race Codes</summary>
        public static List<string> GetLiteralRaceCodes()
        {
            List<string> literalRaceCodes = new List<string>();
            literalRaceCodes.Add(NvssRace.AmericanIndianOrAlaskanNativeLiteral1);
            literalRaceCodes.Add(NvssRace.AmericanIndianOrAlaskanNativeLiteral2);
            literalRaceCodes.Add(NvssRace.OtherAsianLiteral1);
            literalRaceCodes.Add(NvssRace.OtherAsianLiteral2);
            literalRaceCodes.Add(NvssRace.OtherPacificIslandLiteral1);
            literalRaceCodes.Add(NvssRace.OtherPacificIslandLiteral2);
            literalRaceCodes.Add(NvssRace.OtherRaceLiteral1);
            literalRaceCodes.Add(NvssRace.OtherRaceLiteral2);
            return literalRaceCodes;
        }
    };
    /// <summary> String representations of IJE Ethnicity fields </summary>
    public static class NvssEthnicity {
        /// <summary> Mexican </summary>
        public const string Mexican = "HispanicMexican";
        /// <summary> PuertoRican </summary>
        public const string PuertoRican = "HispanicPuertoRican";
        /// <summary> Cuban </summary>
        public const string Cuban = "HispanicCuban";
        /// <summary> Other </summary>
        public const string Other = "HispanicOther";
        /// <summary> Literal </summary>
        public const string Literal = "HispanicLiteral";
    }


}