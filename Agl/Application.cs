using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agl.Dto;
using Agl.Logic;
using Agl.Service.Contract;
using Microsoft.Practices.Unity;

namespace Agl
{
    public class Application
    {
        private readonly IUnityContainer _unityContainer;
        public Application(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Run()
        {
            try
            {
                var aglService = _unityContainer.Resolve<IAgl>("AglService");
                var peopleDto = aglService.Get<List<PeopleDto>>("People");

                IPeople people = new People();
                var petNamesOfMaleOwners = people.GetPetNamesByOwnerGender(PetType.Cat, Gender.Male, peopleDto);
                var petNamesOfFemaleOwners = people.GetPetNamesByOwnerGender(PetType.Cat, Gender.Female, peopleDto);

                Console.WriteLine("**********PET NAMES**********");
                Console.WriteLine("Male");
                if (petNamesOfMaleOwners != null)
                {
                    foreach (var pet in petNamesOfMaleOwners)
                    {
                        Console.WriteLine(string.Format("-- {0}", pet));
                    }
                }
                Console.WriteLine("Female");
                if(petNamesOfFemaleOwners != null)
                {
                    foreach (var pet in petNamesOfFemaleOwners)
                    {
                        Console.WriteLine(string.Format("-- {0}", pet));
                    }
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception Happened - {0}", ex));
            }
            
            Console.ReadLine();
        }
    }
}
