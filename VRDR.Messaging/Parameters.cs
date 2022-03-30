
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace VRDR
{
    /// <summary>Class
    /// <c>BaseMessage</c> is the base class of all messages.
    /// This partial class was automatically generated from https://github.com/nightingaleproject/vital_records_fhir_messaging_ig
    ///</summary>
    public partial class BaseMessage
    {
    /// <summary>R_YR</summary>
    /// <value>uint </value>
    public uint? R_YR
    {
      get
      {
          return GetParameterUnsignedInt( "R_YR");
      }
      set
      {
            SetParameterUnsignedInt(value, "R_YR");
      }
    }
    /// <summary>R_MO</summary>
    /// <value>uint </value>
    public uint? R_MO
    {
      get
      {
          return GetParameterUnsignedInt( "R_MO");
      }
      set
      {
            SetParameterUnsignedInt(value, "R_MO");
      }
    }
    /// <summary>R_DY</summary>
    /// <value>uint </value>
    public uint? R_DY
    {
      get
      {
          return GetParameterUnsignedInt( "R_DY");
      }
      set
      {
            SetParameterUnsignedInt(value, "R_DY");
      }
    }
    /// <summary>CS</summary>
    /// <value>uint </value>
    public uint? CS
    {
      get
      {
          return GetParameterUnsignedInt( "CS");
      }
      set
      {
            SetParameterUnsignedInt(value, "CS");
      }
    }
    /// <summary>SHIP</summary>
    /// <value>string </value>
    public string SHIP
    {
      get
      {
          return GetParameterString( "SHIP");
      }
      set
      {
            SetParameterString(value, "SHIP");
      }
    }
    /// <summary>SYS_REJ</summary>
    /// <value>uint </value>
    public uint? SYS_REJ
    {
      get
      {
          return GetParameterUnsignedInt( "SYS_REJ");
      }
      set
      {
            SetParameterUnsignedInt(value, "SYS_REJ");
      }
    }
    /// <summary>INT_REJ</summary>
    /// <value>uint </value>
    public uint? INT_REJ
    {
      get
      {
          return GetParameterUnsignedInt( "INT_REJ");
      }
      set
      {
            SetParameterUnsignedInt(value, "INT_REJ");
      }
    }
    /// <summary>DETHNICE</summary>
    /// <value>string </value>
    public string DETHNICE
    {
      get
      {
          return GetParameterString( "coded_ethnicity", "DETHNICE");
      }
      set
      {
            SetParameterString(value, "coded_ethnicity", "DETHNICE");
      }
    }
    /// <summary>DETHNIC5C</summary>
    /// <value>string </value>
    public string DETHNIC5C
    {
      get
      {
          return GetParameterString( "coded_ethnicity", "DETHNIC5C");
      }
      set
      {
            SetParameterString(value, "coded_ethnicity", "DETHNIC5C");
      }
    }
    /// <summary>RACE1E</summary>
    /// <value>string </value>
    public string RACE1E
    {
      get
      {
          return GetParameterString( "coded_race", "RACE1E");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE1E");
      }
    }
    /// <summary>RACE2E</summary>
    /// <value>string </value>
    public string RACE2E
    {
      get
      {
          return GetParameterString( "coded_race", "RACE2E");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE2E");
      }
    }
    /// <summary>RACE3E</summary>
    /// <value>string </value>
    public string RACE3E
    {
      get
      {
          return GetParameterString( "coded_race", "RACE3E");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE3E");
      }
    }
    /// <summary>RACE4E</summary>
    /// <value>string </value>
    public string RACE4E
    {
      get
      {
          return GetParameterString( "coded_race", "RACE4E");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE4E");
      }
    }
    /// <summary>RACE5E</summary>
    /// <value>string </value>
    public string RACE5E
    {
      get
      {
          return GetParameterString( "coded_race", "RACE5E");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE5E");
      }
    }
    /// <summary>RACE6E</summary>
    /// <value>string </value>
    public string RACE6E
    {
      get
      {
          return GetParameterString( "coded_race", "RACE6E");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE6E");
      }
    }
    /// <summary>RACE7E</summary>
    /// <value>string </value>
    public string RACE7E
    {
      get
      {
          return GetParameterString( "coded_race", "RACE7E");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE7E");
      }
    }
    /// <summary>RACE8E</summary>
    /// <value>string </value>
    public string RACE8E
    {
      get
      {
          return GetParameterString( "coded_race", "RACE8E");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE8E");
      }
    }
    /// <summary>RACE16C</summary>
    /// <value>string </value>
    public string RACE16C
    {
      get
      {
          return GetParameterString( "coded_race", "RACE16C");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE16C");
      }
    }
    /// <summary>RACE17C</summary>
    /// <value>string </value>
    public string RACE17C
    {
      get
      {
          return GetParameterString( "coded_race", "RACE17C");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE17C");
      }
    }
    /// <summary>RACE18C</summary>
    /// <value>string </value>
    public string RACE18C
    {
      get
      {
          return GetParameterString( "coded_race", "RACE18C");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE18C");
      }
    }
    /// <summary>RACE19C</summary>
    /// <value>string </value>
    public string RACE19C
    {
      get
      {
          return GetParameterString( "coded_race", "RACE19C");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE19C");
      }
    }
    /// <summary>RACE20C</summary>
    /// <value>string </value>
    public string RACE20C
    {
      get
      {
          return GetParameterString( "coded_race", "RACE20C");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE20C");
      }
    }
    /// <summary>RACE21C</summary>
    /// <value>string </value>
    public string RACE21C
    {
      get
      {
          return GetParameterString( "coded_race", "RACE21C");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE21C");
      }
    }
    /// <summary>RACE22C</summary>
    /// <value>string </value>
    public string RACE22C
    {
      get
      {
          return GetParameterString( "coded_race", "RACE22C");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE22C");
      }
    }
    /// <summary>RACE23C</summary>
    /// <value>string </value>
    public string RACE23C
    {
      get
      {
          return GetParameterString( "coded_race", "RACE23C");
      }
      set
      {
            SetParameterString(value, "coded_race", "RACE23C");
      }
    }
    /// <summary>DETHNIC1</summary>
    /// <value>string </value>
    public string DETHNIC1
    {
      get
      {
          return GetParameterString( "input_ethnicity", "DETHNIC1");
      }
      set
      {
            SetParameterString(value, "input_ethnicity", "DETHNIC1");
      }
    }
    /// <summary>DETHNIC2</summary>
    /// <value>string </value>
    public string DETHNIC2
    {
      get
      {
          return GetParameterString( "input_ethnicity", "DETHNIC2");
      }
      set
      {
            SetParameterString(value, "input_ethnicity", "DETHNIC2");
      }
    }
    /// <summary>DETHNIC3</summary>
    /// <value>string </value>
    public string DETHNIC3
    {
      get
      {
          return GetParameterString( "input_ethnicity", "DETHNIC3");
      }
      set
      {
            SetParameterString(value, "input_ethnicity", "DETHNIC3");
      }
    }
    /// <summary>DETHNIC4</summary>
    /// <value>string </value>
    public string DETHNIC4
    {
      get
      {
          return GetParameterString( "input_ethnicity", "DETHNIC4");
      }
      set
      {
            SetParameterString(value, "input_ethnicity", "DETHNIC4");
      }
    }
    /// <summary>DETHNIC5</summary>
    /// <value>string </value>
    public string DETHNIC5
    {
      get
      {
          return GetParameterString( "input_ethnicity", "DETHNIC5");
      }
      set
      {
            SetParameterString(value, "input_ethnicity", "DETHNIC5");
      }
    }
    /// <summary>RACE1</summary>
    /// <value>string </value>
    public string RACE1
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE1");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE1");
      }
    }
    /// <summary>RACE2</summary>
    /// <value>string </value>
    public string RACE2
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE2");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE2");
      }
    }
    /// <summary>RACE3</summary>
    /// <value>string </value>
    public string RACE3
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE3");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE3");
      }
    }
    /// <summary>RACE4</summary>
    /// <value>string </value>
    public string RACE4
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE4");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE4");
      }
    }
    /// <summary>RACE5</summary>
    /// <value>string </value>
    public string RACE5
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE5");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE5");
      }
    }
    /// <summary>RACE6</summary>
    /// <value>string </value>
    public string RACE6
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE6");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE6");
      }
    }
    /// <summary>RACE7</summary>
    /// <value>string </value>
    public string RACE7
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE7");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE7");
      }
    }
    /// <summary>RACE8</summary>
    /// <value>string </value>
    public string RACE8
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE8");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE8");
      }
    }
    /// <summary>RACE9</summary>
    /// <value>string </value>
    public string RACE9
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE9");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE9");
      }
    }
    /// <summary>RACE10</summary>
    /// <value>string </value>
    public string RACE10
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE10");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE10");
      }
    }
    /// <summary>RACE11</summary>
    /// <value>string </value>
    public string RACE11
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE11");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE11");
      }
    }
    /// <summary>RACE12</summary>
    /// <value>string </value>
    public string RACE12
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE12");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE12");
      }
    }
    /// <summary>RACE13</summary>
    /// <value>string </value>
    public string RACE13
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE13");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE13");
      }
    }
    /// <summary>RACE14</summary>
    /// <value>string </value>
    public string RACE14
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE14");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE14");
      }
    }
    /// <summary>RACE15</summary>
    /// <value>string </value>
    public string RACE15
    {
      get
      {
          return GetParameterString( "input_race_flags", "RACE15");
      }
      set
      {
            SetParameterString(value, "input_race_flags", "RACE15");
      }
    }
    /// <summary>RACE16</summary>
    /// <value>string </value>
    public string RACE16
    {
      get
      {
          return GetParameterString( "input_race_literals", "RACE16");
      }
      set
      {
            SetParameterString(value, "input_race_literals", "RACE16");
      }
    }
    /// <summary>RACE17</summary>
    /// <value>string </value>
    public string RACE17
    {
      get
      {
          return GetParameterString( "input_race_literals", "RACE17");
      }
      set
      {
            SetParameterString(value, "input_race_literals", "RACE17");
      }
    }
    /// <summary>RACE18</summary>
    /// <value>string </value>
    public string RACE18
    {
      get
      {
          return GetParameterString( "input_race_literals", "RACE18");
      }
      set
      {
            SetParameterString(value, "input_race_literals", "RACE18");
      }
    }
    /// <summary>RACE19</summary>
    /// <value>string </value>
    public string RACE19
    {
      get
      {
          return GetParameterString( "input_race_literals", "RACE19");
      }
      set
      {
            SetParameterString(value, "input_race_literals", "RACE19");
      }
    }
    /// <summary>RACE20</summary>
    /// <value>string </value>
    public string RACE20
    {
      get
      {
          return GetParameterString( "input_race_literals", "RACE20");
      }
      set
      {
            SetParameterString(value, "input_race_literals", "RACE20");
      }
    }
    /// <summary>RACE21</summary>
    /// <value>string </value>
    public string RACE21
    {
      get
      {
          return GetParameterString( "input_race_literals", "RACE21");
      }
      set
      {
            SetParameterString(value, "input_race_literals", "RACE21");
      }
    }
    /// <summary>RACE22</summary>
    /// <value>string </value>
    public string RACE22
    {
      get
      {
          return GetParameterString( "input_race_literals", "RACE22");
      }
      set
      {
            SetParameterString(value, "input_race_literals", "RACE22");
      }
    }
    /// <summary>RACE23</summary>
    /// <value>string </value>
    public string RACE23
    {
      get
      {
          return GetParameterString( "input_race_literals", "RACE23");
      }
      set
      {
            SetParameterString(value, "input_race_literals", "RACE23");
      }
    }
    /// <summary>COD1A</summary>
    /// <value>string </value>
    public string COD1A
    {
      get
      {
          return GetParameterString( "input_causes_of_death", "COD1A");
      }
      set
      {
            SetParameterString(value, "input_causes_of_death", "COD1A");
      }
    }
    /// <summary>COD1B</summary>
    /// <value>string </value>
    public string COD1B
    {
      get
      {
          return GetParameterString( "input_causes_of_death", "COD1B");
      }
      set
      {
            SetParameterString(value, "input_causes_of_death", "COD1B");
      }
    }
    /// <summary>COD1C</summary>
    /// <value>string </value>
    public string COD1C
    {
      get
      {
          return GetParameterString( "input_causes_of_death", "COD1C");
      }
      set
      {
            SetParameterString(value, "input_causes_of_death", "COD1C");
      }
    }
    /// <summary>COD1D</summary>
    /// <value>string </value>
    public string COD1D
    {
      get
      {
          return GetParameterString( "input_causes_of_death", "COD1D");
      }
      set
      {
            SetParameterString(value, "input_causes_of_death", "COD1D");
      }
    }
    /// <summary>INTERVAL1A</summary>
    /// <value>string </value>
    public string INTERVAL1A
    {
      get
      {
          return GetParameterString( "input_causes_of_death", "INTERVAL1A");
      }
      set
      {
            SetParameterString(value, "input_causes_of_death", "INTERVAL1A");
      }
    }
    /// <summary>INTERVAL1B</summary>
    /// <value>string </value>
    public string INTERVAL1B
    {
      get
      {
          return GetParameterString( "input_causes_of_death", "INTERVAL1B");
      }
      set
      {
            SetParameterString(value, "input_causes_of_death", "INTERVAL1B");
      }
    }
    /// <summary>INTERVAL1C</summary>
    /// <value>string </value>
    public string INTERVAL1C
    {
      get
      {
          return GetParameterString( "input_causes_of_death", "INTERVAL1C");
      }
      set
      {
            SetParameterString(value, "input_causes_of_death", "INTERVAL1C");
      }
    }
    /// <summary>INTERVAL1D</summary>
    /// <value>string </value>
    public string INTERVAL1D
    {
      get
      {
          return GetParameterString( "input_causes_of_death", "INTERVAL1D");
      }
      set
      {
            SetParameterString(value, "input_causes_of_death", "INTERVAL1D");
      }
    }
    /// <summary>OTHERCONDITION</summary>
    /// <value>string </value>
    public string OTHERCONDITION
    {
      get
      {
          return GetParameterString( "input_causes_of_death", "OTHERCONDITION");
      }
      set
      {
            SetParameterString(value, "input_causes_of_death", "OTHERCONDITION");
      }
    }
    /// <summary>MANNER</summary>
    /// <value>string </value>
    public string MANNER
    {
      get
      {
          return GetParameterString( "input_misc_fields", "MANNER");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "MANNER");
      }
    }
    /// <summary>TRX_FLG</summary>
    /// <value>string </value>
    public string TRX_FLG
    {
      get
      {
          return GetParameterString( "input_misc_fields", "TRX_FLG");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "TRX_FLG");
      }
    }
    /// <summary>AUTOP</summary>
    /// <value>string </value>
    public string AUTOP
    {
      get
      {
          return GetParameterString( "input_misc_fields", "AUTOP");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "AUTOP");
      }
    }
    /// <summary>AUTOPF</summary>
    /// <value>string </value>
    public string AUTOPF
    {
      get
      {
          return GetParameterString( "input_misc_fields", "AUTOPF");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "AUTOPF");
      }
    }
    /// <summary>TOBAC</summary>
    /// <value>string </value>
    public string TOBAC
    {
      get
      {
          return GetParameterString( "input_misc_fields", "TOBAC");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "TOBAC");
      }
    }
    /// <summary>PREG</summary>
    /// <value>string </value>
    public string PREG
    {
      get
      {
          return GetParameterString( "input_misc_fields", "PREG");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "PREG");
      }
    }
    /// <summary>PREG_BYPASS</summary>
    /// <value>string </value>
    public string PREG_BYPASS
    {
      get
      {
          return GetParameterString( "input_misc_fields", "PREG_BYPASS");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "PREG_BYPASS");
      }
    }
    /// <summary>DOI_MO</summary>
    /// <value>uint </value>
    public uint? DOI_MO
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "DOI_MO");
      }
      set
      {
            SetParameterUnsignedInt(value, "input_misc_fields", "DOI_MO");
      }
    }
    /// <summary>DOI_DY</summary>
    /// <value>uint </value>
    public uint? DOI_DY
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "DOI_DY");
      }
      set
      {
            SetParameterUnsignedInt(value, "input_misc_fields", "DOI_DY");
      }
    }
    /// <summary>DOI_YR</summary>
    /// <value>uint </value>
    public uint? DOI_YR
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "DOI_YR");
      }
      set
      {
            SetParameterUnsignedInt(value, "input_misc_fields", "DOI_YR");
      }
    }
    /// <summary>TOI_HR</summary>
    /// <value>uint </value>
    public uint? TOI_HR
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "TOI_HR");
      }
      set
      {
            SetParameterUnsignedInt(value, "input_misc_fields", "TOI_HR");
      }
    }
    /// <summary>WORKINJ</summary>
    /// <value>string </value>
    public string WORKINJ
    {
      get
      {
          return GetParameterString( "input_misc_fields", "WORKINJ");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "WORKINJ");
      }
    }
    /// <summary>CERTL</summary>
    /// <value>string </value>
    public string CERTL
    {
      get
      {
          return GetParameterString( "input_misc_fields", "CERTL");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "CERTL");
      }
    }
    /// <summary>INACT</summary>
    /// <value>uint </value>
    public uint? INACT
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "INACT");
      }
      set
      {
            SetParameterUnsignedInt(value, "input_misc_fields", "INACT");
      }
    }
    /// <summary>AUXNO2</summary>
    /// <value>uint </value>
    public uint? AUXNO2
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "AUXNO2");
      }
      set
      {
            SetParameterUnsignedInt(value, "input_misc_fields", "AUXNO2");
      }
    }
    /// <summary>STATESP</summary>
    /// <value>string </value>
    public string STATESP
    {
      get
      {
          return GetParameterString( "input_misc_fields", "STATESP");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "STATESP");
      }
    }
    /// <summary>SUR_MO</summary>
    /// <value>uint </value>
    public uint? SUR_MO
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "SUR_MO");
      }
      set
      {
            SetParameterUnsignedInt(value, "input_misc_fields", "SUR_MO");
      }
    }
    /// <summary>SUR_DY</summary>
    /// <value>uint </value>
    public uint? SUR_DY
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "SUR_DY");
      }
      set
      {
            SetParameterUnsignedInt(value, "input_misc_fields", "SUR_DY");
      }
    }
    /// <summary>SUR_YR</summary>
    /// <value>uint </value>
    public uint? SUR_YR
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "SUR_YR");
      }
      set
      {
            SetParameterUnsignedInt(value, "input_misc_fields", "SUR_YR");
      }
    }
    /// <summary>TOI_UNIT</summary>
    /// <value>string </value>
    public string TOI_UNIT
    {
      get
      {
          return GetParameterString( "input_misc_fields", "TOI_UNIT");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "TOI_UNIT");
      }
    }
    /// <summary>INJPL</summary>
    /// <value>string </value>
    public string INJPL
    {
      get
      {
          return GetParameterString( "INJPL");
      }
      set
      {
            SetParameterString(value, "INJPL");
      }
    }
    /// <summary>MAN_UC</summary>
    /// <value>string </value>
    public string MAN_UC
    {
      get
      {
          return GetParameterString( "MAN_UC");
      }
      set
      {
            SetParameterString(value, "MAN_UC");
      }
    }
    /// <summary>ACME_UC</summary>
    /// <value>string </value>
    public string ACME_UC
    {
      get
      {
          return GetParameterString( "ACME_UC");
      }
      set
      {
            SetParameterString(value, "ACME_UC");
      }
    }
    /// <summary>RAC</summary>
    /// <value>string </value>
    public string RAC
    {
      get
      {
          return GetParameterString( "RAC");
      }
      set
      {
            SetParameterString(value, "RAC");
      }
    }
    /// <summary>lineNumber</summary>
    /// <value>uint </value>
    public uint? lineNumber
    {
      get
      {
          return GetParameterUnsignedInt( "EAC", "lineNumber");
      }
      set
      {
            SetParameterUnsignedInt(value, "EAC", "lineNumber");
      }
    }
    /// <summary>position</summary>
    /// <value>uint </value>
    public uint? position
    {
      get
      {
          return GetParameterUnsignedInt( "EAC", "position");
      }
      set
      {
            SetParameterUnsignedInt(value, "EAC", "position");
      }
    }
    /// <summary>coding</summary>
    /// <value>string </value>
    public string coding
    {
      get
      {
          return GetParameterString( "EAC", "coding");
      }
      set
      {
            SetParameterString(value, "EAC", "coding");
      }
    }
    /// <summary>e_code_indicator</summary>
    /// <value>string </value>
    public string e_code_indicator
    {
      get
      {
          return GetParameterString( "EAC", "e_code_indicator");
      }
      set
      {
            SetParameterString(value, "EAC", "e_code_indicator");
      }
    }
    /// <summary>jurisdiction_id</summary>
    /// <value>string </value>
    public string jurisdiction_id
    {
      get
      {
          return GetParameterString( "jurisdiction_id");
      }
      set
      {
            SetParameterString(value, "jurisdiction_id");
      }
    }
    /// <summary>cert_no</summary>
    /// <value>uint </value>
    public uint? cert_no
    {
      get
      {
          return GetParameterUnsignedInt( "cert_no");
      }
      set
      {
            SetParameterUnsignedInt(value, "cert_no");
      }
    }
    /// <summary>death_year</summary>
    /// <value>uint </value>
    public uint? death_year
    {
      get
      {
          return GetParameterUnsignedInt( "death_year");
      }
      set
      {
            SetParameterUnsignedInt(value, "death_year");
      }
    }
    /// <summary>state_auxiliary_id</summary>
    /// <value>string </value>
    public string state_auxiliary_id
    {
      get
      {
          return GetParameterString( "state_auxiliary_id");
      }
      set
      {
            SetParameterString(value, "state_auxiliary_id");
      }
    }
    /// <summary>block_count</summary>
    /// <value>uint </value>
    public uint? block_count
    {
      get
      {
          return GetParameterUnsignedInt( "block_count");
      }
      set
      {
            SetParameterUnsignedInt(value, "block_count");
      }
    }
    /// <summary>alias_decedent_first_name</summary>
    /// <value>string </value>
    public string alias_decedent_first_name
    {
      get
      {
          return GetParameterString( "alias_decedent_first_name");
      }
      set
      {
            SetParameterString(value, "alias_decedent_first_name");
      }
    }
    /// <summary>alias_decedent_last_name</summary>
    /// <value>string </value>
    public string alias_decedent_last_name
    {
      get
      {
          return GetParameterString( "alias_decedent_last_name");
      }
      set
      {
            SetParameterString(value, "alias_decedent_last_name");
      }
    }
    /// <summary>alias_decedent_middle_name</summary>
    /// <value>string </value>
    public string alias_decedent_middle_name
    {
      get
      {
          return GetParameterString( "alias_decedent_middle_name");
      }
      set
      {
            SetParameterString(value, "alias_decedent_middle_name");
      }
    }
    /// <summary>alias_decedent_name_suffix</summary>
    /// <value>string </value>
    public string alias_decedent_name_suffix
    {
      get
      {
          return GetParameterString( "alias_decedent_name_suffix");
      }
      set
      {
            SetParameterString(value, "alias_decedent_name_suffix");
      }
    }
    /// <summary>alias_father_surname</summary>
    /// <value>string </value>
    public string alias_father_surname
    {
      get
      {
          return GetParameterString( "alias_father_surname");
      }
      set
      {
            SetParameterString(value, "alias_father_surname");
      }
    }
    /// <summary>alias_social_security_number</summary>
    /// <value>string </value>
    public string alias_social_security_number
    {
      get
      {
          return GetParameterString( "alias_social_security_number");
      }
      set
      {
            SetParameterString(value, "alias_social_security_number");
      }
    }
  }
}
