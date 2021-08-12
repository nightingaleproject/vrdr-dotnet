using System;

namespace VRDR
{
    /// <summary>Helper class providing more descriptive definitions for each PlaceCode field</summary>
    public class PlaceCode
    {
        /// <summary>Two letter state abbreviation</summary>
        public String State { get; }
        /// <summary>Unabbreviated county name</summary>
        public String County { get; }
        /// <summary>Three digit county code</summary>
        public String CountyCode { get; }
        /// <summary>Unabbreviated city name</summary>
        public String City { get; }
        /// <summary>Description, normally either blank or "City of", "Township of", etc</summary>
        public String Description { get; }
        /// <summary>The representative PlaceCode corresponding to all other fields</summary>
        public String Code { get; }
        /// <summary>The empty constructor, used by the Default case when performing LINQ queries and there is no match</summary>
        public PlaceCode() { }
        /// <summary>The complete constructor, normally used when declaring a PlaceCode</summary>
        public PlaceCode(String state, String county, String countycode, String city, String description, String code)
        {
            this.State = state;
            this.County = county;
            this.CountyCode = countycode;
            this.City = city;
            this.Description = description;
            this.Code = code;
        }
    }
}
