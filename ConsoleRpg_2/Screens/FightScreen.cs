using System;
using System.Linq;
using ConsoleRpg_2.Configurations;
using ConsoleRpg_2.Extensions;
using ConsoleRpg_2.GameObjects.Characters;
using ConsoleRpg_2.Ui;

namespace ConsoleRpg_2.Screens
{
    public class FightScreen
    {
        private Fight Fight { get; set; }

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

        
        public ScreenInputProcessResult ProcessInput(ConsoleKey key)
        {
            var result = new ScreenInputProcessResult();
            
            var prevChar = _activeCharacter;
            _activeCharacter = Fight.Queue.First();

            if (prevChar != _activeCharacter)
            {
                _gameLog.WriteLine($"{_activeCharacter.Name}'s turn.");

                Fight.BeginTurn(_activeCharacter);
                
                result.RerenderFlag = true;
                return result;
            }
            
            var fightResult = _activeCharacter.FightComponent.Process(key);

            if (fightResult.TurnEnd || _activeCharacter?.Stats.ActionPoints == 0)
            {
                Fight.EndCurrentTurn();
                _activeCharacter = null;
                result.RerenderFlag = true;
                return result;
            }
            
            result.RerenderFlag = true;
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
            
            _activeCharacter?.FightComponent.Render();
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
    }
}