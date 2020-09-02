using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleRpg_2.Configurations;
using ConsoleRpg_2.Extensions;
using ConsoleRpg_2.GameObjects.Character;
using ConsoleRpg_2.Ui;

namespace ConsoleRpg_2.Engine
{
    public class Fight
    {
        private readonly Random _random = new Random();
        private List<Character> Characters { get; set; }

        private const int queueSize = 10;
        private List<Character> Queue { get; set; }

        public Fight(List<Character> characters)
        {
            Characters = characters;
            UpdateQueue();
        }

        private void UpdateQueue()
        {
            var queueProbabilities = Characters.Select(c =>
                {
                    double probability = 1.0;
                    if (c.Stats.Agility < 8)
                    {
                        probability = 0.9f;
                    }
                    if (c.Stats.Agility < 6)
                    {
                        probability = 0.8f;
                    }
                    if (c.Stats.Agility < 5)
                    {
                        probability = 0.5f;
                    }
                    if (c.Stats.Agility < 2)
                    {
                        probability = 0.3f;
                    }

                    return new
                    {
                        Character = c,
                        Probability = probability
                    };
                })
                .ToList();

            for (int i = 0; i < queueSize - Queue.Count; ++i)
            {
                Character toPut = null;
                while (toPut == null)
                {
                    toPut = queueProbabilities
                        .FirstOrDefault(qp => _random.NextDouble() < qp.Probability)
                        ?.Character;
                }
                
                Queue.Add(toPut);
            }
        }

        public void NextTurn()
        {
            var character = Queue.First();

            character.TakeFightTurn();
            
            Queue.Remove(character);
        }
        
        public void Update()
        {
            
        }
    }
    
    
    public enum FightScreenState
    {
        Fight,
        HotBarUse
    }
    
    public class FightScreen
    {
        private FightScreenState _screenState = FightScreenState.Fight;
        
        private readonly GameLog _gameLog;
        private readonly Character _currentCharacter;

        public FightScreen(GameLog gameLog, Character currentCharacter)
        {
            _gameLog = gameLog;
            _currentCharacter = currentCharacter;
        }
        
        public void Update()
        {
            _gameLog.WriteLine("[DEBUG] Update fight");
        }

        public ScreenInputProcessResult ProcessInput(ConsoleKey key)
        {
            var result = new ScreenInputProcessResult();
            
            switch (_screenState)
            {
                case FightScreenState.Fight:
                    result = ProcessFightInput(key);
                    break;
                case FightScreenState.HotBarUse:
                    result = ProcessHotBarUseInput(key);
                    break;
            }
            
            return result;
        }

        private ScreenInputProcessResult ProcessFightInput(ConsoleKey key)
        {
            var result = new ScreenInputProcessResult();

            switch (key)
            {
                case KeyMapping.HotBar_1:
                    if (_currentCharacter.HotBar.Slot1.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot1;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_2:
                    if (_currentCharacter.HotBar.Slot2.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot2;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_3:
                    if (_currentCharacter.HotBar.Slot3.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot3;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_4:
                    if (_currentCharacter.HotBar.Slot4.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot4;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_5:
                    if (_currentCharacter.HotBar.Slot5.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot5;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_6:
                    if (_currentCharacter.HotBar.Slot6.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot6;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_7:
                    if (_currentCharacter.HotBar.Slot7.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot7;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_8:
                    if (_currentCharacter.HotBar.Slot8.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot8;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_9:
                    if (_currentCharacter.HotBar.Slot9.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot9;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_10:
                    if (_currentCharacter.HotBar.Slot10.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot10;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
            }

            return result;
        }

        private ScreenInputProcessResult ProcessHotBarUseInput(ConsoleKey key)
        {
            var result = new ScreenInputProcessResult();
            
            switch (key)
            {
                case KeyMapping.Cancel:
                    _screenState = FightScreenState.Fight;
                    result.RerenderFlag = true;
                    break;
                
                case KeyMapping.PreviousItem:
                    _currentCharacter.HotBar.SelectedSlot.PrevItem();
                    result.RerenderFlag = true;
                    break;
                        
                case KeyMapping.NextItem:
                    _currentCharacter.HotBar.SelectedSlot.NextItem();
                    result.RerenderFlag = true;
                    break;
                        
                case KeyMapping.Confirm:
                    _currentCharacter.HotBar.SelectedSlot.PressCurrentItem();
                    _currentCharacter.HotBar.SelectedSlot = null;
                    _screenState = FightScreenState.Fight;
                    result.RerenderFlag = true;
                    break;
            }

            return result;
        }

        public void Render()
        {
            Console.Clear();

            ConsoleEx.WriteLine($"== Fight ".PadRight(Configuration.BufferLength, '='), ConsoleColor.Green);
            Console.WriteLine();

            RenderHp();
            RenderMp();
            RenderAp();

            Console.WriteLine();
            
            _gameLog.Render();

            RenderState();
        }

        private void RenderState()
        {
            switch (_screenState)
            {
                case FightScreenState.Fight:
                    RenderHotbar();
                    break;
                
                case FightScreenState.HotBarUse:
                    _currentCharacter.HotBar.SelectedSlot.Render();
                    break;
            }
        }
        
        private void RenderHp() 
        { 
            var hpColor = ConsoleColor.Green;

            var hpPercent = (float)_currentCharacter.Stats.Health / (float)_currentCharacter.Stats.MaxHealth;
            
            if (hpPercent <= 0.75)
            {
                hpColor = ConsoleColor.Yellow;
            }
            if (hpPercent <= 0.50)
            {
                hpColor = ConsoleColor.DarkYellow;
            }
            if (hpPercent <= 0.25)
            {
                hpColor = ConsoleColor.Red;
            }
            if (hpPercent <= 0.10)
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
            if (usedActionPoints > 0 && usedActionPoints != _currentCharacter.Stats.MaxActionPoints)
            {
                ConsoleEx.Write("-", ConsoleColor.DarkGray);
            }
            ConsoleEx.WriteLine(string.Join("-", Enumerable.Repeat("#", usedActionPoints)), ConsoleColor.DarkGray);
        }
        
        private void RenderHotbar()
        {
            Console.WriteLine();
            Console.WriteLine("== Your Hotbar ".PadRight(Configuration.BufferLength, '='));
            Console.WriteLine();
            
            var slots = _currentCharacter.HotBar
                .GetSlots();

            for (int i = 0; i < slots.Count; ++i)
            {
                var slotColor = ConsoleColor.Gray;

                if (slots[i].IsOccupied)
                {
                    slotColor = ConsoleColor.White;
                }
                
                ConsoleEx.Write($"{i+1} ".PadRight(3), ConsoleColor.Yellow);
                ConsoleEx.WriteLine($"~ {slots[i].Name}", slotColor);
            }
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
                        refreshFlag = processResult.RerenderFlag;
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
                        refreshFlag = processResult.RerenderFlag;
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
                        refreshFlag = processResult.RerenderFlag;
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