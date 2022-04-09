
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
    /// <summary>status</summary>
    /// <value>Coded value in Dictionary </value>
    public Dictionary<string, string> status
    {
      get
      {
          return GetParameterCoding( "status");
      }
      set
      {
            SetParameterCoding(value, "status");
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
