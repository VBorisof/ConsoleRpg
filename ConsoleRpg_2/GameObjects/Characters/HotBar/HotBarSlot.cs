using System.Linq;
using ConsoleRpg_2.GameObjects.Characters.Skills;
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
            var labels = character.CurrentScene.Characters
                .Select((c, i) => new UiLabel
                {
                    Text = c.Name,
                    Row = i,
                    OnPress = (_, __) =>
                    {
                        if (GameObject is Skill skill)
                        {
                            var skillResult = character.ApplySkill(skill, c);
                            if (! skillResult.IsSuccess)
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