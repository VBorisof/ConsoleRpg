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
    }

    public class GameScreen
    {
        private GameScreenState _screenState;

        private UiSelectList _lookAtList;

        private TalkToScreen _talkToScreen;
        
        private readonly Character _currentCharacter;

        private GameLog _gameLog;
        
        public GameScreen(Character currentCharacter)
        {
            _currentCharacter = currentCharacter;
            _screenState = GameScreenState.World;
            
            _gameLog = new GameLog();
            _gameLog.WriteLine($"[{DateTime.Now}] -- You have entered the game.");
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
                    PrintHelpMemo();
                    break;
                case GameScreenState.LookAt:
                    Console.WriteLine("Look at... (Q) to abort");
                    _lookAtList.Render();
                    break;
                case GameScreenState.TalkTo:
                    _talkToScreen.Render();
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
                case ConsoleKey.K:
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

                case ConsoleKey.L:
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
                
                case ConsoleKey.O:
                    result.SwitchState = GameState.Stats;
                    result.RefreshFlag = true;
                    break;
            }

            return result;
        }

        private ScreenInputProcessResult ProcessLookAtInput(ConsoleKey key)
        {
            var result = new ScreenInputProcessResult();
            
            switch (key)
            {
                case ConsoleKey.Q:
                    _screenState = GameScreenState.World;
                    result.RefreshFlag = true;
                    break;
                
                case ConsoleKey.UpArrow:
                    _lookAtList.PrevItem();
                    result.RefreshFlag = true;
                    break;
                        
                case ConsoleKey.DownArrow:
                    _lookAtList.NextItem();
                    result.RefreshFlag = true;
                    break;
                        
                case ConsoleKey.Enter:
                    _lookAtList.PressCurrentItem();
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

        public void Update()
        {
            
        }


        private void PrintHelpMemo()
        {
            var i = "\x1b[1m\x1b[33mi\x1b[0m";
            var o = "\x1b[1m\x1b[33mo\x1b[0m";
            var j = "\x1b[1m\x1b[33mj\x1b[0m";
            var k = "\x1b[1m\x1b[33mk\x1b[0m";
            var l = "\x1b[1m\x1b[33ml\x1b[0m";
            var x = "\x1b[1m\x1b[33mx\x1b[0m";

            var memo = $@"
            Inventory --- {i} {o} --- Stats 
                        {j} {k} {l}
                       /  |  \
                      /   |   \
                    Use   |   Look at
                        Talk

  {x} - Quit
";
            Console.WriteLine(memo);            
        }
    }
}