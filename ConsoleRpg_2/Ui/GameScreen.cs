using System;
using System.Linq;
using ConsoleRpg_2.Engine;
using ConsoleRpg_2.Extensions;
using ConsoleRpg_2.GameObjects.Character;

namespace ConsoleRpg_2.Ui
{
    public enum GameScreenState
    {
        World,
        LookAt
    }
    
    public class GameScreen
    {
        private GameScreenState _screenState;

        private UiSelectList _lookAtList;
        
        private const int BufferHeight = 25;
        private const int BufferLength = 60;
        
        private readonly Character _currentCharacter;

        private string _gameLog = $"[{DateTime.Now}] -- You have entered the game.";
        
        public GameScreen(Character currentCharacter)
        {
            _currentCharacter = currentCharacter;
            _screenState = GameScreenState.World;
        }
        
        public void Render()
        {
            Console.Clear();
                    
            ConsoleEx.WriteLine($"Press `?` for help.", ConsoleColor.Gray);
            ConsoleEx.WriteLine($"== Game ".PadRight(BufferLength, '='), ConsoleColor.Green);
            Console.WriteLine();
            ConsoleEx.WriteLine($"You are in {_currentCharacter.CurrentScene.Name}", ConsoleColor.White);
            Console.WriteLine();
            ConsoleEx.WriteLine($"{string.Join("\n", _currentCharacter.AnalyzeScene().Split(BufferLength))}", ConsoleColor.Gray);
            Console.WriteLine();
            ConsoleEx.WriteLine("".PadRight(BufferLength, '_'), ConsoleColor.Green);

            var lines = _gameLog.Split("\n")
                .SelectMany(l => l.Split(BufferLength))
                .TakeLast(BufferHeight)
                .ToList();
            
            
            var emptyBufferLines = BufferHeight - lines.Count;
            if (emptyBufferLines > 0)
            {
                var emptyLine = "".PadLeft(BufferLength, ' ');
                lines.AddRange(Enumerable.Repeat(emptyLine, emptyBufferLines));
            }
            
            ConsoleEx.WriteBlock(lines.Select(l => l.PadRight(BufferLength, ' ')), ConsoleColor.White, ConsoleColor.DarkGray);
            
            ConsoleEx.WriteLine("".PadRight(BufferLength, '_'), ConsoleColor.Green, ConsoleColor.DarkGray);

            if (_screenState == GameScreenState.LookAt)
            {
                Console.WriteLine("Look at... (Q) to abort");
                _lookAtList.Render();
            }
        }

        public ScreenInputProcessResult ProcessInput(ConsoleKey key)
        {       
            var result = new ScreenInputProcessResult();

            switch (_screenState)
            {
                case GameScreenState.World:
                    switch (key)
                    {
                        case ConsoleKey.L:
                            var labels = _currentCharacter.CurrentScene.GetObjectDict()
                                .Select((o, i) => 
                                    new UiLabel
                                    {
                                        Text = o.Value.Name,
                                        Row = i + 1,
                                        Column = 1,
                                        OnPress = (_, __) =>
                                        {
                                            _gameLog += $"\n{_currentCharacter.Inspect(o.Value).Response}";
                                        }
                                    }
                                ).ToList();
                            _lookAtList = new UiSelectList(labels);
    
                            _screenState = GameScreenState.LookAt;

                            result.RefreshFlag = true;
                            
                            break;
                
                        case ConsoleKey.P:
                            result.SwitchState = GameState.Stats;
                            result.RefreshFlag = true;
                            break;
                    }
                    break;
                
                case GameScreenState.LookAt:
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
                    break;
            }
            
            return result;
        }

        public void Update()
        {
            
        }
    }
}