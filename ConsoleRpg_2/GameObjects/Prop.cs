using System;
using ConsoleRpg_2.GameObjects.Character.Actions;

namespace ConsoleRpg_2.GameObjects
{
    public class Prop : GameObject
    {
        public string Description { get; set; }

        public Func<Character.Character, InspectionResult> GetInspectedBy;
    }
}