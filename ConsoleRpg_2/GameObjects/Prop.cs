using System;
using ConsoleRpg_2.GameObjects.Characters;
using ConsoleRpg_2.GameObjects.Characters.Actions;

namespace ConsoleRpg_2.GameObjects
{
    public class Prop : GameObject
    {
        public string Description { get; set; }

        public Func<Character, InspectionResult> GetInspectedBy;
    }
}