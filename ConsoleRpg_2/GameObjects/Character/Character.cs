using System;
using System.Collections.Generic;
using ConsoleRpg_2.GameObjects.Character.Actions;
using ConsoleRpg_2.Helpers;

namespace ConsoleRpg_2.GameObjects.Character
{
    public class Character : GameObject
    {
        public StatSet Stats { get; set; }
        public Inventory Inventory { get; set; }
        public List<Skill> Skills { get; set; }

        public string CurrentAction { get; set; }

        public InspectionResult Inspect(GameObject o)
        {
            switch (o)
            {
                case Item item:
                    return Inspect(item);
                case Prop prop:
                    return Inspect(prop);
                //case Decoration decoration:
                //    break;
                case Character character:
                    return Inspect(character);
                default:
                    throw new ArgumentException("Invalid object for inspection.");
            }
        }
        
        public InspectionResult Inspect(Item item)
        {
            return new InspectionResult
            {
                Response = $"{item.Name}\n{item.Description}",
                GuessedStats = item.Effects
            };
        }
        
        public InspectionResult Inspect(Prop prop)
        {
            return prop.GetInspectedBy(this);
        }
        
        public InspectionResult Inspect(Character character)
        {
            var result = new InspectionResult
            {
                GuessedStats = RandomsHelper.GuessStats(character.Stats, Stats.Perception)
            };

            result.Response =
                $"You see a {LexicalHelper.GetStregthEpithet(result.GuessedStats.Strength)} " +
                $"{LexicalHelper.GetDescriptionString(result.GuessedStats.Race, result.GuessedStats.Gender)}.";

            if (Stats.Perception > 3)
            {
                result.Response += $"\n{LexicalHelper.GenderPronoun(character.Stats.Gender)} seems to be doing {character.CurrentAction}";
            }
            
            return result;
        }
    }
}