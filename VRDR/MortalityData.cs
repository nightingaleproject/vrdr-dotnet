using System;
using System.Linq;
using System.Collections.Generic;

namespace VRDR
{
    /// <summary>Data helper class for dealing with IJE mortality data. Follows Singleton-esque pattern!</summary>
    public sealed class MortalityData
    {
        private MortalityData()
        {
        }
        /// <summary>Instance get method for singleton.</summary>
        public static MortalityData Instance { get { return Nested.instance; } }
        private class Nested
        {
            static Nested() { }
            internal static readonly MortalityData instance = new MortalityData();
        }

        // /// <summary>Given a State code, return a random PlaceCode.</summary>
        // public PlaceCode StateCodeToRandomPlace(string state)
        // {
        //     Random random = new Random();
        //     List<PlaceCode> places =
        //         PlaceCodes.Where(t => LinqHelper.EqualsInsensitive(t.State, state)).ToList();
        //     return places[random.Next(places.Count)];
        // }

        /// <summary>Given a State, Territory, or Province name - return the representative State code.</summary>
        public string StateNameToStateCode(string state)
        {
            if (StateTerritoryProvinceCodes.Values.Contains(state))
            {
                // Passed a code so just return it
                return state;
            }
            else
            {
                // Passed a name so look up code
                return DictValueFinderHelper(StateTerritoryProvinceCodes, state);
            }
        }

        /// <summary>Given a Jurisdiction code - return the Jurisdiction name.</summary>
        public string JurisdictionNameToJurisdictionCode(string jurisdiction)
        {

            return DictValueFinderHelper(JurisdictionCodes, jurisdiction);
        }
         /// <summary>Given a Jurisdiction name - return the representative Jurisdiction code.</summary>
        public string JurisdictionCodeToJurisdictionName(string code)
        {
             return DictKeyFinderHelper(JurisdictionCodes, code);
        }

        /// <summary>Given a State, Territory, or Province code - return the representative State, Territory, or Province name.</summary>
        public string StateCodeToStateName(string code)
        {
            return DictKeyFinderHelper(StateTerritoryProvinceCodes, code);
        }

        /// <summary>Given a Country name - return the representative Country code.</summary>
        public string CountryNameToCountryCode(string country)
        {
            if (DictKeyFinderHelper(CountryCodes, country) != null)
            {
                // Passed a code so just return it
                return country;
            }
            else
            {
                // Passed a name so look up code
                return DictValueFinderHelper(CountryCodes, country);
            }
        }

        /// <summary>Given a Country code - return the representative Country name.</summary>
        public string CountryCodeToCountryName(string code)
        {
            return DictKeyFinderHelper(CountryCodes, code);
        }

        // Note: did not delete this, because it is referenced by a line in VRDR.HTTP/Nightingale.cs that I commented out.
        // /// <summary>Given a Race name - return the representative Race code.</summary>
        // public string RaceNameToRaceCode(string name)
        // {
        //     return WRaceNameToRaceCode(name) ?? BAARaceNameToRaceCode(name) ?? ARaceNameToRaceCode(name) ?? AIANRaceNameToRaceCode(name) ?? NHOPIRaceNameToRaceCode(name);
        // }

        // /// <summary>Given a Race code - return the representative Race name.</summary>
        // public string RaceCodeToRaceName(string code)
        // {
        //     return WRaceCodeToRaceName(code) ?? BAARaceCodeToRaceName(code) ?? ARaceCodeToRaceName(code) ?? AIANRaceCodeToRaceName(code) ?? NHOPIRaceCodeToRaceName(code);
        // }

        /// <summary>Given a value in a &lt;string, string&gt; object, return the first matching key.</summary>
        // public string ABC<T>(T obj) where T : IDestination
        private static string DictKeyFinderHelper<T>(T dict, string value) where T : IEnumerable<KeyValuePair<string, string>>
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            return dict.FirstOrDefault(
                t => LinqHelper.EqualsInsensitive(t.Value, value)
            ).Key;
        }

        /// <summary>Given a key in a (string, string) object, return the first matching value.</summary>
        private static string DictValueFinderHelper<T>(T dict, string key) where T : IEnumerable<KeyValuePair<string, string>>
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                return null;
            }
            return dict.FirstOrDefault(
                t => LinqHelper.EqualsInsensitive(t.Key, key)
            ).Value;
        }
        /// <summary>Jurisdiction Codes</summary>
        // JurisdictionCodes uses IJE-defined two-character string as key, and provides the jurisdiction's name.
        // all codes are USPS Postal codes except for YC
         public Dictionary<string, string> JurisdictionCodes = new Dictionary<string, string>
        {
            {"AL","Alabama"},
            {"AK","Alaska"},
            {"AS","American Samoa"},
            {"AZ","Arizona"},
            {"AR","Arkansas"},
            {"CA","California"},
            {"CO","Colorado"},
            {"CT","Connecticut"},
            {"DE","Delaware"},
            {"DC","District of Columbia"},
            {"FL","Florida"},
            {"GA","Georgia"},
            {"GU","Guam"},
            {"HI","Hawaii"},
            {"ID","Idaho"},
            {"IL","Illinois"},
            {"IN","Indiana"},
            {"IA","Iowa"},
            {"KS","Kansas"},
            {"KY","Kentucky"},
            {"LA","Louisiana"},
            {"ME","Maine"},
            {"MD","Maryland"},
            {"MA","Massachusetts"},
            {"MI","Michigan"},
            {"MN","Minnesota"},
            {"MS","Mississippi"},
            {"MO","Missouri"},
            {"MT","Montana"},
            {"NE","Nebraska"},
            {"NV","Nevada"},
            {"NH","New Hampshire"},
            {"NJ","New Jersey"},
            {"NM","New Mexico"},
            {"NY","New York"},
            {"NC","North Carolina"},
            {"ND","North Dakota"},
            {"MP","Northern Mariana Islands"},
            {"OH","Ohio"},
            {"OK","Oklahoma"},
            {"OR","Oregon"},
            {"PA","Pennsylvania"},
            {"PR","Puerto Rico"},
            {"RI","Rhode Island"},
            {"SC","South Carolina"},
            {"SD","South Dakota"},
            {"TN","Tennessee"},
            {"TX","Texas"},
            {"UT","Utah"},
            {"VT","Vermont"},
            {"VI","Virgin Islands"},
            {"VA","Virginia"},
            {"WA","Washington"},
            {"WV","West Virginia"},
            {"WI","Wisconsin"},
            {"WY","Wyoming"},
            {"YC","New York City"},
            {"TT","Test Jurisdiction"}, // This should only be used for testing with NCHS.
            {"TS","STEVE Test Jurisdiction"} // This should only be used for STEVE testing with NCHS.
        };

        /// <summary>State and Territory Province Codes</summary>
        public Dictionary<string, string> StateTerritoryProvinceCodes = new Dictionary<string, string>()
        {
            { "Alabama", "AL" },
            { "Alaska", "AK" },
            { "Arizona", "AZ" },
            { "Arkansas", "AR" },
            { "California", "CA" },
            { "Colorado", "CO" },
            { "Connecticut", "CT" },
            { "Delaware", "DE" },
            { "Florida", "FL" },
            { "Georgia", "GA" },
            { "Hawaii", "HI" },
            { "Idaho", "ID" },
            { "Illinois", "IL" },
            { "Indiana", "IN" },
            { "Iowa", "IA" },
            { "Kansas", "KS" },
            { "Kentucky", "KY" },
            { "Louisiana", "LA" },
            { "Maine", "ME" },
            { "Maryland", "MD" },
            { "Massachusetts", "MA" },
            { "Michigan", "MI" },
            { "Minnesota", "MN" },
            { "Mississippi", "MS" },
            { "Missouri", "MO" },
            { "Montana", "MT" },
            { "Nebraska", "NE" },
            { "Nevada", "NV" },
            { "New Hampshire", "NH" },
            { "New Jersey", "NJ" },
            { "New Mexico", "NM" },
            { "New York", "NY" },
            { "North Carolina", "NC" },
            { "North Dakota", "ND" },
            { "Ohio", "OH" },
            { "Oklahoma", "OK" },
            { "Oregon", "OR" },
            { "Pennsylvania", "PA" },
            { "Rhode Island", "RI" },
            { "South Carolina", "SC" },
            { "South Dakota", "SD" },
            { "Tennessee", "TN" },
            { "Texas", "TX" },
            { "Utah", "UT" },
            { "Vermont", "VT" },
            { "Virginia", "VA" },
            { "Washington", "WA" },
            { "West Virginia", "WV" },
            { "Wisconsin", "WI" },
            { "Wyoming", "WY" },
            { "Northern Marianas", "MP" },
            { "American Samoa", "AS" },
            { "Guam", "GU" },
            { "Virgin Islands", "VI" },
            { "Puerto Rico", "PR" },
            { "Alberta", "AB" },
            { "British Columbia", "BC" },
            { "Manitoba", "MB" },
            { "New Brunswick", "NB" },
            { "Newfoundland", "NF" },
            { "Nova Scotia", "NS" },
            { "Northwest Territories", "NT" },
            { "Nunavut", "NU" },
            { "Ontario", "ON" },
            { "Prince Edward Island", "PE" },
            { "Quebec", "QC" },
            { "Saskatchewan", "SK" },
            { "Yukon", "YK" },
            { "District of Columbia", "DC" }
        };

        /// <summary>Country Codes based on PH_Country_GEC = 2.16.840.1.113883.13.250    </summary>
        public List<KeyValuePair<string, string>> CountryCodes = new List<KeyValuePair<string, string>> {
             new KeyValuePair<string, string>("Aruba", "AA"),
            new KeyValuePair<string, string>("Antigua And Barbuda", "AC"),
            new KeyValuePair<string, string>("United Arab Emirates", "AE"),
            new KeyValuePair<string, string>("﻿afghanistan", "AF"),
            new KeyValuePair<string, string>("Algeria", "AG"),
            new KeyValuePair<string, string>("Azerbaijan", "AJ"),
            new KeyValuePair<string, string>("Albania", "AL"),
            new KeyValuePair<string, string>("Armenia", "AM"),
            new KeyValuePair<string, string>("Andorra", "AN"),
            new KeyValuePair<string, string>("Angola", "AO"),
            new KeyValuePair<string, string>("Argentina", "AR"),
            new KeyValuePair<string, string>("Australia", "AS"),
            new KeyValuePair<string, string>("Ashmore And Cartier Islands", "AT"),
            new KeyValuePair<string, string>("Austria", "AU"),
            new KeyValuePair<string, string>("Anguilla", "AV"),
            new KeyValuePair<string, string>("Akrotiri", "AX"),
            new KeyValuePair<string, string>("Antarctica", "AY"),
            new KeyValuePair<string, string>("Bahrain", "BA"),
            new KeyValuePair<string, string>("Barbados", "BB"),
            new KeyValuePair<string, string>("Botswana", "BC"),
            new KeyValuePair<string, string>("Bermuda", "BD"),
            new KeyValuePair<string, string>("Belgium", "BE"),
            new KeyValuePair<string, string>("Bahamas, The", "BF"),
            new KeyValuePair<string, string>("Bangladesh", "BG"),
            new KeyValuePair<string, string>("Belize", "BH"),
            new KeyValuePair<string, string>("Bosnia And Herzegovina", "BK"),
            new KeyValuePair<string, string>("Bolivia", "BL"),
            new KeyValuePair<string, string>("Burma, Myanmar", "BM"),
            new KeyValuePair<string, string>("Benin", "BN"),
            new KeyValuePair<string, string>("Belarus", "BO"),
            new KeyValuePair<string, string>("Solomon Islands", "BP"),
            new KeyValuePair<string, string>("Brazil", "BR"),
            new KeyValuePair<string, string>("Bassas Da India", "BS"),
            new KeyValuePair<string, string>("Bhutan", "BT"),
            new KeyValuePair<string, string>("Bulgaria", "BU"),
            new KeyValuePair<string, string>("Bouvet Island", "BV"),
            new KeyValuePair<string, string>("Brunei", "BX"),
            new KeyValuePair<string, string>("Burundi", "BY"),
            new KeyValuePair<string, string>("Canada", "CA"),
            new KeyValuePair<string, string>("Cambodia", "CB"),
            new KeyValuePair<string, string>("Chad", "CD"),
            new KeyValuePair<string, string>("Sri Lanka", "CE"),
            new KeyValuePair<string, string>("Congo (brazzaville), Republic Of The Congo", "CF"),
            new KeyValuePair<string, string>("Congo (kinshasa), Democratic Republic Of The Congo, Zaire", "CG"),
            new KeyValuePair<string, string>("China", "CH"),
            new KeyValuePair<string, string>("Chile", "CI"),
            new KeyValuePair<string, string>("Cayman Islands", "CJ"),
            new KeyValuePair<string, string>("Cocos (keeling) Islands", "CK"),
            new KeyValuePair<string, string>("Central And Southern Line Islands", "CL"),
            new KeyValuePair<string, string>("Cameroon", "CM"),
            new KeyValuePair<string, string>("Comoros", "CN"),
            new KeyValuePair<string, string>("Colombia", "CO"),
            new KeyValuePair<string, string>("Coral Sea Islands", "CR"),
            new KeyValuePair<string, string>("Costa Rica", "CS"),
            new KeyValuePair<string, string>("Central African Republic", "CT"),
            new KeyValuePair<string, string>("Cuba", "CU"),
            new KeyValuePair<string, string>("Cape Verde", "CV"),
            new KeyValuePair<string, string>("Cook Islands", "CW"),
            new KeyValuePair<string, string>("Cyprus", "CY"),
            new KeyValuePair<string, string>("Czechoslovakia", "CZ"),
            new KeyValuePair<string, string>("Denmark", "DA"),
            new KeyValuePair<string, string>("Djibouti", "DJ"),
            new KeyValuePair<string, string>("Dahomey", "DM"),
            new KeyValuePair<string, string>("Dominica", "DO"),
            new KeyValuePair<string, string>("Jarvis Island", "DQ"),
            new KeyValuePair<string, string>("Dominican Republic", "DR"),
            new KeyValuePair<string, string>("Dhekelia", "DX"),
            new KeyValuePair<string, string>("East Berlin", "EB"),
            new KeyValuePair<string, string>("Ecuador", "EC"),
            new KeyValuePair<string, string>("Egypt", "EG"),
            new KeyValuePair<string, string>("Ireland", "EI"),
            new KeyValuePair<string, string>("Equatorial Guinea", "EK"),
            new KeyValuePair<string, string>("Estonia", "EN"),
            new KeyValuePair<string, string>("Canton And Enderberry Islands", "EQ"),
            new KeyValuePair<string, string>("Eritrea", "ER"),
            new KeyValuePair<string, string>("El Salvador", "ES"),
            new KeyValuePair<string, string>("Ethiopia", "ET"),
            new KeyValuePair<string, string>("Europa Island", "EU"),
            new KeyValuePair<string, string>("Czech Republic", "EZ"),
            new KeyValuePair<string, string>("French Guiana", "FG"),
            new KeyValuePair<string, string>("Åland, Finland", "FI"),
            new KeyValuePair<string, string>("Fiji", "FJ"),
            new KeyValuePair<string, string>("Falkland Islands (islas Malvinas), Islas Malvinas", "FK"),
            new KeyValuePair<string, string>("Federated States Of Micronesia, Micronesia,federated States Of", "FM"),
            new KeyValuePair<string, string>("Faroe Islands", "FO"),
            new KeyValuePair<string, string>("French Polynesia, Tahiti", "FP"),
            new KeyValuePair<string, string>("France", "FR"),
            new KeyValuePair<string, string>("French Southern And Antarctic Lands", "FS"),
            new KeyValuePair<string, string>("French Territory Of The Affars And Issas", "FT"),
            new KeyValuePair<string, string>("Gambia,the", "GA"),
            new KeyValuePair<string, string>("Gabon", "GB"),
            new KeyValuePair<string, string>("East Germany, German Democratic Republic", "GC"),
            new KeyValuePair<string, string>("Federal Republic Of Germany, West Germany", "GE"),
            new KeyValuePair<string, string>("Georgia", "GG"),
            new KeyValuePair<string, string>("Ghana", "GH"),
            new KeyValuePair<string, string>("Gibraltar", "GI"),
            new KeyValuePair<string, string>("Grenada", "GJ"),
            new KeyValuePair<string, string>("Guernsey", "GK"),
            new KeyValuePair<string, string>("Greenland", "GL"),
            new KeyValuePair<string, string>("Germany", "GM"),
            new KeyValuePair<string, string>("Gilbert And Ellice Islands", "GN"),
            new KeyValuePair<string, string>("Glorioso Islands", "GO"),
            new KeyValuePair<string, string>("Guadeloupe", "GP"),
            new KeyValuePair<string, string>("Greece", "GR"),
            new KeyValuePair<string, string>("Gilbert Islands", "GS"),
            new KeyValuePair<string, string>("Guatemala", "GT"),
            new KeyValuePair<string, string>("Guinea", "GV"),
            new KeyValuePair<string, string>("Guyana", "GY"),
            new KeyValuePair<string, string>("Gaza Strip", "GZ"),
            new KeyValuePair<string, string>("Haiti", "HA"),
            new KeyValuePair<string, string>("Hong Kong", "HK"),
            new KeyValuePair<string, string>("Heard Island And Mcdonald Islands", "HM"),
            new KeyValuePair<string, string>("Honduras", "HO"),
            new KeyValuePair<string, string>("Howland Island", "HQ"),
            new KeyValuePair<string, string>("Croatia", "HR"),
            new KeyValuePair<string, string>("Hungary", "HU"),
            new KeyValuePair<string, string>("Iceland", "IC"),
            new KeyValuePair<string, string>("Indonesia", "ID"),
            new KeyValuePair<string, string>("Isle Of Man", "IM"),
            new KeyValuePair<string, string>("India", "IN"),
            new KeyValuePair<string, string>("British Indian Ocean Territory", "IO"),
            new KeyValuePair<string, string>("Clipperton Island", "IP"),
            new KeyValuePair<string, string>("Us Miscellaneous Pacific Islands", "IQ"),
            new KeyValuePair<string, string>("Iran", "IR"),
            new KeyValuePair<string, string>("Israel", "IS"),
            new KeyValuePair<string, string>("Italy", "IT"),
            new KeyValuePair<string, string>("Israel-syria Demilitarized Zone", "IU"),
            new KeyValuePair<string, string>("Côte D’ivoire, Ivory Coast", "IV"),
            new KeyValuePair<string, string>("Israel-jordan Demilitarized Zone", "IW"),
            new KeyValuePair<string, string>("Iraq-saudi Arabia Neutral Zone", "IY"),
            new KeyValuePair<string, string>("Iraq", "IZ"),
            new KeyValuePair<string, string>("Japan", "JA"),
            new KeyValuePair<string, string>("Jersey", "JE"),
            new KeyValuePair<string, string>("Jamaica", "JM"),
            new KeyValuePair<string, string>("Jan Mayen", "JN"),
            new KeyValuePair<string, string>("Jordan", "JO"),
            new KeyValuePair<string, string>("Johnston Atoll", "JQ"),
            new KeyValuePair<string, string>("Svalbard And Jan Mayen", "JS"),
            new KeyValuePair<string, string>("Juan De Nova Island", "JU"),
            new KeyValuePair<string, string>("Kenya", "KE"),
            new KeyValuePair<string, string>("Kyrgyzstan", "KG"),
            new KeyValuePair<string, string>("Korea,north, North Korea", "KN"),
            new KeyValuePair<string, string>("Kiribati", "KR"),
            new KeyValuePair<string, string>("Korea,south, South Korea", "KS"),
            new KeyValuePair<string, string>("Christmas Island", "KT"),
            new KeyValuePair<string, string>("Kuwait", "KU"),
            new KeyValuePair<string, string>("Kosovo", "KV"),
            new KeyValuePair<string, string>("Kazakhstan", "KZ"),
            new KeyValuePair<string, string>("Laos", "LA"),
            new KeyValuePair<string, string>("Lebanon", "LE"),
            new KeyValuePair<string, string>("Latvia", "LG"),
            new KeyValuePair<string, string>("Lithuania", "LH"),
            new KeyValuePair<string, string>("Liberia", "LI"),
            new KeyValuePair<string, string>("Slovakia", "LO"),
            new KeyValuePair<string, string>("Palmyra Atoll", "LQ"),
            new KeyValuePair<string, string>("Liechtenstein", "LS"),
            new KeyValuePair<string, string>("Lesotho", "LT"),
            new KeyValuePair<string, string>("Luxembourg", "LU"),
            new KeyValuePair<string, string>("Libya", "LY"),
            new KeyValuePair<string, string>("Madagascar", "MA"),
            new KeyValuePair<string, string>("Martinique", "MB"),
            new KeyValuePair<string, string>("Macau", "MC"),
            new KeyValuePair<string, string>("Moldova", "MD"),
            new KeyValuePair<string, string>("Mayotte", "MF"),
            new KeyValuePair<string, string>("Mongolia", "MG"),
            new KeyValuePair<string, string>("Montserrat", "MH"),
            new KeyValuePair<string, string>("Malawi", "MI"),
            new KeyValuePair<string, string>("Montenegro", "MJ"),
            new KeyValuePair<string, string>("Macedonia", "MK"),
            new KeyValuePair<string, string>("Mali", "ML"),
            new KeyValuePair<string, string>("Monaco", "MN"),
            new KeyValuePair<string, string>("Morocco", "MO"),
            new KeyValuePair<string, string>("Mauritius", "MP"),
            new KeyValuePair<string, string>("Midway Islands", "MQ"),
            new KeyValuePair<string, string>("Mauritania", "MR"),
            new KeyValuePair<string, string>("Malta", "MT"),
            new KeyValuePair<string, string>("Oman", "MU"),
            new KeyValuePair<string, string>("Maldives", "MV"),
            new KeyValuePair<string, string>("Mexico", "MX"),
            new KeyValuePair<string, string>("Malaysia", "MY"),
            new KeyValuePair<string, string>("Mozambique", "MZ"),
            new KeyValuePair<string, string>("New Caledonia", "NC"),
            new KeyValuePair<string, string>("Niue", "NE"),
            new KeyValuePair<string, string>("Norfolk Island", "NF"),
            new KeyValuePair<string, string>("Niger", "NG"),
            new KeyValuePair<string, string>("New Hebrides, Vanuatu", "NH"),
            new KeyValuePair<string, string>("Nigeria", "NI"),
            new KeyValuePair<string, string>("Bonaire, Netherlands, Saba, Saint Eustatius", "NL"),
            new KeyValuePair<string, string>("Sint Maarten", "NN"),
            new KeyValuePair<string, string>("Norway", "NO"),
            new KeyValuePair<string, string>("Nepal", "NP"),
            new KeyValuePair<string, string>("Nauru", "NR"),
            new KeyValuePair<string, string>("Suriname", "NS"),
            new KeyValuePair<string, string>("Netherlands Antilles", "NT"),
            new KeyValuePair<string, string>("Nicaragua", "NU"),
            new KeyValuePair<string, string>("New Zealand", "NZ"),
            new KeyValuePair<string, string>("South Sudan", "OD"),
            new KeyValuePair<string, string>("Paraguay", "PA"),
            new KeyValuePair<string, string>("Pitcairn Island", "PC"),
            new KeyValuePair<string, string>("Peru", "PE"),
            new KeyValuePair<string, string>("Paracel Islands", "PF"),
            new KeyValuePair<string, string>("Spratly Islands", "PG"),
            new KeyValuePair<string, string>("Etorofu, Habomai,kunashiri,and Shikotan Islands", "PJ"),
            new KeyValuePair<string, string>("Pakistan", "PK"),
            new KeyValuePair<string, string>("Poland", "PL"),
            new KeyValuePair<string, string>("Panama", "PM"),
            new KeyValuePair<string, string>("Panama", "PN"),
            new KeyValuePair<string, string>("Azores, Portugal", "PO"),
            new KeyValuePair<string, string>("Papua New Guinea", "PP"),
            new KeyValuePair<string, string>("Panama Canal Zone", "PQ"),
            new KeyValuePair<string, string>("Palau", "PS"),
            new KeyValuePair<string, string>("Guinea-bissau", "PU"),
            new KeyValuePair<string, string>("Qatar", "QA"),
            new KeyValuePair<string, string>("Reunion", "RE"),
            new KeyValuePair<string, string>("Rhodesia, Southern Rhodesia", "RH"),
            new KeyValuePair<string, string>("Serbia", "RI"),
            new KeyValuePair<string, string>("Marshall Islands", "RM"),
            new KeyValuePair<string, string>("Saint Martin", "RN"),
            new KeyValuePair<string, string>("Romania", "RO"),
            new KeyValuePair<string, string>("Philippines", "RP"),
            new KeyValuePair<string, string>("Russia", "RS"),
            new KeyValuePair<string, string>("Rwanda", "RW"),
            new KeyValuePair<string, string>("Saudi Arabia", "SA"),
            new KeyValuePair<string, string>("Saint Pierre And Miquelon", "SB"),
            new KeyValuePair<string, string>("Nevis, Saint Kitts And Nevis", "SC"),
            new KeyValuePair<string, string>("Seychelles", "SE"),
            new KeyValuePair<string, string>("South Africa", "SF"),
            new KeyValuePair<string, string>("Senegal", "SG"),
            new KeyValuePair<string, string>("Saint Helena, Ascension And Tristan Da Cunha", "SH"),
            new KeyValuePair<string, string>("Slovenia", "SI"),
            new KeyValuePair<string, string>("Sikkim", "SK"),
            new KeyValuePair<string, string>("Sierra Leone", "SL"),
            new KeyValuePair<string, string>("San Marino", "SM"),
            new KeyValuePair<string, string>("Singapore", "SN"),
            new KeyValuePair<string, string>("Somalia", "SO"),
            new KeyValuePair<string, string>("Spain", "SP"),
            new KeyValuePair<string, string>("Swan Islands", "SQ"),
            new KeyValuePair<string, string>("Spanish Sahara", "SS"),
            new KeyValuePair<string, string>("Saint Lucia", "ST"),
            new KeyValuePair<string, string>("Sudan", "SU"),
            new KeyValuePair<string, string>("Svalbard", "SV"),
            new KeyValuePair<string, string>("Sweden", "SW"),
            new KeyValuePair<string, string>("South Georgia And South Sandwich Islands", "SX"),
            new KeyValuePair<string, string>("Syria", "SY"),
            new KeyValuePair<string, string>("Switzerland", "SZ"),
            new KeyValuePair<string, string>("Saint Barthélemy", "TB"),
            new KeyValuePair<string, string>("United Arab Emirates", "TC"),
            new KeyValuePair<string, string>("Trinidad And Tobago", "TD"),
            new KeyValuePair<string, string>("Tromelin Island", "TE"),
            new KeyValuePair<string, string>("Thailand", "TH"),
            new KeyValuePair<string, string>("Tajikistan", "TI"),
            new KeyValuePair<string, string>("Turks And Caicos Islands", "TK"),
            new KeyValuePair<string, string>("Tokelau", "TL"),
            new KeyValuePair<string, string>("Tonga", "TN"),
            new KeyValuePair<string, string>("Togo", "TO"),
            new KeyValuePair<string, string>("Sao Tome And Principe", "TP"),
            new KeyValuePair<string, string>("Trust Territory Of The Pacific Islands", "TQ"),
            new KeyValuePair<string, string>("Tunisia", "TS"),
            new KeyValuePair<string, string>("East Timor, Timor-leste", "TT"),
            new KeyValuePair<string, string>("Turkey", "TU"),
            new KeyValuePair<string, string>("Tuvalu", "TV"),
            new KeyValuePair<string, string>("Taiwan", "TW"),
            new KeyValuePair<string, string>("Turkmenistan", "TX"),
            new KeyValuePair<string, string>("Tanzania", "TZ"),
            new KeyValuePair<string, string>("Curaçao", "UC"),
            new KeyValuePair<string, string>("Uganda", "UG"),
            new KeyValuePair<string, string>("England, Great Britain, United Kingdom", "UK"),
            new KeyValuePair<string, string>("Ukraine", "UP"),
            new KeyValuePair<string, string>("Soviet Union, Union Of Soviet Socialist Republics", "UR"),
            new KeyValuePair<string, string>("United States", "US"),
            new KeyValuePair<string, string>("Burkina Faso, Upper Volta", "UV"),
            new KeyValuePair<string, string>("Uruguay", "UY"),
            new KeyValuePair<string, string>("Uzbekistan", "UZ"),
            new KeyValuePair<string, string>("Saint Vincent And The Grenadines", "VC"),
            new KeyValuePair<string, string>("Venezuela", "VE"),
            new KeyValuePair<string, string>("British Virgin Islands, Virgin Islands,british", "VI"),
            new KeyValuePair<string, string>("Vietnam", "VM"),
            new KeyValuePair<string, string>("North Vietnam", "VN"),
            new KeyValuePair<string, string>("South Vietnam", "VS"),
            new KeyValuePair<string, string>("Holy See, Vatican City", "VT"),
            new KeyValuePair<string, string>("Namibia", "WA"),
            new KeyValuePair<string, string>("West Berlin", "WB"),
            new KeyValuePair<string, string>("West Bank", "WE"),
            new KeyValuePair<string, string>("Wallis And Futuna", "WF"),
            new KeyValuePair<string, string>("Western Sahara", "WI"),
            new KeyValuePair<string, string>("Wake Island", "WQ"),
            new KeyValuePair<string, string>("Samoa", "WS"),
            new KeyValuePair<string, string>("Swaziland", "WZ"),
            new KeyValuePair<string, string>("Yemen (sana'a)", "YE"),
            new KeyValuePair<string, string>("Serbia And Montenegro, Yugoslavia", "YI"),
            new KeyValuePair<string, string>("Yemen", "YM"),
            new KeyValuePair<string, string>("Ryukyu Islands,southern", "YQ"),
            new KeyValuePair<string, string>("Yemen (aden)", "YS"),
            new KeyValuePair<string, string>("Zambia", "ZA"),
            new KeyValuePair<string, string>("Zimbabwe", "ZI"),
            new KeyValuePair<string, string>("Not Classifiable", "ZZ")
        };
    }
    /// <summary>Internal Helper class which provides Trimming and Case-Insensitive comparison of LINQ Queries.</summary>
    internal static class LinqHelper
    {
        /// <summary>Adds a extension to handle case insensitive comparisons, always Trims second parameter.</summary>
        public static bool EqualsInsensitive(this string str, string value) =>
            string.Equals(str, value.Trim(), StringComparison.OrdinalIgnoreCase);

    }
}
