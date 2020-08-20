using System;
using System.Collections.Generic;
using ConsoleRpg_2.GameObjects;
using ConsoleRpg_2.GameObjects.Character;
using ConsoleRpg_2.Ui;

namespace ConsoleRpg_2.Engine
{
    public class Engine
    {
        private readonly StatScreen _statScreen;
        private Character _currentCharacter;
        private GameState _currentState;
        private GameScreen _gameScreen;

        public Engine()
        {
            var orc = new Character
            {
                Name = "Ogrem",
                CurrentAction = "Doing nothing",
                DefaultAttitude = Attitude.Neutral,
                Inventory = new Inventory
                {
                    Items = new List<Item>
                    {
                        new Item
                        {
                            Name = "Ring of Eternity",
                            Description = "Gives lotsa health",
                            Effects = new StatSet
                            {
                                MaxHealth = 200
                            }
                        }
                    }
                },
                Stats = new StatSet
                {
                    Level = 1,
                    Race = Race.Orc,
                    Gender = Gender.Male,
                    Health = 400,
                    Mana = 50,
                    ActionPoints = 6,
                    MaxHealth = 400,
                    MaxMana = 50,
                    MaxActionPoints = 6,
                    Strength = 8,
                    Perception = 2,
                    Stamina = 6,
                    Charisma = 3,
                    Intelligence = 1,
                    Agility = 5,
                }
            };
            
            var player = new Character
            {
                Name = "Player",
                CurrentAction = "Doing nothing",
                DefaultAttitude = Attitude.Neutral,
                Inventory = new Inventory
                {
                    Items = new List<Item>
                    {
                    }
                },
                Stats = new StatSet
                {
                    Level = 1,
                    Race = Race.Human,
                    Gender = Gender.Male,
                    Health = 150,
                    Mana = 50,
                    ActionPoints = 6,
                    MaxHealth = 150,
                    MaxMana = 50,
                    MaxActionPoints = 6,
                    AvailableBaseSkillPoints = 50,
                    Strength = 5,
                    Perception = 5,
                    Stamina = 5,
                    Charisma = 5,
                    Intelligence = 5,
                    Agility = 5,
                }
            };

            _currentCharacter = player;
            
            var scene = new Scene
            {
                Name = "The Room",
                Description = "Just a silly room. Four walls and a surprising absence of doors or windows.",
                Characters = new List<Character>()
                {
                    orc, player
                },
                Items = new List<Item>(),
                Props = new List<Prop>(),
                Decorations = new List<Decoration>()
            };

            scene.InitializeScene();
            _currentCharacter.CurrentScene = scene;
            
            _currentState = GameState.Playing;
            
            _gameScreen = new GameScreen(_currentCharacter);
            _statScreen = new StatScreen(_currentCharacter);
        }
        

        private void HelpScreen()
        {
            Console.Clear();
        }
        
        public void Run()
        {
            ConsoleKeyInfo key;
            bool refreshFlag = true;
            do
            {
                key = new ConsoleKeyInfo();

                if (refreshFlag)
                {
                    switch (_currentState)
                    {
                        case GameState.Playing:
                            _gameScreen.Render();
                            break;
                        case GameState.Inventory:
                            break;
                        case GameState.Stats:
                            _statScreen.Render();
                            break;
                    }

                    refreshFlag = false;
                }
                
                key = Console.ReadKey(intercept: true);

                
                switch (_currentState)
                {
                    case GameState.Playing:
                    {
                        var processResult = _gameScreen.ProcessInput(key.Key);
                        refreshFlag = processResult.RefreshFlag;
                        if (processResult.SwitchState != null)
                        {
                            _currentState = processResult.SwitchState.Value;
                        }
                        _gameScreen.Update();
                        break;
                    }
                    
                    case GameState.Inventory:
                        break;

                    case GameState.Stats:
                    {
                        var processResult = _statScreen.ProcessInput(key.Key);
                        refreshFlag = processResult.RefreshFlag;
                        if (processResult.SwitchState != null)
                        {
                            _currentState = processResult.SwitchState.Value;
                        }
                        _statScreen.Update();
                        break;
                    }
                }
                
            } 
            while (key.Key != ConsoleKey.X);
            
            Console.Clear();
            Console.WriteLine("You have left the game.");
        }        
    }
}