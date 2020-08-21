using System;
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
        private XmlLoader _loader = new XmlLoader();
        
        public Engine()
        {
            var player = new Character
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
            
            var scene = _loader.LoadScene(1);

            scene.Characters.Add(player);
            _currentCharacter.CurrentScene = scene;
            
            scene.InitializeScene();
            
            _currentState = GameState.World;
            
            _gameScreen = new GameScreen(_currentCharacter);
            _statScreen = new StatScreen(_currentCharacter);
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
                        case GameState.World:
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
                    case GameState.World:
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