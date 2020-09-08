using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleRpg_2.Configurations;
using ConsoleRpg_2.GameObjects.Characters;
using ConsoleRpg_2.GameObjects.Characters.FightComponent;
using ConsoleRpg_2.Screens;
using ConsoleRpg_2.Ui;

namespace ConsoleRpg_2.Engine
{
    public class Engine
    {
        private readonly StatScreen _statScreen;
        private Character _currentCharacter;
        private GameState _currentState;
        private GameScreen _gameScreen;
        private FightScreen _fightScreen;
        private XmlLoader _loader;
        private GameLog _gameLog = new GameLog();
        
        public Engine()
        {
            _gameLog.WriteLine($"[{DateTime.Now}] -- You have entered the game.");
            
            _loader = new XmlLoader(_gameLog);
            
            var player = new Character(_gameLog)
            {
                Name = "Player",
                CharacterType = CharacterType.Player,
                DefaultAttitude = Attitude.Neutral,
                Inventory = new Inventory(),
                Stats = new StatSet
                {
                    Level = 1,
                    Race = Race.Human,
                    Gender = Gender.Male,
                    Health = 30,
                    Mana = 50,
                    ActionPoints = 12,
                    MaxHealth = 30,
                    MaxMana = 50,
                    MaxActionPoints = 12,
                    AvailableBaseSkillPoints = 50,
                    Strength = 5,
                    Perception = 5,
                    Stamina = 5,
                    Charisma = 5,
                    Intelligence = 5,
                    Agility = 10,
                }
            };
            player.FightComponent = new PlayerFightComponent(player);
            
            _currentCharacter = player;
            
            var scene = _loader.LoadScene(1);

            scene.Characters.Add(player);
            _currentCharacter.CurrentScene = scene;
            
            scene.InitializeScene();
            
            _currentState = GameState.World;
            
            _gameScreen = new GameScreen(_gameLog, _currentCharacter);
            _statScreen = new StatScreen(_currentCharacter);
        }
        
        public void Run()
        {
            ConsoleKeyInfo key;
            bool isRunning = true;
            bool rerenderFlag = true;
            do
            {
                if (rerenderFlag)
                {
                    switch (_currentState)
                    {
                        case GameState.World:
                            _gameScreen.Render();
                            break;
                        case GameState.Inventory:
                            break;
                        case GameState.Stats:
                            _statScreen.Render();
                            break;
                        case GameState.Fight:
                            _fightScreen.Render();
                            break;
                        case GameState.GameOver:
                            Console.Clear();
                            _gameLog.Render();
                            Console.WriteLine("Press any key to exit.");
                            break;
                    }

                    rerenderFlag = false;
                }

                // In case of a fight, don't ask for the key, if we are waiting for someone else's turn.
                if (_currentState == GameState.Fight && !_fightScreen.Manual)
                {
                    key = new ConsoleKeyInfo(); 
                }
                else
                {
                    key = Console.ReadKey(intercept: true);
                }

                
                switch (_currentState)
                {
                    case GameState.World:
                    {
                        var processResult = _gameScreen.ProcessInput(key.Key);
                        rerenderFlag = processResult.RerenderFlag;
                        if (processResult.SwitchState != null)
                        {
                            _currentState = processResult.SwitchState.Value;
                        }
                        if (_currentState == GameState.Fight)
                        {
                            var fightChars = new List<FightCharacter>
                            {
                                new FightCharacter(_currentCharacter, FightCharacterFaction.Player)
                            };
                            
                            fightChars.AddRange(
                                _currentCharacter.CurrentScene.Characters
                                    .Except(new [] {_currentCharacter})
                                .Select(c => new FightCharacter(c, FightCharacterFaction.Enemy))
                            );
                            
                            _fightScreen = new FightScreen(new Fight(fightChars), _gameLog, _currentCharacter);
                        }
                        break;
                    }
                    
                    case GameState.Inventory:
                        break;

                    case GameState.Stats:
                    {
                        var processResult = _statScreen.ProcessInput(key.Key);
                        rerenderFlag = processResult.RerenderFlag;
                        if (processResult.SwitchState != null)
                        {
                            _currentState = processResult.SwitchState.Value;
                        }
                        _statScreen.Update();
                        break;
                    }

                    case GameState.Fight:
                    {
                        var processResult = _fightScreen.ProcessInput(key.Key);
                        rerenderFlag = processResult.RerenderFlag;
                        if (processResult.SwitchState != null)
                        {
                            _currentState = processResult.SwitchState.Value;
                        }
                        break;
                    }
                    
                    case GameState.GameOver:
                    {
                        isRunning = false;
                        break;
                    }
                }
            } 
            while (key.Key != KeyMapping.ExitGame && isRunning);
            
            Console.Clear();
            Console.WriteLine("You have left the game.");
        }        
    }
}