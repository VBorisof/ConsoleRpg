﻿namespace ConsoleRpg_2.GameObjects.Characters.Skills
{
    public class SkillResult
    {
        public bool IsSuccess { get; set; }
        public string Comment { get; set; }
        public bool IsMissed { get; set; }
        public int Damage { get; set; }
    }
}