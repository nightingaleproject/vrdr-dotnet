
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace VRDR
{
    /// <summary>Class <c>BaseMessage</c> is the base class of all messages.</summary>
    public partial class BaseMessage
    {
    /// <summary>rec_yr</summary>
    /// <value>uint </value>
    public uint? rec_yr
    {
      get
      {
          return GetParameterUnsignedInt( "rec_yr");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "rec_yr");
          }
      }
    }
    /// <summary>rec_mo</summary>
    /// <value>uint </value>
    public uint? rec_mo
    {
      get
      {
          return GetParameterUnsignedInt( "rec_mo");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "rec_mo");
          }
      }
    }
    /// <summary>rec_dy</summary>
    /// <value>uint </value>
    public uint? rec_dy
    {
      get
      {
          return GetParameterUnsignedInt( "rec_dy");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "rec_dy");
          }
      }
    }
    /// <summary>cs</summary>
    /// <value>uint </value>
    public uint? cs
    {
      get
      {
          return GetParameterUnsignedInt( "cs");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "cs");
          }
      }
    }
    /// <summary>ship</summary>
    /// <value>string </value>
    public string ship
    {
      get
      {
          return GetParameterString( "ship");
      }
      set
      {
            SetParameterString(value, "ship");
        }
        }
    /// <summary>sys_rej</summary>
    /// <value>uint </value>
    public uint? sys_rej
    {
      get
      {
          return GetParameterUnsignedInt( "sys_rej");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "sys_rej");
          }
      }
    }
    /// <summary>int_rej</summary>
    /// <value>uint </value>
    public uint? int_rej
    {
      get
      {
          return GetParameterUnsignedInt( "int_rej");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "int_rej");
          }
      }
    }
    /// <summary>DETHNICE</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> DETHNICE
    {
      get
      {
          return GetParameterCoding( "coded_ethnicity", "DETHNICE");
      }
      set
      {
            SetParameterCoding(value, "coded_ethnicity", "DETHNICE");
        }
        }
    /// <summary>DETHNIC5C</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> DETHNIC5C
    {
      get
      {
          return GetParameterCoding( "coded_ethnicity", "DETHNIC5C");
      }
      set
      {
            SetParameterCoding(value, "coded_ethnicity", "DETHNIC5C");
        }
        }
    /// <summary>RACE1E</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE1E
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE1E");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE1E");
        }
        }
    /// <summary>RACE2E</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE2E
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE2E");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE2E");
        }
        }
    /// <summary>RACE3E</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE3E
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE3E");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE3E");
        }
        }
    /// <summary>RACE4E</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE4E
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE4E");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE4E");
        }
        }
    /// <summary>RACE5E</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE5E
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE5E");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE5E");
        }
        }
    /// <summary>RACE6E</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE6E
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE6E");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE6E");
        }
        }
    /// <summary>RACE7E</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE7E
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE7E");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE7E");
        }
        }
    /// <summary>RACE8E</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE8E
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE8E");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE8E");
        }
        }
    /// <summary>RACE16C</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE16C
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE16C");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE16C");
        }
        }
    /// <summary>RACE17C</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE17C
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE17C");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE17C");
        }
        }
    /// <summary>RACE18C</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE18C
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE18C");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE18C");
        }
        }
    /// <summary>RACE19C</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE19C
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE19C");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE19C");
        }
        }
    /// <summary>RACE20C</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE20C
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE20C");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE20C");
        }
        }
    /// <summary>RACE21C</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE21C
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE21C");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE21C");
        }
        }
    /// <summary>RACE22C</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE22C
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE22C");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE22C");
        }
        }
    /// <summary>RACE23C</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> RACE23C
    {
      get
      {
          return GetParameterCoding( "coded_race", "RACE23C");
      }
      set
      {
            SetParameterCoding(value, "coded_race", "RACE23C");
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
    /// <summary>manner</summary>
    /// <value>string </value>
    public string manner
    {
      get
      {
          return GetParameterString( "input_misc_fields", "manner");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "manner");
        }
        }
    /// <summary>injpl</summary>
    /// <value>string </value>
    public string injpl
    {
      get
      {
          return GetParameterString( "input_misc_fields", "injpl");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "injpl");
        }
        }
    /// <summary>trx_flg</summary>
    /// <value>string </value>
    public string trx_flg
    {
      get
      {
          return GetParameterString( "input_misc_fields", "trx_flg");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "trx_flg");
        }
        }
    /// <summary>autop</summary>
    /// <value>string </value>
    public string autop
    {
      get
      {
          return GetParameterString( "input_misc_fields", "autop");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "autop");
        }
        }
    /// <summary>autopf</summary>
    /// <value>string </value>
    public string autopf
    {
      get
      {
          return GetParameterString( "input_misc_fields", "autopf");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "autopf");
        }
        }
    /// <summary>tobac</summary>
    /// <value>string </value>
    public string tobac
    {
      get
      {
          return GetParameterString( "input_misc_fields", "tobac");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "tobac");
        }
        }
    /// <summary>preg</summary>
    /// <value>string </value>
    public string preg
    {
      get
      {
          return GetParameterString( "input_misc_fields", "preg");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "preg");
        }
        }
    /// <summary>preg_bypass</summary>
    /// <value>string </value>
    public string preg_bypass
    {
      get
      {
          return GetParameterString( "input_misc_fields", "preg_bypass");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "preg_bypass");
        }
        }
    /// <summary>doi_mo</summary>
    /// <value>uint </value>
    public uint? doi_mo
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "doi_mo");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "input_misc_fields", "doi_mo");
          }
      }
    }
    /// <summary>doi_dy</summary>
    /// <value>uint </value>
    public uint? doi_dy
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "doi_dy");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "input_misc_fields", "doi_dy");
          }
      }
    }
    /// <summary>doi_yr</summary>
    /// <value>uint </value>
    public uint? doi_yr
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "doi_yr");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "input_misc_fields", "doi_yr");
          }
      }
    }
    /// <summary>toi_hr</summary>
    /// <value>uint </value>
    public uint? toi_hr
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "toi_hr");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "input_misc_fields", "toi_hr");
          }
      }
    }
    /// <summary>workinj</summary>
    /// <value>string </value>
    public string workinj
    {
      get
      {
          return GetParameterString( "input_misc_fields", "workinj");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "workinj");
        }
        }
    /// <summary>certl</summary>
    /// <value>string </value>
    public string certl
    {
      get
      {
          return GetParameterString( "input_misc_fields", "certl");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "certl");
        }
        }
    /// <summary>inact</summary>
    /// <value>uint </value>
    public uint? inact
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "inact");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "input_misc_fields", "inact");
          }
      }
    }
    /// <summary>auxno2</summary>
    /// <value>uint </value>
    public uint? auxno2
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "auxno2");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "input_misc_fields", "auxno2");
          }
      }
    }
    /// <summary>statesp</summary>
    /// <value>string </value>
    public string statesp
    {
      get
      {
          return GetParameterString( "input_misc_fields", "statesp");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "statesp");
        }
        }
    /// <summary>sur_mo</summary>
    /// <value>uint </value>
    public uint? sur_mo
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "sur_mo");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "input_misc_fields", "sur_mo");
          }
      }
    }
    /// <summary>sur_dy</summary>
    /// <value>uint </value>
    public uint? sur_dy
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "sur_dy");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "input_misc_fields", "sur_dy");
          }
      }
    }
    /// <summary>sur_yr</summary>
    /// <value>uint </value>
    public uint? sur_yr
    {
      get
      {
          return GetParameterUnsignedInt( "input_misc_fields", "sur_yr");
      }
      set
    {

        if(value != null){
            uint u = (uint)value;
            SetParameterUnsignedInt(u, "input_misc_fields", "sur_yr");
          }
      }
    }
    /// <summary>toi_unit</summary>
    /// <value>string </value>
    public string toi_unit
    {
      get
      {
          return GetParameterString( "input_misc_fields", "toi_unit");
      }
      set
      {
            SetParameterString(value, "input_misc_fields", "toi_unit");
        }
        }
    /// <summary>manual_underlying_cause_of_death</summary>
    /// <value>string </value>
    public string manual_underlying_cause_of_death
    {
      get
      {
          return GetParameterString( "manual_underlying_cause_of_death");
      }
      set
      {
            SetParameterString(value, "manual_underlying_cause_of_death");
        }
        }
    /// <summary>acme_underlying_cause_of_death</summary>
    /// <value>string </value>
    public string acme_underlying_cause_of_death
    {
      get
      {
          return GetParameterString( "acme_underlying_cause_of_death");
      }
      set
      {
            SetParameterString(value, "acme_underlying_cause_of_death");
        }
        }
    /// <summary>record_cause_of_death</summary>
    /// <value>string </value>
    public string record_cause_of_death
    {
      get
      {
          return GetParameterString( "record_cause_of_death");
      }
      set
      {
            SetParameterString(value, "record_cause_of_death");
        }
        }
  }
}
