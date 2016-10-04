using System;
using System.Collections.Generic;
using Agl.Dto;
using Agl.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AglUnitTest
{
    [TestClass]
    public class UnitTestPeopleLogic
    {
        private static List<PeopleDto> payloadWithNoCats;
        private static List<PeopleDto> payloadWithNoPets;
        private static List<PeopleDto> payloadWithOnlyCats;
        private static List<PeopleDto> payloadWithCatsAndDogs;

        private static IPeople people;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            people = new People();

            payloadWithNoPets = new List<PeopleDto>()
            {
                new PeopleDto()
                {
                    Name = "One",
                    Gender = "Male",
                    Pets = null
                }
            };

            payloadWithNoCats = new List<PeopleDto>()
            {
                new PeopleDto()
                {
                    Name = "One",
                    Gender = "Male",
                    Pets = new List<PetDto>() {new PetDto() {Name = "Name1", Type = "Dog"}}
                }
            };

            payloadWithOnlyCats = new List<PeopleDto>()
            {
                new PeopleDto()
                {
                    Name = "One",
                    Gender = "Male",
                    Pets = new List<PetDto>() {
                        new PetDto() {Name = "abc", Type = "Cat"},
                        new PetDto() { Name = "def", Type = "Cat" }
                    }
                }
            };

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


            //_etMock = new Mock<IExactTarget>();
            //_configMock = new Mock<IConfigManager>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPetNamesByOwnerGender_WithNullListOfPeople()
        {
            var petNames = people.GetPetNamesByOwnerGender(PetType.Cat, Gender.Male, null);
        }

        [TestMethod]
        public void GetPetNamesByOwnerGender_WithNoPetsPayload()
        {
            var petNames = people.GetPetNamesByOwnerGender(PetType.Cat, Gender.Male, payloadWithNoPets);
            Assert.IsNull(petNames);
        }

        [TestMethod]
        public void GetPetNamesByOwnerGender_RequestingForPetsNotInPayload()
        {
            var petNames = people.GetPetNamesByOwnerGender(PetType.Dog, Gender.Male, payloadWithOnlyCats);
            Assert.IsNull(petNames);
        }

        [TestMethod]
        public void GetPetNamesByOwnerGender_RequestingWithWrongGender()
        {
            var petNames = people.GetPetNamesByOwnerGender(PetType.Cat, Gender.Female, payloadWithOnlyCats);
            Assert.IsNull(petNames);
        }

        [TestMethod]
        public void GetPetNamesByOwnerGender_ProperPayLoad()
        {
            var petNames = people.GetPetNamesByOwnerGender(PetType.Cat, Gender.Female, payloadWithCatsAndDogs);
            Assert.AreEqual(petNames.Length, 1);
            Assert.AreEqual(petNames[0], "def");

            petNames = people.GetPetNamesByOwnerGender(PetType.Cat, Gender.Male, payloadWithCatsAndDogs);
            Assert.AreEqual(petNames.Length, 3);
            Assert.AreEqual(petNames[0], "abc");
            Assert.AreEqual(petNames[1], "ghi");
            Assert.AreEqual(petNames[2], "jkl");
        }
    }
}
