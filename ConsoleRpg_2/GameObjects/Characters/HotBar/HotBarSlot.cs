using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConsoleRpg_2.GameObjects.Characters.Skills;
using ConsoleRpg_2.Helpers;
using ConsoleRpg_2.Ui;

namespace ConsoleRpg_2.GameObjects.Characters.HotBar
{
    public class HotBarSlot
    {
        private readonly GameLog _gameLog;
        public GameObject GameObject { get; set; }

        public bool IsOccupied => GameObject != null;
        public string Name => IsOccupied ? GameObject.Name : "[EMPTY]";
        
        private UiSelectList _hotBarUseList;

        public HotBarSlot(GameLog gameLog)
        {
            _gameLog = gameLog;
        }
        
        
        public void Render()
        {
            _hotBarUseList.Render();
        }

        public void PrevItem()
        {
            _hotBarUseList.PrevItem();
        }
        public void NextItem()
        {
            _hotBarUseList.NextItem();
        }
        public void PressCurrentItem()
        {
            _hotBarUseList.PressCurrentItem();
        }
        
        public void UpdateCharacterSelectList(Character character)
        {
            IEnumerable<Character> applicableCharacters = character.CurrentScene.Characters;

            if (GameObject is Skill skill)
            {
                switch (skill.SkillType)
                {
                    case SkillType.Melee:
                        applicableCharacters = applicableCharacters.Except(new[] {character});
                        break;
                    case SkillType.Heal:
                        break;
                    case SkillType.Projectile:
                        applicableCharacters = applicableCharacters.Except(new[] {character});
                        break;
                    case SkillType.AreaOfEffect:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            var labels = applicableCharacters
                .Select((c, i) =>
                {
                    var text = c.Name;

                    var verbosity = LexicalVerbosity.Poor;
                    
                    if (character.Stats.Perception >= 4)
                    {
                        verbosity = LexicalVerbosity.Normal;
                    }
                    else if (character.Stats.Perception >= 6)
                    {
                        verbosity = LexicalVerbosity.High;
                    }
                    else if (character.Stats.Perception >= 8)
                    {
                        verbosity = LexicalVerbosity.VeryHigh;
                    }

                    if (character.Stats.Perception >= 2)
                    {
                        text += $" ({LexicalHelper.GetHealthString(c.Stats.Health, c.Stats.MaxHealth, verbosity)})";
                    }
                    
                    return new UiLabel
                    {
                        Text = text,
                        Row = i,
                        OnPress = (_, __) =>
                        {
                            if (GameObject is Skill appliedSkill)
                            {
                                var skillResult = character.ApplySkill(appliedSkill, c);
                                if (!skillResult.IsSuccess)
                                {
                                    _gameLog.WriteLine(skillResult.Comment);
                                }
                                else
                                {
                                    if (skillResult.Damage >= 0)
                                    {
                                        _gameLog.WriteLine(
                                            $"You deal {skillResult.Damage} damage to {c.Name}."
                                        );
                                    }
                                    else
                                    {
                                        _gameLog.WriteLine(
                                            $"You heal {c.Name} for {-skillResult.Damage} health points."
                                        );
                                    }
                                }
                            }
                        }
                    };
                })
                .ToList();
    
            var title = $"Use {Name} on...";
    
            if (GameObject is Skill)
            {
                title = $"Apply {Name} on...";
            }
            
            _hotBarUseList = new UiSelectList(labels)
            {
                Title = title
            };
        }
    }
}