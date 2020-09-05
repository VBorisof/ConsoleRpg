namespace ConsoleRpg_2.GameObjects.Characters.Skills
{
    public class Skill : GameObject
    {
        public string Description { get; set; }
        public int Level { get; set; }
        public int ActionPoints { get; set; }
        public int ManaConsumption { get; set; }
        public SkillType SkillType { get; set; }
        public int BasePower { get; set; }
    }
}