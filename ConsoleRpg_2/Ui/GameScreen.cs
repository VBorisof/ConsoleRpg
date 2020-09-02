using System;
using System.Linq;
using ConsoleRpg_2.Configurations;
using ConsoleRpg_2.Engine;
using ConsoleRpg_2.Extensions;
using ConsoleRpg_2.GameObjects.Character;

namespace ConsoleRpg_2.Ui
{
    public enum GameScreenState
    {
        World,
        LookAt,
        TalkTo,
        HotBarUse
    }

    public enum GameScreenBottomMenuState
    {
        Help,
        Hotbar
    }

    public class GameScreen
    {
        private GameScreenState _screenState;
        private GameScreenBottomMenuState _bottomMenuState = GameScreenBottomMenuState.Help;

        private UiSelectList _lookAtList;

        private TalkToScreen _talkToScreen;
        
        private readonly Character _currentCharacter;

        private GameLog _gameLog;
        
        public GameScreen(GameLog gameLog, Character currentCharacter)
        {
            _currentCharacter = currentCharacter;
            _screenState = GameScreenState.World;
            
            _gameLog = gameLog;
        }

        
        private void RenderGameLog()
        {
            ConsoleEx.WriteLine($"== Game ".PadRight(Configuration.BufferLength, '='), ConsoleColor.Green);
            Console.WriteLine();
            ConsoleEx.WriteLine($"You are in {_currentCharacter.CurrentScene.Name}", ConsoleColor.White);
            Console.WriteLine();
            ConsoleEx.WriteLine($"{string.Join("\n", _currentCharacter.AnalyzeScene().Split(Configuration.BufferLength))}", ConsoleColor.Gray);
            Console.WriteLine();
            
            _gameLog.Render();
        }

        private void RenderState()
        {
            switch (_screenState)
            {
                case GameScreenState.World:
                    switch (_bottomMenuState)
                    {
                        case GameScreenBottomMenuState.Help:
                            PrintHelpMemo();
                            break;
                        case GameScreenBottomMenuState.Hotbar:
                            PrintHotBar();
                            break;
                    }
                    break;
                case GameScreenState.LookAt:
                    Console.WriteLine("Look at... (Q) to abort");
                    _lookAtList.Render();
                    break;
                case GameScreenState.TalkTo:
                    _talkToScreen.Render();
                    break;
                case GameScreenState.HotBarUse:
                    _currentCharacter.HotBar.SelectedSlot.Render();
                    break;
            }
        }
        
        public void Render()
        {
            Console.Clear();
                    
            RenderGameLog();
            
            RenderState();
        }

        public ScreenInputProcessResult ProcessWorldInput(ConsoleKey key)
        {
            var result = new ScreenInputProcessResult();
            
            switch (key)
            {
                case KeyMapping.ShowTalkMenu:
                {
                    _talkToScreen = TalkToScreen.CreateOrDefault(_currentCharacter);

                    if (_talkToScreen != null)
                    {
                        _screenState = GameScreenState.TalkTo;
                    }
                    else
                    {
                        _gameLog.WriteLine("There is no one you can talk to here.");
                    }
                    
                    result.RefreshFlag = true;
                    
                    break;
                }

                case KeyMapping.ShowLookMenu:
                {
                    var labels = _currentCharacter.CurrentScene.GetObservableObjects()
                        .Except(new[] {_currentCharacter})
                        .Select((o, i) =>
                            new UiLabel
                            {
                                Text = o.Name,
                                Row = i,
                                OnPress = (_, __) =>
                                {
                                    _gameLog.WriteLine($"{_currentCharacter.Inspect(o).Response}");
                                }
                            }
                        ).ToList();

                    if (labels.Any())
                    {
                        _lookAtList = new UiSelectList(labels);
                        
                        _screenState = GameScreenState.LookAt;
                    }
                    else
                    {
                        _gameLog.WriteLine("There is nothing to look at here.");
                    }

                    result.RefreshFlag = true;

                    break;
                }
                
                case KeyMapping.ShowStats:
                    result.SwitchState = GameState.Stats;
                    result.RefreshFlag = true;
                    break;
                
                case KeyMapping.SlideRight:
                    _bottomMenuState++;

                    if (! Enum.IsDefined(typeof(GameScreenBottomMenuState), _bottomMenuState))
                    {
                        _bottomMenuState = GameScreenBottomMenuState.Help;
                    }
                    result.RefreshFlag = true;

                    break;
                    
                case KeyMapping.SlideLeft:
                    _bottomMenuState--;

                    if (! Enum.IsDefined(typeof(GameScreenBottomMenuState), _bottomMenuState))
                    {
                        _bottomMenuState = GameScreenBottomMenuState.Hotbar;
                    }
                    result.RefreshFlag = true;
                    
                    break;
                
                case KeyMapping.HotBar_1:
                    if (_currentCharacter.HotBar.Slot1.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot1;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = GameScreenState.HotBarUse;
                        result.RefreshFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_2:
                    if (_currentCharacter.HotBar.Slot2.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot2;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = GameScreenState.HotBarUse;
                        result.RefreshFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_3:
                    if (_currentCharacter.HotBar.Slot3.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot3;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = GameScreenState.HotBarUse;
                        result.RefreshFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_4:
                    if (_currentCharacter.HotBar.Slot4.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot4;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = GameScreenState.HotBarUse;
                        result.RefreshFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_5:
                    if (_currentCharacter.HotBar.Slot5.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot5;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = GameScreenState.HotBarUse;
                        result.RefreshFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_6:
                    if (_currentCharacter.HotBar.Slot6.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot6;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = GameScreenState.HotBarUse;
                        result.RefreshFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_7:
                    if (_currentCharacter.HotBar.Slot7.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot7;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = GameScreenState.HotBarUse;
                        result.RefreshFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_8:
                    if (_currentCharacter.HotBar.Slot8.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot8;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = GameScreenState.HotBarUse;
                        result.RefreshFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_9:
                    if (_currentCharacter.HotBar.Slot9.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot9;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = GameScreenState.HotBarUse;
                        result.RefreshFlag = true;
                    }
                    break;
                
                case KeyMapping.HotBar_10:
                    if (_currentCharacter.HotBar.Slot10.IsOccupied)
                    {
                        _currentCharacter.HotBar.SelectedSlot = _currentCharacter.HotBar.Slot10;
                        _currentCharacter.HotBar.SelectedSlot.UpdateCharacterSelectList(_currentCharacter);
                        
                        _screenState = GameScreenState.HotBarUse;
                        result.RefreshFlag = true;
                    }
                    break;
            }

            return result;
        }

        private ScreenInputProcessResult ProcessLookAtInput(ConsoleKey key)
        {
            var result = new ScreenInputProcessResult();
            
            switch (key)
            {
                case KeyMapping.Cancel:
                    _screenState = GameScreenState.World;
                    result.RefreshFlag = true;
                    break;
                
                case KeyMapping.PreviousItem:
                    _lookAtList.PrevItem();
                    result.RefreshFlag = true;
                    break;
                        
                case KeyMapping.NextItem:
                    _lookAtList.NextItem();
                    result.RefreshFlag = true;
                    break;
                        
                case KeyMapping.Confirm:
                    _lookAtList.PressCurrentItem();
                    _screenState = GameScreenState.World;
                    result.RefreshFlag = true;
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
                    _screenState = GameScreenState.World;
                    result.RefreshFlag = true;
                    break;
                
                case KeyMapping.PreviousItem:
                    _currentCharacter.HotBar.SelectedSlot.PrevItem();
                    result.RefreshFlag = true;
                    break;
                        
                case KeyMapping.NextItem:
                    _currentCharacter.HotBar.SelectedSlot.NextItem();
                    result.RefreshFlag = true;
                    break;
                        
                case KeyMapping.Confirm:
                    _currentCharacter.HotBar.SelectedSlot.PressCurrentItem();
                    _currentCharacter.HotBar.SelectedSlot = null;
                    _screenState = GameScreenState.World;
                    result.RefreshFlag = true;
                    break;
            }

            return result;
        }
        
        public ScreenInputProcessResult ProcessInput(ConsoleKey key)
        {       
            var result = new ScreenInputProcessResult();

            switch (_screenState)
            {
                case GameScreenState.World:
                    result = ProcessWorldInput(key);
                    break;
                
                case GameScreenState.LookAt:
                    result = ProcessLookAtInput(key);
                    break;
           
                case GameScreenState.HotBarUse:
                    result = ProcessHotBarUseInput(key);
                    break;
                
                case GameScreenState.TalkTo:
                    // Overengineering in the flesh...
                    result = _talkToScreen.ProcessInput(key, endDialogueCallback: () =>
                    {
                        _screenState = GameScreenState.World;
                        _talkToScreen = null;
                    });

                    if (result.SwitchState == GameState.World)
                    {
                        _screenState = GameScreenState.World;
                    }
                    
                    break;
            }
            
            return result;
        }

        private void PrintHelpMemo()
        {
            Console.WriteLine();
            Console.WriteLine("== Memo ".PadRight(Configuration.BufferLength, '='));
            
            var i = "\x1b[1m\x1b[33mi\x1b[0m";
            var o = "\x1b[1m\x1b[33mo\x1b[0m";
            var j = "\x1b[1m\x1b[33mj\x1b[0m";
            var k = "\x1b[1m\x1b[33mk\x1b[0m";
            var l = "\x1b[1m\x1b[33ml\x1b[0m";
            var x = "\x1b[1m\x1b[33mx\x1b[0m";
            var left  = "\x1b[1m\x1b[33m<-\x1b[0m";
            var right = "\x1b[1m\x1b[33m->\x1b[0m";

            var memo = $@"
            Inventory --- {i} {o} --- Stats 
                        {j} {k} {l}
                       /  |  \
                      /   |   \
                   Talk   |   Look at
                        Skills

  {x} - Quit      {left} {right} - Cycle this menu 
";
            Console.WriteLine(memo);
        }

        private void PrintHotBar()
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
}