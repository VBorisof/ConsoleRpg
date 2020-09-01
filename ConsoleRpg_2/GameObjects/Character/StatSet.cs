namespace ConsoleRpg_2.GameObjects.Character
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

        public bool IsDead => Health <= 0; 

        public int AvailableBaseSkillPoints { get; set; }

        public int Strength { get; set; }
        public int Perception { get; set; }
        public int Stamina { get; set; }
        public int Charisma { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        
        public int AvailableSkillPoints { get; set; }

        public int Speech { get; set; }
        public int Lockpick { get; set; }
        public int Meelee { get; set; }
        public int Magic { get; set; }
        public int Sneak { get; set; }
        public int Repair { get; set; }        
    }
}