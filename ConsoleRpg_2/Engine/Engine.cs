using System;
using ConsoleRpg_2.Configurations;
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
                    Health = 150,
                    Mana = 50,
                    ActionPoints = 12,
                    MaxHealth = 150,
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

            _currentCharacter = player;
            
            var scene = _loader.LoadScene(1);

            scene.Characters.Add(player);
            _currentCharacter.CurrentScene = scene;
            
            scene.InitializeScene();
            
            _currentState = GameState.Fight;
            
            _gameScreen = new GameScreen(_gameLog, _currentCharacter);
            _statScreen = new StatScreen(_currentCharacter);
            _fightScreen = new FightScreen(new Fight(scene.Characters), _gameLog, _currentCharacter);
        }
        
        public void Run()
        {
            ConsoleKeyInfo key;
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
                    }

                    rerenderFlag = false;
                }

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
                        _fightScreen.Update();
                        break;
                    }
                }
                
            } 
            while (key.Key != KeyMapping.ExitGame);
            
            Console.Clear();
            Console.WriteLine("You have left the game.");
        }        
    }
}