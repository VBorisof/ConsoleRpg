using System;
using System.Linq;
using System.Threading;
using ConsoleRpg_2.Configurations;
using ConsoleRpg_2.Extensions;
using ConsoleRpg_2.GameObjects.Character;
using ConsoleRpg_2.Ui;

namespace ConsoleRpg_2.Engine
{
    public class FightComponentResult
    {
        public bool TurnEnd { get; set; }
    }

    public interface IFightComponent
    {
        FightComponentResult Process(ConsoleKey key);
    }
    
    public class NpcFightComponent : IFightComponent
    {
        private readonly GameLog _gameLog;
        private readonly Character _npc;

        public NpcFightComponent(GameLog gameLog, Character npc)
        {
            _gameLog = gameLog;
            _npc = npc;
        }
        
        
        public FightComponentResult Process(ConsoleKey key)
        {
            var result = new FightComponentResult();
            
            Thread.Sleep(1000);
            var target = _npc.CurrentScene.Characters.First(c => c != _npc);
            
            var skillResult = _npc.ApplySkill(_npc.Skills.First(s => s.SkillType == SkillType.Meelee), target);
            if (! skillResult.IsSuccess)
            {
                _gameLog.WriteLine(skillResult.Comment);    
            }
            else
            {
                if (skillResult.Damage >= 0)
                {
                    _gameLog.WriteLine(
                        $"{_npc.Name} deals {skillResult.Damage} damage to {target.Name}."
                    );
                }
                else
                {
                    _gameLog.WriteLine(
                        $"{_npc.Name} heals {target.Name} for {-skillResult.Damage} health points."
                    );
                }    
            }

            return result;
        }
    }
    
    public class PlayerFightComponent : IFightComponent
    {
        private readonly Character _player;
        private FightScreenState _screenState = FightScreenState.Fight;
        
        public PlayerFightComponent(Character player)
        {
            _player = player;
        }
        
        
        public FightComponentResult Process(ConsoleKey key)
        {
            var result = new FightComponentResult();
            
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

        public FightComponentResult ProcessFightInput(ConsoleKey key)
        {
            var result = new FightComponentResult();
            
            switch (key)
            {
                case KeyMapping.HotBar_1:
                    if (_player.HotBar.Slot1.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot1;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_2:
                    if (_player.HotBar.Slot2.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot2;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_3:
                    if (_player.HotBar.Slot3.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot3;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_4:
                    if (_player.HotBar.Slot4.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot4;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_5:
                    if (_player.HotBar.Slot5.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot5;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_6:
                    if (_player.HotBar.Slot6.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot6;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_7:
                    if (_player.HotBar.Slot7.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot7;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_8:
                    if (_player.HotBar.Slot8.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot8;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_9:
                    if (_player.HotBar.Slot9.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot9;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_10:
                    if (_player.HotBar.Slot10.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot10;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.EndTurn:
                    result.TurnEnd = true;
                    break;
            }

            return result;
        }
        
        private FightComponentResult ProcessHotBarUseInput(ConsoleKey key)
        {
            var result = new FightComponentResult();
            
            switch (key)
            {
                case KeyMapping.Cancel:
                    _screenState = FightScreenState.Fight;
                    break;
                
                case KeyMapping.PreviousItem:
                    _player.HotBar.SelectedSlot.PrevItem();
                    break;
                        
                case KeyMapping.NextItem:
                    _player.HotBar.SelectedSlot.NextItem();
                    break;
                        
                case KeyMapping.Confirm:
                    _player.HotBar.SelectedSlot.PressCurrentItem();
                    _player.HotBar.SelectedSlot = null;
                    _screenState = FightScreenState.Fight;
                    break;
            }

            return result;
        }
    }
    
    public class FightScreen
    {
        private Fight Fight { get; set; }

        private FightScreenState _screenState = FightScreenState.Fight;
        
        private readonly GameLog _gameLog;
        private readonly Character _player;

        private Character _activeCharacter;

        public bool Manual => _activeCharacter?.CharacterType == CharacterType.Player;
        
        public FightScreen(Fight fight, GameLog gameLog, Character player)
        {
            Fight = fight;
            _gameLog = gameLog;
            _player = player;
        }
        
        public void Update()
        {
            
        }

        public ScreenInputProcessResult ProcessInput(ConsoleKey key)
        {
            var result = new ScreenInputProcessResult();
            
            var prevChar = _activeCharacter;
            _activeCharacter = Fight.Queue.First();

            if (prevChar != _activeCharacter)
            {
                _gameLog.WriteLine($"{_activeCharacter.Name}'s turn.");

                _screenState = Manual ? FightScreenState.Fight : FightScreenState.Waiting;

                Fight.BeginTurn(_activeCharacter);
                
                result.RerenderFlag = true;
                return result;
            }
            
            if (Manual)
            {   
                switch (_screenState)
                {
                    case FightScreenState.Fight:
                        result = ProcessFightInput(key);
                        break;
                    case FightScreenState.HotBarUse:
                        result = ProcessHotBarUseInput(key);
                        break;
                }
            }
            else
            {
                Thread.Sleep(1000);
                _gameLog.WriteLine($"NPC {_activeCharacter.Name} Does something...");
                
                
                
                
                Fight.EndCurrentTurn();
                _activeCharacter = null;
            }
   
            if (_activeCharacter?.Stats.ActionPoints == 0)
            {
                Fight.EndCurrentTurn();
                _activeCharacter = null;
                result.RerenderFlag = true;
                return result;
            }
            
            result.RerenderFlag = true;
            return result;
        }

        private ScreenInputProcessResult ProcessFightInput(ConsoleKey key)
        {
            var result = new ScreenInputProcessResult();
            
            switch (key)
            {
                case KeyMapping.HotBar_1:
                    if (_player.HotBar.Slot1.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot1;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_2:
                    if (_player.HotBar.Slot2.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot2;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_3:
                    if (_player.HotBar.Slot3.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot3;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_4:
                    if (_player.HotBar.Slot4.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot4;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_5:
                    if (_player.HotBar.Slot5.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot5;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_6:
                    if (_player.HotBar.Slot6.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot6;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_7:
                    if (_player.HotBar.Slot7.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot7;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_8:
                    if (_player.HotBar.Slot8.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot8;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_9:
                    if (_player.HotBar.Slot9.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot9;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_10:
                    if (_player.HotBar.Slot10.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot10;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _screenState = FightScreenState.HotBarUse;
                        result.RerenderFlag = true;
                    }
                    break;
                
                case KeyMapping.EndTurn:
                    Fight.EndCurrentTurn();
                    _activeCharacter = null;
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
                    _player.HotBar.SelectedSlot.PrevItem();
                    result.RerenderFlag = true;
                    break;
                        
                case KeyMapping.NextItem:
                    _player.HotBar.SelectedSlot.NextItem();
                    result.RerenderFlag = true;
                    break;
                        
                case KeyMapping.Confirm:
                    _player.HotBar.SelectedSlot.PressCurrentItem();
                    _player.HotBar.SelectedSlot = null;
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

            Console.WriteLine();
            
            RenderQueue();
            
            RenderState();
        }

        private void RenderQueue()
        {
            Console.WriteLine(
                "~QUEUE~"
                .PadLeft(Configuration.BufferLength/2, '=')
                .PadRight(Configuration.BufferLength, '='));


            ConsoleEx.Write(Fight.Queue.First().Name, ConsoleColor.Green);
            ConsoleEx.Write(" > ", ConsoleColor.White);

            var restOfChars = string.Join(" > ", Fight.Queue.Skip(1).Select(c => c.Name));
            var allowedLength = Configuration.BufferLength - (Fight.Queue.First().Name.Length + " > ".Length);
            
            ConsoleEx.Write(string.Join("", restOfChars.Take(allowedLength)), ConsoleColor.Gray);
            
            Console.WriteLine();
            
            Console.WriteLine("".PadRight(Configuration.BufferLength, '='));
        }
        
        private void RenderState()
        {
            if (_activeCharacter.CharacterType == CharacterType.Player)
            {
                switch (_screenState)
                {
                    case FightScreenState.Waiting:
                        break;
                    
                    case FightScreenState.Fight:
                        RenderHotbar();
                        break;
                    
                    case FightScreenState.HotBarUse:
                        _player.HotBar.SelectedSlot.Render();
                        break;
                }
            }
        }
        
        private void RenderHp() 
        { 
            var hpColor = ConsoleColor.Green;

            var hpPercent = (float)_player.Stats.Health / (float)_player.Stats.MaxHealth;
            
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
            ConsoleEx.WriteLine($"{_player.Stats.Health}/{_player.Stats.MaxHealth}", hpColor);
        }
        private void RenderMp() 
        { 
            ConsoleEx.Write("MP | ", ConsoleColor.White);
            ConsoleEx.WriteLine(
                $"{_player.Stats.Mana}/{_player.Stats.MaxMana}", 
                _player.Stats.Mana <= 0 ? ConsoleColor.Gray : ConsoleColor.Cyan
            );
        }
        private void RenderAp() 
        { 
            var usedActionPoints = _player.Stats.MaxActionPoints - _player.Stats.ActionPoints;
            
            ConsoleEx.Write("AP | ", ConsoleColor.White);
            ConsoleEx.Write(string.Join("-", Enumerable.Repeat("#", _player.Stats.ActionPoints)), ConsoleColor.Green);
            if (usedActionPoints > 0 && usedActionPoints != _player.Stats.MaxActionPoints)
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
            
            var slots = _player.HotBar
                .GetSlots();

            for (int i = 0; i < slots.Count; ++i)
            {
                var slotColor = ConsoleColor.Gray;

                if (slots[i].IsOccupied)
                {
                    slotColor = ConsoleColor.White;
                }
                
                ConsoleEx.Write($"{i+1} ".PadRight(3), ConsoleColor.Yellow);
                ConsoleEx.Write($"~ {slots[i].Name}", slotColor);
                if (slots[i].GameObject is Skill skill)
                {
                    ConsoleEx.Write($" ({skill.ActionPoints} AP)", ConsoleColor.Cyan);
                }
                
                Console.WriteLine();
            }
            
            Console.WriteLine();
            ConsoleEx.WriteLine("Use <SPACE> to end your turn.", ConsoleColor.Gray);
        }
    }
}