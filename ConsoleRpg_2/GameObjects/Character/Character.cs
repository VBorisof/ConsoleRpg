using System;
using System.Collections.Generic;
using ConsoleRpg_2.Engine;
using ConsoleRpg_2.GameObjects.Character.Actions;
using ConsoleRpg_2.GameObjects.Character.Dialogues;
using ConsoleRpg_2.Helpers;

namespace ConsoleRpg_2.GameObjects.Character
{
    public class HotBar
    {
        public GameObject Slot1 { get; set; }
        public GameObject Slot2 { get; set; }
        public GameObject Slot3 { get; set; }
        public GameObject Slot4 { get; set; }
        public GameObject Slot5 { get; set; }
        public GameObject Slot6 { get; set; }
        public GameObject Slot7 { get; set; }
        public GameObject Slot8 { get; set; }
        public GameObject Slot9 { get; set; }
        public GameObject Slot10 { get; set; }
        public GameObject Slot11 { get; set; }
        public GameObject Slot12 { get; set; }

        public List<GameObject> GetSlots()
        {
            return new List<GameObject>
            {
                Slot1,
                Slot2,
                Slot3,
                Slot4,
                Slot5,
                Slot6,
                Slot7,
                Slot8,
                Slot9,
                Slot10,
                Slot11,
                Slot12,
            };
        }
    }
    
    public class Character : GameObject
    {
        public Scene CurrentScene { get; set; }
        public StatSet Stats { get; set; }
        public Inventory Inventory { get; set; }
        public List<Skill> Skills { get; set; }
        public Dialogue Dialogue { get; set; }
        public HotBar HotBar { get; set; }

        public Attitude DefaultAttitude { get; set; }

        public Dictionary<Character, Attitude> Attitudes { get; set; } = new Dictionary<Character, Attitude>();

        public string CurrentAction { get; set; }

        public Character()
        {
            var attack = new Skill
            {
                Name = "Attack",
                Description = "Basic Attack",
                SkillType = SkillType.Meelee,
                Level = 1,
                BasePower = 1,
                ActionPoints = 2,
                ManaConsumption = 0
            }; 
            
            Skills = new List<Skill>
            {
                attack  
            };

            HotBar = new HotBar
            {
                Slot1 = attack
            };
        }
        
        
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

        public SkillResult ApplySkill(Skill skill, Character target)
        {
            if (Stats.Mana < skill.ManaConsumption)
            {
                return new SkillResult
                {
                    IsSuccess = false,
                    Comment = "Not enough mana."
                };
            }

            var result = new SkillResult
            {
                IsSuccess = true,
            };
            switch (skill.SkillType)
            {
                case SkillType.Meelee:
                    result.Damage = skill.BasePower * (Stats.Strength / 2);
                    break;
                case SkillType.Heal:
                    result.Damage = - (skill.BasePower * (Stats.Intelligence / 2));
                    break;
                case SkillType.Projectile:
                    result.Damage = skill.BasePower * (Stats.Intelligence);
                    break;
                case SkillType.AreaOfEffect:
                    result.Damage = skill.BasePower * (Stats.Intelligence / 3);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Stats.Mana -= skill.ManaConsumption;

            return result;
        }
    }
}