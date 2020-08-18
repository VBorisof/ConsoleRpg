using System.Collections.Generic;

namespace ConsoleRpg_2
{
    public class Item : GameObject
    {
        public string Description { get; set; }
        public StatSet Effects { get; set; }
        public List<Skill> GivenSkills { get; set; }
    }
}