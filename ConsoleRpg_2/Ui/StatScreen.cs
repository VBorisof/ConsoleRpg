using System;
using System.Collections.Generic;
using ConsoleRpg_2.Engine;
using ConsoleRpg_2.Extensions;
using ConsoleRpg_2.GameObjects.Character;

namespace ConsoleRpg_2.Ui
{
    public class StatScreen
    {
        private readonly Character _character;
        private readonly UiSelectList _selectList;

        private readonly UiValue _strength    ;
        private readonly UiValue _perception  ;
        private readonly UiValue _stamina     ;
        private readonly UiValue _charisma    ;
        private readonly UiValue _intelligence;
        private readonly UiValue _agility     ;
        
        public StatScreen(Character character)
        {
            _character = character;
            
            var elems = new List<UiElement>
            {
                new UiLabel
                {
                    Row = 1,
                    Column = 1,
                    Text = "Strength     : "

                },
                new UiLabel
                {
                    Row = 2,
                    Column = 1,
                    Text = "Perception   : "

                },
                new UiLabel
                {
                    Row = 3,
                    Column = 1,
                    Text = "Stamina      : "

                },
                new UiLabel
                {
                    Row = 4,
                    Column = 1,
                    Text = "Charisma     : "
                },
                new UiLabel
                {
                    Row = 5,
                    Column = 1,
                    Text = "Intelligence : "
                },
                new UiLabel
                {
                    Row = 6,
                    Column = 1,
                    Text = "Agility      : "
                }
            };
            
            _strength     = new UiValue
            {
                Row = 1,
                Column = 2,
                Text = _character.Stats.Strength.ToString()
            };
            _perception   = new UiValue
            {
                Row = 2,
                Column = 2,
                Text = _character.Stats.Perception.ToString()
            };
            _stamina      = new UiValue
            {
                Row = 3,
                Column = 2,
                Text = _character.Stats.Stamina.ToString()
            };
            _charisma     = new UiValue
            {
                Row = 4,
                Column = 2,
                Text = _character.Stats.Charisma.ToString()
            };
            _intelligence = new UiValue
            {
                Row = 5,
                Column = 2,
                Text = _character.Stats.Intelligence.ToString()
            };
            _agility      = new UiValue
            {
                Row = 6,
                Column = 2,
                Text = _character.Stats.Agility.ToString()
            };
            
            elems.Add(_strength);    
            elems.Add(_perception);  
            elems.Add(_stamina);     
            elems.Add(_charisma);    
            elems.Add(_intelligence);
            elems.Add(_agility);
            
            _selectList = new UiSelectList(elems);
        }

        public void NextItem()
        {
            _selectList.NextItem();
        }
        
        public void PrevItem()
        {
            _selectList.PrevItem();
        }

        public void AdjustBaseSkillValue(int amount)
        {
            if (_character.Stats.AvailableBaseSkillPoints < amount)
            {
                return;
            }
            
            var uiValue = _selectList.GetCurrentUiValueOrDefault();
            if (uiValue == _strength)
            {
                if (amount < 0 && _character.Stats.Strength <= 0)
                {
                    return;
                }
                _character.Stats.Strength += amount;
            }
            else if (uiValue == _perception)
            {
                if (amount < 0 && _character.Stats.Perception <= 0)
                {
                    return;
                }
                _character.Stats.Perception += amount;
            }
            else if (uiValue == _stamina)
            {
                if (amount < 0 && _character.Stats.Stamina  <= 0)
                {
                    return;
                }
                _character.Stats.Stamina += amount;
            }
            else if (uiValue == _charisma)
            {
                if (amount < 0 && _character.Stats.Charisma <= 0)
                {
                    return;
                }
                _character.Stats.Charisma += amount;
            }
            else if (uiValue == _intelligence)
            {
                if (amount < 0 && _character.Stats.Intelligence <= 0)
                {
                    return;
                }
                _character.Stats.Intelligence += amount;
            }
            else if (uiValue == _agility)
            {
                if (amount < 0 && _character.Stats.Agility <= 0)
                {
                    return;
                }
                _character.Stats.Agility += amount;
            }
            else
            {
                return;
            }

            _character.Stats.AvailableBaseSkillPoints -= amount;
        }
        
        public void Update()
        {
            _strength.Text = _character.Stats.Strength.ToString();
            _perception.Text = _character.Stats.Perception.ToString();
            _stamina.Text = _character.Stats.Stamina.ToString();
            _charisma.Text = _character.Stats.Charisma.ToString();
            _intelligence.Text = _character.Stats.Intelligence.ToString();
            _agility.Text = _character.Stats.Agility.ToString();
        }

        public void Render()
        {
            Console.Clear();
                    
            ConsoleEx.WriteLine($"Press `?` for help.", ConsoleColor.DarkGray);
            ConsoleEx.WriteLine($"== Stats =====================================", ConsoleColor.Green);                    

            Console.WriteLine();
            
            Console.WriteLine($"{_character.Name} -- Level {_character.Stats.Level}");
            Console.WriteLine($"{_character.Stats.Race} {_character.Stats.Gender}");
            
            Console.WriteLine();
            
            ConsoleEx.Write($"HP : ", ConsoleColor.Gray); ConsoleEx.WriteLine($"{_character.Stats.Health}/{_character.Stats.MaxHealth}", ConsoleColor.White);
            ConsoleEx.Write($"MP : ", ConsoleColor.Gray); ConsoleEx.WriteLine($"{_character.Stats.Mana}/{_character.Stats.MaxMana}", ConsoleColor.White);
            ConsoleEx.Write($"AP : ", ConsoleColor.Gray); ConsoleEx.WriteLine($"{_character.Stats.ActionPoints}/{_character.Stats.MaxActionPoints}", ConsoleColor.White);

            Console.WriteLine();

            if (_character.Stats.AvailableBaseSkillPoints > 0)
            {
                Console.WriteLine($"Available Points : {_character.Stats.AvailableBaseSkillPoints}\n");
            }
            
            _selectList.Render();
            
            Console.WriteLine();
            
            Console.WriteLine($"______________________________________________", ConsoleColor.Green);
        }

        public ScreenInputProcessResult ProcessInput(ConsoleKey key)
        {
            var result = new ScreenInputProcessResult();
            
            if (key == ConsoleKey.Q)
            {
                result.SwitchState = GameState.Playing;
                result.RefreshFlag = true;
            }

            if (key == ConsoleKey.UpArrow)
            {
                PrevItem();
                result.RefreshFlag = true;
            }
            if (key == ConsoleKey.DownArrow)
            {
                NextItem();
                result.RefreshFlag = true;
            }
            if (key == ConsoleKey.LeftArrow)
            {
                AdjustBaseSkillValue(-1);
                result.RefreshFlag = true;
            }
            if (key == ConsoleKey.RightArrow)
            {
                AdjustBaseSkillValue(1);
                result.RefreshFlag = true;
            }

            return result;
        }
    }
}