using System.Collections.Generic;
using ConsoleRpg_2.GameObjects.Characters;
using ConsoleRpg_2.GameObjects.Characters.Skills;

namespace ConsoleRpg_2.GameObjects
{
    public class Item : GameObject
    {
        public string Description { get; set; }
        public StatSet Effects { get; set; }
        public List<Skill> GivenSkills { get; set; }
    }
}