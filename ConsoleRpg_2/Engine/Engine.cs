using System;
using System.Linq;
using ConsoleRpg_2.Configurations;
using ConsoleRpg_2.Extensions;
using ConsoleRpg_2.GameObjects.Character;
using ConsoleRpg_2.Ui;

namespace ConsoleRpg_2.Engine
{
    public class FightScreen
    {
        private readonly GameLog _gameLog;
        private readonly Character _currentCharacter;

        public FightScreen(GameLog gameLog, Character currentCharacter)
        {
            _gameLog = gameLog;
            _currentCharacter = currentCharacter;
        }
        
        public void Update()
        {
            
        }

        public ScreenInputProcessResult ProcessInput(ConsoleKey key)
        {
            return new ScreenInputProcessResult
            {
                SwitchState = null,
                RefreshFlag = true
            };
        }

        public void Render()
        {
            Console.Clear();
            
            ConsoleEx.WriteLine($"== Fight ".PadRight(Configuration.BufferLength, '='), ConsoleColor.Green);
            Console.WriteLine();

            RenderHp();
            RenderMp();
            RenderAp();
            
            _gameLog.Render();
            
            Console.WriteLine();
        }
        
        private void RenderHp() 
        { 
            var hpColor = ConsoleColor.Green;

            var hpPercent = (float)_currentCharacter.Stats.Health / (float)_currentCharacter.Stats.MaxHealth;
            
            if (_currentCharacter.Stats.Health <= 0.75)
            {
                hpColor = ConsoleColor.Yellow;
            }
            if (_currentCharacter.Stats.Health <= 0.50)
            {
                hpColor = ConsoleColor.DarkYellow;
            }
            if (_currentCharacter.Stats.Health <= 0.25)
            {
                hpColor = ConsoleColor.Red;
            }
            if (_currentCharacter.Stats.Health <= 0.10)
            {
                hpColor = ConsoleColor.DarkRed;
            }
            
            ConsoleEx.Write("HP | ", ConsoleColor.White);
            ConsoleEx.WriteLine($"{_currentCharacter.Stats.Health}/{_currentCharacter.Stats.MaxHealth}", hpColor);
        }
        private void RenderMp() 
        { 
            ConsoleEx.Write("MP | ", ConsoleColor.White);
            ConsoleEx.WriteLine(
                $"{_currentCharacter.Stats.Mana}/{_currentCharacter.Stats.MaxMana}", 
                _currentCharacter.Stats.Mana <= 0 ? ConsoleColor.Gray : ConsoleColor.Cyan
            );
        }
        private void RenderAp() 
        { 
            var usedActionPoints = _currentCharacter.Stats.MaxActionPoints - _currentCharacter.Stats.ActionPoints;
            
            ConsoleEx.Write("AP | ", ConsoleColor.White);
            ConsoleEx.Write(string.Join("-", Enumerable.Repeat("#", _currentCharacter.Stats.ActionPoints)), ConsoleColor.Green);
            ConsoleEx.WriteLine(string.Join("-", Enumerable.Repeat("#", usedActionPoints)).PadLeft(usedActionPoints + 1, '-'), ConsoleColor.DarkGray);
        }
    }
    
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
                DefaultAttitude = Attitude.Neutral,
                Inventory = new Inventory(),
                Stats = new StatSet
                {
                    Level = 1,
                    Race = Race.Human,
                    Gender = Gender.Male,
                    Health = 150,
                    Mana = 50,
                    ActionPoints = 3,
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
            
            var scene = _loader.LoadScene(1);

            scene.Characters.Add(player);
            _currentCharacter.CurrentScene = scene;
            
            scene.InitializeScene();
            
            _currentState = GameState.Fight;
            
            _gameScreen = new GameScreen(_gameLog, _currentCharacter);
            _statScreen = new StatScreen(_currentCharacter);
            _fightScreen = new FightScreen(_gameLog, _currentCharacter);
        }
        
        public void Run()
        {
            ConsoleKeyInfo key;
            bool refreshFlag = true;
            do
            {
                if (refreshFlag)
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

                    refreshFlag = false;
                }
                
                key = Console.ReadKey(intercept: true);

                
                switch (_currentState)
                {
                    case GameState.World:
                    {
                        var processResult = _gameScreen.ProcessInput(key.Key);
                        refreshFlag = processResult.RefreshFlag;
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
                        refreshFlag = processResult.RefreshFlag;
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
                        refreshFlag = processResult.RefreshFlag;
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