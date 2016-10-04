using System.Collections.Generic;

namespace Agl.Logic
{
    public enum PetType
    {
        Cat,
        Dog
    }
    public enum Gender
    {
        Male,
        Female
    }
    public interface IPeople
    {
        string[] GetPetNamesByOwnerGender(PetType petType, Gender gender, List<Dto.PeopleDto> lstPeople);
    }
}
