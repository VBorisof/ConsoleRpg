namespace ConsoleRpg_2
{
    public class StatSet
    {
        public int Level { get; set; }
        public Race Race { get; set; }
        public Gender Gender { get; set; }
        
        public int Health { get; set; }
        public int Mana { get; set; }
        public int ActionPoints { get; set; }

        public int MaxHealth { get; set; }
        public int MaxMana { get; set; }
        public int MaxActionPoints { get; set; }

        public int Strength { get; set; }
        public int Perception { get; set; }
        public int Stamina { get; set; }
        public int Charisma { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
    }
}