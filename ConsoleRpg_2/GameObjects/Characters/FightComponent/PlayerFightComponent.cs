using System;
using ConsoleRpg_2.Configurations;
using ConsoleRpg_2.Extensions;
using ConsoleRpg_2.GameObjects.Characters.Skills;
using ConsoleRpg_2.Screens;

namespace ConsoleRpg_2.GameObjects.Characters.FightComponent
{
    public class PlayerFightComponent : IFightComponent
    {
        private readonly Character _player;
        private FightComponentState _componentState = FightComponentState.Fight;
        
        public PlayerFightComponent(Character player)
        {
            _player = player;
        }


        public void Render()
        {
            switch (_componentState)
            {
                case FightComponentState.Waiting:
                    break;
                    
                case FightComponentState.Fight:
                    RenderHotbar();
                    break;
                    
                case FightComponentState.HotBarUse:
                    _player.HotBar.SelectedSlot.Render();
                    break;
            }
        }
        
        public FightComponentResult Process(ConsoleKey key)
        {
            var result = new FightComponentResult();
            
            switch (_componentState)
            {
                case FightComponentState.Fight:
                    result = ProcessFightInput(key);
                    break;
                case FightComponentState.HotBarUse:
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
                        
                        _componentState = FightComponentState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_2:
                    if (_player.HotBar.Slot2.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot2;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _componentState = FightComponentState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_3:
                    if (_player.HotBar.Slot3.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot3;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _componentState = FightComponentState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_4:
                    if (_player.HotBar.Slot4.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot4;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _componentState = FightComponentState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_5:
                    if (_player.HotBar.Slot5.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot5;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _componentState = FightComponentState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_6:
                    if (_player.HotBar.Slot6.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot6;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _componentState = FightComponentState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_7:
                    if (_player.HotBar.Slot7.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot7;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _componentState = FightComponentState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_8:
                    if (_player.HotBar.Slot8.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot8;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _componentState = FightComponentState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_9:
                    if (_player.HotBar.Slot9.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot9;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _componentState = FightComponentState.HotBarUse;
                    }
                    break;
                
                case KeyMapping.HotBar_10:
                    if (_player.HotBar.Slot10.IsOccupied)
                    {
                        _player.HotBar.SelectedSlot = _player.HotBar.Slot10;
                        _player.HotBar.SelectedSlot.UpdateCharacterSelectList(_player);
                        
                        _componentState = FightComponentState.HotBarUse;
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
                    _componentState = FightComponentState.Fight;
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
                    _componentState = FightComponentState.Fight;
                    break;
            }

            return result;
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