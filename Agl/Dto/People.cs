using System.Collections.Generic;

namespace Agl.Dto
{
    public class PeopleDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public List<PetDto> Pets { get; set; }
    }

    public class PetDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
