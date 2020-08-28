namespace ConsoleRpg_2.GameObjects.Character
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int ActionPoints { get; set; }
        public int ManaConsumption { get; set; }
        public SkillType SkillType { get; set; }
        public int BasePower { get; set; }
    }
}