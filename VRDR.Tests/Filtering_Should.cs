using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace VRDR.Tests
{
    public class Filtering_Should
    {
        
        FilterService noFieldsFilterService = new FilterService("./fixtures/filter/NoFieldsIJEFilter.json", "./fixtures/filter/IJEToFHIRMapping.json");
        FilterService allFieldsFilterService = new FilterService("./fixtures/filter/AllFieldsIJEFilter.json", "./fixtures/filter/IJEToFHIRMapping.json");
        FilterService NCHSFilterService = new FilterService("./fixtures/filter/NCHSIJEFilter.json", "./fixtures/filter/IJEToFHIRMapping.json");

        [Fact]
        public void ADDRESS_DShouldEqual()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/ADDRESS_D-test.json"));
            BaseMessage postFilteredBaseMessage;
            
            try
            {
                postFilteredBaseMessage = NCHSFilterService.filterMessage(preFilteredBaseMessage);
                BaseMessage.Parse(postFilteredBaseMessage.ToJson());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
            DeathRecord preFilteredDeathRecord = getDeathRecordFromMessage(preFilteredBaseMessage);
            DeathRecord postFilteredDeathRecord = getDeathRecordFromMessage(postFilteredBaseMessage);
            
            Assert.Equal(preFilteredDeathRecord.DeathLocationAddress, postFilteredDeathRecord.DeathLocationAddress);
        }
        
        [Fact]
        public void LIMITSShouldEqual_1()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/LIMITS-test_1.json"));
            BaseMessage postFilteredBaseMessage;
            
            try
            {
                postFilteredBaseMessage = NCHSFilterService.filterMessage(preFilteredBaseMessage);
                BaseMessage.Parse(postFilteredBaseMessage.ToJson());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
            DeathRecord preFilteredDeathRecord = getDeathRecordFromMessage(preFilteredBaseMessage);
            DeathRecord postFilteredDeathRecord = getDeathRecordFromMessage(postFilteredBaseMessage);
            
            Assert.Equal(preFilteredDeathRecord.ResidenceWithinCityLimits, postFilteredDeathRecord.ResidenceWithinCityLimits);
        }
        
        [Fact]
        public void LIMITSShouldEqual_2()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/LIMITS-test_2.json"));
            BaseMessage postFilteredBaseMessage;
            
            try
            {
                postFilteredBaseMessage = NCHSFilterService.filterMessage(preFilteredBaseMessage);
                BaseMessage.Parse(postFilteredBaseMessage.ToJson());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
            DeathRecord preFilteredDeathRecord = getDeathRecordFromMessage(preFilteredBaseMessage);
            DeathRecord postFilteredDeathRecord = getDeathRecordFromMessage(postFilteredBaseMessage);
            
            Assert.Equal(preFilteredDeathRecord.ResidenceWithinCityLimits, postFilteredDeathRecord.ResidenceWithinCityLimits);
        }
        
        [Fact]
        public void PreFilteredFileEqualsFilteredFile()
        {
            List<string> passedFields = new List<string>();
            List<string> failedFields = new List<string>();
            
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/Pre-filtered-file.json"));
            BaseMessage postFilteredBaseMessage;

            try
            {
                postFilteredBaseMessage = allFieldsFilterService.filterMessage(preFilteredBaseMessage);
                BaseMessage.Parse(postFilteredBaseMessage.ToJson());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            DeathRecord preFilteredDeathRecord = getDeathRecordFromMessage(preFilteredBaseMessage);
            DeathRecord postFilteredDeathRecord = getDeathRecordFromMessage(postFilteredBaseMessage);
            
            foreach (var property in typeof(DeathRecord).GetProperties())
            {
                // Console.WriteLine("==================================");
                object preFilteredObject = JsonConvert.SerializeObject(property.GetValue(preFilteredDeathRecord), Formatting.Indented);
                object postFilteredObject = JsonConvert.SerializeObject(property.GetValue(postFilteredDeathRecord), Formatting.Indented);

                if (!preFilteredObject.Equals(postFilteredObject))
                {
                    failedFields.Add(property.Name);
                    
                    // Console.WriteLine($"{property.Name}");
                    // Console.WriteLine(preFilteredObject);
                    // Console.WriteLine(postFilteredObject);
                }
                else
                {
                    passedFields.Add(property.Name);
                    
                    // Console.WriteLine($"{property.Name}");
                    // Console.WriteLine(preFilteredObject);
                    // Console.WriteLine(postFilteredObject);
                }
            }
            
            Console.WriteLine("=============Field filtering test=============");
            Console.WriteLine($"Passed field count = {passedFields.Count} ({JsonConvert.SerializeObject(passedFields)})");
            Console.WriteLine($"Failed field count = {failedFields.Count} ({JsonConvert.SerializeObject(failedFields)})");
        }

        [Fact]
        public void FilteringNoFields()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/Pre-filtered-file.json"));

            try
            {
                string postFilteredBaseMessage = allFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
                BaseMessage.Parse(postFilteredBaseMessage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        [Fact]
        public void FilteringPlusParsingTest_1()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/Filtering+Parsing_1.json"));

            try
            {
                string postFilteredBaseMessage = NCHSFilterService.filterMessage(preFilteredBaseMessage).ToJson();
                BaseMessage.Parse(postFilteredBaseMessage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        [Fact]
        public void FilteringPlusParsingTest_2()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/LIMITS-test_1.json"));

            try
            {
                string postFilteredBaseMessage = NCHSFilterService.filterMessage(preFilteredBaseMessage).ToJson();
                BaseMessage.Parse(postFilteredBaseMessage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        [Fact]
        public void FilteringPlusParsingTest_3()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/LIMITS-test_2.json"));

            try
            {
                string postFilteredBaseMessage = NCHSFilterService.filterMessage(preFilteredBaseMessage).ToJson();
                BaseMessage.Parse(postFilteredBaseMessage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        [Fact]
        public void FilteringPlusParsingTest_4()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/ADDRESS_D-test.json"));

            try
            {
                string postFilteredBaseMessage = NCHSFilterService.filterMessage(preFilteredBaseMessage).ToJson();
                BaseMessage.Parse(postFilteredBaseMessage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        [Fact]
        public void FilteringPlusParsingTest_5()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/Pre-filtered-file.json"));

            try
            {
                string postFilteredBaseMessage = NCHSFilterService.filterMessage(preFilteredBaseMessage).ToJson();
                BaseMessage.Parse(postFilteredBaseMessage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Fact]
        public void FilterAllFields_1()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/Pre-filtered-file.json"));

            try
            {
                string postFilteredBaseMessage = noFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
                BaseMessage.Parse(postFilteredBaseMessage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        [Fact]
        public void FilterAllFields_2()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/ADDRESS_D-test.json"));

            try
            {
                string postFilteredBaseMessage = noFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
                BaseMessage.Parse(postFilteredBaseMessage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        [Fact]
        public void FilterAllFields_3()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/LIMITS-test_1.json"));

            try
            {
                string postFilteredBaseMessage = noFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
                BaseMessage.Parse(postFilteredBaseMessage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Fact]
        public void FilterFilePerJurisdictionFilters()
        {
            List<string> passedJurisdictions = new List<string>();
            List<string> failedJurisdictions = new List<string>();

            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/Pre-filtered-file.json"));

            string jurisdictionFilterText = File.ReadAllText("./fixtures/filter/JurisdictionFieldsIJEFilter.json");
            Dictionary<string, string[]> jurisdictionFilterJSON = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(jurisdictionFilterText);

            string[] jurisdictionArray = jurisdictionFilterJSON.Keys.ToArray();
            foreach (var jurisdictionId in jurisdictionArray)
            {
                string jurisdictionFilter = JsonConvert.SerializeObject(jurisdictionFilterJSON[jurisdictionId]);
                
                FilterService jurisdictionFilterService = new FilterService(jurisdictionFilter, "./fixtures/filter/IJEToFHIRMapping.json", false);
                
                try
                {
                    string postFilteredBaseMessage = jurisdictionFilterService.filterMessage(preFilteredBaseMessage).ToJson();
                    BaseMessage.Parse(postFilteredBaseMessage);
                    passedJurisdictions.Add(jurisdictionId);
                }
                catch (Exception e)
                {
                    failedJurisdictions.Add(jurisdictionId);
                    throw new Exception(e.Message);
                }
            }
            
            Console.WriteLine("=============Jurisdiction filtering test=============");
            Console.WriteLine($"Passed jurisdiction count = {passedJurisdictions.Count} ({JsonConvert.SerializeObject(passedJurisdictions)})");
            Console.WriteLine($"Failed jurisdiction count = {failedJurisdictions.Count} ({JsonConvert.SerializeObject(failedJurisdictions)})");
        }
        
        private static DeathRecord getDeathRecordFromMessage(BaseMessage message)
        {
                
            Type messageType = message.GetType();

            // Get the original deathrecord from 
            // DeathRecordSubmissionMessage or DeathRecordUpdateMessage

            DeathRecord dr = null;
            if (messageType == typeof(DeathRecordSubmissionMessage))
            {
                var drsm = message as DeathRecordSubmissionMessage;
                dr = drsm?.DeathRecord;
            }
            else if (messageType == typeof(DeathRecordUpdateMessage))
            {
                var drum = message as DeathRecordUpdateMessage;
                dr = drum?.DeathRecord;
            }
            else
            {
                return dr;
            }

            return dr;
        }
    }
}