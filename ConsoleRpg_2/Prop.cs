using System;

namespace ConsoleRpg_2
{
    public class Prop : GameObject
    {
        public string Description { get; set; }

        public Func<Character, InspectionResult> GetInspectedBy;
    }
}