using System;
using System.Linq;
using ConsoleRpg_2.Engine;
using ConsoleRpg_2.Extensions;
using ConsoleRpg_2.GameObjects.Character;

namespace ConsoleRpg_2.Ui
{
    public class GameScreen
    {
        private const int BufferSize = 20;
        private const int BufferLength = 60;
        
        private readonly Character _currentCharacter;

        private string _gameLog = $"[{DateTime.Now}] -- You have entered the game.";
        
        public GameScreen(Character currentCharacter)
        {
            _currentCharacter = currentCharacter;
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

            var lines = _gameLog.Split(BufferLength)
                .SelectMany(l => l.Split("\n").TakeLast(BufferSize))
                .ToList();
            
            var emptyBufferLines = BufferSize - lines.Count;
            if (emptyBufferLines > 0)
            {
                var emptyLine = "".PadLeft(BufferLength, ' ');
                lines.AddRange(Enumerable.Repeat(emptyLine, emptyBufferLines));
            }
            
            ConsoleEx.WriteBlock(lines.Select(l => l.PadRight(BufferLength, ' ')), ConsoleColor.White, ConsoleColor.DarkGray);
            
            ConsoleEx.WriteLine("".PadRight(BufferLength, '_'), ConsoleColor.Green, ConsoleColor.DarkGray);
        }

        private void ProcessLookAt()
        {
            Console.WriteLine("Look at...");
            var dict = _currentCharacter.CurrentScene.GetObjectDict();
            foreach (var obj in dict)
            {
                Console.WriteLine($"{obj.Key} : {obj.Value.Name}");
            }

            var decisionKey = Console.ReadKey(intercept: true);
            int.TryParse(decisionKey.KeyChar.ToString(), out var decisionIndex);
                                            
            while (decisionKey.Key != ConsoleKey.Q && !dict.ContainsKey(decisionIndex))
            {
                Console.WriteLine("Wrong input. Try again or use <q> to abort.");
                        
                decisionKey = Console.ReadKey(intercept: true);
                int.TryParse(decisionKey.KeyChar.ToString(), out decisionIndex);
            }

            if (decisionKey.Key == ConsoleKey.Q)
            {
                return;
            }
                    
            _gameLog += $"\n{_currentCharacter.Inspect(dict[decisionIndex]).Response}";
        }
        
        public ScreenInputProcessResult ProcessInput(ConsoleKey key)
        {       
            var result = new ScreenInputProcessResult();
            
            switch (key)
            {
                case ConsoleKey.L:
                    ProcessLookAt();
                    break;
                case ConsoleKey.P:
                    result.SwitchState = GameState.Stats;
                    result.RefreshFlag = true;
                    break;
            }

            return result;
        }

        public void Update()
        {
            
        }
    }
}