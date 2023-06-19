using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace VRDR.Tests
{
    public class Filtering_Should
    {
        private readonly ITestOutputHelper _testOutputHelper;

        FilterService noFieldsFilterService = new FilterService("./fixtures/filter/NoFieldsIJEFilter.json", "./fixtures/filter/IJEToFHIRMapping.json");
        FilterService allFieldsFilterService = new FilterService("./fixtures/filter/AllFieldsIJEFilter.json", "./fixtures/filter/IJEToFHIRMapping.json");
        FilterService NCHSFilterService = new FilterService("./fixtures/filter/NCHSIJEFilter.json", "./fixtures/filter/IJEToFHIRMapping.json");

        public Filtering_Should(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ADDRESS_DShouldEqual()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/ADDRESS_D-test.json"));
            BaseMessage postFilteredBaseMessage;
            
            _testOutputHelper.WriteLine(preFilteredBaseMessage.GetType().Name);
            _testOutputHelper.WriteLine("DeathRecordSubmissionMessage");
            _testOutputHelper.WriteLine(preFilteredBaseMessage.GetType().Name.Equals("DeathRecordSubmissionMessage").ToString());
            
            try
            {
                postFilteredBaseMessage = NCHSFilterService.filterMessage(preFilteredBaseMessage);
                BaseMessage.Parse(postFilteredBaseMessage.ToJson());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
            DeathRecord preFilteredDeathRecord = BaseMessage.GetDeathRecordFromMessage(preFilteredBaseMessage);
            DeathRecord postFilteredDeathRecord = BaseMessage.GetDeathRecordFromMessage(postFilteredBaseMessage);
            
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
            
            DeathRecord preFilteredDeathRecord = BaseMessage.GetDeathRecordFromMessage(preFilteredBaseMessage);
            DeathRecord postFilteredDeathRecord = BaseMessage.GetDeathRecordFromMessage(postFilteredBaseMessage);
            
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
            
            DeathRecord preFilteredDeathRecord = BaseMessage.GetDeathRecordFromMessage(preFilteredBaseMessage);
            DeathRecord postFilteredDeathRecord = BaseMessage.GetDeathRecordFromMessage(postFilteredBaseMessage);
            
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

            DeathRecord preFilteredDeathRecord = BaseMessage.GetDeathRecordFromMessage(preFilteredBaseMessage);
            DeathRecord postFilteredDeathRecord = BaseMessage.GetDeathRecordFromMessage(postFilteredBaseMessage);
            
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

            _testOutputHelper.WriteLine("=============Field filtering test=============");
            _testOutputHelper.WriteLine($"Passed field count = {passedFields.Count} ({JsonConvert.SerializeObject(passedFields)})");
            _testOutputHelper.WriteLine($"Failed field count = {failedFields.Count} ({JsonConvert.SerializeObject(failedFields)})");
            
            Assert.Empty(failedFields);
        }

        [Fact]
        public void FilteringNoFields()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/Pre-filtered-file.json"));
            
            string postFilteredBaseMessage = allFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
            DeathRecordSubmissionMessage parsedBaseMessage = BaseMessage.Parse<DeathRecordSubmissionMessage>(postFilteredBaseMessage);
            
            Assert.Null(parsedBaseMessage.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", parsedBaseMessage.MessageType);
            Assert.Equal("2023NV000026", parsedBaseMessage.NCHSIdentifier);
            Assert.Equal((uint)000026, parsedBaseMessage.CertNo);
            Assert.Equal((uint)2023, parsedBaseMessage.DeathYear);
            Assert.Null(parsedBaseMessage.StateAuxiliaryId);
            Assert.Equal("Y", parsedBaseMessage.DeathRecord.EmergingIssue1_1);
        }
        
        [Fact]
        public void FilteringPlusParsingTest_1()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/Filtering+Parsing_1.json"));

            string postFilteredBaseMessage = allFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
            DeathRecordSubmissionMessage parsedBaseMessage = BaseMessage.Parse<DeathRecordSubmissionMessage>(postFilteredBaseMessage);
            
            Assert.Equal("2022-09-23T09:09:00", parsedBaseMessage.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", parsedBaseMessage.MessageType);
            Assert.Equal("2022KS000230", parsedBaseMessage.NCHSIdentifier);
            Assert.Equal((uint)000230, parsedBaseMessage.CertNo);
            Assert.Equal((uint)2022, parsedBaseMessage.DeathYear);
            Assert.Equal("202204000230", parsedBaseMessage.StateAuxiliaryId);
            Assert.Null(parsedBaseMessage.DeathRecord.EmergingIssue1_1);
        }
        
        [Fact]
        public void FilteringPlusParsingTest_2()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/LIMITS-test_1.json"));

            string postFilteredBaseMessage = allFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
            DeathRecordSubmissionMessage parsedBaseMessage = BaseMessage.Parse<DeathRecordSubmissionMessage>(postFilteredBaseMessage);
            
            Assert.Null(parsedBaseMessage.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", parsedBaseMessage.MessageType);
            Assert.Equal("2023NV000026", parsedBaseMessage.NCHSIdentifier);
            Assert.Equal((uint)000026, parsedBaseMessage.CertNo);
            Assert.Equal((uint)2023, parsedBaseMessage.DeathYear);
            Assert.Null(parsedBaseMessage.StateAuxiliaryId);
            Assert.Equal("Y", parsedBaseMessage.DeathRecord.EmergingIssue1_1);
        }
        
        [Fact]
        public void FilteringPlusParsingTest_3()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/LIMITS-test_2.json"));

            string postFilteredBaseMessage = allFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
            DeathRecordSubmissionMessage parsedBaseMessage = BaseMessage.Parse<DeathRecordSubmissionMessage>(postFilteredBaseMessage);
            
            Assert.Null(parsedBaseMessage.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", parsedBaseMessage.MessageType);
            Assert.Equal("2023NV000031", parsedBaseMessage.NCHSIdentifier);
            Assert.Equal((uint)000031, parsedBaseMessage.CertNo);
            Assert.Equal((uint)2023, parsedBaseMessage.DeathYear);
            Assert.Null(parsedBaseMessage.StateAuxiliaryId);
            Assert.Null(parsedBaseMessage.DeathRecord.EmergingIssue1_1);
        }
        
        [Fact]
        public void FilteringPlusParsingTest_4()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/ADDRESS_D-test.json"));

            string postFilteredBaseMessage = allFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
            DeathRecordSubmissionMessage parsedBaseMessage = BaseMessage.Parse<DeathRecordSubmissionMessage>(postFilteredBaseMessage);
            
            Assert.Null(parsedBaseMessage.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", parsedBaseMessage.MessageType);
            Assert.Equal("2022TS000532", parsedBaseMessage.NCHSIdentifier);
            Assert.Equal((uint)000532, parsedBaseMessage.CertNo);
            Assert.Equal((uint)2022, parsedBaseMessage.DeathYear);
            Assert.Null(parsedBaseMessage.StateAuxiliaryId);
            Assert.Equal("Y", parsedBaseMessage.DeathRecord.EmergingIssue1_1);
        }
        
        [Fact]
        public void FilteringPlusParsingTest_5()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/Pre-filtered-file.json"));

            string postFilteredBaseMessage = allFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
            DeathRecordSubmissionMessage parsedBaseMessage = BaseMessage.Parse<DeathRecordSubmissionMessage>(postFilteredBaseMessage);
            
            Assert.Null(parsedBaseMessage.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", parsedBaseMessage.MessageType);
            Assert.Equal("2023NV000026", parsedBaseMessage.NCHSIdentifier);
            Assert.Equal((uint)000026, parsedBaseMessage.CertNo);
            Assert.Equal((uint)2023, parsedBaseMessage.DeathYear);
            Assert.Null(parsedBaseMessage.StateAuxiliaryId);
            Assert.Equal("Y", parsedBaseMessage.DeathRecord.EmergingIssue1_1);
        }

        [Fact]
        public void FilterAllFields_1()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/Pre-filtered-file.json"));

            string postFilteredBaseMessage = noFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
            DeathRecordSubmissionMessage parsedBaseMessage = BaseMessage.Parse<DeathRecordSubmissionMessage>(postFilteredBaseMessage);
            
            Assert.Null(parsedBaseMessage.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", parsedBaseMessage.MessageType);
            Assert.Equal("2023NV000026", parsedBaseMessage.NCHSIdentifier);
            Assert.Equal((uint)000026, parsedBaseMessage.CertNo);
            Assert.Equal((uint)2023, parsedBaseMessage.DeathYear);
            Assert.Null(parsedBaseMessage.StateAuxiliaryId);
            Assert.Null(parsedBaseMessage.DeathRecord.EmergingIssue1_1);
        }
        
        [Fact]
        public void FilterAllFields_2()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/ADDRESS_D-test.json"));

            string postFilteredBaseMessage = noFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
            DeathRecordSubmissionMessage parsedBaseMessage = BaseMessage.Parse<DeathRecordSubmissionMessage>(postFilteredBaseMessage);
            
            Assert.Null(parsedBaseMessage.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", parsedBaseMessage.MessageType);
            Assert.Equal("2022TS000532", parsedBaseMessage.NCHSIdentifier);
            Assert.Equal((uint)000532, parsedBaseMessage.CertNo);
            Assert.Equal((uint)2022, parsedBaseMessage.DeathYear);
            Assert.Null(parsedBaseMessage.StateAuxiliaryId);
            Assert.Null(parsedBaseMessage.DeathRecord.EmergingIssue1_1);
        }
        
        [Fact]
        public void FilterAllFields_3()
        {
            BaseMessage preFilteredBaseMessage = BaseMessage.Parse(File.ReadAllText("./fixtures/filter/LIMITS-test_1.json"));

            string postFilteredBaseMessage = noFieldsFilterService.filterMessage(preFilteredBaseMessage).ToJson();
            DeathRecordSubmissionMessage parsedBaseMessage = BaseMessage.Parse<DeathRecordSubmissionMessage>(postFilteredBaseMessage);
            
            Assert.Null(parsedBaseMessage.DeathRecord.DateOfDeathPronouncement);
            Assert.Equal("http://nchs.cdc.gov/vrdr_submission", parsedBaseMessage.MessageType);
            Assert.Equal("2023NV000026", parsedBaseMessage.NCHSIdentifier);
            Assert.Equal((uint)000026, parsedBaseMessage.CertNo);
            Assert.Equal((uint)2023, parsedBaseMessage.DeathYear);
            Assert.Null(parsedBaseMessage.StateAuxiliaryId);
            Assert.Null(parsedBaseMessage.DeathRecord.EmergingIssue1_1);
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
            
            _testOutputHelper.WriteLine("=============Jurisdiction filtering test=============");
            _testOutputHelper.WriteLine($"Passed jurisdiction count = {passedJurisdictions.Count} ({JsonConvert.SerializeObject(passedJurisdictions)})");
            _testOutputHelper.WriteLine($"Failed jurisdiction count = {failedJurisdictions.Count} ({JsonConvert.SerializeObject(failedJurisdictions)})");
            
            Assert.Empty(failedJurisdictions);
        }
    }
}