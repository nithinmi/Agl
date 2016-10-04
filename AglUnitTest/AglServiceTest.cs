using System.Collections.Generic;
using System.Net;
using Agl.Common;
using Agl.Dto;
using Agl.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;

namespace AglUnitTest
{
    [TestClass]
    public class AglServiceTest
    {
        private static Mock<IRestClient> _restClientMock;
        private static Mock<IAppConfig> _appConfigMock;
        private static List<PeopleDto> payloadWithCatsAndDogs;
        
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            _restClientMock = new Mock<IRestClient>();
            _appConfigMock = new Mock<IAppConfig>();

            payloadWithCatsAndDogs = new List<PeopleDto>()
            {
                new PeopleDto()
                {
                    Name = "One",
                    Gender = "Male",
                    Pets = new List<PetDto>() {
                        new PetDto() {Name = "abc", Type = "Cat"},
                        new PetDto() { Name = "def", Type = "Dog" }
                    }
                },
                new PeopleDto()
                {
                    Name = "Two",
                    Gender = "Female",
                    Pets = new List<PetDto>() {
                        new PetDto() {Name = "abc", Type = "Dog"},
                        new PetDto() { Name = "def", Type = "Cat" }
                    }
                },
                new PeopleDto()
                {
                    Name = "Three",
                    Gender = "Male",
                    Pets = new List<PetDto>() {
                        new PetDto() {Name = "ghi", Type = "Cat"},
                        new PetDto() { Name = "jkl", Type = "Cat" }
                    }
                }
            };
        }

        [TestMethod]
        public void AglService_Request()
        {
            _appConfigMock.Setup(i => i.AglApiUrl).Returns("http://agl-developer-test.azurewebsites.netyyyyyy");
            _restClientMock.Setup(i => i.Execute(It.IsAny<RestRequest>()).StatusCode).Returns(HttpStatusCode.OK);
            _restClientMock.Setup(i => i.Execute(It.IsAny<RestRequest>()).Content).Returns(JsonConvert.SerializeObject(payloadWithCatsAndDogs));

            AglService agl = new AglService(_appConfigMock.Object, _restClientMock.Object);
            var response = agl.Get<List<PeopleDto>>("Test");

            Assert.AreEqual(3, response.Count);
        }

        [TestMethod]
        public void AglService_RequestWithInvalidAddress()
        {
            _appConfigMock.Setup(i => i.AglApiUrl).Returns("http://agl-developer-test.azurewebsites.netyyyyyy");

            AglService agl = new AglService(_appConfigMock.Object, new RestClient());
            var response = agl.Get<List<PeopleDto>>("People");

            Assert.IsNull(response);
        }

        [TestMethod]
        public void AglService_RequestWithInvalidResource()
        {
            _appConfigMock.Setup(i => i.AglApiUrl).Returns("http://agl-developer-test.azurewebsites.net");
            //_restClientMock.Setup(i => i.Execute(It.IsAny<RestRequest>()).Content).Returns(JsonConvert.SerializeObject(payloadWithCatsAndDogs));

            AglService agl = new AglService(_appConfigMock.Object, new RestClient());
            var response = agl.Get<List<PeopleDto>>("Test");

            Assert.IsNull(response);
        }
    }
}
