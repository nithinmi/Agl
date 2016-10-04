using System;
using System.Collections.Generic;
using System.Linq;

namespace Agl.Logic
{
    public class People: IPeople
    {
        /// <summary>
        /// Get the pet names for a specific owner gender ordered alphabeticaly 
        /// </summary>
        /// <param name="petType">Dog, Cat etc</param>
        /// <param name="gender">Male, Female</param>
        /// <param name="lstPeople">List of people with pet detailss</param>
        /// <returns></returns>
        public string[] GetPetNamesByOwnerGender(PetType petType, Gender gender, List<Dto.PeopleDto> lstPeople)
        {
            if(lstPeople == null)
                throw new ArgumentNullException("lstPeople cannot be null");

            var groupByGender = from p in lstPeople
                group p.Pets by p.Gender
                into g
                select new {gender = g.Key, Data = g.ToList()};

            var genderGroup = groupByGender.FirstOrDefault(i => i.gender.ToLower().Equals(gender.ToString().ToLower()));
            if (genderGroup != null)
            {
                var genderData = genderGroup.Data;
                var petNames = genderData.ToList().Where(e => e != null)
                        .SelectMany(i => i.FindAll(x => x.Type.ToLower().Equals(petType.ToString().ToLower())))
                        .OrderBy(f => f.Name)
                        .Select(v => v.Name).ToArray();
                return petNames.Length == 0 ? null : petNames;
            }
            return null;
        }
    }
}
