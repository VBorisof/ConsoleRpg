using System;
using System.Collections.Generic;
using ConsoleRpg_2.Engine;
using ConsoleRpg_2.GameObjects.Character.Actions;
using ConsoleRpg_2.GameObjects.Character.Dialogues;
using ConsoleRpg_2.Helpers;

namespace ConsoleRpg_2.GameObjects.Character
{
    public class Character : GameObject
    {
        public Scene CurrentScene { get; set; }
        public StatSet Stats { get; set; }
        public Inventory Inventory { get; set; }
        public List<Skill> Skills { get; set; }
        public Dialogue Dialogue { get; set; }

        public Attitude DefaultAttitude { get; set; }

        public Dictionary<Character, Attitude> Attitudes { get; set; } = new Dictionary<Character, Attitude>();

        public string CurrentAction { get; set; }

        
        public string AnalyzeScene()
        {
            var descriptions = CurrentScene.GetSceneDescriptions();
            string result = "";

            if (Stats.Perception > 1)
            {
                result += $"{descriptions.GeneralDescription}";
            }

            if (Stats.Perception > 3)
            {
                result += $"{descriptions.CharacterDescription}";
            }

            if (Stats.Perception >= 5)
            {
                result += $"\n{descriptions.PropDescription}";
            }

            if (Stats.Perception >= 8)
            {
                var attitude = CurrentScene.GetAverageCharacterAttitude(this);
                switch (attitude)
                {
                    case Attitude.Friendly:
                        result += "\nYou feel extremely welcomed.";
                        break;
                    case Attitude.Inclined:
                        result += "\nYou feel welcomed here.";
                        break;
                    case Attitude.Neutral:
                        result += "\nYou feel folks are okay with you here.";
                        break;
                    case Attitude.Agitated:
                        result += "\nYou feel uneasy here.";
                        break;
                    case Attitude.Hostile:
                        result += "\nThe fight is imminent.";
                        break;
                }
            }

            return result;
        }
        
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
                result.Response += $"\n{LexicalHelper.GenderPronoun(character.Stats.Gender)} seems to be {character.CurrentAction}.";
            }
            
            return result;
        }
    }
}